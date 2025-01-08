using AccountsService.Models.Authorization;

namespace AccountsService.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<bool> CheckEmailAsync(string email);
        public Task<string> GenerateCodeAsync(string email);
        public Task<string> ResetPasswordAsync(string email);
        public Task<string> RegisterAsync(RegisterModel register);
        public Task<string> LoginAsync(LoginModel login);
    }
}
