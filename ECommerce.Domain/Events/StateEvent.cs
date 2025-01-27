using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class StateEvent : BaseEvent
    {
        public Guid StateId { get; }

        public string Name { get; }

        public Guid CountryId { get; }

        public StateEvent(Guid stateId, string name, Guid countryId, eEventType eventType)
        {
            if (stateId == Guid.Empty)
                throw new ArgumentException("StateId cannot be empty.", nameof(stateId));

            StateId = stateId;
            Name = name;
            CountryId = countryId;
            EventType = eventType;
        }
    }
}