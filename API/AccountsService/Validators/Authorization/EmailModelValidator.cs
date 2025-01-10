using FluentValidation;

namespace AccountsService.Validators.Authorization
{
    public class EmailModelValidator : AbstractValidator<string>
    {
        public EmailModelValidator() 
        {
            RuleFor(x => x)
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters")
                .EmailAddress().WithMessage("Invalid email format");
        }
    }
}
