using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class OrderCreateFromCartDTOValidator : AbstractValidator<OrderCreateFromCartDTO>
    {
        public OrderCreateFromCartDTOValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required.")
                .MaximumLength(50).WithMessage("Payment method must not exceed 50 characters.");
        }
    }
}