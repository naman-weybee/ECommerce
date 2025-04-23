using ECommerce.Application.DTOs.CartItem;
using FluentValidation;

namespace ECommerce.Application.Validators.CartItem
{
    public class CartItemQuantityUpdateDTOValidator : AbstractValidator<CartItemQuantityUpdateDTO>
    {
        public CartItemQuantityUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User Id is required.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}