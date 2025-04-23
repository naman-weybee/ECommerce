using ECommerce.Application.DTOs.User;
using FluentValidation;

namespace ECommerce.Application.Validators.User
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