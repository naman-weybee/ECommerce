namespace ECommerce.Application.DTOs.CartItem
{
    public class CartItemUpsertDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}