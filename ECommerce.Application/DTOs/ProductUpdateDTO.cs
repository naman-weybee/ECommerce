using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class ProductUpdateDTO
    {
        [Required(ErrorMessage = "Product ID is required.")]
        public Guid Id { get; set; }

        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Range(1, 100000, ErrorMessage = "Price must be between 1,000 and 100,000.")]
        public decimal? Price { get; set; }

        [StringLength(3, ErrorMessage = "Currency must be exactly 3 characters.")]
        public string? Currency { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int? Stock { get; set; }

        public string? SKU { get; set; }

        public string? Brand { get; set; }

        public int? CategoryId { get; set; }
    }
}