using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class RoleCreateDTOValidator : AbstractValidator<RoleCreateDTO>
    {
        public RoleCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(100).WithMessage("Role name cannot exceed 100 characters.");
        }
    }
}