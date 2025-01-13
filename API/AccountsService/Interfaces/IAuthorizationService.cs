using AccountsService.Models;
using AccountsService.Models.Authorization;

namespace AccountsService.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<ResponseModel> CheckEmailAsync(string email);
        public Task<string> GenerateCodeAsync(string email);
        public Task<string> ResetPasswordAsync(string email);
        public Task<string> RegisterAsync(RegisterModel register);
        public Task<int> LoginAsync(LoginModel login);
    }
}
