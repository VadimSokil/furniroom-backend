using AccountsService.Models.Authorization;

namespace AccountsService.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<string> CheckEmailAsync(EmailModel email);
        public Task<string> GenerateCodeAsync(EmailModel email);
        public Task<string> ResetPasswordAsync(EmailModel email);
        public Task<string> RegisterAsync(RegisterModel register);
        public Task<int> LoginAsync(LoginModel login);
    }
}
