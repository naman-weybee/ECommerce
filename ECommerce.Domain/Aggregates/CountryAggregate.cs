using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class CountryAggregate : AggregateRoot<Country>
    {
        private readonly IDomainEventCollector _eventCollector;

        public Country Country { get; set; }

        public CountryAggregate(Country entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            Country = entity;
            Entity.States ??= [];
            _eventCollector = eventCollector;
        }

        public void CreateCountry()
        {
            Entity.CreateCountry(Entity.Name, Entity.States);

            EventType = eEventType.CountryCreated;
            RaiseDomainEvent();
        }

        public void UpdateCountry()
        {
            Entity.UpdateCountry(Entity.Id, Entity.Name, Entity.States);

            EventType = eEventType.CountryUpdated;
            RaiseDomainEvent();
        }

        public void DeleteCountry()
        {
            EventType = eEventType.CountryDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new CountryEvent(Entity.Id, Entity.Name, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}