using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class CategoryAggregate : AggregateRoot<Category>
    {
        private readonly IMediator _mediator;

        public Category Category { get; set; }

        public CategoryAggregate(Category entity, IMediator mediator)
            : base(entity, mediator)
        {
            Category = entity;
            Category.Products ??= new List<Product>();
            Category.SubCategories ??= new List<Category>();
            _mediator = mediator;
        }

        public async Task CreateCategory(Category category)
        {
            Category.CreateCategory(category.Name, category.Description, Category.Products, Category.SubCategories, category.ParentCategoryId);

            EventType = eEventType.CategoryCreated;
            await RaiseDomainEvent();
        }

        public async Task UpdateCategory(Category category)
        {
            Category.UpdateCategory(category.Id, category.Name, category.Description, Category.Products, Category.SubCategories, category.ParentCategoryId);

            EventType = eEventType.CategoryUpdated;
            await RaiseDomainEvent();
        }

        public async Task AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            Category.AddProduct(product);

            EventType = eEventType.ProductAddedInCategory;
            await RaiseDomainEvent();
        }

        public async Task RemoveProduct(Guid productId)
        {
            var item = Category.Products.FirstOrDefault(o => o.Id == productId)
                ?? throw new InvalidOperationException("Product not found.");

            Category.RemoveProduct(item);

            EventType = eEventType.ProductRemovedFromCategory;
            await RaiseDomainEvent();
        }

        public async Task AddSubCategory(Category subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory), "Subcategory cannot be null.");

            if (subCategory.Id == Category.Id)
                throw new InvalidOperationException("A category cannot be its own parent.");

            Category.AddSubCategory(subCategory);

            EventType = eEventType.SubCategoryAddedInCategory;
            await RaiseDomainEvent();
        }

        public async Task RemoveSubCategory()
        {
            EventType = eEventType.SubCategoryRemovedFromCategory;
            await RaiseDomainEvent();
        }

        public async Task DeleteCategory()
        {
            EventType = eEventType.CategoryDeleted;
            await RaiseDomainEvent();
        }

        private async Task RaiseDomainEvent()
        {
            var domainEvent = new CategoryEvent(Category.Id, Category.Name, Category.ParentCategoryId, EventType);
            await RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}