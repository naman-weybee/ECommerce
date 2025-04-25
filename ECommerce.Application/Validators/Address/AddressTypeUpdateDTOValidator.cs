using ECommerce.Application.DTOs.Address;
using FluentValidation;

namespace ECommerce.Application.Validators.Address
{
    public class AddressTypeUpdateDTOValidator : AbstractValidator<AddressTypeUpdateDTO>
    {
        public AddressTypeUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Address ID is required.");

            RuleFor(x => x.AdderessType)
                .IsInEnum().WithMessage("Adderess Type is required");
        }
    }
}