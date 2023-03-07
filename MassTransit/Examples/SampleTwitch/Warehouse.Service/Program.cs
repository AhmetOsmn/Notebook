using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Warehouse.Components;

namespace Warehouse.Service
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Warehouse Service";

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
                       cfg.AddConsumersFromNamespaceContaining<AllocateInventoryConsumer>();

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