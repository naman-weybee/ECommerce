using ECommerce.Domain.ValueObjects;

namespace ECommerce.Application.DTOs.CartItem
{
    public class CartItemDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public Money UnitPrice { get; set; }
    }
}