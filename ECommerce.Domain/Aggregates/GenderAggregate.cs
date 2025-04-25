using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class GenderAggregate : AggregateRoot<Gender>
    {
        private readonly IDomainEventCollector _eventCollector;

        public Gender Gender { get; set; }

        public GenderAggregate(Gender entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            Gender = entity;
            _eventCollector = eventCollector;
        }

        public void CreateGender()
        {
            Entity.CreateGender(Entity.Name);

            EventType = eEventType.GenderCreated;
            RaiseDomainEvent();
        }

        public void UpdateGender()
        {
            Entity.UpdateGender(Entity.Id, Entity.Name);

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
            var domainEvent = new GenderEvent(Entity.Id, Entity.Name, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}