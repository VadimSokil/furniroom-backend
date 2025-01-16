using AccountsService.Models;
using AccountsService.Models.Authorization;

namespace AccountsService.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<ResponseModel> CheckEmailAsync(string email);
        public Task<ResponseModel> GenerateCodeAsync(string? email);
        public Task<ResponseModel> ResetPasswordAsync(string? email);
        public Task<ResponseModel> RegisterAsync(RegisterModel register);
        public Task<ResponseModel> LoginAsync(LoginModel login);
    }
}
