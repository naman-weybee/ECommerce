using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class Category : Base
    {
        public Guid Id { get; set; }

        [ForeignKey("Category")]
        public Guid? ParentCategoryId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Length(1, 500)]
        public string? Description { get; set; }

        public Category? ParentCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }

        public Category()
        {
        }

        public Category(string name, string? description, Guid? parentCategoryId = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
            Products = new List<Product>();
            SubCategories = new List<Category>();
        }

        public void CreateCategory(string name, string? description, Guid? parentCategoryId = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
            Products = new List<Product>();
            SubCategories = new List<Category>();
        }

        public void UpdateCategory(Guid id, string name, string? description, Guid? parentCategoryId = null)
        {
            Id = id;
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
            Products = new List<Product>();
            SubCategories = new List<Category>();

            StatusUpdated();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);

            StatusUpdated();
        }

        public void RemoveProduct(Product product)
        {
            Products.Remove(product);

            StatusUpdated();
        }

        public void AddSubCategory(Category subCategory)
        {
            SubCategories.Add(subCategory);

            subCategory.SetParentCategory(this);
            StatusUpdated();
        }

        public void RemoveSubCategory(Category subCategory)
        {
            SubCategories.Remove(subCategory);

            StatusUpdated();
        }

        private void SetParentCategory(Category parentCategory)
        {
            ParentCategory = parentCategory;
            ParentCategoryId = parentCategory.Id;
        }
    }
}