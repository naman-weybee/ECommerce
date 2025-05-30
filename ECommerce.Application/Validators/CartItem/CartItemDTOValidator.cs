using ECommerce.Application.DTOs.CartItem;
using FluentValidation;

namespace ECommerce.Application.Validators.CartItem
{
    public class CartItemDTOValidator : AbstractValidator<CartItemDTO>
    {
        public CartItemDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Cart Item ID is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.UnitPrice)
                .NotNull().WithMessage("Unit price is required.");
        }
    }
}