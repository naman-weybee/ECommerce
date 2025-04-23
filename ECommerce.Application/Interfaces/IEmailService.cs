using ECommerce.Application.DTOs.Email;

namespace ECommerce.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailSendDTO dto);
    }
}