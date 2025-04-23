using ECommerce.Application.DTOs.OTP;
using FluentValidation;

namespace ECommerce.Application.Validators.OTP
{
    public class OTPTokenDTOValidator : AbstractValidator<OTPTokenDTO>
    {
        public OTPTokenDTOValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token is required.");
        }
    }
}