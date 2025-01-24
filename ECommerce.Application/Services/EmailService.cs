using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Shared.Constants;
using System.Net;
using System.Net.Mail;

namespace ECommerce.Application.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(EmailSendDTO dto)
        {
            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(Constants.Email, Constants.Password),
                EnableSsl = true
            };

            using var message = new MailMessage(Constants.Email, dto.ReceiverEmail, dto.Subject, dto.Body);

            if (dto.IsHtml)
                message.IsBodyHtml = true;

            await smtpClient.SendMailAsync(message);
        }
    }
}