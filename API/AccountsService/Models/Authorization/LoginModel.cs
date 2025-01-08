namespace AccountsService.Models.Authorization
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
