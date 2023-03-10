using MassTransit;
using MassTransit.Courier.Contracts;
using Microsoft.Extensions.Logging;
using Sample.Components.Consumers;

namespace Sample.Components.BatchConsumers
{
    public class RoutingSlipBatchEventConsumer : IConsumer<Batch<RoutingSlipCompleted>>
    {
        private readonly ILogger<RoutingSlipEventConsumer> _logger;

        public RoutingSlipBatchEventConsumer(ILogger<RoutingSlipEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Batch<RoutingSlipCompleted>> context)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.Log(LogLevel.Information, "Routing Slip Completed: {TrackingNumbers}", string.Join(",", context.Message.Select(x => x.Message.TrackingNumber)));

            return Task.CompletedTask;

        }
    }
}
