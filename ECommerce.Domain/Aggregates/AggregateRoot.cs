using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class AggregateRoot<TEntity>
        where TEntity : class
    {
        public eEventType EventType { get; set; } = eEventType.Unknown;

        public readonly List<INotification> _domainEvents = new();

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public TEntity Entity { get; set; }

        public AggregateRoot(TEntity entity)
        {
            Entity = entity;
        }

        public void RaiseDomainEvent(INotification domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}