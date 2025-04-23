using ECommerce.Application.DTOs.CartItem;
using FluentValidation;

namespace ECommerce.Application.Validators.CartItem
{
    public class CartItemUnitPriceUpdateDTOValidator : AbstractValidator<CartItemUnitPriceUpdateDTO>
    {
        public CartItemUnitPriceUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User Id is required.");

            RuleFor(x => x.UnitPrice)
                .NotNull().WithMessage("UnitPrice is required.");
        }
    }
}