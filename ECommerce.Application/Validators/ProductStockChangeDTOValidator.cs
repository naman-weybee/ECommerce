using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class ProductStockChangeDTOValidator : AbstractValidator<ProductStockChangeDTO>
    {
        public ProductStockChangeDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity should be greater than zero.");
        }
    }
}