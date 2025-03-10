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

            RuleFor(x => x.EntityName)
                .NotEmpty().WithMessage("Entity name is required.")
                .MaximumLength(100).WithMessage("Entity name cannot exceed 100 characters.");

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