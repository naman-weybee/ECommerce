using ECommerce.Application.DTOs.State;
using FluentValidation;

namespace ECommerce.Application.Validators.State
{
    public class StateDTOValidator : AbstractValidator<StateDTO>
    {
        public StateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("State ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("State name is required.")
                .MaximumLength(100).WithMessage("State name cannot exceed 100 characters.");

            RuleFor(x => x.CountryId)
                .NotEmpty().WithMessage("Country ID is required.");
        }
    }
}