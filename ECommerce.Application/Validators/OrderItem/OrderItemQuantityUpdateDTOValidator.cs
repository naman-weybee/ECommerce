using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators.OrderItem
{
    public class OrderItemQuantityUpdateDTOValidator : AbstractValidator<OrderItemQuantityUpdateDTO>
    {
        public OrderItemQuantityUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

            RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User Id is required");

            RuleFor(x => x.Quantity)
            .NotEmpty().WithMessage("Quantity is required");
        }
    }
}