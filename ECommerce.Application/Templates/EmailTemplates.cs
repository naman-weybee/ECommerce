using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Shared.Constants;

namespace ECommerce.Application.Templates
{
    public class EmailTemplates : IEmailTemplates
    {
        private readonly IEmailService _emailService;

        public EmailTemplates(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendVerificationEmailAsync(User user)
        {
            var verificationLink = $"https://{Constants.MyIpv4}/api/v1/Auth/verify-email?token={Uri.EscapeDataString(user.EmailVerificationToken!)}";

            var dto = new EmailSendDTO()
            {
                ReceiverEmail = user.Email,
                Subject = "Email Verification Required",
                Body = $@"
                        <p>Dear <b>{user.FirstName} {user.LastName}</b>,</p>
                        <p>Thank you for registering with us. To complete your registration, please verify your email address by clicking the link below:</p>
                        <p><a href='{verificationLink}' target='_blank'>Verify Email Address</a></p>
                        <p>If you did not request this verification, please ignore this email.</p>
                        <br/>
                        <p>Best regards,</p>
                        <p>ECommerce Pvt Ltd.</p>",
                IsHtml = true,
                Link = verificationLink
            };

            await _emailService.SendEmailAsync(dto);
        }
    }
}