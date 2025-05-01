using ECommerce.Application.DTOs.State;
using FluentValidation;

namespace ECommerce.Application.Validators.State
{
    public class StateUpsertDTOValidator : AbstractValidator<StateUpsertDTO>
    {
        public StateUpsertDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("State name is required.")
                .MaximumLength(100).WithMessage("State name cannot exceed 100 characters.");

            RuleFor(x => x.CountryId)
                .NotEmpty().WithMessage("Country ID is required.");
        }
    }
}