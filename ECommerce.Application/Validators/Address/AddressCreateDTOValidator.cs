using ECommerce.Application.DTOs.Address;
using FluentValidation;

namespace ECommerce.Application.Validators.Address
{
    public class AddressCreateDTOValidator : AbstractValidator<AddressCreateDTO>
    {
        public AddressCreateDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required")
                .MaximumLength(100).WithMessage("First Name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required")
                .MaximumLength(100).WithMessage("Last Name cannot exceed 50 characters.");

            RuleFor(x => x.CountryId)
                .NotEmpty().WithMessage("CountryId is required");

            RuleFor(x => x.StateId)
                .NotEmpty().WithMessage("StateId is required");

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("CityId is required");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("PostalCode is required")
                .MaximumLength(20).WithMessage("Postal Code cannot exceed 20 characters.");

            RuleFor(x => x.AdderessType)
                .IsInEnum().WithMessage("Adderess Type is required");

            RuleFor(x => x.AddressLine)
                .NotEmpty().WithMessage("Address Line is required")
                .MaximumLength(500).WithMessage("Address Line cannot exceed 20 characters.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Please provide a valid phone number.");
        }
    }
}