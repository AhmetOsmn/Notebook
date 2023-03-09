using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Components.Consumers;
using Sample.Components.CourierActivities;
using Sample.Components.StateMachines;
using Sample.Components.StateMachines.OrderStateMachineActivities;
using System.Diagnostics;
using Warehouse.Contracts;

namespace Sample.Service
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Service";

            var isService = !(Debugger.IsAttached || args.Contains("--console"));

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
                   services.AddScoped<AcceptOrderActivity>();
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

                       cfg.UsingAzureServiceBus((context, cfgx) =>
                       {
                           cfgx.Host("Endpoint=sb://sample-twitch-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=APo2ZqgNHIloVG95QCDKpEQfE+1zvXSp2+ASbKUflYk=");

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

            if (isService) await host.Build().RunAsync();
            else await host.RunConsoleAsync();

            Console.ReadKey();
        }
    }
}