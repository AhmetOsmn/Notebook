using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Components.Consumers;
using Sample.Contracts;
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
                       cfg.AddConsumer<SubmitOrderConsumer>();

                       cfg.UsingRabbitMq((context, cfgx) =>
                       {
                           cfgx.ReceiveEndpoint(RmqConstants.QueueName, e =>
                           {
                               e.ConfigureConsumer<SubmitOrderConsumer>(context);
                           });
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