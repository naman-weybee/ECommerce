using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class GenderDTOValidator : AbstractValidator<GenderDTO>
    {
        public GenderDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Gender ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Gender name is required.")
                .MaximumLength(100).WithMessage("Gender name cannot exceed 100 characters.");
        }
    }
}