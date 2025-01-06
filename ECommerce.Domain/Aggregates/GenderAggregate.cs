using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;

namespace ECommerce.Domain.Aggregates
{
    public class GenderAggregate : AggregateRoot<Gender>
    {
        public Gender Gender { get; set; }

        public GenderAggregate(Gender entity)
            : base(entity)
        {
            Gender = entity;
        }

        public void CreateGender(Gender gender)
        {
            Gender.CreateGender(gender.Name);

            EventType = eEventType.GenderCreated;
            RaiseDomainEvent();
        }

        public void UpdateGender(Gender gender)
        {
            Gender.UpdateGender(gender.Id, gender.Name);

            EventType = eEventType.GenderUpdated;
            RaiseDomainEvent();
        }

        public void DeleteGender()
        {
            EventType = eEventType.GenderDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new GenderEvent(Gender.Id, EventType);
            RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}