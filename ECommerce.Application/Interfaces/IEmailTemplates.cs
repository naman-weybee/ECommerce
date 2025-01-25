using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IEmailTemplates
    {
        Task SendVerificationEmailAsync(User user);
    }
}