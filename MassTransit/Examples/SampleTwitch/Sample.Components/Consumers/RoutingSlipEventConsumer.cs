using MassTransit;
using MassTransit.Courier.Contracts;
using Microsoft.Extensions.Logging;

namespace Sample.Components.Consumers
{
    public class RoutingSlipEventConsumer :
        IConsumer<RoutingSlipCompleted>,
        IConsumer<RoutingSlipFaulted>,
        IConsumer<RoutingSlipActivityCompleted>
    {
        private readonly ILogger<RoutingSlipEventConsumer> _logger;

        public RoutingSlipEventConsumer(ILogger<RoutingSlipEventConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<RoutingSlipCompleted> context)
        {

            Console.WriteLine($"Routing Slip Completed: {context.Message.TrackingNumber}");

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<RoutingSlipFaulted> context)
        {

            Console.WriteLine($"Routing Slip Activity Faulted: {context.Message.TrackingNumber} {context.Message.ActivityExceptions.FirstOrDefault()}");

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<RoutingSlipActivityCompleted> context)
        {
            Console.WriteLine($"Routing Slip Activity Completed: {context.Message.TrackingNumber} {context.Message.ActivityName}");

            return Task.CompletedTask;
        }
    }
}
