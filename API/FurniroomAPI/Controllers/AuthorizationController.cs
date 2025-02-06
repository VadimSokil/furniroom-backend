using FurniroomAPI.Interfaces;
using FurniroomAPI.Models.Response;
using Microsoft.AspNetCore.Mvc;
using static FurniroomAPI.Models.AuthorizationModels;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        public string requestId = Guid.NewGuid().ToString();

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet("check-email")]
        public async Task<ActionResult<GatewayResponseModel>> CheckEmail([FromQuery] EmailModel emailModel)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: GET, Эндпоинт: check-email");

            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {emailModel.Email}");

                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
                };
            }
            else
            {
                var serviceResponse = await _authorizationService.CheckEmailAsync(emailModel, requestId);
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

        [HttpGet("generate-code")]
        public async Task<ActionResult<GatewayResponseModel>> GenerateCode([FromQuery] EmailModel emailModel)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: GET, Эндпоинт: generate-code");

            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {emailModel.Email}");

                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
                };
            }
            
            else
            {
                var serviceResponse = await _authorizationService.GenerateCodeAsync(emailModel, requestId);
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

        [HttpPost("reset-password")]
        public async Task<ActionResult<GatewayResponseModel>> ResetPassword([FromBody] EmailModel emailModel)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: POST, Эндпоинт: reset-password");

            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {emailModel.Email}");

                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
                };
            }
            else
            {
                var serviceResponse = await _authorizationService.ResetPasswordAsync(emailModel, requestId);
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

        [HttpPost("register")]
        public async Task<ActionResult<GatewayResponseModel>> Register([FromBody] RegisterModel register)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: POST, Эндпоинт: register");

            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {register}");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
                };
            }
            else
            {
                var serviceResponse = await _authorizationService.RegisterAsync(register, requestId);
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

        [HttpPost("login")]
        public async Task<ActionResult<GatewayResponseModel>> Login([FromBody] LoginModel login)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: POST, Эндпоинт: login");

            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {login}");

                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
                };
            }
            else
            {
                var serviceResponse = await _authorizationService.LoginAsync(login, requestId);
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