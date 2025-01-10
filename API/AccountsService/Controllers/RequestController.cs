using AccountsService.Interfaces;
using AccountsService.Models.Request;
using AccountsService.Validators.Request; 
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace AccountsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestsService;
        private readonly OrderModelValidator _orderValidator; 
        private readonly QuestionModelValidator _questionValidator; 

        public RequestController(IRequestService requestsService)
        {
            _requestsService = requestsService;
            _orderValidator = new OrderModelValidator(); 
            _questionValidator = new QuestionModelValidator(); 
        }

        [HttpPost("add-order")]
        public async Task<ActionResult> AddOrder([FromBody] OrderModel order)
        {
            // Валидация заказа
            var validationResult = _orderValidator.Validate(order);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            try
            {
                var resultMessage = await _requestsService.AddOrderAsync(order);
                if (resultMessage.StartsWith("OrderId is already taken"))
                {
                    return BadRequest(new { errors = resultMessage });
                }

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
            var validationResult = _questionValidator.Validate(question);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            try
            {
                var resultMessage = await _requestsService.AddQuestionAsync(question);
                if (resultMessage.StartsWith("QuestionId is already taken"))
                {
                    return BadRequest(new { errors = resultMessage });
                }

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
