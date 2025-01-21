using ECommerce.Application.DTOs;
using FluentValidation;

namespace ECommerce.API.Validators
{
    public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>
    {
        public UserCreateDTOValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(100).WithMessage("First Name cannot exceed 100 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(100).WithMessage("Last Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please provide a valid email address.")
                .MaximumLength(256).WithMessage("Email cannot exceed 256 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Please provide a valid phone number.");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("RoleId is required.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.GenderId)
                .NotEmpty().WithMessage("GenderId is required.");
        }
    }
}