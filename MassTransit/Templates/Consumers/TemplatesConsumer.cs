namespace Company.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;

    public class TemplatesConsumer :
        IConsumer<Templates>
    {
        public Task Consume(ConsumeContext<Templates> context)
        {
            return Task.CompletedTask;
        }
    }
}