using ECommerce.Domain.ValueObjects;

namespace ECommerce.Application.DTOs
{
    public class OrderItemUpdateDTO
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public Money UnitPrice { get; set; }
    }
}