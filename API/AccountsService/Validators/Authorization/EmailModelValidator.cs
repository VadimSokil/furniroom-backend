using AccountsService.Models.Authorization;
using FluentValidation;

namespace AccountsService.Validators.Authorization
{
    public class EmailModelValidator : AbstractValidator<EmailModel>
    {
        public EmailModelValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email не может быть пустым.")
                .MaximumLength(100).WithMessage("Email не может превышать 100 символов.")
                .EmailAddress().WithMessage("Некорректный формат электронной почты.");
        }
    }
}
