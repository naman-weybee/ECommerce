using ECommerce.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class OrderItemDTO
    {
        public Guid Id { get; private set; }

        [Required(ErrorMessage = "Order ID is required.")]
        public Guid OrderId { get; private set; }

        [Required(ErrorMessage = "Product ID is required.")]
        public Guid ProductId { get; private set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; private set; }

        [Required(ErrorMessage = "Unit price is required.")]
        public Money UnitPrice { get; private set; }
    }
}