using FurniroomAPI.Models;

namespace FurniroomAPI.Interfaces
{
    public interface IAuthorizationService
    {
        public Task CheckEmailExists(string email);
        public Task GenerateVerificationCode(string email);
        public Task AddNewUser(RegisterModel register);
        public Task Login(LoginModel login);
        public Task ResetPassword(string email);
    }
}
