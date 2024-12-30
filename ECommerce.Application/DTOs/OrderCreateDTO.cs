using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Application.DTOs
{
    public class OrderCreateDTO
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public eOrderStatus OrderStatus { get; set; } = eOrderStatus.Pending;

        public Money TotalAmount { get; set; }

        public string PaymentMethod { get; set; }

        public Guid ShippingAddressId { get; set; }

        public ICollection<OrderItem> OrderItems { get; private set; }
    }
}