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

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _emailValidator = new EmailModelValidator(); 
        }

        [HttpGet("check-email")]
        public async Task<ActionResult> CheckEmail([FromQuery(Name = "email")] string email)
        {
            var validationResult = _emailValidator.Validate(email);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            try
            {
                var result = await _authorizationService.CheckEmailAsync(email); 
                return Ok(new { message = result });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }

        [HttpGet("generate-code")]
        public async Task<ActionResult> GenerateCode([FromQuery(Name = "email")] string email)
        {
            var validationResult = _emailValidator.Validate(email);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            try
            {
                var code = await _authorizationService.GenerateCodeAsync(email); 
                return Ok(new { code });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }

        [HttpGet("reset-password")]
        public async Task<ActionResult> ResetPassword([FromQuery(Name = "email")] string email)
        {
            var validationResult = _emailValidator.Validate(email);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { errors = validationResult.Errors });
            }

            try
            {
                var result = await _authorizationService.ResetPasswordAsync(email); 
                return Ok(new { message = result });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel register)
        {
            try
            {
                var result = await _authorizationService.RegisterAsync(register);
                return Ok(new { message = result });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel login)
        {
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
