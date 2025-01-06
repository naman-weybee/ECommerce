using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Application.DTOs
{
    public class OrderCreateDTO
    {
        public Guid UserId { get; set; }

        public Money TotalAmount { get; set; }

        public string PaymentMethod { get; set; }

        public Guid ShippingAddressId { get; set; }
    }
}