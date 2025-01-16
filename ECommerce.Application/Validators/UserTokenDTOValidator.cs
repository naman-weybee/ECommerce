using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class UserTokenDTOValidator : AbstractValidator<UserTokenDTO>
    {
        public UserTokenDTOValidator()
        {
            RuleFor(x => x.AccessToken)
            .NotEmpty().WithMessage("Access Token is required.");

            RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh Token is required.");
        }
    }
}