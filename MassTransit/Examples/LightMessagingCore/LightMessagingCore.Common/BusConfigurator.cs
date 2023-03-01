using MassTransit;

namespace LightMessagingCore.Common
{
    public static class BusConfigurator
    {
        //private static readonly Lazy<BusConfigurator> _instance = new Lazy<BusConfigurator>(() => new BusConfigurator());

        //private BusConfigurator() { }

        //public static BusConfigurator Instance => _instance.Value;

        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator> registration = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {

                cfg.Host(new Uri(MqConstants.RmqUri), h =>
                {
                    h.Username(MqConstants.RmqUserName);
                    h.Password(MqConstants.RmqPassword);
                });

                registration?.Invoke(cfg);
            });
        }
    }
}
