using ECommerce.Application.DTOs.OTP;
using FluentValidation;

namespace ECommerce.Application.Validators.OTP
{
    public class OTPCreateDTOValidator : AbstractValidator<OTPCreateDTO>
    {
        public OTPCreateDTOValidator()
        {
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("OTP Type is required");
        }
    }
}