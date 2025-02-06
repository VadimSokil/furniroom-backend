using AccountsService.Models.Response;
using static FurniroomAPI.Models.AccountModels;

namespace FurniroomAPI.Interfaces
{
    public interface IAccountService
    {
        public Task<ServiceResponseModel> GetAccountInformationAsync(AccountIdModel accountId, string requestId);
        public Task<ServiceResponseModel> GetAccountOrdersAsync(AccountIdModel accountId, string requestId);
        public Task<ServiceResponseModel> ChangeNameAsync(ChangeNameModel changeName, string requestId);
        public Task<ServiceResponseModel> ChangeEmailAsync(ChangeEmailModel changeEmail, string requestId);
        public Task<ServiceResponseModel> ChangePasswordAsync(ChangePasswordModel changePassword, string requestId);
        public Task<ServiceResponseModel> DeleteAccountAsync(AccountIdModel accountId, string requestId);
    }
}
