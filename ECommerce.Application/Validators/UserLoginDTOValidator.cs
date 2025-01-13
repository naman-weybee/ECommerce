using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class UserLoginDTOValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginDTOValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
        }
    }
}