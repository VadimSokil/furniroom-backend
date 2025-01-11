using FurniroomAPI.Interfaces;
using FurniroomAPI.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost("add-order")]
        public async Task<ActionResult<string>> AddOrder([FromBody] OrderModel order)
        {
            var result = await _requestService.AddOrderAsync(order);
            return Ok(result);
        }

        [HttpPost("add-question")]
        public async Task<ActionResult<string>> AddQuestion([FromBody] QuestionModel question)
        {
            var result = await _requestService.AddQuestionAsync(question);
            return Ok(result); 
        }
    }
}
