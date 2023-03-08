using MassTransit;
using Warehouse.Contracts;

namespace Warehouse.Components.StateMachines
{
    public class AllocationStateMachine : MassTransitStateMachine<AllocationState>
    {
        public AllocationStateMachine()
        {
            Event(() => AllocationCreated, x => x.CorrelateById(m => m.Message.AllocationId));

            Schedule(() => HoldExpiration, x => x.HoldDurationToken, s =>
            {
                s.Delay = TimeSpan.FromHours(1);
                s.Received = x => x.CorrelateById(m => m.Message.AllocationId);
            });

            InstanceState(x => x.CurrentState);

            Initially(
                When(AllocationCreated)
                .Schedule(
                    HoldExpiration,
                    context => context.Init<AllocationHoldDurationExpired>(new
                    {
                        context.Message.AllocationId
                    }),
                    context => context.Message.HoldDuration)
                .TransitionTo(Allocated)
            );

            During(Allocated,
                When(HoldExpiration.Received)
                .ThenAsync(context => Console.Out.WriteLineAsync($"Allocation was released: {context.Instance.CorrelationId}"))
                .Finalize());

            SetCompletedWhenFinalized();
        }

        public Schedule<AllocationState, AllocationHoldDurationExpired> HoldExpiration { get; set; }

        public Event<AllocationCreated> AllocationCreated { get; set; }

        public State Allocated { get; set; }
    }
}
