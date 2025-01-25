using ECommerce.Domain.Enums;

namespace ECommerce.Application.Interfaces
{
    public interface IEmailTemplates
    {
        Task SendVerificationEmailAsync(Guid userId);

        Task SendOrderEmailAsync(Guid orderId, Guid userId, eEventType eventType);
    }
}