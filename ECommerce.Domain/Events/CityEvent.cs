using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class CityEvent : BaseEvent
    {
        public Guid CityId { get; }

        public string Name { get; }

        public Guid StateId { get; }

        public CityEvent(Guid cityId, string name, Guid stateId, eEventType eventType)
        {
            if (cityId == Guid.Empty)
                throw new ArgumentException("CityId cannot be empty.", nameof(cityId));

            CityId = cityId;
            Name = name;
            StateId = stateId;
            EventType = eventType;
        }
    }
}