using ECommerce.Application.DTOs;

namespace ECommerce.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailSendDTO dto);
    }
}