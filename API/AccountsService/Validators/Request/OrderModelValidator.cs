using AccountsService.Models.Request;
using FluentValidation;

namespace AccountsService.Validators.Request
{
    public class OrderModelValidator : AbstractValidator<OrderModel>
    {
        public OrderModelValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId cannot be empty.")
                .GreaterThan(0).WithMessage("OrderId must be a positive number.");

            RuleFor(x => x.OrderDate)
                .NotEmpty().WithMessage("OrderDate cannot be empty.")
                .MaximumLength(20).WithMessage("OrderDate cannot exceed 20 characters.");

            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("AccountId cannot be empty.")
                .GreaterThan(0).WithMessage("AccountId must be a positive number.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber cannot be empty.")
                .MaximumLength(20).WithMessage("PhoneNumber cannot exceed 20 characters.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country cannot be empty.")
                .MaximumLength(500).WithMessage("Country cannot exceed 500 characters.");

            RuleFor(x => x.Region)
                .NotEmpty().WithMessage("Region cannot be empty.")
                .MaximumLength(500).WithMessage("Region cannot exceed 500 characters.");

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("District cannot be empty.")
                .MaximumLength(500).WithMessage("District cannot exceed 500 characters.");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street cannot be empty.")
                .MaximumLength(500).WithMessage("Street cannot exceed 500 characters.");

            RuleFor(x => x.HouseNumber)
                .NotEmpty().WithMessage("HouseNumber cannot be empty.")
                .MaximumLength(10).WithMessage("HouseNumber cannot exceed 10 characters.");

            RuleFor(x => x.OrderText)
                .NotEmpty().WithMessage("OrderText cannot be empty.")
                .MaximumLength(20000).WithMessage("OrderText cannot exceed 20000 characters.");

            RuleFor(x => x.DeliveryType)
                .NotEmpty().WithMessage("DeliveryType cannot be empty.")
                .MaximumLength(10).WithMessage("DeliveryType cannot exceed 10 characters.");
        }
    }
}
