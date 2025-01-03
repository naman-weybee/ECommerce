using ECommerce.Domain.ValueObjects;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class CartItemEvent : BaseEvent
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public Guid ProductId { get; }

        public CartItemEvent(Guid id, Guid userId, Guid productId, eEventType eventType)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(id));

            Id = id;
            UserId = userId;
            ProductId = productId;
            EventType = eventType;
        }
    }
}
