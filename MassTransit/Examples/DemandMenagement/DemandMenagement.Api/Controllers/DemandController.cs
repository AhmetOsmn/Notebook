using DemandManagement.MessageContracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DemandManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemandController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Post(string subject, string description)
        {
            var bus = BusConfigurator.ConfigureBus();

            var sendToUri = new Uri($"{RabbitMqConsts.RabbitMqUri}{RabbitMqConsts.RegisterDemandServiceQueue}");
            var endPoint = await bus.GetSendEndpoint(sendToUri);

            await endPoint.Send<IRegisterDemandCommand>(new
            {
                Subject = subject,
                Description = description
            });

            return Ok();
        }
    }
}
