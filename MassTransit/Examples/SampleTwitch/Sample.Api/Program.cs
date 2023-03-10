using MassTransit;
using MassTransit.MongoDbIntegration.MessageData;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sample.Contracts;
using Sample.Service;
using Serilog;
using Serilog.Events;

namespace Sample.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Api";

            SetSerilog();

            var builder = WebApplication.CreateBuilder(args);

            #region Azure Service Bus Configs
            //builder.Services.AddApplicationInsightsTelemetry();

            //builder.Services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
            //{
            //    module.IncludeDiagnosticSourceActivities.Add("MassTransit");
            //});
            #endregion


            builder.Services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

            builder.Services.AddMassTransit(cfg =>
            {
                #region Azure Service Bus Configs
                //cfg.AddBus(provider =>
                //{
                //    return Bus.Factory.CreateUsingAzureServiceBus(cfg =>
                //    {
                //        cfg.Host("Endpoint=sb://sample-twitch-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=APo2ZqgNHIloVG95QCDKpEQfE+1zvXSp2+ASbKUflYk=");
                //    });
                //});
                #endregion

                cfg.AddBus(x => ConfigureBus());

                cfg.AddRequestClient<SubmitOrder>(new Uri($"queue:{RmqConstants.QueueName}"));

                cfg.AddRequestClient<CheckOrder>();
            });

            builder.Services.AddHostedService<MassTransitConsoleHostedService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseSerilog();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        static IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(x =>
            {
                MessageDataDefaults.ExtraTimeToLive = TimeSpan.FromDays(1);
                MessageDataDefaults.Threshold = 2000;
                MessageDataDefaults.AlwaysWriteToRepository = false;

                x.UseMessageData(new MongoDbMessageDataRepository("mongodb://127.0.0.1:27017", "attachments"));
            });
        }

        static void SetSerilog()
        {
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Information()
              .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
              .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
              .Enrich.FromLogContext()
              .WriteTo.Console()
              .CreateLogger();
        }
    }
}