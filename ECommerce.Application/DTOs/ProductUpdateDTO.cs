using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class ProductUpdateDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required, MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required, StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be a 3-letter ISO code.")]
        public string Currency { get; set; }

        [Required, Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }

        [MaxLength(50, ErrorMessage = "SKU cannot exceed 50 characters.")]
        public string? SKU { get; set; }

        [MaxLength(50, ErrorMessage = "Brand cannot exceed 50 characters.")]
        public string? Brand { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}