using System.ComponentModel.DataAnnotations;

namespace AccountsService.Models.Account
{
    public class AccountInformationModel
    {
        [Required]
        public string? AccountName { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
