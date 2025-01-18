using System.ComponentModel.DataAnnotations;

namespace AccountsService.Models.Account
{
    public class ChangePasswordModel
    {
        [Required]
        public string? OldPasswordHash { get; set; }
        [Required]
        public string? NewPasswordHash { get; set; }
    }
}
