using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class OTPCreateDTOValidator : AbstractValidator<OTPCreateDTO>
    {
        public OTPCreateDTOValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("OTP Type is required");
        }
    }
}