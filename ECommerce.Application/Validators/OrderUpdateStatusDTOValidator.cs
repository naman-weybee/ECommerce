using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class OrderUpdateStatusDTOValidator : AbstractValidator<OrderUpdateStatusDTO>
    {
        public OrderUpdateStatusDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Order Id is required.");

            RuleFor(x => x.OrderStatus)
                .IsInEnum().WithMessage("Invalid order status.");
        }
    }
}