using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class GenderCreateDTOValidator : AbstractValidator<GenderCreateDTO>
    {
        public GenderCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Gender name is required.")
                .MaximumLength(100).WithMessage("Gender name cannot exceed 100 characters.");
        }
    }
}