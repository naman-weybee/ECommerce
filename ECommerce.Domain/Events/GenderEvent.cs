using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class GenderEvent : BaseEvent
    {
        public Guid GenderId { get; }

        public string Name { get; }

        public GenderEvent(Guid genderId, string name, eEventType eventType)
        {
            if (genderId == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(genderId));

            GenderId = genderId;
            Name = name;
            EventType = eventType;
        }
    }
}