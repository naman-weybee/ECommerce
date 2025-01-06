using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class RoleDTOValidator : AbstractValidator<RoleDTO>
    {
        public RoleDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Role ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(100).WithMessage("Role name cannot exceed 100 characters.");
        }
    }
}