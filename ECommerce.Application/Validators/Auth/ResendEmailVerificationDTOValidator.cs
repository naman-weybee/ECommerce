using ECommerce.Application.DTOs.Auth;
using FluentValidation;

namespace ECommerce.Application.Validators.Auth
{
    public class ResendEmailVerificationDTOValidator : AbstractValidator<ResendEmailVerificationDTO>
    {
        public ResendEmailVerificationDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");
        }
    }
}