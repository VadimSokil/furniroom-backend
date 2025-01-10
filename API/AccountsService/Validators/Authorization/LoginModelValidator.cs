using AccountsService.Models.Authorization;
using FluentValidation;

namespace AccountsService.Validators.Authorization
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator() 
        {
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
