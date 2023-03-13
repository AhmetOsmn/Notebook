namespace Company.Sagas
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;

    public class TemplatesSaga :
        ISaga,
        InitiatedBy<InitiateTemplates>,
        Orchestrates<UpdateTemplates>,
        Observes<NotifyTemplates, TemplatesSaga>
    {
        public Guid CorrelationId { get; set; }
        public string Value { get; set; }

        public Expression<Func<TemplatesSaga, NotifyTemplates, bool>> CorrelationExpression =>
            (saga, message) => saga.Value == message.Value;

        public Task Consume(ConsumeContext<InitiateTemplates> context)
        {
            Value = context.Message.Value;

            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<UpdateTemplates> context)
        {
            Value = context.Message.Value;
            
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<NotifyTemplates> context)
        {
            return Task.CompletedTask;
        }
    }
}

