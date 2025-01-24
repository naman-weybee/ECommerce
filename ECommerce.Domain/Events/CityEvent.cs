using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class CityEvent : BaseEvent
    {
        public Guid Id { get; }
        public string Name { get; }
        public Guid StateId { get; }

        public CityEvent(Guid id, string name, Guid stateId, eEventType eventType)
        {
            Id = id;
            Name = name;
            StateId = stateId;
            EventType = eventType;
        }
    }
}