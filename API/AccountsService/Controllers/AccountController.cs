﻿using AccountsService.Interfaces;
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

        [HttpPut("change-name")]
        public async Task<IActionResult> ChangeName([FromQuery] string oldName, [FromQuery] string newName)
        {
            var result = await _accountService.ChangeNameAsync(oldName, newName);
            return Ok(new { message = result });
        }

        [HttpPut("change-email")]
        public async Task<IActionResult> ChangeEmail([FromQuery] string oldEmail, [FromQuery] string newEmail)
        {
            var result = await _accountService.ChangeEmailAsync(oldEmail, newEmail);
            return Ok(new { message = result });
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromQuery] string oldPasswordHash, [FromQuery] string newPasswordHash)
        {
            var result = await _accountService.ChangePasswordAsync(oldPasswordHash, newPasswordHash);
            return Ok(new { message = result });
        }

        [HttpDelete("delete-account")]
        public async Task<IActionResult> DeleteAccount([FromQuery] int accountId)
        {
            var result = await _accountService.DeleteAccountAsync(accountId);
            return Ok(new { message = result });
        }
    }
}