using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class OrderItemUnitPriceUpdateDTOValidator : AbstractValidator<OrderItemUnitPriceUpdateDTO>
    {
        public OrderItemUnitPriceUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User Id is required");

            RuleFor(x => x.UnitPrice)
                .NotNull().WithMessage("Unit price is required.");
        }
    }
}