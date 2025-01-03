using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;

namespace ECommerce.Domain.Aggregates
{
    public class AddressAggregate : AggregateRoot<Address>
    {
        public Address Address { get; set; }

        public AddressAggregate(Address entity)
            : base(entity)
        {
            Address = entity;
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
            var domainEvent = new AddressEvent(Address.Id, EventType);
            RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}