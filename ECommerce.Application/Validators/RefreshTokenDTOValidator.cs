using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class RefreshTokenDTOValidator : AbstractValidator<RefreshTokenDTO>
    {
        public RefreshTokenDTOValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token is required.")
                .MaximumLength(256).WithMessage("Token cannot exceed 256 characters.");

            RuleFor(x => x.RevokedDate)
                .NotNull().WithMessage("Revoked date is required.");

            RuleFor(x => x.ExpiredDate)
                .NotNull().WithMessage("Expired date is required.");
        }
    }
}