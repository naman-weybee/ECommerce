using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class OrderItemEvent : BaseEvent
    {
        public Guid OrderItemId { get; }

        public Guid OrderId { get; }

        public Guid ProductId { get; }

        public OrderItemEvent(Guid orderItemId, Guid orderId, Guid productId, eEventType eventType)
        {
            if (orderItemId == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(orderItemId));

            if (orderId == Guid.Empty)
                throw new ArgumentException("OrderId cannot be empty.", nameof(orderId));

            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty.", nameof(productId));

            OrderItemId = orderItemId;
            OrderId = orderId;
            ProductId = productId;
            EventType = eventType;
        }
    }
}