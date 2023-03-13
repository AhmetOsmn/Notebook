namespace Company.Consumers
{
    using MassTransit;

    public class TemplatesConsumerDefinition :
        ConsumerDefinition<TemplatesConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<TemplatesConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}