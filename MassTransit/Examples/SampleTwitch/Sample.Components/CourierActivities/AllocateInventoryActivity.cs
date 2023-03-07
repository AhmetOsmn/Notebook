using MassTransit;
using Warehouse.Contracts;

namespace Sample.Components.CourierActivities
{
    public class AllocateInventoryActivity : IActivity<AllocateInventoryArguments, AllocateInventoryLog>
    {
        private readonly IRequestClient<AllocateInventory> _client;

        public AllocateInventoryActivity(IRequestClient<AllocateInventory> client)
        {
            _client = client;
        }

        public async Task<CompensationResult> Compensate(CompensateContext<AllocateInventoryLog> context)
        {
            await context.Publish<AllocationReleaseRequested>(new
            {
                context.Log.AllocationId,
                Reason = "Order Faulted"
            });

            return context.Compensated();
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<AllocateInventoryArguments> context)
        {
            var orderId = context.Arguments.OrderId;

            var itemNumber = context.Arguments.ItemNumber;
            if (string.IsNullOrEmpty(itemNumber)) throw new InvalidOperationException(nameof(itemNumber));

            var quantity = context.Arguments.Quantity;
            if (quantity <= 0.0m) throw new InvalidOperationException(nameof(quantity));

            var allocationId = NewId.NextGuid();

            var response = await _client.GetResponse<InventoryAllocated>(new
            {
                AllocationId = allocationId,
                ItemNumber = itemNumber,
                Quantity = quantity,
            });

            return context.Completed(new
            {
                AllocationId = allocationId
            });
        }
    }
}
