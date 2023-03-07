using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Sample.Contracts;

namespace Sample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IRequestClient<SubmitOrder> _submitOrderRequestClient;
        private readonly IRequestClient<CheckOrder> _checkOrderRequestClient;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderController(
            ILogger<OrderController> logger,
            IRequestClient<SubmitOrder> submitOrderRequestClient,
            ISendEndpointProvider sendEndpointProvider,
            IRequestClient<CheckOrder> checkOrderRequestClient,
            IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _submitOrderRequestClient = submitOrderRequestClient;
            _sendEndpointProvider = sendEndpointProvider;
            _checkOrderRequestClient = checkOrderRequestClient;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var (status, notFound) = await _checkOrderRequestClient.GetResponse<OrderStatus, OrderNotFound>(new
            {
                OrderId = id
            });

            if (status.IsCompletedSuccessfully)
            {
                var response = await status;
                return Ok(response.Message);
            }
            else
            {
                var response = await notFound;
                return NotFound(response.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Guid id, string customerNumber)
        {
            var (accepted, rejected) = await _submitOrderRequestClient.GetResponse<OrderSubmissionAccepted, OrderSubmissionRejected>(new
            {
                OrderId = id,
                InVar.Timestamp,
                CustomerNumber = customerNumber
            });

            if (accepted.IsCompletedSuccessfully)
            {
                var response = await accepted;

                return Ok(response.Message);
            }
            else
            {
                var response = await rejected;

                return BadRequest(response.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Guid id, string customerNumber)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"exchange:{RmqConstants.QueueName}"));

            await endpoint.Send<SubmitOrder>(new
            {
                OrderId = id,
                InVar.Timestamp,
                CustomerNumber = customerNumber
            });

            return Accepted();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(Guid id)
        {
            await _publishEndpoint.Publish<OrderAccepted>(new
            {
                OrderId = id,
                InVar.Timestamp
            });

            return Accepted();
        }

    }
}
