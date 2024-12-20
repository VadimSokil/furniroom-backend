using FurniroomAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPaymentController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IPaymentService _paymentService;

        public DeliveryPaymentController(IDeliveryService deliveryService, IPaymentService paymentService)
        {
            _deliveryService = deliveryService;
            _paymentService = paymentService;
        }

        [HttpGet("delivery")]
        public async Task<IActionResult> GetAllDeliveryInfo()
        {
            var notes = await _deliveryService.GetAllDeliveryInfo();
            return Ok(notes);
        }

        [HttpGet("payment")]
        public async Task<IActionResult> GetAllPaymentInfo()
        {
            var notes = await _paymentService.GetAllPaymentInfo();
            return Ok(notes);
        }
    }
}
