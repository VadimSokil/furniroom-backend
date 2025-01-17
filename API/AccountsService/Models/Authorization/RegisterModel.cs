using System.ComponentModel.DataAnnotations;

namespace AccountsService.Models.Authorization
{
    public class RegisterModel
    {
        [Required]
        public int? AccountId { get; set; }
        [Required]
        public string? AccountName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PasswordHash { get; set; }
    }
}
