namespace FurniroomAPI.Interfaces
{
    public interface IAccountService
    {
        public Task<string> GetAccountInformationAsync(int accountId);
        public Task<string> GetAccountOrdersAsync(int accountId);
        public Task<string> ChangeNameAsync(string oldName, string newName);
        public Task<string> ChangeEmailAsync(string oldEmail, string newEmail);
        public Task<string> ChangePasswordAsync(string oldPasswordHash, string newPasswordHash);
        public Task<string> DeleteAccountAsync(int accountId);
    }
}
