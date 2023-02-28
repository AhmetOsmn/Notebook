using DemandManagement.MessageContracts;
using MassTransit;

namespace DemandManagement.Notification
{
    public class DemandRegisteredEventConsumer : IConsumer<IRegisteredDemandEvent>
    {
        public async Task Consume(ConsumeContext<IRegisteredDemandEvent> context)
        {
            await Console.Out.WriteLineAsync($"Notification sent: Demand id {context.Message.DemandId}");
        }
    }
}
