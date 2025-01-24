using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.API.Validators
{
    public class OrderDTOValidator : AbstractValidator<OrderDTO>
    {
        public OrderDTOValidator()
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

            RuleFor(x => x.OrderPlacedDate)
                .NotEmpty().WithMessage("Order placed date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Order placed date cannot be in the future.");

            RuleFor(x => x.OrderShippedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Order shipped date cannot be in the future.");

            RuleFor(x => x.OrderDeliveredDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Order delivered date cannot be in the future.");

            RuleFor(x => x.OrderCanceledDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Order canceled date cannot be in the future.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required.")
                .Length(3, 50).WithMessage("Payment method should be between 3 and 50 characters.");

            RuleFor(x => x.TotalAmount)
                .NotNull().WithMessage("Total amount is required.");

            RuleFor(x => x.OrderItems)
                .NotEmpty().WithMessage("Order items are required.")
                .Must(items => items.Count > 0).WithMessage("Order must contain at least one item.");
        }
    }
}