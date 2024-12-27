﻿using System.ComponentModel.DataAnnotations;

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

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }

        public Category(string name, string? description, Category? parentCategory = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ParentCategory = parentCategory;
            ParentCategoryId = parentCategory?.Id;
            Products = new List<Product>();
            SubCategories = new List<Category>();
        }

        public void CreateCategory(string name, string? description, Category? parentCategory = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ParentCategory = parentCategory;
            ParentCategoryId = parentCategory?.Id;
            Products = new List<Product>();
            SubCategories = new List<Category>();
        }

        public void UpdateCategory(string name, string? description, Category? parentCategory = null)
        {
            Name = name;
            Description = description;
            ParentCategory = parentCategory;
            ParentCategoryId = parentCategory?.Id;
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

        public void DeleteCategory()
        {
            StatusDeleted();
        }
    }
}