using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using SmartLifeSolution.BLL.Helpers;
using SmartLifeSolution.Controllers.Base;

namespace SmartLifeSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly StripePayment _service;
        public PaymentController(StripePayment service)
        {
            _service = service;
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] PaymentDto model)
        {
            using var reader = new StreamReader(Request.Body);
            var payload = await reader.ReadToEndAsync();

            return Ok();
        }


    }
}
