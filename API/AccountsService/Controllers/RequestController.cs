using AccountsService.Interfaces;
using AccountsService.Models.Request;
using AccountsService.Models.Response;
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
        public async Task<ActionResult<ServiceResponseModel>> AddOrder([FromBody] OrderModel order)
        {
            var result = await _requestsService.AddOrderAsync(order);
            return Ok(result);
        }

        [HttpPost("add-question")]
        public async Task<ActionResult<ServiceResponseModel>> AddQuestion([FromBody] QuestionModel question)
        {
            var result = await _requestsService.AddQuestionAsync(question);
            return Ok(result);
        }
    }
}
