using FurniroomAPI.Interfaces;
using FurniroomAPI.Models.Response;
using Microsoft.AspNetCore.Mvc;
using static FurniroomAPI.Models.RequestModels;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        public string requestId = Guid.NewGuid().ToString();

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost("add-order")]
        public async Task<ActionResult<GatewayResponseModel>> AddOrder([FromBody] OrderModel order)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: POST, Эндпоинт: add-order");

            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {order}");

                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
                };
            }
            else
            {
                var serviceResponse = await _requestService.AddOrderAsync(order, requestId);
                var gatewayResponse = new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = serviceResponse.Status,
                    Message = serviceResponse.Message,
                    Data = serviceResponse.Data
                };
                return Ok(gatewayResponse);
            }
        }

        [HttpPost("add-question")]
        public async Task<ActionResult<GatewayResponseModel>> AddQuestion([FromBody] QuestionModel question)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: POST, Эндпоинт: add-question");

            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {question}");

                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
                };
            }
            else
            {
                var serviceResponse = await _requestService.AddQuestionAsync(question, requestId);
                var gatewayResponse = new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = serviceResponse.Status,
                    Message = serviceResponse.Message,
                    Data = serviceResponse.Data
                };

                return Ok(gatewayResponse);
            }
        }

    }
}
