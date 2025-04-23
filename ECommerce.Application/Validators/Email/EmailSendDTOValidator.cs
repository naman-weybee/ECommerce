using ECommerce.Application.DTOs.Email;
using FluentValidation;

namespace ECommerce.Application.Validators.Email
{
    public class EmailSendDTOValidator : AbstractValidator<EmailSendDTO>
    {
        public EmailSendDTOValidator()
        {
            RuleFor(x => x.ReceiverEmail)
                .NotEmpty().WithMessage("Receiver Email is required.");

            RuleFor(x => x.Subject)
                .NotEmpty().WithMessage("Email Subject is required.");

            RuleFor(x => x.Body)
                .NotEmpty().WithMessage("Email Body is required.");
        }
    }
}