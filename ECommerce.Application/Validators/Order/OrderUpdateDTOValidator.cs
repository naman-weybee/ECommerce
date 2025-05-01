using ECommerce.Application.DTOs.OrderItem;
using FluentValidation;

namespace ECommerce.Application.Validators.Order
{
    public class OrderUpdateDTOValidator : AbstractValidator<OrderUpdateDTO>
    {
        public OrderUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Order ID is required.");

            RuleFor(x => x.BillingAddressId)
                .NotEmpty().WithMessage("Order Billing Address Id is required.");

            RuleFor(x => x.ShippingAddressId)
                .NotEmpty().WithMessage("Order Shipping Address Id is required.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required.")
                .Length(3, 50).WithMessage("Payment method should be between 3 and 50 characters.");
        }
    }
}