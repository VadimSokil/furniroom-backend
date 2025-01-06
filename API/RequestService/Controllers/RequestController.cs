using Microsoft.AspNetCore.Mvc;
using RequestService.Interfaces;
using RequestService.Models;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestsService;

        public RequestController(IRequestService requestsService)
        {
            _requestsService = requestsService;
        }

        [HttpPost("add-order")]
        public async Task<ActionResult> AddOrder([FromBody] OrderModel order)
        {
            await _requestsService.AddOrderAsync(order);
            return CreatedAtAction(nameof(AddOrder), new { id = order.OrderId }, order);
        }

        [HttpPost("add-question")]
        public async Task<ActionResult> AddQuestion([FromBody] QuestionModel question)
        {
            await _requestsService.AddQuestionAsync(question);
            return CreatedAtAction(nameof(AddQuestion), new { id = question.QuestionId }, question);
        }
    }
}
