using ECommerce.Application.DTOs.OTP;
using FluentValidation;

namespace ECommerce.Application.Validators.OTP
{
    public class OTPCreateFromEmailDTOValidator : AbstractValidator<OTPCreateFromEmailDTO>
    {
        public OTPCreateFromEmailDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("OTP Type is required");
        }
    }
}