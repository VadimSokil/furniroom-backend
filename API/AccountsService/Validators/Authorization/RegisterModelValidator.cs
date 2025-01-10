using AccountsService.Models.Authorization;
using FluentValidation;

namespace AccountsService.Validators.Authorization
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("AccountId cannot be empty.")
                .GreaterThan(0).WithMessage("AccountId must be a positive number.");

            RuleFor(x => x.AccountName)
                .NotEmpty().WithMessage("AccountName cannot be empty.")
                .MaximumLength(50).WithMessage("AccountName cannot exceed 50 characters.");

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

