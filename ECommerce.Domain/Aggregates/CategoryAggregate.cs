using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class CategoryAggregate : AggregateRoot<Category>
    {
        private readonly IDomainEventCollector _eventCollector;

        public Category Category { get; set; }

        public CategoryAggregate(Category entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            Category = entity;
            Category.Products ??= new List<Product>();
            Category.SubCategories ??= new List<Category>();
            _eventCollector = eventCollector;
        }

        public void CreateCategory(Category category)
        {
            Category.CreateCategory(category.Name, category.Description, Category.Products, Category.SubCategories, category.ParentCategoryId);

            EventType = eEventType.CategoryCreated;
            RaiseDomainEvent();
        }

        public void UpdateCategory(Category category)
        {
            Category.UpdateCategory(category.Id, category.Name, category.Description, Category.Products, Category.SubCategories, category.ParentCategoryId);

            EventType = eEventType.CategoryUpdated;
            RaiseDomainEvent();
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            Category.AddProduct(product);

            EventType = eEventType.ProductAddedInCategory;
            RaiseDomainEvent();
        }

        public void RemoveProduct(Guid productId)
        {
            var item = Category.Products.FirstOrDefault(o => o.Id == productId)
                ?? throw new InvalidOperationException("Product not found.");

            Category.RemoveProduct(item);

            EventType = eEventType.ProductRemovedFromCategory;
            RaiseDomainEvent();
        }

        public void AddSubCategory(Category subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory), "Subcategory cannot be null.");

            if (subCategory.Id == Category.Id)
                throw new InvalidOperationException("A category cannot be its own parent.");

            Category.AddSubCategory(subCategory);

            EventType = eEventType.SubCategoryAddedInCategory;
            RaiseDomainEvent();
        }

        public void RemoveSubCategory()
        {
            EventType = eEventType.SubCategoryRemovedFromCategory;
            RaiseDomainEvent();
        }

        public void DeleteCategory()
        {
            EventType = eEventType.CategoryDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new CategoryEvent(Category.Id, Category.Name, Category.ParentCategoryId, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}