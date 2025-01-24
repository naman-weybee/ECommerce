using ECommerce.Domain.ValueObjects;

namespace ECommerce.Application.DTOs
{
    public class OrderCreateDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid BillingAddressId { get; set; }

        public Guid ShippingAddressId { get; set; }

        public string PaymentMethod { get; set; }

        public Money TotalAmount { get; set; }
    }
}