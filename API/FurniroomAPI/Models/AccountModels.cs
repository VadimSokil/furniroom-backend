using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurniroomAPI.Models
{
    public class AccountModels
    {
        public class AccountIdModel
        {
            [Required(ErrorMessage = "Account ID is a required field, must be of type int and cannot be empty")]
            [Range(1, int.MaxValue, ErrorMessage = "Account ID must be a positive number.")]
            [StringLength(10, ErrorMessage = "Account ID cannot exceed 10 characters.")]
            [DefaultValue(1)]
            public int AccountId { get; set; }
        }

        public class ChangeNameModel
        {
            [Required(ErrorMessage = "Old name is a required field, must be of type string and cannot be empty.")]
            [StringLength(100, ErrorMessage = "Old name cannot exceed 100 characters.")]
            [DefaultValue("string")]
            public string OldName { get; set; }

            [Required(ErrorMessage = "New name is a required field, must be of type string and cannot be empty.")]
            [StringLength(100, ErrorMessage = "New name cannot exceed 100 characters.")]
            [DefaultValue("string")]
            public string NewName { get; set; }
        }

        public class ChangeEmailModel
        {
            [Required(ErrorMessage = "Old email is a required field, must be of type string and cannot be empty.")]
            [EmailAddress(ErrorMessage = "Email address should follow the format - username@company.com")]
            [StringLength(254, ErrorMessage = "Old email cannot exceed 254 characters.")]
            [DefaultValue("string")]
            public string OldEmail { get; set; }

            [Required(ErrorMessage = "New email is a required field, must be of type string and cannot be empty.")]
            [EmailAddress(ErrorMessage = "Email address should follow the format - username@company.com")]
            [StringLength(254, ErrorMessage = "New email cannot exceed 254 characters.")]
            [DefaultValue("string")]
            public string NewEmail { get; set; }
        }

        public class ChangePasswordModel
        {
            [Required(ErrorMessage = "Old password hash is a required field, must be of type string and cannot be empty.")]
            [StringLength(128, ErrorMessage = "Old password hash cannot exceed 128 characters.")]
            [DefaultValue("string")]
            public string OldPasswordHash { get; set; }

            [Required(ErrorMessage = "New password hash is a required field, must be of type string and cannot be empty.")]
            [StringLength(128, ErrorMessage = "New password hash cannot exceed 128 characters.")]
            [DefaultValue("string")]
            public string NewPasswordHash { get; set; }
        }
    }
}
