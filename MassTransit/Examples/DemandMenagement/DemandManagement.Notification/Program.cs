using DemandManagement.MessageContracts;
using MassTransit;

namespace DemandManagement.Notification
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Notification";

            var bus = BusConfigurator.ConfigureBus((cfg) =>
            {
                cfg.ReceiveEndpoint(RabbitMqConsts.NotificationServiceQueue, e =>
                {
                    e.Consumer<DemandRegisteredEventConsumer>();
                    e.UseMessageRetry(r => r.Immediate(5));
                });
            });

            bus.StartAsync();

            Console.WriteLine("Listening for Notification events.. Press enter to exit.");

            Console.ReadLine();

            bus.StopAsync();
        }
    }
}