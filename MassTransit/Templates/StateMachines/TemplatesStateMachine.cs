namespace Company.StateMachines
{
    using Contracts;
    using MassTransit;

    public class TemplatesStateMachine :
        MassTransitStateMachine<TemplatesState> 
    {
        public TemplatesStateMachine()
        {
            InstanceState(x => x.CurrentState, Created);

            Event(() => TemplatesEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

            Initially(
                When(TemplatesEvent)
                    .Then(context => context.Instance.Value = context.Data.Value)
                    .TransitionTo(Created)
            );

            SetCompletedWhenFinalized();
        }

        public State Created { get; private set; }

        public Event<TemplatesEvent> TemplatesEvent { get; private set; }
    }
}