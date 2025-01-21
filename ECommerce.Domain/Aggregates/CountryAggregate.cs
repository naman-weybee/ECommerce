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
            Country.States ??= new List<State>();
            _eventCollector = eventCollector;
        }

        public void CreateCountry(Country country)
        {
            Country.CreateCountry(country.Name, country.States);

            EventType = eEventType.CountryCreated;
            RaiseDomainEvent();
        }

        public void UpdateCountry(Country country)
        {
            Country.UpdateCountry(country.Id, country.Name, country.States);

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
            var domainEvent = new CountryEvent(Country.Id, Country.Name, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}