using MassTransit;
using MassTransit.Courier.Contracts;
using Sample.Contracts;

namespace Sample.Components.Consumers
{
    public class FulfillOrderConsumer : IConsumer<FulfillOrder>
    {
        public async Task Consume(ConsumeContext<FulfillOrder> context)
        {
            await Console.Out.WriteLineAsync($"-----> FulfillOrderConsumer: {context.Message.OrderId}");

            if (context.Message.CustomerNumber.StartsWith("INVALID")) throw new InvalidOperationException("We tried, but the customer is invalid.");

            // if (context.Message.CustomerNumber.StartsWith("MAYBE") && new Random().Next(100) > 85) throw new ApplicationException("We randomly exploded, so sad, much tear.");

            var builder = new RoutingSlipBuilder(NewId.NextGuid());

            builder.AddActivity("AllocateInventory", new Uri("queue:allocate-inventory_execute"), new
            {
                ItemNumber = "ITEM123",
                Quantity = 10
            });

            builder.AddActivity("PaymentActivity", new Uri("queue:payment_execute"), new
            {
                CardNumber = context.Message.PaymentCardNumber ?? "5999-123-456-789",
                Amount = 99.95m
            });

            builder.AddVariable("OrderId", context.Message.OrderId);

            await builder.AddSubscription(
                context.SourceAddress,
                RoutingSlipEvents.Faulted | RoutingSlipEvents.Supplemental,
                RoutingSlipEventContents.None,
                x => x.Send<OrderFulfillmentFaulted>(new
                {
                    context.Message.OrderId,
                }));

            await builder.AddSubscription(
                context.SourceAddress,
                RoutingSlipEvents.Completed | RoutingSlipEvents.Supplemental,
                RoutingSlipEventContents.None,
                x => x.Send<OrderFulfillmentCompleted>(new
                {
                    context.Message.OrderId,
                }));

            var routingSlip = builder.Build();

            await context.Execute(routingSlip);
        }
    }
}
