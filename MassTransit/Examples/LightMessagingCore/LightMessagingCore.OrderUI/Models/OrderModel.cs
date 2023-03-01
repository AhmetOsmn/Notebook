using LightMessagingCore.Messaging;

namespace LightMessagingCore.OrderUI.Models
{
    public class OrderModel : IOrderCommand
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
    }
}
