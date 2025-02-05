using AccountsService.Models.Account;
using FurniroomAPI.Interfaces;
using FurniroomAPI.Models.Response;
using FurniroomAPI.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        public string requestId = Guid.NewGuid().ToString();
        public ValidationMethods validationMethods = new ValidationMethods();

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("account-information")]
        public async Task<ActionResult<GatewayResponseModel>> AccountInformation([FromQuery][Required] int? accountId)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: GET, Эндпоинт: account-information");

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
            else if (!validationMethods.IsNotEmptyValue(accountId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Account ID не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(accountId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Account ID должен быть положительным числом.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID must be a positive number."
                };
            }
            else
            {
                var serviceResponse = await _accountService.GetAccountInformationAsync((int)accountId, requestId);
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

        [HttpGet("account-orders")]
        public async Task<ActionResult<GatewayResponseModel>> AccountOrders([FromQuery][Required] int? accountId)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: GET, Эндпоинт: account-orders");

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
            else if (!validationMethods.IsNotEmptyValue(accountId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Account ID не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(accountId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Account ID должен быть положительным числом.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID must be a positive number."
                };
            }
            else
            {
                var serviceResponse = await _accountService.GetAccountOrdersAsync((int)accountId, requestId);
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

        [HttpPut("change-name")]
        public async Task<ActionResult<GatewayResponseModel>> ChangeName([FromBody] ChangeNameModel changeName)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: PUT, Эндпоинт: change-name");

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
            else if (!validationMethods.IsNotEmptyValue(changeName.OldName))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Старое имя аккаунта не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Old name cannot be empty."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(changeName.NewName))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Новое имя аккаунта не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "New name cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(changeName.OldName, 50))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Старое имя аккаунта не может превышать 50 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Old name cannot exceed 50 characters in length."
                };
            }
            else if (!validationMethods.IsValidLength(changeName.NewName, 50))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Новое имя аккаунта не может превышать 50 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "New name cannot exceed 50 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _accountService.ChangeNameAsync(changeName, requestId);
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

        [HttpPut("change-email")]
        public async Task<ActionResult<GatewayResponseModel>> ChangeEmail([FromBody] ChangeEmailModel changeEmail)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: PUT, Эндпоинт: change-email");

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
            else if (!validationMethods.IsNotEmptyValue(changeEmail.OldEmail))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Старый Email адрес не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Old email address cannot be empty."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(changeEmail.NewEmail))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Новый Email адрес не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "New email address cannot be empty."
                };
            }
            else if (!validationMethods.IsValidEmail(changeEmail.OldEmail))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Некорректный формат старого email адреса.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect old email address format."
                };
            }
            else if (!validationMethods.IsValidEmail(changeEmail.NewEmail))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Некорректный формат нового email адреса.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect new email address format."
                };
            }
            else if (!validationMethods.IsValidLength(changeEmail.OldEmail, 254))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Новый Email адрес не может превышать 254 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Old email address cannot exceed 254 characters in length."
                };
            }
            else if (!validationMethods.IsValidLength(changeEmail.NewEmail, 254))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Старый Email адрес не может превышать 254 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "New email address cannot exceed 254 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _accountService.ChangeEmailAsync(changeEmail, requestId);
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

        [HttpPut("change-password")]
        public async Task<ActionResult<GatewayResponseModel>> ChangePassword([FromBody] ChangePasswordModel changePassword)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: PUT, Эндпоинт: change-password");

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
            else if (!validationMethods.IsNotEmptyValue(changePassword.NewPasswordHash))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Старый хэш пароля не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "New password hash cannot be empty."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(changePassword.OldPasswordHash))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Новый хэш пароля не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Old password hash cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(changePassword.OldPasswordHash, 128))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Старый хэш пароля не может превышать 128 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Old password hash cannot exceed 128 characters in length."
                };
            }
            else if (!validationMethods.IsValidLength(changePassword.NewPasswordHash, 128))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Новый хэш пароля не может превышать 128 символов.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "New password hash cannot exceed 128 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _accountService.ChangePasswordAsync(changePassword, requestId);
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

        [HttpDelete("delete-account")]
        public async Task<ActionResult<GatewayResponseModel>> DeleteAccount([FromQuery][Required] int? accountId)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: DELETE, Эндпоинт: delete-account");

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
            else if (!validationMethods.IsNotEmptyValue(accountId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Account ID не может быть пустым.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(accountId))
            {
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации. Account ID должен быть положительным числом.");
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID must be a positive number."
                };
            }
            else
            {
                var serviceResponse = await _accountService.DeleteAccountAsync((int)accountId, requestId);
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
