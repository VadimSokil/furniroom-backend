using System.ComponentModel.DataAnnotations;

namespace FurniroomAPI.Models.Authorization
{
    public class AuthorizationModels
    {
        public class EmailModel
        {
            [Required(ErrorMessage = "Email address cannot be empty or missing.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            [StringLength(254, ErrorMessage = "Email address cannot exceed 254 characters.")]
            public string Email { get; set; }
        }

        public class RegisterModel
        {
            [Required(ErrorMessage = "This field cannot be empty or missing.")]
            [Range(1, int.MaxValue, ErrorMessage = "Account ID must be a positive number.")]
            public int AccountId { get; set; }

            [Required(ErrorMessage = "This field cannot be empty or missing.")]
            [StringLength(100, ErrorMessage = "AccountName cannot exceed 100 characters.")]
            public string AccountName { get; set; }

            [Required(ErrorMessage = "This field cannot be empty or missing.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            [StringLength(254, ErrorMessage = "Email cannot exceed 254 characters.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "This field cannot be empty or missing.")]
            [StringLength(128, ErrorMessage = "Password hash cannot exceed 128 characters.")]
            public string PasswordHash { get; set; }
        }

        public class LoginModel
        {
            [Required(ErrorMessage = "This field cannot be empty or missing.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            [StringLength(254, ErrorMessage = "Email cannot exceed 254 characters.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "This field cannot be empty or missing.")]
            [StringLength(128, ErrorMessage = "Password hash cannot exceed 128 characters.")]
            public string PasswordHash { get; set; }
        }
    }
}
