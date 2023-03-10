using MassTransit;
using Sample.Contracts;

namespace Sample.Components.Consumers
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            Console.WriteLine($"-----> ConsumedMessage {RmqConstants.GetMessageCount()}: {context.Message.CustomerNumber}");
            RmqConstants.AddOneToCounter();

            if (context.Message.CustomerNumber.Contains("test", StringComparison.OrdinalIgnoreCase))
            {
                if (context.RequestId != null)
                {
                    await context.RespondAsync<OrderSubmissionRejected>(new
                    {
                        InVar.Timestamp,
                        context.Message.OrderId,
                        context.Message.CustomerNumber,
                        Reason = "contains test keyword"
                    });
                }

                return;
            }

            MessageData<string> notes = context.Message.Notes;
            if (notes.HasValue)
            {
                string notesValue = await notes.Value;

                Console.WriteLine("NOTES: {0}",notesValue);
            }


            await context.Publish<OrderSubmitted>(new
            {
                context.Message.PaymentCardNumber,
                context.Message.OrderId,
                context.Message.Timestamp,
                context.Message.CustomerNumber,
                context.Message.Notes
            });

            if (context.RequestId != null)
            {
                await context.RespondAsync<OrderSubmissionAccepted>(new
                {
                    InVar.Timestamp,
                    context.Message.OrderId,
                    context.Message.CustomerNumber,
                });
            }
        }
    }
}
