using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.API.Validators
{
    public class AddressUpdateDTOValidator : AbstractValidator<AddressUpdateDTO>
    {
        public AddressUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Address ID is required.");

            RuleFor(x => x.Street)
                .MaximumLength(200).WithMessage("Street cannot exceed 200 characters.");

            RuleFor(x => x.City)
                .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

            RuleFor(x => x.State)
                .MaximumLength(100).WithMessage("State cannot exceed 100 characters.");

            RuleFor(x => x.PostalCode)
                .MaximumLength(20).WithMessage("Postal Code cannot exceed 20 characters.");

            RuleFor(x => x.Country)
                .MaximumLength(100).WithMessage("Country cannot exceed 100 characters.");
        }
    }
}