using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class AddressAggregate : AggregateRoot<Address>
    {
        private readonly IDomainEventCollector _eventCollector;

        public Address Address { get; set; }

        public AddressAggregate(Address entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            Address = entity;
            _eventCollector = eventCollector;
        }

        public void CreateAddress(Address address)
        {
            Address.CreateAddress(address.Street, address.City, address.State, address.PostalCode, address.Country);

            EventType = eEventType.AddressCreated;
            RaiseDomainEvent();
        }

        public void UpdateAddress(Address address)
        {
            Address.UpdateAddress(address.Id, address.Street, address.City, address.State, address.PostalCode, address.Country);

            EventType = eEventType.AddressUpdated;
            RaiseDomainEvent();
        }

        public void DeleteAddress()
        {
            EventType = eEventType.AddressDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new AddressEvent(Address.Id, Address.Street, Address.City, Address.State, Address.PostalCode, Address.Country, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}