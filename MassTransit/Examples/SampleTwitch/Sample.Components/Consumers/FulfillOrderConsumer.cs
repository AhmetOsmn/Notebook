using MassTransit;
using Sample.Contracts;

namespace Sample.Components.Consumers
{
    public class FulfillOrderConsumer : IConsumer<FulfillOrder>
    {
        public async Task Consume(ConsumeContext<FulfillOrder> context)
        {
            var builder = new RoutingSlipBuilder(NewId.NextGuid());

            builder.AddActivity("AllocateInventory",new Uri("queue:allocete-inventory_execute"), new
            {
                ItemNumber = "ITEM123",
                Quantity = 10
            });

            builder.AddVariable("OrderId", context.Message.OrderId);

            var routingSlip = builder.Build();

            await context.Execute(routingSlip);
        }
    }
}
