using ECommerce.Application.DTOs.User;
using FluentValidation;

namespace ECommerce.Application.Validators.User
{
    public class PasswordResetDTOValidator : AbstractValidator<PasswordResetDTO>
    {
        public PasswordResetDTOValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Invalid Token.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required.");
        }
    }
}