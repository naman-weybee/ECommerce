using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class CountryCreateDTOValidator : AbstractValidator<CountryCreateDTO>
    {
        public CountryCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Country name is required.")
                .MaximumLength(100).WithMessage("Country name cannot exceed 100 characters.");
        }
    }
}