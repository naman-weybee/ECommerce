namespace ECommerce.Application.DTOs
{
    public class OrderCreateFromCartDTO
    {
        public Guid UserId { get; set; }

        public string PaymentMethod { get; set; }
    }
}