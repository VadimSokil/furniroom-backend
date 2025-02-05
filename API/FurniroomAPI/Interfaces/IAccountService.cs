using AccountsService.Models.Account;
using AccountsService.Models.Response;

namespace FurniroomAPI.Interfaces
{
    public interface IAccountService
    {
        public Task<ServiceResponseModel> GetAccountInformationAsync(int accountId, string requestId);
        public Task<ServiceResponseModel> GetAccountOrdersAsync(int accountId, string requestId);
        public Task<ServiceResponseModel> ChangeNameAsync(ChangeNameModel changeName, string requestId);
        public Task<ServiceResponseModel> ChangeEmailAsync(ChangeEmailModel changeEmail, string requestId);
        public Task<ServiceResponseModel> ChangePasswordAsync(ChangePasswordModel changePassword, string requestId);
        public Task<ServiceResponseModel> DeleteAccountAsync(int accountId, string requestId);
    }
}
