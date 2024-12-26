using MediatR;

namespace ECommerce.Domain.Events
{
    public class OrderEvent : INotification
    {
        public Guid OrderId { get; private set; }

        public Guid UserId { get; private set; }

        public decimal TotalAmount { get; private set; }

        public OrderEvent(Guid orderId, Guid userId, decimal totalAmount)
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
        }
    }
}