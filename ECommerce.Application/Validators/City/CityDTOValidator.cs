using ECommerce.Application.DTOs.City;
using FluentValidation;

namespace ECommerce.Application.Validators.City
{
    public class CityDTOValidator : AbstractValidator<CityDTO>
    {
        public CityDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("City ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("City name is required.")
                .MaximumLength(100).WithMessage("City name cannot exceed 100 characters.");

            RuleFor(x => x.StateId)
                .NotEmpty().WithMessage("State ID is required.");
        }
    }
}