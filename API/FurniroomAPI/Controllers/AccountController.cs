using AccountsService.Models.Account;
using FurniroomAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FurniroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("account-information")]
        public async Task<ActionResult<string>> AccountInformation([FromQuery] int accountId)
        {
            var result = await _accountService.GetAccountInformationAsync(accountId);
            return Ok(result);
        }

        [HttpGet("account-orders")]
        public async Task<ActionResult<string>> AccountOrders([FromQuery] int accountId)
        {
            var result = await _accountService.GetAccountOrdersAsync(accountId);
            return Ok(result);
        }

        [HttpPut("change-name")]
        public async Task<ActionResult<string>> ChangeName([FromBody] ChangeNameModel changeName)
        {
            
            var result = await _accountService.ChangeNameAsync(changeName);
            return Ok(result);
        }

        [HttpPut("change-email")]
        public async Task<ActionResult<string>> ChangeEmail([FromBody] ChangeEmailModel changeEmail)
        {

            var result = await _accountService.ChangeEmailAsync(changeEmail);
            return Ok(result);
        }

        [HttpPut("change-password")]
        public async Task<ActionResult<string>> ChangePassword([FromBody] ChangePasswordModel changePassword)
        {

            var result = await _accountService.ChangePasswordAsync(changePassword);
            return Ok(result);
        }

        [HttpDelete("delete-account")]
        public async Task<ActionResult<string>> DeleteAccount([FromBody] int? accountId)
        {
            var result = await _accountService.DeleteAccountAsync(accountId);
            return Ok(result);
        }
    }
}
