using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class ProductCreateDTOValidator : AbstractValidator<ProductCreateDTO>
    {
        public ProductCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price is required.");

            RuleFor(x => x.Currency)
                .NotNull().WithMessage("Currency is required.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");

            RuleFor(x => x.SKU)
                .MaximumLength(50).WithMessage("SKU cannot exceed 50 characters.");

            RuleFor(x => x.Brand)
                .MaximumLength(50).WithMessage("Brand cannot exceed 50 characters.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category Id is required.");
        }
    }
}