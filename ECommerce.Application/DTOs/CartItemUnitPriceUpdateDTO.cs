using ECommerce.Domain.ValueObjects;

namespace ECommerce.Application.DTOs
{
    public class CartItemUnitPriceUpdateDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Money UnitPrice { get; set; }
    }
}