using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Category : Base
    {
        public Guid Id { get; private set; }

        [MaxLength(100)]
        public string Name { get; private set; }

        [Length(1, 500)]
        public string? Description { get; private set; }

        public Category? ParentCategory { get; private set; }

        public Guid? ParentCategoryId { get; private set; }

        public virtual ICollection<Product> Products { get; private set; }

        public virtual ICollection<Category> SubCategories { get; private set; }

        public Category(string name, string? description, Category? parentCategory = null)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
                throw new ArgumentException("Name must be between 1 and 100 characters.", nameof(name));

            if (description?.Length > 500)
                throw new ArgumentException("Description must be between 1 and 500 characters.", nameof(description));

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ParentCategory = parentCategory;
            ParentCategoryId = parentCategory?.Id;

            Products = new List<Product>();
            SubCategories = new List<Category>();
        }

        public void AddSubCategory(Category subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory), "Subcategory cannot be null.");

            if (subCategory.Id == Id)
                throw new InvalidOperationException("A category cannot be its own parent.");

            SubCategories.Add(subCategory);

            subCategory.SetParentCategory(this);
            Status_Updated();
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