using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class ProductCreateDTO
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, 100000, ErrorMessage = "Price must be between 1,000 and 100,000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Currency is required.")]
        [StringLength(3, ErrorMessage = "Currency must be exactly 3 characters.")]
        public string Currency { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }

        public string? SKU { get; set; }

        public string? Brand { get; set; }

        public int? CategoryId { get; set; }
    }
}