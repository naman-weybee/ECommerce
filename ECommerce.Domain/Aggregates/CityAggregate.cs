using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class CityAggregate : AggregateRoot<City>
    {
        private readonly IDomainEventCollector _eventCollector;

        public City City { get; set; }

        public CityAggregate(City entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            City = entity;
            _eventCollector = eventCollector;
        }

        public void CreateCity()
        {
            Entity.CreateCity(Entity.Name, Entity.StateId);

            EventType = eEventType.CityCreated;
            RaiseDomainEvent();
        }

        public void UpdateCity()
        {
            Entity.UpdateCity(Entity.Id, Entity.Name, Entity.StateId);

            EventType = eEventType.CityUpdated;
            RaiseDomainEvent();
        }

        public void DeleteCity()
        {
            EventType = eEventType.CityDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new CityEvent(Entity.Id, Entity.Name, Entity.StateId, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}