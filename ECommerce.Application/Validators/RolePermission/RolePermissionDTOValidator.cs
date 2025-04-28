using ECommerce.Application.DTOs.RolePermission;
using FluentValidation;

namespace ECommerce.Application.Validators.RolePermission
{
    public class RolePermissionDTOValidator : AbstractValidator<RolePermissionDTO>
    {
        public RolePermissionDTOValidator()
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