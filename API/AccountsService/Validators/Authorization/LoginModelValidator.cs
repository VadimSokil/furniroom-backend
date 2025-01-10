using AccountsService.Models.Authorization;
using FluentValidation;

namespace AccountsService.Validators.Authorization
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("PasswordHash cannot be empty.")
                .MaximumLength(500).WithMessage("PasswordHash cannot exceed 500 characters.");
        }
    }
}
