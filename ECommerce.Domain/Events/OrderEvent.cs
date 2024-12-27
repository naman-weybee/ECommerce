using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class OrderEvent : BaseEvent
    {
        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderEvent(Guid orderId, Guid userId, decimal totalAmount, eEventType eEventType)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException("OrderId cannot be empty.", nameof(orderId));

            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));

            if (totalAmount <= 0)
                throw new ArgumentException("TotalAmount must be greater than zero.", nameof(totalAmount));

            OrderId = orderId;
            UserId = userId;
            TotalAmount = totalAmount;
            EventType = eEventType;
        }
    }
}