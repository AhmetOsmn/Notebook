using Automatonymous;
using MassTransit;
using Sample.Contracts;

namespace Sample.Components.StateMachines.OrderStateMachineActivities
{
    public class AcceptOrderActivity : IStateMachineActivity<OrderState, OrderAccepted>
    {
        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<OrderState, OrderAccepted> context, IBehavior<OrderState, OrderAccepted> next)
        {
            Console.WriteLine($"Hello world. Order is {context.Message.OrderId}");

            var consumeContext = context.GetPayload<ConsumeContext>();

            var sendEndpoint = await consumeContext.GetSendEndpoint(new Uri("exchange:fulfill-order"));

            await sendEndpoint.Send<FulfillOrder>(new
            {
                context.Message.OrderId
            }, context.CancellationToken);

            await next.Execute(context).ConfigureAwait(false);
        }

        public Task Faulted<TException>(BehaviorExceptionContext<OrderState, OrderAccepted, TException> context, IBehavior<OrderState, OrderAccepted> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("accept-order");
        }
    }
}
