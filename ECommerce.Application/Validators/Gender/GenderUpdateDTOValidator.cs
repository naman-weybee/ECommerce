using ECommerce.Application.DTOs.Gender;
using FluentValidation;

namespace ECommerce.Application.Validators.Gender
{
    public class GenderUpdateDTOValidator : AbstractValidator<GenderUpdateDTO>
    {
        public GenderUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Gender ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Gender name is required.")
                .MaximumLength(100).WithMessage("Gender name cannot exceed 100 characters.");
        }
    }
}