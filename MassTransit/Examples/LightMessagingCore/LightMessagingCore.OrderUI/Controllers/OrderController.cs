using LightMessagingCore.Common;
using LightMessagingCore.Messaging;
using LightMessagingCore.OrderUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LightMessagingCore.OrderUI.Controllers
{
    public class OrderController : Controller
    {

        public async Task<IActionResult> Index(OrderModel orderModel)
        {
            if(orderModel.OrderId > 0) await CreateOrder(orderModel);

            return View();
        }

        private async Task CreateOrder(OrderModel orderModel)
        {
            var busControl = BusConfigurator.ConfigureBus();
            var sendToUri = new Uri($"{MqConstants.RmqUri}{MqConstants.OrderQueueName}");
            var sendEndpoint = await busControl.GetSendEndpoint(sendToUri);

            await sendEndpoint.Send<IOrderCommand>(new {orderModel.OrderId, orderModel.OrderCode});
        }
    }
}
