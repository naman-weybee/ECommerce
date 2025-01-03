using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class AddressEvent : BaseEvent
    {
        public Guid AddressId { get; }

        public AddressEvent(Guid addressId, eEventType eventType)
        {
            if (addressId == Guid.Empty)
                throw new ArgumentException("AddressId cannot be empty.", nameof(addressId));

            AddressId = addressId;
            EventType = eventType;
        }
    }
}