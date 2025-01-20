using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class UserClaimsDTOValidator : AbstractValidator<UserClaimsDTO>
    {
        public UserClaimsDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.");
        }
    }
}