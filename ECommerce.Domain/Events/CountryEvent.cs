using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class CountryEvent : BaseEvent
    {
        public Guid CountryId { get; }

        public string Name { get; }

        public CountryEvent(Guid countryId, string name, eEventType eventType)
        {
            if (countryId == Guid.Empty)
                throw new ArgumentException("CountryId cannot be empty.", nameof(countryId));

            CountryId = countryId;
            Name = name;
            EventType = eventType;
        }
    }
}