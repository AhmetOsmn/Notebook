namespace Sample.Api.Models
{
    public class OrderStatusModel
    {
        public Guid OrderId { get; set; }
        public string State { get; set; }
    }
}