using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class RefreshTokenCreateDTOValidator : AbstractValidator<RefreshTokenCreateDTO>
    {
        public RefreshTokenCreateDTOValidator()
        {
        }
    }
}