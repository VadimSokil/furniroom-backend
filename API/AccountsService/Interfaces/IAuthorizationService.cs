using AccountsService.Models.Authorization;

namespace AccountsService.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<string> CheckEmailAsync(string email);
        public Task<string> GenerateCodeAsync(string email);
        public Task<string> ResetPasswordAsync(string email);
        public Task RegisterAsync(RegisterModel register);
        public Task<string> LoginAsync(LoginModel login);
    }
}
