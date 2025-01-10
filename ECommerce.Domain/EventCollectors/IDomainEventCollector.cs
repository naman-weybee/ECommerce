using MediatR;

namespace ECommerce.Infrastructure.Services
{
    public interface IDomainEventCollector
    {
        void AddDomainEvent(INotification domainEvent);

        Task PublishAsync();
    }
}