using ECommerce.Application.DTOs.Email;
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

            var senderAddress = new MailAddress(Constants.Email, "ECommerce Pvt Ltd");
            var receiverAddress = new MailAddress(dto.ReceiverEmail);

            using var message = new MailMessage(senderAddress, receiverAddress)
            {
                Subject = dto.Subject,
                Body = dto.Body,
                IsBodyHtml = dto.IsHtml
            };

            await smtpClient.SendMailAsync(message);
        }
    }
}