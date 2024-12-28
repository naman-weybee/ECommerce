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
            Address.UpdateAddress(address.Street, address.City, address.State, address.PostalCode, address.Country);

            EventType = eEventType.AddressUpdated;
            RaiseDomainEvent();
        }

        public void DeleteAddress()
        {
            if (Address.IsDeleted)
                throw new InvalidOperationException("Cannot delete already deleted Address.");

            EventType = eEventType.AddressDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new AddressEvent(Address.Id, Address.Street, Address.City, Address.State, Address.PostalCode, Address.Country, EventType);
            RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}