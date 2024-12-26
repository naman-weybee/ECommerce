using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class OrderUpdateDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Order status is required.")]
        [EnumDataType(typeof(eOrderStatus), ErrorMessage = "Invalid order status.")]
        public eOrderStatus OrderStatus { get; set; }

        [Required(ErrorMessage = "Total amount is required.")]
        public Money TotalAmount { get; set; }

        [Required(ErrorMessage = "Payment method is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Payment method should be between 3 and 50 characters.")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Shipping address is required.")]
        public Address ShippingAddress { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}