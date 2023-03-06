using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Sample.Components.Consumers;
using Sample.Components.StateMachines;
using System.Diagnostics;

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
                   services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

                   services.AddMassTransit(cfg =>
                   {
                       cfg.AddConsumersFromNamespaceContaining<SubmitOrderConsumer>();

                       cfg.AddSagaStateMachine<OrderStateMachine, OrderState>(typeof(OrderStateMachineDefinition))
                       .RedisRepository("localhost:6379");

                       cfg.UsingRabbitMq((context, cfgx) =>
                       {
                           cfgx.ConfigureEndpoints(context);
                       });
                   });

                   services.AddHostedService<MassTransitConsoleHostedService>();
               });

            if (isService) await host.Build().RunAsync();
            else await host.RunConsoleAsync();

            Console.ReadKey();
        }
    }
}