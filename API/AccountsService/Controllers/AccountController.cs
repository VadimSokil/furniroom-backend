using AccountsService.Interfaces;
using AccountsService.Models.Account;
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
        public async Task<ActionResult<ResponseModel>> ChangeName([FromQuery] ChangeNameModel changeName)
        {
            if (!validationMethods.IsNotEmptyValue(changeName.OldName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old name cannot be empty"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(changeName.NewName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New name cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(changeName.OldName, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old name exceeds the maximum allowed length of 50 characters"
                };
            }
            else if (!validationMethods.IsValidLength(changeName.NewName, 50))
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
                var result = await _accountService.ChangeNameAsync(changeName);
                return Ok(result);
            }
        }

        [HttpPut("change-email")]
        public async Task<ActionResult<ResponseModel>> ChangeEmail([FromQuery] ChangeEmailModel changeEmail)
        {
            if (!validationMethods.IsNotEmptyValue(changeEmail.OldEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email cannot be empty"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(changeEmail.NewEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New email cannot be empty"
                };
            }
            else if (!validationMethods.IsValidEmail(changeEmail.OldEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email address format is invalid"
                };
            }
            else if (!validationMethods.IsValidEmail(changeEmail.NewEmail))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New email address format is invalid"
                };
            }
            else if (!validationMethods.IsValidLength(changeEmail.OldEmail, 254))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old email exceeds the maximum allowed length of 254 characters"
                };
            }
            else if (!validationMethods.IsValidLength(changeEmail.NewEmail, 254))
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
                var result = await _accountService.ChangeEmailAsync(changeEmail);
                return Ok(result);
            }
        }

        [HttpPut("change-password")]
        public async Task<ActionResult<ResponseModel>> ChangePassword([FromQuery] ChangePasswordModel changePassword)
        {
            if (!validationMethods.IsNotEmptyValue(changePassword.NewPasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New password cannot be empty"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(changePassword.OldPasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old password cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(changePassword.OldPasswordHash, 128))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Old password exceeds the maximum allowed length of 128 characters"
                };
            }
            else if (!validationMethods.IsValidLength(changePassword.NewPasswordHash, 128))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "New password exceeds the maximum allowed length of 128 characters"
                };
            }
            else
            {
                var result = await _accountService.ChangePasswordAsync(changePassword);
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
