using ECommerce.Application.DTOs.Auth;
using FluentValidation;

namespace ECommerce.Application.Validators.Auth
{
    public class AccessTokenCreateDTOValidator : AbstractValidator<AccessTokenCreateDTO>
    {
        public AccessTokenCreateDTOValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh Token is required.");
        }
    }
}