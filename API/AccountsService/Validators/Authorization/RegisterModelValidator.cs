using AccountsService.Models.Authorization;
using FluentValidation;

namespace AccountsService.Validators.Authorization
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator() 
        {
            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("AccountId не может быть пустым.")
                .GreaterThan(0).WithMessage("AccountId должен быть положительным числом.");

            RuleFor(x => x.AccountName)
                .NotEmpty().WithMessage("AccountName не может быть пустым.")
                .MaximumLength(50).WithMessage("AccountName не может превышать 50 символов.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email не может быть пустым.")
                .MaximumLength(100).WithMessage("Email не может превышать 100 символов.")
                .EmailAddress().WithMessage("Некорректный формат электронной почты.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("PasswordHash не может быть пустым.")
                .MaximumLength(500).WithMessage("PasswordHash не может превышать 500 символов.");
        }
    }
}
