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
            Entity.Products ??= [];
            Entity.SubCategories ??= [];
            _eventCollector = eventCollector;
        }

        public void CreateCategory()
        {
            Entity.CreateCategory(Entity.Name, Entity.Description, Entity.Products, Entity.SubCategories, Entity.ParentCategoryId);

            EventType = eEventType.CategoryCreated;
            RaiseDomainEvent();
        }

        public void UpdateCategory()
        {
            Entity.UpdateCategory(Entity.Id, Entity.Name, Entity.Description, Entity.Products, Entity.SubCategories, Entity.ParentCategoryId);

            EventType = eEventType.CategoryUpdated;
            RaiseDomainEvent();
        }

        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            Entity.AddProduct(product);

            EventType = eEventType.ProductAddedInCategory;
            RaiseDomainEvent();
        }

        public void RemoveProduct(Guid productId)
        {
            var item = Entity.Products.FirstOrDefault(o => o.Id == productId)
                ?? throw new InvalidOperationException("Product not found.");

            Entity.RemoveProduct(item);

            EventType = eEventType.ProductRemovedFromCategory;
            RaiseDomainEvent();
        }

        public void AddSubCategory(Category subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory), "Subcategory cannot be null.");

            if (subCategory.Id == Entity.Id)
                throw new InvalidOperationException("A category cannot be its own parent.");

            Entity.AddSubCategory(subCategory);

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
            var domainEvent = new CategoryEvent(Entity.Id, Entity.Name, Entity.ParentCategoryId, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}