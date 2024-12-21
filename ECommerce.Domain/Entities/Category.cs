using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Category : Base
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Length(1, 500)]
        public string? Description { get; set; }

        public Category? ParentCategoryId { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }
    }
}