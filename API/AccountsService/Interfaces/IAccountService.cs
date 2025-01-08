using AccountsService.Models.Account;

namespace AccountsService.Interfaces
{
    public interface IAccountService
    {
        public Task<AccountInformationModel> GetAccountInformationAsync(int accountId);
        public Task<List<AccountOrdersModel>> GetAccountOrdersAsync(int accountId);
    }
}
