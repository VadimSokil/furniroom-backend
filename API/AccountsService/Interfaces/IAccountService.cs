using AccountsService.Models;

namespace AccountsService.Interfaces
{
    public interface IAccountService
    {
        public Task<ResponseModel> GetAccountInformationAsync(int? accountId);
        public Task<ResponseModel> GetAccountOrdersAsync(int? accountId);
        public Task<ResponseModel> ChangeNameAsync(string? oldName, string? newName);
        public Task<ResponseModel> ChangeEmailAsync(string? oldEmail, string? newEmail);
        public Task<ResponseModel> ChangePasswordAsync(string? oldPasswordHash, string? newPasswordHash);
        public Task<ResponseModel> DeleteAccountAsync(int? accountId);
    }
}
