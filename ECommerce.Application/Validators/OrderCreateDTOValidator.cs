using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.API.Validators
{
    public class OrderCreateDTOValidator : AbstractValidator<OrderCreateDTO>
    {
        public OrderCreateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.BillingAddressId)
                .NotEmpty().WithMessage("Order Billing Address Id is required.");

            RuleFor(x => x.ShippingAddressId)
                .NotEmpty().WithMessage("Order Shipping Address Id is required.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required.")
                .Length(3, 50).WithMessage("Payment method should be between 3 and 50 characters.");

            RuleFor(x => x.TotalAmount)
                .NotNull().WithMessage("Total amount is required.");
        }
    }
}