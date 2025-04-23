namespace ECommerce.Application.DTOs.CartItem
{
    public class CartItemQuantityUpdateDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int Quantity { get; set; }
    }
}