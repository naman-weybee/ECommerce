using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class RefreshTokenCreateDTOValidator : AbstractValidator<RefreshTokenCreateDTO>
    {
        public RefreshTokenCreateDTOValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}