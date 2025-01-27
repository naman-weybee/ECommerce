using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
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