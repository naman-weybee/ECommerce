namespace ECommerce.Application.DTOs
{
    public class OrderCreateFromCartDTO
    {
        public Guid UserId { get; set; }

        public Guid BillingAddressId { get; set; }

        public Guid ShippingAddressId { get; set; }

        public string PaymentMethod { get; set; }
    }
}