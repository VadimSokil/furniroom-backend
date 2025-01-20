using AccountsService.Models.Account;
using AccountsService.Models.Response;

namespace FurniroomAPI.Interfaces
{
    public interface IAccountService
    {
        public Task<ServiceResponseModel> GetAccountInformationAsync(int accountId);
        public Task<ServiceResponseModel> GetAccountOrdersAsync(int accountId);
        public Task<ServiceResponseModel> ChangeNameAsync(ChangeNameModel changeName);
        public Task<ServiceResponseModel> ChangeEmailAsync(ChangeEmailModel changeEmail);
        public Task<ServiceResponseModel> ChangePasswordAsync(ChangePasswordModel changePassword);
        public Task<ServiceResponseModel> DeleteAccountAsync(int accountId);
    }
}
