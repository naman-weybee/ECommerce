using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.API.Validators
{
    public class OrderUpdateDTOValidator : AbstractValidator<OrderUpdateDTO>
    {
        public OrderUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Order ID is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.OrderStatus)
                .IsInEnum().WithMessage("Invalid order status.");

            RuleFor(x => x.TotalAmount)
                .NotNull().WithMessage("Total amount is required.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required.")
                .Length(3, 50).WithMessage("Payment method should be between 3 and 50 characters.");

            RuleFor(x => x.ShippingAddressId)
                .NotEmpty().WithMessage("Shipping address ID is required.");

            RuleFor(x => x.OrderItems)
                .NotEmpty().WithMessage("Order items are required.")
                .Must(items => items.Count > 0).WithMessage("Order must contain at least one item.");
        }
    }
}