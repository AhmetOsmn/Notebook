using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Contracts;

namespace Sample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IPublishEndpoint publishEndpoint;

        public CustomerController(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id, string customerNumber)
        {
            await publishEndpoint.Publish<CustomerAccuntClosed>(new
            {
                CustomerId = id,
                CustomerNumber = customerNumber
            });

            return Ok();
        }
    }
}
