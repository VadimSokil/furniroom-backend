using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RequestService.Interfaces;
using RequestService.Models;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] OrderModel order)
        {
            await _ordersService.AddOrder(order);
            return CreatedAtAction(nameof(AddOrder), new { id = order.order_id }, order);
        }
    }
}
