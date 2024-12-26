using ECommerce.Domain.Entities;
using ECommerce.Domain.Events;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class CategoryAggregate
    {
        public Category Category { get; set; }

        public List<Product> Products { get; set; }

        public List<Category> SubCategories { get; set; }

        private readonly List<INotification> _domainEvents = new();

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public CategoryAggregate(Category category)
        {
            Category = category;
            Products = Category.Products.ToList() ?? new List<Product>();
            SubCategories = Category.SubCategories.ToList() ?? new List<Category>();
        }

        public void AddSubCategory(Category subCategory)
        {
            if (subCategory == null)
                throw new ArgumentNullException(nameof(subCategory), "Subcategory cannot be null.");

            if (subCategory.Id == Category.Id)
                throw new InvalidOperationException("A category cannot be its own parent.");

            Category.AddSubCategory(subCategory);
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var categoryEvent = new CategoryEvent(Category.Id, Category.Name, Category.ParentCategoryId);
            _domainEvents.Add(categoryEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}