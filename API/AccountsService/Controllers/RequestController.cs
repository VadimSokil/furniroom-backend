using AccountsService.Interfaces;
using AccountsService.Models.Request;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

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
            try
            {
                await _requestsService.AddOrderAsync(order);
                return CreatedAtAction(nameof(AddOrder), new
                {
                    id = order.OrderId
                }, new
                {
                    message = "Order successfully added"
                });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new
                {
                    message = "Не удалось установить связь с базой данных."
                });
            }
        }

        [HttpPost("add-question")]
        public async Task<ActionResult> AddQuestion([FromBody] QuestionModel question)
        {
            try
            {
                await _requestsService.AddQuestionAsync(question);
                return CreatedAtAction(nameof(AddQuestion), new
                {
                    id = question.QuestionId
                }, new
                {
                    message = "Question successfully added"
                });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new
                {
                    message = "Не удалось установить связь с базой данных."
                });
            }
        }
    }
}
