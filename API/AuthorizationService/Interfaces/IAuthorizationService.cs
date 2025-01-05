using AuthorizationService.Models;

namespace AuthorizationService.Interfaces
{
    public interface IAuthorizationService
    {
        bool CheckEmailExists(string email);
        int GenerateVerificationCode(string email);
        bool AddNewUser(RegisterModel register);
        string Login(LoginModel login);
        string ResetPassword(string email);

    }
}
