using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;

namespace ECommerce.Domain.Aggregates
{
    public class CategoryAggregate : AggregateRoot<Category>
    {
        public Category Category { get; set; }

        public List<Product>? Products { get; set; }

        public List<Category>? SubCategories { get; set; }

        public CategoryAggregate(Category entity)
            : base(entity)
        {
            Category = entity;
            Products = entity.Products.ToList() ?? new List<Product>();
            SubCategories = entity.SubCategories.ToList() ?? new List<Category>();
        }

        public void CreateCategory(Category category)
        {
            Category.CreateCategory(category.Name, category.Description, category.ParentCategoryId);

            EventType = eEventType.CategoryCreated;
            RaiseDomainEvent();
        }

        public void UpdateCategory(Category category)
        {
            Category.UpdateCategory(category.Name, category.Description, category.ParentCategoryId);

            EventType = eEventType.CategoryUpdated;
            RaiseDomainEvent();
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            Category.AddProduct(product);

            EventType = eEventType.CategoryUpdated;
            RaiseDomainEvent();
        }

        public void RemoveProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            Category.RemoveProduct(product);

            EventType = eEventType.CategoryUpdated;
            RaiseDomainEvent();
        }

        public void AddSubCategory(Category subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory), "Subcategory cannot be null.");

            if (subCategory.Id == Category.Id)
                throw new InvalidOperationException("A category cannot be its own parent.");

            Category.AddSubCategory(subCategory);

            EventType = eEventType.CategoryUpdated;
            RaiseDomainEvent();
        }

        public void RemoveSubCategory(Category subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory), "Subcategory cannot be null.");

            if (subCategory.Id == Category.Id)
                throw new InvalidOperationException("A category cannot be its own parent.");

            Category.RemoveSubCategory(subCategory);

            EventType = eEventType.CategoryUpdated;
            RaiseDomainEvent();
        }

        public void DeleteCategory()
        {
            if (Category.IsDeleted)
                throw new InvalidOperationException("Cannnot delete already deleted Category.");

            EventType = eEventType.CategoryDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new CategoryEvent(Category.Id, Category.Name, Category.ParentCategoryId, EventType);
            RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}