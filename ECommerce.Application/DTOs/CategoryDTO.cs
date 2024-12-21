using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }

        public virtual ICollection<ProductDTO> Products { get; set; }

        public virtual ICollection<CategoryDTO> SubCategories { get; set; }
    }
}