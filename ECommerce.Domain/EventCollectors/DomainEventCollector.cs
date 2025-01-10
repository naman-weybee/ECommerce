using MediatR;

namespace ECommerce.Infrastructure.Services
{
    public class DomainEventCollector : IDomainEventCollector
    {
        private readonly IMediator _mediator;

        private readonly List<INotification> _domainEvents = new List<INotification>();

        public DomainEventCollector(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public async Task PublishAsync()
        {
            foreach (var domainEvent in _domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }

            _domainEvents.Clear();
        }
    }
}