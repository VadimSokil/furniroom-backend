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

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet("check-email")]
        public async Task<ActionResult<ServiceResponseModel>> CheckEmail([FromQuery]string email)
        {
            var result = await _authorizationService.CheckEmailAsync(email);
            return Ok(result);
        }

        [HttpGet("generate-code")]
        public async Task<ActionResult<ServiceResponseModel>> GenerateCode([FromQuery]string email)
        {
            var result = await _authorizationService.GenerateCodeAsync(email);
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ServiceResponseModel>> ResetPassword([FromQuery]string email)
        {
            var result = await _authorizationService.ResetPasswordAsync(email);
            return Ok(result);
        }


        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponseModel>> Register([FromBody] RegisterModel register)
        {
            var result = await _authorizationService.RegisterAsync(register);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponseModel>> Login([FromBody] LoginModel login)
        {
            var result = await _authorizationService.LoginAsync(login);
            return Ok(result);
        }
    }
}
