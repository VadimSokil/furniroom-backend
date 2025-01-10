using AccountsService.Models.Request;
using FluentValidation;

namespace AccountsService.Validators.Request
{
    public class QuestionModelValidator : AbstractValidator<QuestionModel>
    {
        public QuestionModelValidator() 
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty().WithMessage("QuestionId не может быть пустым.")
                .GreaterThan(0).WithMessage("QuestionId должен быть положительным числом.");

            RuleFor(x => x.QuestionDate)
                .NotEmpty().WithMessage("QuestionDate не может быть пустым.")
                .MaximumLength(20).WithMessage("QuestionDate не может превышать 20 символов.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName не может быть пустым.")
                .MaximumLength(255).WithMessage("UserName не может превышать 255 символов.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber не может быть пустым.")
                .MaximumLength(20).WithMessage("PhoneNumber не может превышать 20 символов.");

            RuleFor(x => x.QuestionText)
                .NotEmpty().WithMessage("QuestionText не может быть пустым.")
                .MaximumLength(20000).WithMessage("QuestionText не может превышать 20000 символов.");
        }
    }
}
