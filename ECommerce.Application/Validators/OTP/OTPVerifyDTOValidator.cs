using ECommerce.Application.DTOs.OTP;
using FluentValidation;

namespace ECommerce.Application.Validators.OTP
{
    public class OTPVerifyDTOValidator : AbstractValidator<OTPVerifyDTO>
    {
        public OTPVerifyDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress().WithMessage("Invalid Email Address.");

            RuleFor(x => x.Code)
                .NotEmpty().Length(6).WithMessage("Invalid Code.");
        }
    }
}