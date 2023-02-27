namespace Company.Consumers
{
    using Contracts;
    using MassTransit;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class GettingStartedConsumer : IConsumer<GettingStarted>
    {

        private readonly ILogger<GettingStartedConsumer> _logger;

        public GettingStartedConsumer(ILogger<GettingStartedConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<GettingStarted> context)
        {
            _logger.LogInformation("Received Text: {Text}", context.Message.Value);
            return Task.CompletedTask;
        }
    }
}