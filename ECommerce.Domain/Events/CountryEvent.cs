using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class CountryEvent : BaseEvent
    {
        public Guid Id { get; }

        public string Name { get; }

        public CountryEvent(Guid id, string name, eEventType eventType)
        {
            Id = id;
            Name = name;
            EventType = eventType;
        }
    }
}