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
            State.Cities ??= new List<City>();
            _eventCollector = eventCollector;
        }

        public void CreateState(State state)
        {
            State.CreateState(state.Name, state.CountryId, state.Cities);

            EventType = eEventType.StateCreated;
            RaiseDomainEvent();
        }

        public void UpdateState(State state)
        {
            State.UpdateState(state.Id, state.Name, state.CountryId, state.Cities);

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
            var domainEvent = new StateEvent(State.Id, State.Name, State.CountryId, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}