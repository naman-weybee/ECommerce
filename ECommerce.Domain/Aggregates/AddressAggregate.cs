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
            Address.CreateAddress(address.UserId, address.FirstName, address.LastName, address.CountryId, address.StateId, address.CityId, address.PostalCode, address.AdderessType, address.AddressLine, address.PhoneNumber);

            EventType = eEventType.AddressCreated;
            RaiseDomainEvent();
        }

        public void UpdateAddress(Address address)
        {
            Address.UpdateAddress(address.Id, address.UserId, address.FirstName, address.LastName, address.CountryId, address.StateId, address.CityId, address.PostalCode, address.AdderessType, address.AddressLine, address.PhoneNumber);

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
            Address.UpdateAddressType(eAddressType.Default);
        }

        private void SetAsBillingAddress()
        {
            EventType = eEventType.SetAsBillingAddress;
            Address.UpdateAddressType(eAddressType.Billing);
        }

        private void SetAsShippingAddress()
        {
            EventType = eEventType.SetAsShippingAddress;
            Address.UpdateAddressType(eAddressType.Shipping);
        }

        public void DeleteAddress()
        {
            EventType = eEventType.AddressDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new AddressEvent(Address.Id, Address.UserId, Address.FirstName, Address.LastName, Address.CountryId, Address.StateId, Address.CityId, Address.PostalCode, Address.AdderessType, Address.AddressLine, Address.PhoneNumber, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}