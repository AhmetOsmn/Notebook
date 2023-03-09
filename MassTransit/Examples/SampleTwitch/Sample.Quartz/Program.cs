using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Quartz;
using Serilog;
using Serilog.Events;

namespace Sample.Quartz
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.Configure<RabbitMqTransportOptions>(builder.Configuration.GetSection("RabbitMqTransport"));
            var connectionString = builder.Configuration.GetConnectionString("quartz");

            builder.Services.AddHealthChecks().AddCheck<SqlServerHealthCheck>("sql");

            builder.Services.AddQuartz(q =>
            {
                q.SchedulerName = "MassTransit-Scheduler";
                q.SchedulerId = "AUTO";

                q.UseMicrosoftDependencyInjectionJobFactory();

                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 10;
                });

                q.UseTimeZoneConverter();

                q.UsePersistentStore(s =>
                {
                    s.UseProperties = true;
                    s.RetryInterval = TimeSpan.FromSeconds(15);

                    s.UseSqlServer(connectionString);

                    s.UseJsonSerializer();

                    s.UseClustering(c =>
                    {
                        c.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
                        c.CheckinInterval = TimeSpan.FromSeconds(10);
                    });
                });
            });

            builder.Services.AddMassTransit(x =>
            {
                x.AddPublishMessageScheduler();

                x.AddQuartzConsumers();

                x.AddConsumer<SampleConsumer>();

                x.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host("Endpoint=sb://sample-twitch-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=APo2ZqgNHIloVG95QCDKpEQfE+1zvXSp2+ASbKUflYk=");

                    cfg.UsePublishMessageScheduler();

                    cfg.ConfigureEndpoints(context);
                });
            });

            builder.Services.Configure<MassTransitHostOptions>(options =>
            {
                options.WaitUntilStarted = true;
            });

            builder.Services.AddQuartzHostedService(options =>
            {
                options.StartDelay = TimeSpan.FromSeconds(5);
                options.WaitForJobsToComplete = true;
            });

            builder.Services.AddHostedService<SuperWorker>();

            builder.Host.UseSerilog();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                {
                    Predicate = check => check.Tags.Contains("ready"),
                    ResponseWriter = HealthCheckResponseWriter
                });

                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions { ResponseWriter = HealthCheckResponseWriter });

                endpoints.MapControllers();
            });

            app.MapRazorPages();

            app.Run();
        }

        public static Task HealthCheckResponseWriter(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result.ToJsonString());
        }
    }
}