using AccountsService.Models.Account;

namespace FurniroomAPI.Interfaces
{
    public interface IAccountService
    {
        public Task<string> GetAccountInformationAsync(int? accountId);
        public Task<string> GetAccountOrdersAsync(int? accountId);
        public Task<string> ChangeNameAsync(ChangeNameModel changeName);
        public Task<string> ChangeEmailAsync(ChangeEmailModel changeEmail);
        public Task<string> ChangePasswordAsync(ChangePasswordModel changePassword);
        public Task<string> DeleteAccountAsync(int? accountId);
    }
}
