using ECommerce.Application.DTOs.RefreshToken;
using FluentValidation;

namespace ECommerce.Application.Validators.RefreshToken
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