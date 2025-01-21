using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class StateEvent : BaseEvent
    {
        public Guid Id { get; }

        public string Name { get; }

        public Guid CountryId { get; }

        public StateEvent(Guid id, string name, Guid countryId, eEventType eventType)
        {
            Id = id;
            Name = name;
            CountryId = countryId;
            EventType = eventType;
        }
    }
}
