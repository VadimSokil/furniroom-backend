using AccountsService.Interfaces;
using AccountsService.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace AccountsService.Controllers
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
            var resultMessage = await _requestsService.AddOrderAsync(order);
            return Ok(new { message = "Order successfully added" });
        }

        [HttpPost("add-question")]
        public async Task<ActionResult> AddQuestion([FromBody] QuestionModel question)
        {
            var resultMessage = await _requestsService.AddQuestionAsync(question);
            return Ok(new { message = "Question successfully added" });
        }
    }
}
