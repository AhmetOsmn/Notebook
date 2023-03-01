using LightMessagingCore.Messaging;
using MassTransit;

namespace LightMessagingCore.OrderService
{
    public class OrderCommandConsumer : IConsumer<IOrderCommand>
    {
        public async Task Consume(ConsumeContext<IOrderCommand> context)
        {
            var orderCommand = context.Message;

            await Console.Out.WriteLineAsync($"Order Id: {orderCommand.OrderId} - Order Code: {orderCommand.OrderCode}");
        }
    }
}
