using FurniroomAPI.Interfaces;
using FurniroomAPI.Models.Authorization;
using FurniroomAPI.Models.Response;
using FurniroomAPI.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        public string requestId = Guid.NewGuid().ToString();
        public ValidationMethods validationMethods = new ValidationMethods();

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet("check-email")]
        public async Task<ActionResult<GatewayResponseModel>> CheckEmail([FromQuery] AuthorizationModels.EmailModel emailModel)
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
                    Message = "Your query is missing some fields."
                };
            }
            else
            {
                var serviceResponse = await _authorizationService.CheckEmailAsync(emailModel.Email, requestId);
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
        public async Task<ActionResult<GatewayResponseModel>> GenerateCode([FromQuery][Required] string? email)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: GET, Эндпоинт: generate-code");

            if (!ModelState.IsValid)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Ваш запрос не содержит всех необходимых полей.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(email))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Email адрес не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot be empty."
                };
            }
            else if (!validationMethods.IsValidEmail(email))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Некорректный формат email адреса.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect email address format."
                };
            }
            else if (!validationMethods.IsValidLength(email, 254))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Email адрес не может превышать 254 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot exceed 254 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _authorizationService.GenerateCodeAsync(email, requestId);
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
        public async Task<ActionResult<GatewayResponseModel>> ResetPassword([FromBody][Required] string? email)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: POST, Эндпоинт: reset-password");

            if (!ModelState.IsValid)
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Ваш запрос не содержит всех необходимых полей.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(email))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Email адрес не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot be empty."
                };
            }
            else if (!validationMethods.IsValidEmail(email))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Некорректный формат email адреса.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect email address format."
                };
            }
            else if (!validationMethods.IsValidLength(email, 254))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Email адрес не может превышать 254 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot exceed 254 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _authorizationService.ResetPasswordAsync(email, requestId);
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
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Ваш запрос не содержит всех необходимых полей.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(register.AccountId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Account ID не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(register.AccountId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Account ID должен быть положительным числом.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID must be a positive number."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(register.AccountName))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Имя аккаунта не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account name cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(register.AccountName, 50))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Имя аккаунта не может превышать 50 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account name cannot exceed 50 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(register.Email))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Email адрес не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot be empty."
                };
            }
            else if (!validationMethods.IsValidEmail(register.Email))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Некорректный формат email адреса.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect email address format."
                };
            }
            else if (!validationMethods.IsValidLength(register.Email, 254))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Email адрес не может превышать 254 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot exceed 254 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(register.PasswordHash))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Хэш пароля не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Password hash cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(register.PasswordHash, 128))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Хэш пароля не может превышать 128 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Password hash cannot exceed 128 characters in length."
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
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Ваш запрос не содержит всех необходимых полей.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(login.Email))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Email адрес не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot be empty."
                };
            }
            else if (!validationMethods.IsValidEmail(login.Email))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Некорректный формат email адреса.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect email address format."
                };
            }
            else if (!validationMethods.IsValidLength(login.Email, 254))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Email адрес не может превышать 254 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot exceed 254 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(login.PasswordHash))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Хэш пароля не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Password hash cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(login.PasswordHash, 128))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Хэш пароля не может превышать 128 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Password hash cannot exceed 128 characters in length."
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