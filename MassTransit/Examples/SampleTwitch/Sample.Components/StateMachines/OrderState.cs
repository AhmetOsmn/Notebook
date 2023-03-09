using MassTransit;
using MongoDB.Bson.Serialization.Attributes;

namespace Sample.Components.StateMachines
{
    public class OrderState :
        SagaStateMachineInstance,
        ISagaVersion
    {

        [BsonId]
        public Guid CorrelationId { get; set; }
        public int Version { get; set; }
        public string CurrentState { get; set; }
        public string CustomerNumber { get; set; }
        public string FaultReason { get; set; }
        public string PaymentCardNumber { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? SubmitDate { get; set; }
    }
}