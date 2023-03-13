namespace Company.Sagas
{
    using MassTransit;

    public class TemplatesSagaDefinition :
        SagaDefinition<TemplatesSaga>
    {
        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<TemplatesSaga> sagaConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}