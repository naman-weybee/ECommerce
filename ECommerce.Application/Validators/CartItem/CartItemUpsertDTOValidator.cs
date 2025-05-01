using ECommerce.Application.DTOs.CartItem;
using FluentValidation;

namespace ECommerce.Application.Validators.CartItem
{
    public class CartItemUpsertDTOValidator : AbstractValidator<CartItemUpsertDTO>
    {
        public CartItemUpsertDTOValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}