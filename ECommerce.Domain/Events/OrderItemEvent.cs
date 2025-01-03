using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class OrderItemEvent : BaseEvent
    {
        public Guid Id { get; }

        public Guid OrderId { get; }

        public Guid ProductId { get; }

        public OrderItemEvent(Guid id, Guid orderId, Guid productId, eEventType eventType)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));

            if (orderId == Guid.Empty)
                throw new ArgumentException("OrderId cannot be empty.", nameof(orderId));

            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty.", nameof(productId));

            Id = id;
            OrderId = orderId;
            ProductId = productId;
            EventType = eventType;
        }
    }
}