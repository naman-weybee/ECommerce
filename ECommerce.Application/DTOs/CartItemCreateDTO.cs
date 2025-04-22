namespace ECommerce.Application.DTOs
{
    public class CartItemCreateDTO
    {
        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}