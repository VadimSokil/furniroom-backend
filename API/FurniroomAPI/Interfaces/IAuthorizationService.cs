using AccountsService.Models.Response;
using static FurniroomAPI.Models.Authorization.AuthorizationModels;

namespace FurniroomAPI.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<ServiceResponseModel> CheckEmailAsync(EmailModel email, string requestId);
        public Task<ServiceResponseModel> GenerateCodeAsync(EmailModel email, string requestId);
        public Task<ServiceResponseModel> ResetPasswordAsync(EmailModel email, string requestId);
        public Task<ServiceResponseModel> RegisterAsync(RegisterModel register, string requestId);
        public Task<ServiceResponseModel> LoginAsync(LoginModel login, string requestId);
    }
}
