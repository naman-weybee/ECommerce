namespace ECommerce.Application.DTOs
{
    public class OrderCreateFromCartDTO
    {
        public Guid UserId { get; set; }
        
        public Guid AddressId { get; set; }

        public string PaymentMethod { get; set; }
    }
}