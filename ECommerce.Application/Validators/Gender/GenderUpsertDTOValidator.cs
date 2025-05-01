using ECommerce.Application.DTOs.Gender;
using FluentValidation;

namespace ECommerce.Application.Validators.Gender
{
    public class GenderUpsertDTOValidator : AbstractValidator<GenderUpsertDTO>
    {
        public GenderUpsertDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Gender name is required.")
                .MaximumLength(100).WithMessage("Gender name cannot exceed 100 characters.");
        }
    }
}