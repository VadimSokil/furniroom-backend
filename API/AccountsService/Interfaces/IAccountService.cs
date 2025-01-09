using AccountsService.Models.Account;

namespace AccountsService.Interfaces
{
    public interface IAccountService
    {
        public Task<AccountInformationModel> GetAccountInformationAsync(int accountId);
        public Task<List<AccountOrdersModel>> GetAccountOrdersAsync(int accountId);
        public Task<string> ChangeNameAsync(string oldName, string newName);
        public Task<string> ChangeEmailAsync(string oldEmail, string newEmail);
        public Task<string> ChangePasswordAsync(string oldPasswordHash, string newPasswordHash);
        public Task<string> DeleteAccountAsync(int accountId);
    }
}
