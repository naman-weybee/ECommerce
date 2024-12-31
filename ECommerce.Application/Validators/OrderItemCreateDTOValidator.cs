using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.API.Validators
{
    public class OrderItemCreateDTOValidator : AbstractValidator<OrderItemCreateDTO>
    {
        public OrderItemCreateDTOValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("Order ID is required.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.");

            RuleFor(x => x.UnitPrice)
                .NotNull().WithMessage("Unit price is required.");
        }
    }
}