using MassTransit;
using MassTransit.Testing;
using Sample.Components.StateMachines;
using Sample.Contracts;

namespace Sample.Components.Tests
{
    [TestFixture]
    public class Submitting_an_order
    {
        [Test]
        public async Task Should_respond_with_acceptance_if_ok()
        {
            var orderStateMachine = new OrderStateMachine();
            var harness = new InMemoryTestHarness();
            var saga = harness.StateMachineSaga<OrderState, OrderStateMachine>(orderStateMachine);

            await harness.Start();

            try
            {
                var orderId = NewId.NextGuid();

                await harness.Bus.Publish<OrderSubmitted>(new
                {
                    OrderId = orderId,
                    InVar.Timestamp,
                    CustomerNumber = "12345"
                });

                Assert.That(saga.Created.Select(x => x.CorrelationId == orderId).Any(), Is.True);

                var instanceId = await saga.Exists(orderId, x => x.Submitted);
                Assert.That(instanceId, Is.Not.Null);

                var instance = saga.Sagas.Contains(instanceId.Value);
                Assert.That(instance.CustomerNumber, Is.EqualTo("12345"));
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Test]
        public async Task Should_respond_to_status_checks()
        {
            var orderStateMachine = new OrderStateMachine();
            var harness = new InMemoryTestHarness();
            var saga = harness.StateMachineSaga<OrderState, OrderStateMachine>(orderStateMachine);

            await harness.Start();

            try
            {
                var orderId = NewId.NextGuid();

                await harness.Bus.Publish<OrderSubmitted>(new
                {
                    OrderId = orderId,
                    InVar.Timestamp,
                    CustomerNumber = "12345"
                });

                Assert.That(saga.Created.Select(x => x.CorrelationId == orderId).Any(), Is.True);

                var instanceId = await saga.Exists(orderId, x => x.Submitted);
                Assert.That(instanceId, Is.Not.Null);

                var requestClient = await harness.ConnectRequestClient<CheckOrder>();

                var response = await requestClient.GetResponse<OrderStatus>(new
                {
                    OrderId = orderId
                });

                Assert.That(response.Message.State, Is.EqualTo(orderStateMachine.Submitted.Name));

            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}
