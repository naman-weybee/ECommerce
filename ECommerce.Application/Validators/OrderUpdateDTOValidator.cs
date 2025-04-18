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

            RuleFor(x => x.BillingAddressId)
                .NotEmpty().WithMessage("Order Billing Address Id is required.");

            RuleFor(x => x.ShippingAddressId)
                .NotEmpty().WithMessage("Order Shipping Address Id is required.");

            RuleFor(x => x.OrderStatus)
                .IsInEnum().WithMessage("Invalid order status.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required.")
                .Length(3, 50).WithMessage("Payment method should be between 3 and 50 characters.");

            RuleFor(x => x.TotalAmount)
                .NotNull().WithMessage("Total amount is required.");
        }
    }
}