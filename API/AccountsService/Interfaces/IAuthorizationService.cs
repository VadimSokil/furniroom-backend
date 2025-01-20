using AccountsService.Models.Response;
using AccountsService.Models.Authorization;

namespace AccountsService.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<ServiceResponseModel> CheckEmailAsync(string email);
        public Task<ServiceResponseModel> GenerateCodeAsync(string email);
        public Task<ServiceResponseModel> ResetPasswordAsync(string email);
        public Task<ServiceResponseModel> RegisterAsync(RegisterModel register);
        public Task<ServiceResponseModel> LoginAsync(LoginModel login);
    }
}
