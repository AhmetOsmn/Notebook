using DemandManagement.MessageContracts;
using MassTransit;

namespace DemandManagement.Registration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Registration";

            var bus = BusConfigurator.ConfigureBus((cfg) =>
            {
                cfg.ReceiveEndpoint(RabbitMqConsts.RegisterDemandServiceQueue, e =>
                {
                    e.Consumer<RegisterDemandCommandConsumer>();
                    e.UseCircuitBreaker(cb =>
                    {
                        cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                        cb.TripThreshold = 15;
                        cb.ActiveThreshold = 10;
                        cb.ResetInterval = TimeSpan.FromMinutes(5);
                    });
                });
            });

            bus.StartAsync();

            Console.WriteLine("Listening for Register Demand Commands.. Press enter to exit.");

            Console.ReadLine();

            bus.StopAsync();
        }
    }
}