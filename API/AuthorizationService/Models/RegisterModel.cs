namespace AccountsService.Models
{
    public class RegisterModel
    {
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
