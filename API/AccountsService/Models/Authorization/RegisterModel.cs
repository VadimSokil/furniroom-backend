namespace AccountsService.Models.Authorization
{
    public class RegisterModel
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
