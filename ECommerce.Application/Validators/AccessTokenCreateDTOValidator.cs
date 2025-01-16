using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
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