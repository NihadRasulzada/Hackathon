using Api.Extensions;
using Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("pay/{reservationId}")]
        public async Task<IActionResult> Pay(string reservationId)
        {
            var response = await _paymentService.Pay(reservationId);
            return this.HandleResponse(response);
        }
    }
}
