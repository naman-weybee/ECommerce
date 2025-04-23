using ECommerce.Application.DTOs.User;
using FluentValidation;

namespace ECommerce.Application.Validators.User
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