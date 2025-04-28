using ECommerce.Application.DTOs.Role;
using FluentValidation;

namespace ECommerce.Application.Validators.Role
{
    public class RoleUpsertDTOValidator : AbstractValidator<RoleUpsertDTO>
    {
        public RoleUpsertDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(100).WithMessage("Role name cannot exceed 100 characters.");
        }
    }
}