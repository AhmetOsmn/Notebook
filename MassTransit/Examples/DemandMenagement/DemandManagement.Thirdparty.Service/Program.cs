using DemandManagement.MessageContracts;
using MassTransit;

namespace DemandManagement.Thirdparty.Service
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Thirdparty Service";

            var bus = BusConfigurator.ConfigureBus((cfg) =>
            {
                cfg.ReceiveEndpoint(RabbitMqConsts.ThirdPartyServiceQueue, e =>
                {
                    e.Consumer<DemandRegisteredEventConsumer>();
                    e.UseRateLimit(1000, TimeSpan.FromMinutes(1));
                });
            });

            bus.StartAsync();

            Console.WriteLine("Listening for Thirdparty Service events.. Press enter to exit.");

            Console.ReadLine();

            bus.StopAsync();
        }
    }
}