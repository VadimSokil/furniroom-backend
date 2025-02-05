using AccountsService.Models.Response;
using FurniroomAPI.Models.Authorization;

namespace FurniroomAPI.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<ServiceResponseModel> CheckEmailAsync(string email, string requestId);
        public Task<ServiceResponseModel> GenerateCodeAsync(string email, string requestId);
        public Task<ServiceResponseModel> ResetPasswordAsync(string email, string requestId);
        public Task<ServiceResponseModel> RegisterAsync(RegisterModel register, string requestId);
        public Task<ServiceResponseModel> LoginAsync(LoginModel login, string requestId);
    }
}
