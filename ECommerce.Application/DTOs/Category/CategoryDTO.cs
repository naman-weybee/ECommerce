using ECommerce.Application.DTOs.Product;

namespace ECommerce.Application.DTOs.Category
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public virtual ICollection<ProductDTO> Products { get; set; }

        public virtual ICollection<CategoryDTO> SubCategories { get; set; }
    }
}