using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class GenderEvent : BaseEvent
    {
        public Guid Id { get; }

        public string Name { get; }

        public GenderEvent(Guid id, string name, eEventType eventType)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));

            Id = id;
            Name = name;
            EventType = eventType;
        }
    }
}