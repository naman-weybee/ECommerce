using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.OrderItem
{
    public class OrderUpdateDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid BillingAddressId { get; set; }

        public Guid ShippingAddressId { get; set; }

        public string PaymentMethod { get; set; }
    }
}