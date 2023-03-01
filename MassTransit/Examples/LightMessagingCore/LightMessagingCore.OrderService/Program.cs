using LightMessagingCore.Common;
using MassTransit;

namespace LightMessagingCore.OrderService
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Order Service";

            var bus = BusConfigurator.ConfigureBus((cfg) =>
            {
                cfg.ReceiveEndpoint(MqConstants.OrderQueueName, c =>
                {
                    c.Consumer<OrderCommandConsumer>();
                });
            });

            bus.StartAsync();

            Console.WriteLine("Listening order command.. Press enter for exit.");
            Console.ReadLine();

            bus.StopAsync();
        }
    }
}