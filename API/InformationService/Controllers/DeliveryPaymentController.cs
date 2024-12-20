using InformationService.Interfaces;
using InformationService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InformationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPaymentController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IPaymentService _paymentService;

        DeliveryPaymentController(IDeliveryService deliveryService, IPaymentService paymentService)
        {
            _deliveryService = deliveryService;
            _paymentService = paymentService;
        }

        [HttpGet("delivery")]
        public async Task<ActionResult<List<DeliveryPaymentsModel>>> GetAllDeliveryInfo()
        {
            var note = await _deliveryService.GetAllDeliveryInfo();
            return Ok(note);
        }

        [HttpGet("payment")]
        public async Task<ActionResult<List<DeliveryPaymentsModel>>> GetAllPaymentsInfo()
        {
            var note = await _paymentService.GetAllPaymentsInfo();
            return Ok(note);
        }
    }
}
