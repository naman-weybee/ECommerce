using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class RevokeRefreshTokenDTOValidator : AbstractValidator<RevokeRefreshTokenDTO>
    {
        public RevokeRefreshTokenDTOValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh Token is required.");
        }
    }
}