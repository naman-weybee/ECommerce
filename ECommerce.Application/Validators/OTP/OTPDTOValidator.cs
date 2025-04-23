using ECommerce.Application.DTOs.OTP;
using FluentValidation;

namespace ECommerce.Application.Validators.OTP
{
    public class OTPDTOValidator : AbstractValidator<OTPDTO>
    {
        public OTPDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("OTP ID is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("OTP Code is required.")
                .Length(6).WithMessage("Code must be a 6-letter code.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid OTP Type.");

            RuleFor(x => x.IsUsed)
                .NotNull().WithMessage("IsUsed is required.");
        }
    }
}