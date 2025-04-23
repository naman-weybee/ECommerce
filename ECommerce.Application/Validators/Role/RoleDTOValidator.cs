using ECommerce.Application.DTOs.Role;
using FluentValidation;

namespace ECommerce.Application.Validators.Role
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

            RuleFor(x => x.RoleEntity)
                .IsInEnum().NotEmpty().WithMessage("Role entity is required.");

            RuleFor(x => x.HasViewPermission)
                .NotEmpty().WithMessage("View permission is required.");

            RuleFor(x => x.HasCreateOrUpdatePermission)
                .NotEmpty().WithMessage("Create or update permission is required.");

            RuleFor(x => x.HasDeletePermission)
                .NotEmpty().WithMessage("Delete permission is required.");

            RuleFor(x => x.HasFullPermission)
                .NotEmpty().WithMessage("Full permission is required.");
        }
    }
}