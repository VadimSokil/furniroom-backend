using AccountsService.Models.Request;
using FluentValidation;

namespace AccountsService.Validators.Request
{
    public class OrderModelValidator : AbstractValidator<OrderModel>
    {
        public OrderModelValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId не может быть пустым.")
                .GreaterThan(0).WithMessage("OrderId должен быть положительным числом.");

            RuleFor(x => x.OrderDate)
                .NotEmpty().WithMessage("OrderDate не может быть пустым.")
                .MaximumLength(20).WithMessage("OrderDate не может превышать 20 символов.");

            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("AccountId не может быть пустым.")
                .GreaterThan(0).WithMessage("AccountId должен быть положительным числом.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber не может быть пустым.")
                .MaximumLength(20).WithMessage("PhoneNumber не может превышать 20 символов.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country не может быть пустым.")
                .MaximumLength(500).WithMessage("Country не может превышать 500 символов.");

            RuleFor(x => x.Region)
                .NotEmpty().WithMessage("Region не может быть пустым.")
                .MaximumLength(500).WithMessage("Region не может превышать 500 символов.");

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("District не может быть пустым.")
                .MaximumLength(500).WithMessage("District не может превышать 500 символов.");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Street не может быть пустым.")
                .MaximumLength(500).WithMessage("Street не может превышать 500 символов.");

            RuleFor(x => x.HouseNumber)
                .NotEmpty().WithMessage("HouseNumber не может быть пустым.")
                .MaximumLength(10).WithMessage("HouseNumber не может превышать 10 символов.");

            RuleFor(x => x.OrderText)
                .NotEmpty().WithMessage("OrderText не может быть пустым.")
                .MaximumLength(20000).WithMessage("OrderText не может превышать 20000 символов.");

            RuleFor(x => x.DeliveryType)
                .NotEmpty().WithMessage("DeliveryType не может быть пустым.")
                .MaximumLength(10).WithMessage("DeliveryType не может превышать 10 символов.");

            RuleFor(x => x).Custom((model, context) =>
            {
                var expectedProperties = new HashSet<string>
                {
                    nameof(model.OrderId),
                    nameof(model.OrderDate),
                    nameof(model.AccountId),
                    nameof(model.PhoneNumber),
                    nameof(model.Country),
                    nameof(model.Region),
                    nameof(model.District),
                    nameof(model.Street),
                    nameof(model.HouseNumber),
                    nameof(model.OrderText),
                    nameof(model.DeliveryType)
                };

                var actualProperties = model.GetType().GetProperties()
                    .Select(prop => prop.Name);

                foreach (var property in actualProperties)
                {
                    if (!expectedProperties.Contains(property) &&
                        property != nameof(model.City) &&
                        property != nameof(model.Village) &&
                        property != nameof(model.ApartmentNumber))
                    {
                        context.AddFailure($"Лишнее поле: {property}");
                    }
                }

                foreach (var property in expectedProperties)
                {
                    if (!actualProperties.Contains(property))
                    {
                        context.AddFailure($"Отсутствует обязательное поле: {property}");
                    }
                }
            });
        }
    }
}
