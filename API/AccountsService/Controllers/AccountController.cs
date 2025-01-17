using AccountsService.Interfaces;
using AccountsService.Models.Response;
using AccountsService.Validation;
using Microsoft.AspNetCore.Mvc;

namespace AccountsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";
        public ValidationMethods validationMethods = new ValidationMethods();

        public AccountController(IAccountService accountService) 
        {
            _accountService = accountService;
        }

        [HttpGet("get-account-info")]
        public async Task<ActionResult<ResponseModel>> GetAccountInfo([FromQuery] int? accountId)
        {
            if (!validationMethods.IsNotEmptyValue(accountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID cannot be empty"
                };
            }
            else if (!validationMethods.IsValidDigit(accountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID must be a positive number"
                };
            }
            else
            {
                var result = await _accountService.GetAccountInformationAsync(accountId);
                return Ok(result);
            }
        }

        [HttpGet("get-orders")]
        public async Task<ActionResult<ResponseModel>> GetOrders([FromQuery] int? accountId)
        {
            if (!validationMethods.IsNotEmptyValue(accountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID cannot be empty"
                };
            }
            else if (!validationMethods.IsValidDigit(accountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID must be a positive number"
                };
            }
            else
            {
                var result = await _accountService.GetAccountOrdersAsync(accountId);
                return Ok(result);
            }
        }

        [HttpPut("change-name")]
        public async Task<ActionResult<ResponseModel>> ChangeName([FromQuery] string? oldName, [FromQuery] string? newName)
        {
            if (!validationMethods.IsNotEmptyValue(oldName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old name cannot be empty"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(newName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New name cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(oldName, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old name exceeds the maximum allowed length of 50 characters"
                };
            }
            else if (!validationMethods.IsValidLength(newName, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New name exceeds the maximum allowed length of 50 characters"
                };
            }
            else
            {
                var result = await _accountService.ChangeNameAsync(oldName, newName);
                return Ok(result);
            }
        }

        [HttpPut("change-email")]
        public async Task<ActionResult<ResponseModel>> ChangeEmail([FromQuery] string? oldEmail, [FromQuery] string? newEmail)
        {
            if (!validationMethods.IsNotEmptyValue(oldEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email cannot be empty"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(newEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New email cannot be empty"
                };
            }
            else if (!validationMethods.IsValidEmail(oldEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email address format is invalid"
                };
            }
            else if (!validationMethods.IsValidEmail(newEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New email address format is invalid"
                };
            }
            else if (!validationMethods.IsValidLength(oldEmail, 254))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email exceeds the maximum allowed length of 254 characters"
                };
            }
            else if (!validationMethods.IsValidLength(newEmail, 254))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New email exceeds the maximum allowed length of 254 characters"
                };
            }
            else
            {
                var result = await _accountService.ChangeEmailAsync(oldEmail, newEmail);
                return Ok(result);
            }
        }

        [HttpPut("change-password")]
        public async Task<ActionResult<ResponseModel>> ChangePassword([FromQuery] string? oldPasswordHash, [FromQuery] string? newPasswordHash)
        {
            if (!validationMethods.IsNotEmptyValue(newPasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New password cannot be empty"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(oldPasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old password cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(oldPasswordHash, 128))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email exceeds the maximum allowed length of 128 characters"
                };
            }
            else if (!validationMethods.IsValidLength(newPasswordHash, 128))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New email exceeds the maximum allowed length of 128 characters"
                };
            }
            else
            {
                var result = await _accountService.ChangePasswordAsync(oldPasswordHash, newPasswordHash);
                return Ok(result);
            }
        }

        [HttpDelete("delete-account")]
        public async Task<ActionResult<ResponseModel>> DeleteAccount([FromQuery] int? accountId)
        {
            if (!validationMethods.IsNotEmptyValue(accountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID cannot be empty"
                };
            }
            else if (!validationMethods.IsValidDigit(accountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID must be a positive number"
                };
            }
            else
            {
                var result = await _accountService.DeleteAccountAsync(accountId);
                return Ok(result);
            }
        }
    }
}
