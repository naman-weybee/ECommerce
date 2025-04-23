using ECommerce.Application.DTOs.Category;
using FluentValidation;

namespace ECommerce.Application.Validators.Category
{
    public class CategoryUpdateDTOValidator : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Category ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}