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
        public ValidationMethods validationMethods = new ValidationMethods();

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet("check-email")]
        public async Task<ActionResult<GatewayResponseModel>> CheckEmail([FromQuery][Required] string? email)
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
            else if (!validationMethods.IsNotEmptyValue(email))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot be empty."
                };
            }
            else if (!validationMethods.IsValidEmail(email))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect email address format."
                };
            }
            else if (!validationMethods.IsValidLength(email, 254))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot exceed 254 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _authorizationService.CheckEmailAsync(email);
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
            if (!ModelState.IsValid)
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(email))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot be empty."
                };
            }
            else if (!validationMethods.IsValidEmail(email))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect email address format."
                };
            }
            else if (!validationMethods.IsValidLength(email, 254))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot exceed 254 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _authorizationService.GenerateCodeAsync(email);
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
            if (!ModelState.IsValid)
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(email))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot be empty."
                };
            }
            else if (!validationMethods.IsValidEmail(email))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect email address format."
                };
            }
            else if (!validationMethods.IsValidLength(email, 254))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot exceed 254 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _authorizationService.ResetPasswordAsync(email);
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
            if (!ModelState.IsValid)
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(register.AccountId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID cannot be empty."
                };
            }
            else if (!validationMethods.IsValidDigit(register.AccountId))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account ID must be a positive number."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(register.AccountName))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account name cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(register.AccountName, 50))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Account name cannot exceed 50 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(register.Email))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot be empty."
                };
            }
            else if (!validationMethods.IsValidEmail(register.Email))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect email address format."
                };
            }
            else if (!validationMethods.IsValidLength(register.Email, 254))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot exceed 254 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(register.PasswordHash))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Password hash cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(register.PasswordHash, 128))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Password hash cannot exceed 128 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _authorizationService.RegisterAsync(register);
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
            if (!ModelState.IsValid)
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Your query is missing some fields."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(login.Email))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot be empty."
                };
            }
            else if (!validationMethods.IsValidEmail(login.Email))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Incorrect email address format."
                };
            }
            else if (!validationMethods.IsValidLength(login.Email, 254))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Email address cannot exceed 254 characters in length."
                };
            }
            else if (!validationMethods.IsNotEmptyValue(login.PasswordHash))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Password hash cannot be empty."
                };
            }
            else if (!validationMethods.IsValidLength(login.Email, 128))
            {
                return new GatewayResponseModel
                {
                    Date = currentDateTime,
                    Status = false,
                    Message = "Password hash cannot exceed 128 characters in length."
                };
            }
            else
            {
                var serviceResponse = await _authorizationService.LoginAsync(login);
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
