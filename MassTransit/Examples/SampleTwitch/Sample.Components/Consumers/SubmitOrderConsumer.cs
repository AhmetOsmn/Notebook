using MassTransit;
using Microsoft.Extensions.Logging;
using Sample.Contracts;

namespace Sample.Components.Consumers
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        private readonly ILogger<SubmitOrderConsumer> _logger;

        public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            Console.WriteLine($"SubmitOrderConsumer: {context.Message.CustomerNumber}");

            if (context.Message.CustomerNumber.Contains("test", StringComparison.OrdinalIgnoreCase))
            {
                await context.RespondAsync<OrderSubmissionRejected>(new
                {
                    InVar.Timestamp,
                    context.Message.OrderId,
                    context.Message.CustomerNumber,
                    Reason = "contains test keyword"
                });

                return;
            }

            await context.RespondAsync<OrderSubmissionAccepted>(new
            {
                InVar.Timestamp,
                context.Message.OrderId,
                context.Message.CustomerNumber,
            });
        }
    }
}
