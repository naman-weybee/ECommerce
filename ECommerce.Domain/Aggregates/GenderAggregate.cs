using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class GenderAggregate : AggregateRoot<Gender>
    {
        private readonly IMediator _mediator;

        public Gender Gender { get; set; }

        public GenderAggregate(Gender entity, IMediator mediator)
            : base(entity, mediator)
        {
            Gender = entity;
            _mediator = mediator;
        }

        public async Task CreateGender(Gender gender)
        {
            Gender.CreateGender(gender.Name);

            EventType = eEventType.GenderCreated;
            await RaiseDomainEvent();
        }

        public async Task UpdateGender(Gender gender)
        {
            Gender.UpdateGender(gender.Id, gender.Name);

            EventType = eEventType.GenderUpdated;
            await RaiseDomainEvent();
        }

        public async Task DeleteGender()
        {
            EventType = eEventType.GenderDeleted;
            await RaiseDomainEvent();
        }

        private async Task RaiseDomainEvent()
        {
            var domainEvent = new GenderEvent(Gender.Id, Gender.Name, EventType);
            await RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}