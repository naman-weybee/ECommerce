using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class GenderEvent : BaseEvent
    {
        public Guid Id { get; }

        public GenderEvent(Guid id, eEventType eventType)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));

            Id = id;
            EventType = eventType;
        }
    }
}