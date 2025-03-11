using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.Application.Validators
{
    public class RoleEntityDTOValidator : AbstractValidator<RoleEntityDTO>
    {
        public RoleEntityDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("RoleEntity Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("RoleEntity name is required.")
                .MaximumLength(100).WithMessage("RoleEntity name cannot exceed 100 characters.");
        }
    }
}