using ECommerce.Application.DTOs.User;
using FluentValidation;

namespace ECommerce.Application.Validators.User
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