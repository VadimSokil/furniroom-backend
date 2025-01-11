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
        public async Task<ActionResult<string>> AccountInformation(int accountId)
        {
            var result = await _accountService.GetAccountInformationAsync(accountId);
            return Ok(result);
        }

        [HttpGet("account-orders")]
        public async Task<ActionResult<string>> AccountOrders(int accountId)
        {
            var result = await _accountService.GetAccountOrdersAsync(accountId);
            return Ok(result);
        }

        [HttpPut("change-name")]
        public async Task<ActionResult<string>> ChangeName([FromBody] string oldName, string newName)
        {
            
            var result = await _accountService.ChangeNameAsync(oldName, newName);
            return Ok(result);
        }

        [HttpPut("change-email")]
        public async Task<ActionResult<string>> ChangeEmail([FromBody] string oldEmail, string newEmail)
        {

            var result = await _accountService.ChangeNameAsync(oldEmail, newEmail);
            return Ok(result);
        }

        [HttpPut("change-password")]
        public async Task<ActionResult<string>> ChangePassword([FromBody] string oldPassword, string newPassword)
        {

            var result = await _accountService.ChangeNameAsync(oldPassword, newPassword);
            return Ok(result);
        }

        [HttpDelete("delete-account")]
        public async Task<ActionResult<string>> DeleteAccount(int accountId)
        {
            var result = await _accountService.DeleteAccountAsync(accountId);
            return Ok(result);
        }
    }
}
