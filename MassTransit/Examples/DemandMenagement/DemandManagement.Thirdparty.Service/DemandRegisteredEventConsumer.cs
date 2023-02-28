using DemandManagement.MessageContracts;
using MassTransit;

namespace DemandManagement.Thirdparty.Service
{
    public class DemandRegisteredEventConsumer : IConsumer<IRegisteredDemandEvent>
    {
        public async Task Consume(ConsumeContext<IRegisteredDemandEvent> context)
        {
            await Console.Out.WriteLineAsync($"Thirdparty integration done: Demand id {context.Message.DemandId}");
        }
    }
}
