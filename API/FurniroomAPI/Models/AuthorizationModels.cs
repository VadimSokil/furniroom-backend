using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FurniroomAPI.Models
{
    public class AuthorizationModels
    {

        public class EmailModel
        {
            [Required(ErrorMessage = "Email address is a required field, must be of type string and cannot be empty.")]
            [EmailAddress(ErrorMessage = "Email address should follow the format - username@company.com")]
            [StringLength(254, ErrorMessage = "Email address cannot exceed 254 characters.")]
            [DefaultValue("string")]
            public string Email { get; set; }
        }

        public class RegisterModel
        {
            [Required(ErrorMessage = "Account ID is a required field, must be of type int and cannot be empty")]
            [Range(1, int.MaxValue, ErrorMessage = "Account ID must be a positive number.")]
            [StringLength(10, ErrorMessage = "Account ID cannot exceed 10 characters.")]
            [DefaultValue(1)]
            public int AccountId { get; set; }

            [Required(ErrorMessage = "Account name is a required field, must be of type string and cannot be empty.")]
            [StringLength(100, ErrorMessage = "Account name cannot exceed 100 characters.")]
            [DefaultValue("string")]
            public string AccountName { get; set; }

            [Required(ErrorMessage = "Email address is a required field, must be of type string and cannot be empty.")]
            [EmailAddress(ErrorMessage = "Email address should follow the format - username@company.com")]
            [StringLength(254, ErrorMessage = "Email cannot exceed 254 characters.")]
            [DefaultValue("string")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password hash is a required field, must be of type string and cannot be empty.")]
            [StringLength(128, ErrorMessage = "Password hash cannot exceed 128 characters.")]
            [DefaultValue("string")]
            public string PasswordHash { get; set; }
        }

        public class LoginModel
        {
            [Required(ErrorMessage = "Email address is a required field, must be of type string and cannot be empty.")]
            [EmailAddress(ErrorMessage = "Email address should follow the format - username@company.com")]
            [StringLength(254, ErrorMessage = "Email cannot exceed 254 characters.")]
            [DefaultValue("string")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password hash is a required field, must be of type string and cannot be empty.")]
            [StringLength(128, ErrorMessage = "Password hash cannot exceed 128 characters.")]
            [DefaultValue("string")]
            public string PasswordHash { get; set; }
        }
    }
}
