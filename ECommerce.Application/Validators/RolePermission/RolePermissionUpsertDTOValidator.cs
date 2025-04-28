using ECommerce.Application.DTOs.RolePermission;
using FluentValidation;

namespace ECommerce.Application.Validators.RolePermission
{
    internal class RolePermissionUpsertDTOValidator : AbstractValidator<RolePermissionUpsertDTO>
    {
        public RolePermissionUpsertDTOValidator()
        {
            RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("Role ID is required.");

            RuleFor(x => x.RoleEntityId)
            .NotEmpty()
            .WithMessage("RoleEntityId ID is required.");
        }
    }
}