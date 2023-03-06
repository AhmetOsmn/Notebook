using MassTransit;
using MassTransit.Testing;
using Sample.Components.Consumers;
using Sample.Contracts;

namespace Sample.Components.Tests
{
    [TestFixture]
    public class WhenAnOrderRequestIsConsumed
    {
        [Test]
        public async Task Should_respond_with_acceptance_if_ok()
        {
            var harness = new InMemoryTestHarness();
            var consumer = harness.Consumer<SubmitOrderConsumer>();

            await harness.Start();

            try
            {
                var orderId = NewId.NextGuid();

                var requestClient = await harness.ConnectRequestClient<SubmitOrder>();

                var response = await requestClient.GetResponse<OrderSubmissionAccepted>(new
                {
                    OrderId = orderId,
                    CustomerNumber = "12345",
                    InVar.Timestamp
                });

                Assert.That(response.Message.OrderId, Is.EqualTo(orderId));
                Assert.That(consumer.Consumed.Select<SubmitOrder>().Any(), Is.True);
                Assert.That(harness.Sent.Select<OrderSubmissionAccepted>().Any(), Is.True);
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Test]
        public async Task Should_respond_with_rejected_if_ok()
        {
            var harness = new InMemoryTestHarness();
            var consumer = harness.Consumer<SubmitOrderConsumer>();

            await harness.Start();

            try
            {
                var orderId = NewId.NextGuid();

                var requestClient = await harness.ConnectRequestClient<SubmitOrder>();

                var response = await requestClient.GetResponse<OrderSubmissionRejected>(new
                {
                    OrderId = orderId,
                    CustomerNumber = "12345test",
                    InVar.Timestamp
                });

                Assert.That(response.Message.OrderId, Is.EqualTo(orderId));
                Assert.That(consumer.Consumed.Select<SubmitOrder>().Any(), Is.True);
                Assert.That(harness.Sent.Select<OrderSubmissionRejected>().Any(), Is.True);
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Test]
        public async Task ShouldC_consume_submit_order_commands()
        {
            var harness = new InMemoryTestHarness();
            var consumer = harness.Consumer<SubmitOrderConsumer>();

            await harness.Start();

            try
            {
                var orderId = NewId.NextGuid();

                await harness.InputQueueSendEndpoint.Send<SubmitOrder>(new
                {
                    OrderId = orderId,
                    CustomerNumber = "12345",
                    InVar.Timestamp
                });

                Assert.That(consumer.Consumed.Select<SubmitOrder>().Any(), Is.True);
                Assert.That(harness.Sent.Select<OrderSubmissionAccepted>().Any(), Is.False);
                Assert.That(harness.Sent.Select<OrderSubmissionRejected>().Any(), Is.False);
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Test]
        public async Task Should_publish_order_submitted_event()
        {
            var harness = new InMemoryTestHarness();
            harness.Consumer<SubmitOrderConsumer>();

            await harness.Start();

            try
            {
                var orderId = NewId.NextGuid();

                await harness.InputQueueSendEndpoint.Send<SubmitOrder>(new
                {
                    OrderId = orderId,
                    CustomerNumber = "12345",
                    InVar.Timestamp
                });
                Assert.That(harness.Published.Select<OrderSubmitted>().Any(),Is.True);
            }
            finally
            {
                await harness.Stop();
            }
        }

        [Test]
        public async Task Should_not_publish_order_submitted_event_when_rejected()
        {
            var harness = new InMemoryTestHarness();
            var consumer = harness.Consumer<SubmitOrderConsumer>();

            await harness.Start();

            try
            {
                var orderId = NewId.NextGuid();

                await harness.InputQueueSendEndpoint.Send<SubmitOrder>(new
                {
                    OrderId = orderId,
                    CustomerNumber = "test",
                    InVar.Timestamp
                });

                Assert.That(consumer.Consumed.Select<SubmitOrder>().Any(), Is.True);
                Assert.That(harness.Published.Select<OrderSubmitted>().Any(), Is.False);
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}