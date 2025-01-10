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
                    message = "Заказ успешно добавлен."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (MySqlException ex) when (ex.Number == 1042) 
            {
                return StatusCode(503, new { message = "Соединение с базой данных отсутствует." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка на сервере.", details = ex.Message });
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
                    message = "Вопрос успешно добавлен."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (MySqlException ex) when (ex.Number == 1042) 
            {
                return StatusCode(503, new { message = "Соединение с базой данных отсутствует." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка на сервере.", details = ex.Message });
            }
        }
    }
}
