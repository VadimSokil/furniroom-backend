using FurniroomAPI.Models.Authorization;

namespace FurniroomAPI.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<string> CheckEmailAsync(string email);
        public Task<string> GenerateCodeAsync(string email);
        public Task<string> ResetPasswordAsync(string email);
        public Task<string> RegisterAsync(RegisterModel register);
        public Task<int> LoginAsync(LoginModel login);
    }
}
