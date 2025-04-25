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

        public void CreateAddress()
        {
            Entity.CreateAddress(Entity.UserId, Entity.FirstName, Entity.LastName, Entity.CountryId, Entity.StateId, Entity.CityId, Entity.PostalCode, Entity.AdderessType, Entity.AddressLine, Entity.PhoneNumber);

            EventType = eEventType.AddressCreated;
            RaiseDomainEvent();
        }

        public void UpdateAddress()
        {
            Entity.UpdateAddress(Entity.Id, Entity.UserId, Entity.FirstName, Entity.LastName, Entity.CountryId, Entity.StateId, Entity.CityId, Entity.PostalCode, Entity.AdderessType, Entity.AddressLine, Entity.PhoneNumber);

            EventType = eEventType.AddressUpdated;
            RaiseDomainEvent();
        }

        public void UpdateAddressType(eAddressType newAddressType)
        {
            switch (newAddressType)
            {
                case eAddressType.Default:
                    SetAsDefaultAddress();
                    break;

                case eAddressType.Billing:
                    SetAsBillingAddress();
                    break;

                case eAddressType.Shipping:
                    SetAsShippingAddress();
                    break;
            }

            RaiseDomainEvent();
        }

        private void SetAsDefaultAddress()
        {
            EventType = eEventType.SetAsDefaultAddress;
            Entity.UpdateAddressType(eAddressType.Default);
        }

        private void SetAsBillingAddress()
        {
            EventType = eEventType.SetAsBillingAddress;
            Entity.UpdateAddressType(eAddressType.Billing);
        }

        private void SetAsShippingAddress()
        {
            EventType = eEventType.SetAsShippingAddress;
            Entity.UpdateAddressType(eAddressType.Shipping);
        }

        public void DeleteAddress()
        {
            EventType = eEventType.AddressDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new AddressEvent(Entity.Id, Entity.UserId, Entity.FirstName, Entity.LastName, Entity.CountryId, Entity.StateId, Entity.CityId, Entity.PostalCode, Entity.AdderessType, Entity.AddressLine, Entity.PhoneNumber, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}