using Microsoft.AspNetCore.Mvc;
using AccountsService.Interfaces;
using AccountsService.Models.Authorization;
using MySql.Data.MySqlClient;

namespace AccountsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet("check-email")]
        public async Task<ActionResult> CheckEmail([FromQuery] EmailModel email)
        {
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
        public async Task<ActionResult> GenerateCode([FromQuery] EmailModel email)
        {
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
        public async Task<ActionResult> ResetPassword([FromQuery] EmailModel email)
        {
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
                var result = await _authorizationService.LoginAsync(login);
                return Ok(new { message = result });
            }
            catch (MySqlException ex)
            {
                return StatusCode(500, new { message = "Не удалось установить связь с базой данных." });
            }
        }
    }
}
