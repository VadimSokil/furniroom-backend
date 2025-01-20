using AccountsService.Interfaces;
using AccountsService.Models.Account;
using AccountsService.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace AccountsService.Controllers
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

        [HttpGet("get-account-info")]
        public async Task<ActionResult<ServiceResponseModel>> GetAccountInfo([FromQuery]int accountId)
        {
            
            var result = await _accountService.GetAccountInformationAsync(accountId);
            return Ok(result);

        }

        [HttpGet("get-orders")]
        public async Task<ActionResult<ServiceResponseModel>> GetOrders([FromQuery]int accountId)
        {
            var result = await _accountService.GetAccountOrdersAsync(accountId);
            return Ok(result);
        }

        [HttpPut("change-name")]
        public async Task<ActionResult<ServiceResponseModel>> ChangeName([FromBody] ChangeNameModel changeName)
        {
            var result = await _accountService.ChangeNameAsync(changeName);
            return Ok(result);
        }

        [HttpPut("change-email")]
        public async Task<ActionResult<ServiceResponseModel>> ChangeEmail([FromBody] ChangeEmailModel changeEmail)
        {
            var result = await _accountService.ChangeEmailAsync(changeEmail);
            return Ok(result);
        }

        [HttpPut("change-password")]
        public async Task<ActionResult<ServiceResponseModel>> ChangePassword([FromBody] ChangePasswordModel changePassword)
        {
            var result = await _accountService.ChangePasswordAsync(changePassword);
            return Ok(result);
        }

        [HttpDelete("delete-account")]
        public async Task<ActionResult<ServiceResponseModel>> DeleteAccount([FromQuery]int accountId)
        {
            var result = await _accountService.DeleteAccountAsync(accountId);
            return Ok(result);
        }
    }
}
