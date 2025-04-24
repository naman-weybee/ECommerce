using ECommerce.Application.DTOs.Role;
using FluentValidation;

namespace ECommerce.Application.Validators.Role
{
    public class RoleCreateDTOValidator : AbstractValidator<RoleUpsertDTO>
    {
        public RoleCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(100).WithMessage("Role name cannot exceed 100 characters.");

            RuleFor(x => x.RoleEntity)
                .IsInEnum().NotEmpty().WithMessage("Role entity is required.");

            RuleFor(x => x.HasViewPermission)
                .NotNull().WithMessage("View permission is required.");

            RuleFor(x => x.HasCreateOrUpdatePermission)
                .NotNull().WithMessage("Create or update permission is required.");

            RuleFor(x => x.HasDeletePermission)
                .NotNull().WithMessage("Delete permission is required.");

            RuleFor(x => x.HasFullPermission)
                .NotNull().WithMessage("Full permission is required.");
        }
    }
}