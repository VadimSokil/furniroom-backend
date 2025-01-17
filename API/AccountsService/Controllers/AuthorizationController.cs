using Microsoft.AspNetCore.Mvc;
using AccountsService.Interfaces;
using AccountsService.Models.Authorization;
using AccountsService.Models.Response;
using AccountsService.Validation;

namespace AccountsService.Controllers
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
        public async Task<ActionResult<ResponseModel>> CheckEmail([FromQuery] string? email)
        {
            if(!validationMethods.IsString(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address must be a string"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address cannot be empty"
                };
            }
            else if (!validationMethods.IsValidEmail(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "The email address format is invalid"
                };
            }
            else if (!validationMethods.IsValidLength(email, 254))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email exceeds the maximum allowed length of 254 characters"
                };
            }
            else
            {
                var result = await _authorizationService.CheckEmailAsync(email);
                return Ok(result);
            }
        }

        [HttpGet("generate-code")]
        public async Task<ActionResult<ResponseModel>> GenerateCode([FromQuery] string? email)
        {
            if (!validationMethods.IsString(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address must be a string"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address cannot be empty"
                };
            }
            else if (!validationMethods.IsValidEmail(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "The email address format is invalid"
                };
            }
            else if (!validationMethods.IsValidLength(email, 254))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email exceeds the maximum allowed length of 254 characters"
                };
            }
            else
            {
                var result = await _authorizationService.GenerateCodeAsync(email);
                return Ok(result);
            }
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ResponseModel>> ResetPassword([FromBody] string? email)
        {
            if (!validationMethods.IsString(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address must be a string"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address cannot be empty"
                };
            }
            else if (!validationMethods.IsValidEmail(email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "The email address format is invalid"
                };
            }
            else if (!validationMethods.IsValidLength(email, 254))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email exceeds the maximum allowed length of 254 characters"
                };
            }
            else
            {
                var result = await _authorizationService.ResetPasswordAsync(email);
                return Ok(result);
            }
        }


        [HttpPost("register")]
        public async Task<ActionResult<ResponseModel>> Register([FromBody] RegisterModel register)
        {
            if(!validationMethods.IsDigit(register.AccountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID must be a int"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(register.AccountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID cannot be empty"
                };
            }
            else if(!validationMethods.IsValidDigit(register.AccountId))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account ID must be a positive number"
                };
            }
            else if (!validationMethods.IsString(register.AccountName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account name must be a string"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(register.AccountName))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account name cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(register.AccountName, 50))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Account name exceeds the maximum allowed length of 50 characters"
                };
            }
            else if (!validationMethods.IsString(register.Email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address must be a string"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(register.Email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address cannot be empty"
                };
            }
            else if (!validationMethods.IsValidEmail(register.Email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "The email address format is invalid"
                };
            }
            else if (!validationMethods.IsValidLength(register.Email, 254))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email exceeds the maximum allowed length of 254 characters"
                };
            }
            else if (!validationMethods.IsString(register.PasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Password must be a string"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(register.PasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Password cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(register.PasswordHash, 128))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Password exceeds the maximum allowed length of 128 characters"
                };
            }
            else
            {
                var result = await _authorizationService.RegisterAsync(register);
                return Ok(result);
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseModel>> Login([FromBody] LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "The structure of the data is incorrect"
                };
            }
            if (login == null)
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Invalid data structure. Please check your input."
                };
            }
            if (!validationMethods.IsString(login.Email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address must be a string"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(login.Email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email address cannot be empty"
                };
            }
            else if (!validationMethods.IsValidEmail(login.Email))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "The email address format is invalid"
                };
            }
            else if (!validationMethods.IsValidLength(login.Email, 254))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email exceeds the maximum allowed length of 254 characters"
                };
            }
            else if (!validationMethods.IsString(login.PasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Password must be a string"
                };
            }
            else if (!validationMethods.IsNotEmptyValue(login.PasswordHash))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Password cannot be empty"
                };
            }
            else if (!validationMethods.IsValidLength(login.Email, 128))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Password exceeds the maximum allowed length of 128 characters"
                };
            }
            else
            {
                var result = await _authorizationService.LoginAsync(login);
                return Ok(result);
            }
        }
    }
}
