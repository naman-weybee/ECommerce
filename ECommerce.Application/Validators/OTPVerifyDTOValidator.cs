using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
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