using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class CartItemEvent : BaseEvent
    {
        public Guid CartItemId { get; }

        public Guid UserId { get; }

        public Guid ProductId { get; }

        public CartItemEvent(Guid cartItemId, Guid userId, Guid productId, eEventType eventType)
        {
            if (cartItemId == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.", nameof(cartItemId));

            CartItemId = cartItemId;
            UserId = userId;
            ProductId = productId;
            EventType = eventType;
        }
    }
}