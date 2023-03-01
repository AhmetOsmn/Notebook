namespace LightMessagingCore.Messaging
{
    public interface IOrderCommand
    {
        int OrderId { get; }
        string OrderCode { get; }
    }
}