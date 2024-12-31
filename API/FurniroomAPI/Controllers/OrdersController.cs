using FurniroomAPI.Interfaces;
using FurniroomAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderModel order)
        {
            await _orderService.AddOrder(order);
            return Ok();
        }
    }
}
