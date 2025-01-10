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

        [HttpGet("generate-code")]
        public async Task<IActionResult> GenerateCode([FromQuery] string email) 
        {
            try
            {
                var result = await _authorizationService.GenerateCodeAsync(email);
                if (result.Contains("Ошибка"))
                {
                    return BadRequest(new { message = result }); 
                }
                return Ok(new { message = result }); 
            }
            catch (MySqlException ex) when (ex.Number == 1042)
            {
                return StatusCode(503, new { message = "Соединение с базой данных отсутствует." }); 
            }
            
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email)
        {
            try
            {
                var result = await _authorizationService.ResetPasswordAsync(email);
                if (result.Contains("Ошибка"))
                {
                    return BadRequest(new { message = result }); 
                }
                return Ok(new { message = result }); 
            }
            catch (MySqlException ex) when (ex.Number == 1042)
            {
                return StatusCode(503, new { message = "Соединение с базой данных отсутствует." }); 
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                var result = await _authorizationService.LoginAsync(login);
                if (string.IsNullOrEmpty(result))
                {
                    return Unauthorized(new { message = "Неверный логин или пароль." }); 
                }

                return Ok(new { message = "Вход выполнен успешно.", userId = result }); 
            }
            catch (MySqlException ex) when (ex.Number == 1042)
            {
                return StatusCode(503, new { message = "Соединение с базой данных отсутствует." }); 
            }
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
        {
            try
            {
                await _authorizationService.RegisterAsync(register);
                return Ok(new { message = "Пользователь успешно зарегистрирован." }); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message }); 
            }
            catch (MySqlException ex) when (ex.Number == 1042)
            {
                return StatusCode(503, new { message = "Соединение с базой данных отсутствует." }); 
            }
            
        }

        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmail([FromQuery] string email)
        {
            try
            {
                var result = await _authorizationService.CheckEmailAsync(email);

                if (!string.IsNullOrEmpty(result))
                {
                    return BadRequest(new { message = result }); 
                }

                return Ok(new { message = "Почта свободна." }); 
            }
            catch (MySqlException ex) when (ex.Number == 1042)
            {
                return StatusCode(503, new { message = "Соединение с базой данных отсутствует." }); 
            }
            
        }
    }
}
