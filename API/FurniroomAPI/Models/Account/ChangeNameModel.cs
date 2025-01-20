using System.ComponentModel.DataAnnotations;

namespace AccountsService.Models.Account
{
    public class ChangeNameModel
    {
        [Required]
        public string? OldName { get; set; }
        [Required]
        public string? NewName { get; set; }
    }
}
