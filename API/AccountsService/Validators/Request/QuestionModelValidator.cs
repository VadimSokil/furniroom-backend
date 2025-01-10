using AccountsService.Models.Request;
using FluentValidation;

namespace AccountsService.Validators.Request
{
    public class QuestionModelValidator : AbstractValidator<QuestionModel>
    {
        public QuestionModelValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotEmpty().WithMessage("QuestionId cannot be empty.")
                .GreaterThan(0).WithMessage("QuestionId must be a positive number.");

            RuleFor(x => x.QuestionDate)
                .NotEmpty().WithMessage("QuestionDate cannot be empty.")
                .MaximumLength(20).WithMessage("QuestionDate cannot exceed 20 characters.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName cannot be empty.")
                .MaximumLength(255).WithMessage("UserName cannot exceed 255 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber cannot be empty.")
                .MaximumLength(20).WithMessage("PhoneNumber cannot exceed 20 characters.");

            RuleFor(x => x.QuestionText)
                .NotEmpty().WithMessage("QuestionText cannot be empty.")
                .MaximumLength(20000).WithMessage("QuestionText cannot exceed 20000 characters.");
        }
    }
}
