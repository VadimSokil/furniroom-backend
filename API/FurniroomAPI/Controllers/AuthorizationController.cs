using FurniroomAPI.Interfaces;
using FurniroomAPI.Models.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
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
        public async Task<ActionResult<string>> CheckEmail([FromQuery] string email)
        {
            var result = await _authorizationService.CheckEmailAsync(email);
            return Ok(result);
        }

        [HttpGet("generate-code")]
        public async Task<ActionResult<string>> GenerateCode([FromQuery] string email)
        {
            var result = await _authorizationService.GenerateCodeAsync(email);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] string email)
        {
            var result = await _authorizationService.ResetPasswordAsync(email);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterModel register)
        {
            var result = await _authorizationService.RegisterAsync(register);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<int>> Login([FromBody] LoginModel login)
        {
            var result = await _authorizationService.LoginAsync(login);
            return Ok(result);
        }
    }
}
