using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class StateCreateDTOValidator : AbstractValidator<StateCreateDTO>
    {
        public StateCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("State name is required.")
                .MaximumLength(100).WithMessage("State name cannot exceed 100 characters.");

            RuleFor(x => x.CountryId)
                .NotEmpty().WithMessage("Country ID is required.");
        }
    }
}
