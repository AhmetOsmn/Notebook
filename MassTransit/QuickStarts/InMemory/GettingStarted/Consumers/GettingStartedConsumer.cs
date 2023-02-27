namespace Company.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using GettingStarted.Contracts;
    using Microsoft.Extensions.Logging;

    public class GettingStartedConsumer : IConsumer<GettingStarted>
    {
        private readonly ILogger<GettingStartedConsumer> _logger;

        public GettingStartedConsumer(ILogger<GettingStartedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<GettingStarted> context)
        {
            _logger.LogInformation($"Received Text: {context.Message.Value}");
            return Task.CompletedTask;
        }
    }
}
