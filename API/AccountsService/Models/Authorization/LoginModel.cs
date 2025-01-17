using System.ComponentModel.DataAnnotations;

namespace AccountsService.Models.Authorization
{
    public class LoginModel
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PasswordHash { get; set; }
    }
}
