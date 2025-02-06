using FurniroomAPI.Interfaces;
using FurniroomAPI.Models.Response;
using Microsoft.AspNetCore.Mvc;
using static FurniroomAPI.Models.AccountModels;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        public string requestId = Guid.NewGuid().ToString();

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("account-information")]
        public async Task<ActionResult<GatewayResponseModel>> AccountInformation([FromQuery] AccountIdModel accountId)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: GET, Эндпоинт: account-information");

            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {accountId}");

                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
                };
            }
            else
            {
                var serviceResponse = await _accountService.GetAccountInformationAsync(accountId, requestId);
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
        public async Task<ActionResult<GatewayResponseModel>> AccountOrders([FromQuery]AccountIdModel accountId)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: GET, Эндпоинт: account-orders");

            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {accountId}");

                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
                };
            }
            else
            {
                var serviceResponse = await _accountService.GetAccountOrdersAsync(accountId, requestId);
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
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {changeName}");

                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
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
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {changeEmail}");

                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
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
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {changePassword}");

                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
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
        public async Task<ActionResult<GatewayResponseModel>> DeleteAccount([FromQuery] AccountIdModel deleteAccount)
        {
            Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Получен новый запрос, Id запроса: {requestId}, Тип: DELETE, Эндпоинт: delete-account");

            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.Where(v => v.Errors.Count > 0).SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage;
                Console.WriteLine($"[FURNIROOM API LOGS]: Дата: {currentDateTime}, Id запроса: {requestId}, Статус: Ошибка валидации - {deleteAccount.AccountId}");

                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = errorMessage
                };
            }
            else
            {
                var serviceResponse = await _accountService.DeleteAccountAsync(deleteAccount, requestId);
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
