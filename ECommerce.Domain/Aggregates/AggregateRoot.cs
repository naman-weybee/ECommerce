using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Services;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class AggregateRoot<TEntity>
        where TEntity : class
    {
        private readonly IDomainEventCollector _eventCollector;

        public eEventType EventType { get; set; } = eEventType.Unknown;

        public TEntity Entity { get; set; }

        public AggregateRoot(TEntity entity, IDomainEventCollector eventCollector)
        {
            Entity = entity;
            _eventCollector = eventCollector;
        }

        public void RaiseDomainEvent(INotification domainEvent)
        {
            _eventCollector.AddDomainEvent(domainEvent);
        }
    }
}