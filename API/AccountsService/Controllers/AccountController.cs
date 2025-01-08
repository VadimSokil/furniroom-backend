using AccountsService.Interfaces;
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
        public async Task<IActionResult> GetAccountInfo([FromQuery] int accountId)
        {
            var accountInfo = await _accountService.GetAccountInformationAsync(accountId);
            return Ok(accountInfo);
        }

        [HttpGet("get-orders")]
        public async Task<IActionResult> GetOrders([FromQuery] int accountId)
        {
            var orders = await _accountService.GetAccountOrdersAsync(accountId);
            return Ok(orders);
        }
    }
}
