using Microsoft.AspNetCore.Mvc;
using AccountsService.Interfaces;
using AccountsService.Models.Authorization;

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
        public async Task<ActionResult> CheckEmail([FromQuery] string email)
        {
            var result = await _authorizationService.CheckEmailAsync(email);
            return Ok(new { message = result });
        }

        [HttpPost("generate-code")]
        public async Task<ActionResult> GenerateCode([FromBody] string email)
        {
            var code = await _authorizationService.GenerateCodeAsync(email);
            return Ok(new { code });
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] string email)
        {
            var resultMessage = await _authorizationService.ResetPasswordAsync(email);
            return Ok(new { message = resultMessage });
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel register)
        {
            var resultMessage = await _authorizationService.RegisterAsync(register);
            return Ok(new { message = resultMessage });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel login)
        {
            var accountId = await _authorizationService.LoginAsync(login);
            return Ok(new { message = accountId });
        }
    }
}
