using Microsoft.AspNetCore.Mvc;
using FurniroomAPI.Models;
using FurniroomAPI.Interfaces;

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

        [HttpPost("add-user")]
        public async Task<IActionResult> AddNewUser([FromBody] RegisterModel register)
        {
            await _authorizationService.AddNewUser(register);
            return Ok();
        }

        [HttpPost("check-email")]
        public async Task<IActionResult> CheckEmail([FromBody] string email)
        {
            await _authorizationService.CheckEmailExists(email);
            return Ok();
        }

        [HttpPost("generate-code")]
        public async Task<IActionResult> GenerateCode([FromBody] string email)
        {
            await _authorizationService.GenerateVerificationCode(email);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            await _authorizationService.Login(login);
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] string email)
        {
            await _authorizationService.ResetPassword(email);
            return Ok();
        }
    }
}
