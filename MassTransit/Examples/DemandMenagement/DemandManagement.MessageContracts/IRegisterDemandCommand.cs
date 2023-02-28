namespace DemandManagement.MessageContracts
{
    public interface IRegisterDemandCommand
    {
        string Subject { get; }
        string Description { get; }
    }
}
