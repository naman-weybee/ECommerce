using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class AddressEvent : BaseEvent
    {
        public Guid AddressId { get; }

        public string Street { get; }

        public string City { get; }

        public string State { get; }

        public string PostalCode { get; }

        public string Country { get; }

        public AddressEvent(Guid addressId, string street, string city, string state, string postalCode, string country, eEventType eventType)
        {
            if (addressId == Guid.Empty)
                throw new ArgumentException("AddressId cannot be empty.", nameof(addressId));

            AddressId = addressId;
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
            EventType = eventType;
        }
    }
}