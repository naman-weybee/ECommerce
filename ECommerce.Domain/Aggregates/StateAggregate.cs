using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class StateAggregate : AggregateRoot<State>
    {
        private readonly IDomainEventCollector _eventCollector;

        public State State { get; set; }

        public StateAggregate(State entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            State = entity;
            Entity.Cities ??= [];
            _eventCollector = eventCollector;
        }

        public void CreateState()
        {
            Entity.CreateState(Entity.Name, Entity.CountryId, Entity.Cities);

            EventType = eEventType.StateCreated;
            RaiseDomainEvent();
        }

        public void UpdateState()
        {
            Entity.UpdateState(Entity.Id, Entity.Name, Entity.CountryId, Entity.Cities);

            EventType = eEventType.StateUpdated;
            RaiseDomainEvent();
        }

        public void DeleteState()
        {
            EventType = eEventType.StateDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new StateEvent(Entity.Id, Entity.Name, Entity.CountryId, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}