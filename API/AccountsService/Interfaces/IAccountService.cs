using AccountsService.Models.Account;
using AccountsService.Models.Response;

namespace AccountsService.Interfaces
{
    public interface IAccountService
    {
        public Task<ResponseModel> GetAccountInformationAsync(int? accountId);
        public Task<ResponseModel> GetAccountOrdersAsync(int? accountId);
        public Task<ResponseModel> ChangeNameAsync(ChangeNameModel changeName);
        public Task<ResponseModel> ChangeEmailAsync(ChangeEmailModel changeEmail);
        public Task<ResponseModel> ChangePasswordAsync(ChangePasswordModel changePassword);
        public Task<ResponseModel> DeleteAccountAsync(int? accountId);
    }
}
