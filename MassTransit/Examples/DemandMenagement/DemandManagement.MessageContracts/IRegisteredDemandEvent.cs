namespace DemandManagement.MessageContracts
{
    public interface IRegisteredDemandEvent
    {
        Guid DemandId { get; set; }
    }
}