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
        public ValidationMethods validationMethods = new ValidationMethods();

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("account-information")]
        public async Task<ActionResult<GatewayResponseModel>> AccountInformation([FromQuery][Required] int? accountId)
        {
            if (!ModelState.IsValid)
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(accountId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(accountId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID must be a positive number."
                };
            }
            else
            {
                var serviceResponse = await _accountService.GetAccountInformationAsync((int)accountId);
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
            if (!ModelState.IsValid)
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(accountId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(accountId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID must be a positive number."
                };
            }
            else
            {
                var serviceResponse = await _accountService.GetAccountOrdersAsync((int)accountId);
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
            if (!ModelState.IsValid)
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(changeName.OldName))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Old name cannot be empty."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(changeName.NewName))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "New name cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(changeName.OldName, 50))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Old name cannot exceed 50 characters in length."
                };
            }
            else if (!validationMethods.IsValidLength(changeName.NewName, 50))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "New name cannot exceed 50 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _accountService.ChangeNameAsync(changeName);
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
            if (!ModelState.IsValid)
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(changeEmail.OldEmail))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Old email address cannot be empty."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(changeEmail.NewEmail))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "New email address cannot be empty."
                };
            }
            else if (!validationMethods.IsValidEmail(changeEmail.OldEmail))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect old email address format."
                };
            }
            else if (!validationMethods.IsValidEmail(changeEmail.NewEmail))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect new email address format."
                };
            }
            else if (!validationMethods.IsValidLength(changeEmail.OldEmail, 254))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Old email address cannot exceed 254 characters in length."
                };
            }
            else if (!validationMethods.IsValidLength(changeEmail.NewEmail, 254))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "New email address cannot exceed 254 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _accountService.ChangeEmailAsync(changeEmail);
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
            if (!ModelState.IsValid)
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(changePassword.NewPasswordHash))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "New password hash cannot be empty."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(changePassword.OldPasswordHash))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Old password hash cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(changePassword.OldPasswordHash, 128))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Old password hash cannot exceed 128 characters in length."
                };
            }
            else if (!validationMethods.IsValidLength(changePassword.NewPasswordHash, 128))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "New password hash cannot exceed 128 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _accountService.ChangePasswordAsync(changePassword);
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
            if (!ModelState.IsValid)
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(accountId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(accountId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID must be a positive number."
                };
            }
            else
            {
                var serviceResponse = await _accountService.DeleteAccountAsync((int)accountId);
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
