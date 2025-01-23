namespace ECommerce.Application.DTOs
{
    public class OrderItemQuantityUpdateDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public int Quantity { get; set; }
    }
}