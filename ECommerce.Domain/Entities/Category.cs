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

        public Category? ParentCategory { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();

        public void AddSubCategory(Category subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory), "Subcategory cannot be null.");

            if (subCategory.Id == Id)
                throw new InvalidOperationException("A category cannot be its own parent.");

            SubCategories.Add(subCategory);

            subCategory.SetParentCategory(this);
        }

        private void SetParentCategory(Category parentCategory)
        {
            if (parentCategory == null)
                throw new ArgumentNullException(nameof(parentCategory), "Parent category cannot be null.");

            if (parentCategory.Id == Id)
                throw new InvalidOperationException("A category cannot be its own parent.");

            ParentCategory = parentCategory;
            ParentCategoryId = parentCategory.Id;
        }
    }
}