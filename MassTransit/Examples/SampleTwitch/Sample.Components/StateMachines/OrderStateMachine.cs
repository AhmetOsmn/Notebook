﻿using MassTransit;
using Sample.Components.StateMachines.OrderStateMachineActivities;
using Sample.Contracts;

namespace Sample.Components.StateMachines
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            Event(() => OrderSubmitted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderAccepted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => FulfillmentFaulted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => FulfillmentCompleted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => FulfillOrderFaulted, x => x.CorrelateById(m => m.Message.Message.OrderId));

            Event(() => OrderStatusRequested, x =>
            {
                x.CorrelateById(m => m.Message.OrderId);
                x.OnMissingInstance(i => i.ExecuteAsync(async context =>
                {
                    if(context.RequestId.HasValue)
                    {
                        await context.RespondAsync<OrderNotFound>(new
                        {
                            context.Message.OrderId
                        });
                    }
                }));
            });

            Event(() => AccounClosed, x => x.CorrelateBy((saga, context) => saga.CustomerNumber == context.Message.CustomerNumber));

            InstanceState(x => x.CurrentState);

            Initially
            (
                When(OrderSubmitted)
                    .Then(context =>
                    {
                        context.Saga.PaymentCardNumber = context.Message.PaymentCardNumber;
                        context.Saga.SubmitDate = context.Message.Timestamp;
                        context.Saga.CustomerNumber = context.Message.CustomerNumber;
                        context.Saga.Updated = DateTime.UtcNow;
                    })
                    .TransitionTo(Submitted)
            );

            During(Submitted,
                Ignore(OrderSubmitted),
                When(AccounClosed)
                .TransitionTo(Cancaled),
                When(OrderAccepted)
                .Activity(x => x.OfType<AcceptOrderActivity>())
                .TransitionTo(Accepted));

            During(Accepted,
                When(FulfillOrderFaulted)
                .Then(context => Console.WriteLine($"Fulfill Order Faulted:{context.Message.Exceptions.FirstOrDefault()?.Message}"))
                .TransitionTo(Faulted),
                When(FulfillmentFaulted)
                .TransitionTo(Faulted),
                When(FulfillmentCompleted)
                .TransitionTo(Completed)
            );

            DuringAny(
                When(OrderStatusRequested)
                    .RespondAsync(x => x.Init<OrderStatus>(new
                    {
                        OrderId = x.Instance.CorrelationId,
                        State = x.Instance.CurrentState
                    }))
            );

            DuringAny(
                When(OrderSubmitted)
                    .Then(context =>
                    {
                        context.Instance.SubmitDate ??= context.Data.Timestamp;
                        context.Instance.CustomerNumber ??= context.Data.CustomerNumber;
                    })
            );
        }

        public State Submitted { get; private set; }
        public State Accepted { get; private set; }
        public State Cancaled { get; private set; }
        public State Faulted { get; private set; }
        public State Completed { get; private set; }

        public Event<OrderAccepted> OrderAccepted { get; private set; }
        public Event<OrderSubmitted> OrderSubmitted { get; private set; }
        public Event<CheckOrder> OrderStatusRequested { get; private set; }
        public Event<CustomerAccuntClosed> AccounClosed { get; private set; }
        public Event<OrderFulfillmentFaulted> FulfillmentFaulted{ get; private set; }
        public Event<OrderFulfillmentCompleted> FulfillmentCompleted{ get; private set; }
        public Event<Fault<FulfillOrder>> FulfillOrderFaulted{ get; private set; }
    }
 }
