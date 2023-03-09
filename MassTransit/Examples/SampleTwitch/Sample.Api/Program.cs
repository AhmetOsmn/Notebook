using MassTransit;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sample.Contracts;
using Sample.Service;

namespace Sample.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Api";

            var builder = WebApplication.CreateBuilder(args);

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(x =>
                {
                    x.AllowAnyOrigin();
                });
            });

            builder.Services.AddApplicationInsightsTelemetry();

            builder.Services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
            {
                module.IncludeDiagnosticSourceActivities.Add("MassTransit");
            });


            // Add services to the container.
            builder.Services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

            builder.Services.AddMassTransit(cfg =>
            {
                cfg.AddBus(provider =>
                {
                    return Bus.Factory.CreateUsingAzureServiceBus(cfg =>
                    {
                        cfg.Host("Endpoint=sb://sample-twitch-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=APo2ZqgNHIloVG95QCDKpEQfE+1zvXSp2+ASbKUflYk=");
                    });
                });

                cfg.AddRequestClient<SubmitOrder>(new Uri($"queue:{RmqConstants.QueueName}"));

                cfg.AddRequestClient<CheckOrder>();
            });

            builder.Services.AddHostedService<MassTransitConsoleHostedService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(MyAllowSpecificOrigins);


            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}