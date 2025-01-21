using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class AddressTypeUpdateDTOValidator : AbstractValidator<AddressTypeUpdateDTO>
    {
        public AddressTypeUpdateDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Address ID is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");

            RuleFor(x => x.AdderessType)
                .IsInEnum().WithMessage("Adderess Type is required");
        }
    }
}