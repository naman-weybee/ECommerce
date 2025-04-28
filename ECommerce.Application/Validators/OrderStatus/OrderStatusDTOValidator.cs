using ECommerce.Application.DTOs.OrderStatus;
using FluentValidation;

namespace ECommerce.Application.Validators.Order
{
    public class OrderStatusDTOValidator : AbstractValidator<OrderStatusDTO>
    {
        public OrderStatusDTOValidator()
        {
            RuleFor(x => x.StatusId)
            .NotEmpty().WithMessage("StatusId is required.");

            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
        }
    }
}