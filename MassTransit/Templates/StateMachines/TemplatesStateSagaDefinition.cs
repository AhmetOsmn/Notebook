namespace Company.StateMachines
{
    using MassTransit;

    public class TemplatesStateSagaDefinition :
        SagaDefinition<TemplatesState>
    {
        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<TemplatesState> sagaConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}