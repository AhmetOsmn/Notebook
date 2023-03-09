using MassTransit;
using Sample.Contracts;

namespace Sample.Components.Consumers
{
    public class FaultConsumer : IConsumer<Fault<FulfillOrder>>
    {
        public async Task Consume(ConsumeContext<Fault<FulfillOrder>> context)
        {
            await Console.Out.WriteLineAsync($"-----> FaultConsumer: {context.Message.Message.OrderId}");
        }
    }
}
