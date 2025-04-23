using ECommerce.Application.DTOs.Country;
using FluentValidation;

namespace ECommerce.Application.Validators.Country
{
    public class CountryUpdateDTOValidator : AbstractValidator<CountryUpdateDTO>
    {
        public CountryUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Country ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Country name is required.")
                .MaximumLength(100).WithMessage("Country name cannot exceed 100 characters.");
        }
    }
}