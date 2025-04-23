using ECommerce.Application.DTOs.RefreshToken;
using FluentValidation;

namespace ECommerce.Application.Validators.RefreshToken
{
    public class RefreshTokenCreateDTOValidator : AbstractValidator<RefreshTokenCreateDTO>
    {
        public RefreshTokenCreateDTOValidator()
        {
        }
    }
}