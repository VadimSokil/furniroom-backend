using Microsoft.AspNetCore.Mvc;
using AuthorizationService.Interfaces;
using AuthorizationService.Models;

namespace AuthorizationService.Controllers
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

        [HttpPost("check-email")]
        public IActionResult CheckEmail([FromBody] string email)
        {
            bool emailExists = _authorizationService.CheckEmailExists(email);

            if (emailExists)
            {
                return Ok(new { message = "Така пошта вже є в базі." });
            }
            else
            {
                return Ok(new { message = "Чудово, такої пошти ще немає в базі." });
            }
        }

        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] RegisterModel register)
        {
            bool userAdded = _authorizationService.AddNewUser(register);

            if (userAdded)
            {
                return Ok(new { message = "Користувача успішно додано." });
            }
            else
            {
                return BadRequest(new { message = "Помилка: неможливо додати користувача." });
            }
        }

        [HttpPost("generate-code")]
        public IActionResult GenerateCode([FromBody] string email)
        {
            int verificationCode = _authorizationService.GenerateVerificationCode(email);

            return Ok(new
            {
                message = "Код успішно згенеровано.",
                code = verificationCode
            });
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] string email)
        {
            string code = _authorizationService.ResetPassword(email);

            return Ok(new
            {
                message = "Ваш пароль скинуто, ось новий:",
                code = code

            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var response = _authorizationService.Login(loginModel);

            if (response == "Вхід виконано.")
            {
                return Ok(new { message = response });
            }
            else
            {
                return Unauthorized(new { message = response });
            }
        }

    }
}
