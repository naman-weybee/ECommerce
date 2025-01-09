using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class AddressAggregate : AggregateRoot<Address>
    {
        private readonly IMediator _mediator;

        public Address Address { get; set; }

        public AddressAggregate(Address entity, IMediator mediator)
            : base(entity, mediator)
        {
            Address = entity;
            _mediator = mediator;
        }

        public async Task CreateAddress(Address address)
        {
            Address.CreateAddress(address.Street, address.City, address.State, address.PostalCode, address.Country);

            EventType = eEventType.AddressCreated;
            await RaiseDomainEvent();
        }

        public async Task UpdateAddress(Address address)
        {
            Address.UpdateAddress(address.Id, address.Street, address.City, address.State, address.PostalCode, address.Country);

            EventType = eEventType.AddressUpdated;
            await RaiseDomainEvent();
        }

        public async Task DeleteAddress()
        {
            EventType = eEventType.AddressDeleted;
            await RaiseDomainEvent();
        }

        private async Task RaiseDomainEvent()
        {
            var domainEvent = new AddressEvent(Address.Id, Address.Street, Address.City, Address.State, Address.PostalCode, Address.Country, EventType);
            await RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}