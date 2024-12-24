using ECommerce.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class OrderItemCreateDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Order ID is required.")]
        public Guid OrderId { get; set; }

        [Required(ErrorMessage = "Product ID is required.")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit price is required.")]
        public Money UnitPrice { get; set; }
    }
}