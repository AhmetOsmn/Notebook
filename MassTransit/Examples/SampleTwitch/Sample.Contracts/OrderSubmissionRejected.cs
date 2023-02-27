namespace Sample.Contracts
{
    public interface OrderSubmissionRejected
    {
        string CustomerNumber { get; }
        Guid OrderId { get; }
        DateTime Timestamp { get; }
        string Reason { get; }
    }
}