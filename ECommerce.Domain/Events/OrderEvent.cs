using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class OrderEvent : BaseEvent
    {
        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public Guid BillingAddressId { get; set; }

        public Guid ShippingAddressId { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderEvent(Guid orderId, Guid userId, Guid billingAddressId, Guid shippingAddressId, decimal totalAmount, eEventType eventType)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException("OrderId cannot be empty.", nameof(orderId));

            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));

            if (billingAddressId == Guid.Empty)
                throw new ArgumentException("Billing Address Id cannot be empty.", nameof(billingAddressId));

            if (shippingAddressId == Guid.Empty)
                throw new ArgumentException("Shipping Address Id cannot be empty.", nameof(shippingAddressId));

            if (totalAmount <= 0)
                throw new ArgumentException("TotalAmount must be greater than zero.", nameof(totalAmount));

            OrderId = orderId;
            UserId = userId;
            BillingAddressId = billingAddressId;
            ShippingAddressId = shippingAddressId;
            TotalAmount = totalAmount;
            EventType = eventType;
        }
    }
}