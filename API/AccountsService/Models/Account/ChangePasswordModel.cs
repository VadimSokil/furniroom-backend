namespace AccountsService.Models.Account
{
    public class ChangePasswordModel
    {
        public string? OldPasswordHash { get; set; }
        public string? NewPasswordHash { get; set; }
    }
}
