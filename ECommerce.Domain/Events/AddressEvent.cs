using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class AddressEvent : BaseEvent
    {
        public Guid AddressId { get; }

        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid CountryId { get; set; }

        public Guid StateId { get; set; }

        public Guid CityId { get; set; }

        public string PostalCode { get; set; }

        public eAddressType AdderessType { get; set; }

        public string AddressLine { get; set; }

        public string PhoneNumber { get; set; }

        public AddressEvent(Guid addressId, Guid userId, string firstName, string lastName, Guid countryId, Guid stateId, Guid cityId, string postalCode, eAddressType adderessType, string addressLine, string phoneNumber, eEventType eventType)
        {
            if (addressId == Guid.Empty)
                throw new ArgumentException("Address Id cannot be empty.", nameof(addressId));

            AddressId = addressId;
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            CountryId = countryId;
            StateId = stateId;
            CityId = cityId;
            PostalCode = postalCode;
            AdderessType = adderessType;
            AddressLine = addressLine;
            PhoneNumber = phoneNumber;
            EventType = eventType;
        }
    }
}