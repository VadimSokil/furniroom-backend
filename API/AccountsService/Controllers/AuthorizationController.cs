using Microsoft.AspNetCore.Mvc;
using AccountsService.Interfaces;
using MySql.Data.MySqlClient;
using AccountsService.Validators.Authorization;
using AccountsService.Models.Authorization; 

namespace AccountsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        private readonly EmailModelValidator _emailValidator;
        private readonly RegisterModelValidator _registerValidator;
        private readonly LoginModelValidator _loginValidator;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _emailValidator = new EmailModelValidator();
            _registerValidator = new RegisterModelValidator();
            _loginValidator = new LoginModelValidator();
        }

        [HttpGet("check-email")]
        public async Task<ActionResult> CheckEmail([FromQuery] string Email)
        {
            var validationResult = _emailValidator.Validate(Email);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            try
            {
                var result = await _authorizationService.CheckEmailAsync(Email); 
                return Ok(new { message = result });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }

        [HttpGet("generate-code")]
        public async Task<ActionResult> GenerateCode([FromQuery] string Email)
        {
            var validationResult = _emailValidator.Validate(Email);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            try
            {
                var code = await _authorizationService.GenerateCodeAsync(Email); 
                return Ok(new { code });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromQuery] string email)
        {
            var resultMessage = await _authorizationService.ResetPasswordAsync(email);

            if (resultMessage == "Email does not exist in the database.")
            {
                return BadRequest(new { errors = resultMessage }); 
            }

            try
            {
                return Ok(new { message = resultMessage });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel register)
        {
            var resultMessage = await _authorizationService.RegisterAsync(register);
            if (!resultMessage.StartsWith("Account successfully added"))
            {
                return BadRequest(new { errors = resultMessage });
            }

            try
            {
                return Ok(new { message = resultMessage });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel login)
        {
            var validationResult = _loginValidator.Validate(login);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            try
            {
                var accountId = await _authorizationService.LoginAsync(login);
                return Ok(new { message = accountId });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }
    }
}
