﻿using MassTransit;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.AspNetCore.Logging;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Warehouse.Components;

namespace Warehouse.Service
{
    internal class Program
    {
        static TelemetryClient _telemetryClient;
        static DependencyTrackingTelemetryModule _module;

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
                   _module = new DependencyTrackingTelemetryModule();
                   _module.IncludeDiagnosticSourceActivities.Add("MassTransit");

                   var configuration = TelemetryConfiguration.CreateDefault();
                   configuration.InstrumentationKey = "30ae615d-4452-4575-aca9-cf3938f17bf8";
                   configuration.TelemetryInitializers.Add(new HttpDependenciesParsingTelemetryInitializer());

                   _telemetryClient = new TelemetryClient(configuration);
                   _module.Initialize(configuration);

                   var loggerOptions = new Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerOptions();
                   var applicationInsightsLoggerProvider = new ApplicationInsightsLoggerProvider(Options.Create(configuration),
                       Options.Create(loggerOptions));
                   ILoggerFactory factory = new LoggerFactory();
                   factory.AddProvider(applicationInsightsLoggerProvider);
                   LogContext.ConfigureCurrentLogContext(factory);

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