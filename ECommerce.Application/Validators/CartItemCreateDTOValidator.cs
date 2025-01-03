using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class CartItemCreateDTOValidator : AbstractValidator<CartItemCreateDTO>
    {
        public CartItemCreateDTOValidator()
        {
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