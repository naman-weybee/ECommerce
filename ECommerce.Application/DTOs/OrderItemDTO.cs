using ECommerce.Domain.ValueObjects;

namespace ECommerce.Application.DTOs
{
    public class OrderItemDTO
    {
        public Guid Id { get; private set; }

        public Guid OrderId { get; private set; }

        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }

        public Money UnitPrice { get; private set; }
    }
}