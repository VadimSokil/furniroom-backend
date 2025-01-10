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
        private readonly string _connectionString;

        public AuthorizationController(IAuthorizationService authorizationService, string connectionString)
        {
            _authorizationService = authorizationService;
            _connectionString = connectionString;
        }

        [HttpGet("check-email")]
        public async Task<ActionResult> CheckEmail([FromQuery] string email)
        {
            var emailValidator = new EmailModelValidator(_connectionString);
            var validationResult = await emailValidator.ValidateAsync(email);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            try
            {
                var result = await _authorizationService.CheckEmailAsync(email);
                return Ok(new { message = result });
            }
            catch (MySqlException)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }

        [HttpGet("generate-code")]
        public async Task<ActionResult> GenerateCode([FromQuery] string email)
        {
            var emailValidator = new EmailModelValidator(_connectionString);
            var validationResult = await emailValidator.ValidateAsync(email);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            try
            {
                var code = await _authorizationService.GenerateCodeAsync(email);
                return Ok(new { code });
            }
            catch (MySqlException)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromQuery] string email)
        {
            var emailValidator = new EmailModelValidator(_connectionString);
            var validationResult = await emailValidator.ValidateAsync(email);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            try
            {
                var resultMessage = await _authorizationService.ResetPasswordAsync(email);
                if (resultMessage == "Email does not exist in the database.")
                {
                    return BadRequest(new { errors = resultMessage });
                }

                return Ok(new { message = resultMessage });
            }
            catch (MySqlException)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel register)
        {
            var registerValidator = new RegisterModelValidator(_connectionString);
            var validationResult = await registerValidator.ValidateAsync(register);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            try
            {
                var resultMessage = await _authorizationService.RegisterAsync(register);
                if (!resultMessage.StartsWith("Account successfully added"))
                {
                    return BadRequest(new { errors = resultMessage });
                }

                return Ok(new { message = resultMessage });
            }
            catch (MySqlException)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel login)
        {
            var loginValidator = new LoginModelValidator();
            var validationResult = await loginValidator.ValidateAsync(login);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });
            }

            try
            {
                var accountId = await _authorizationService.LoginAsync(login);
                return Ok(new { message = accountId });
            }
            catch (MySqlException)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }
    }
}
