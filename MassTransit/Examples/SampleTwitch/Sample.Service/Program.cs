using MassTransit;
using MassTransit.Courier.Contracts;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Components.BatchConsumers;
using Sample.Components.Consumers;
using Sample.Components.CourierActivities;
using Sample.Components.StateMachines;
using Sample.Components.StateMachines.OrderStateMachineActivities;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using Warehouse.Contracts;

namespace Sample.Service
{
    internal static class Program
    {
        static TelemetryClient _telemetryClient;
        static DependencyTrackingTelemetryModule _module;

        static async Task Main(string[] args)
        {
            Console.Title = "Service";

            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("test log başladı");

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
               .ConfigureServices((context, services) =>
               {
                   //#region Tracking Configure
                   //_module = new DependencyTrackingTelemetryModule();
                   //_module.IncludeDiagnosticSourceActivities.Add("MassTransit");

                   //var configuration = TelemetryConfiguration.CreateDefault();
                   //configuration.InstrumentationKey = "30ae615d-4452-4575-aca9-cf3938f17bf8";
                   //configuration.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());

                   //_telemetryClient = new TelemetryClient(configuration);
                   //_module.Initialize(configuration);

                   //var loggerOptions = new Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerOptions();
                   //var applicationInsightsLoggerProvider = new ApplicationInsightsLoggerProvider(Options.Create(configuration),
                   //    Options.Create(loggerOptions));
                   //ILoggerFactory factory = new LoggerFactory();
                   //factory.AddProvider(applicationInsightsLoggerProvider);
                   //LogContext.ConfigureCurrentLogContext(factory);
                   //#endregion

                   services.AddScoped<AcceptOrderActivity>();
                   services.AddScoped<RoutingSlipBatchEventConsumer>();
                   services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

                   services.AddMassTransit(cfg =>
                   {
                       cfg.AddConsumersFromNamespaceContaining<SubmitOrderConsumer>();
                       cfg.AddActivitiesFromNamespaceContaining<AllocateInventoryActivity>();

                       cfg.AddSagaStateMachine<OrderStateMachine, OrderState>(typeof(OrderStateMachineDefinition))
                       .MongoDbRepository(r =>
                       {
                           r.Connection = "mongodb://127.0.0.1:27017";
                           r.DatabaseName = "orders";
                       });

                       cfg.UsingRabbitMq((context, cfgx) =>
                       {
                           // for azure service bus:
                           // cfgx.Host("Endpoint=sb://sample-twitch-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=APo2ZqgNHIloVG95QCDKpEQfE+1zvXSp2+ASbKUflYk=");
                           cfgx.UseMessageScheduler(new Uri("queue:quartz"));
                           cfgx.ReceiveEndpoint(KebabCaseEndpointNameFormatter.Instance.Consumer<RoutingSlipBatchEventConsumer>(), e =>
                           {
                               e.PrefetchCount = 20;

                               e.Batch<RoutingSlipCompleted>(b =>
                               {
                                   b.MessageLimit = 10;

                                   b.TimeLimit = TimeSpan.FromSeconds(5);

                                   b.Consumer<RoutingSlipBatchEventConsumer, RoutingSlipCompleted>(services.BuildServiceProvider());
                               });
                           });
                           cfgx.ConfigureEndpoints(context);
                       });

                       cfg.AddRequestClient<AllocateInventory>();
                   });

                   services.AddHostedService<MassTransitConsoleHostedService>();
               })
               .ConfigureLogging((h, logging) =>
               {
                   logging.AddConfiguration(h.Configuration.GetSection("Logging"));
                   logging.AddConsole();
               });

            host.UseSerilog();

            if (isService) await host.Build().RunAsync();
            else await host.RunConsoleAsync();

            _telemetryClient?.Flush();
            _module?.Dispose();

            Console.ReadKey();
        }
    }
}