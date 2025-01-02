using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class ProductPriceChangeDTOValidator : AbstractValidator<ProductPriceChangeDTO>
    {
        public ProductPriceChangeDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price is required.");
        }
    }
}