using ECommerce.Application.DTOs.Country;
using FluentValidation;

namespace ECommerce.Application.Validators.Country
{
    public class CountryCreateDTOValidator : AbstractValidator<CountryUpsertDTO>
    {
        public CountryCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Country name is required.")
                .MaximumLength(100).WithMessage("Country name cannot exceed 100 characters.");
        }
    }
}