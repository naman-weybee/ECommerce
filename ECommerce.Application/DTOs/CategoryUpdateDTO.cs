using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class CategoryUpdateDTO
    {
        [Required(ErrorMessage = "Category ID is required.")]
        public Guid Id { get; set; }

        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}