using Microsoft.AspNetCore.Mvc;
using AccountsService.Interfaces;
using AccountsService.Models.Authorization;
using AccountsService.Models.Response;

namespace AccountsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        public string currentDateTime = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss") + " UTC";

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet("check-email")]
        public async Task<ActionResult<ResponseModel>> CheckEmail([FromQuery] string? email)
        {
            if(!(email is string))
            {
                return new ResponseModel
                {
                    Date = currentDateTime,
                    RequestExecution = false,
                    Message = "Email must be a valid string",
                };
            }
            else
            {
                var result = await _authorizationService.CheckEmailAsync(email);
                return Ok(result);
            }
        }

        [HttpGet("generate-code")]
        public async Task<ActionResult> GenerateCode([FromQuery] string? email)
        {
            var result = await _authorizationService.GenerateCodeAsync(email);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] string? email)
        {
            var result = await _authorizationService.ResetPasswordAsync(email);
            return Ok(result);
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel register)
        {
            var result = await _authorizationService.RegisterAsync(register);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel login)
        {
            var result = await _authorizationService.LoginAsync(login);
            return Ok(result);
        }
    }
}
