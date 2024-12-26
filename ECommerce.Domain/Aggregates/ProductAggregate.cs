using ECommerce.Domain.Entities;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class ProductAggregate
    {
        public Product Product { get; set; }

        private readonly List<INotification> _domainEvents = new();

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public ProductAggregate(Product product)
        {
            Product = product;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            Product.IncreaseStock(quantity);
            RaiseDomainEvent();
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            if (quantity > Product.Stock)
                throw new InvalidOperationException("Not enough stock available.");

            Product.DecreaseStock(quantity);
            RaiseDomainEvent();
        }

        public void ChangePrice(Money newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("New price must be greater than zero.");

            Product.ChangePrice(newPrice);
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var orderEvent = new ProductEvent(Product.Id, Product.Name, Product.Price.Amount, Product.Stock);
            _domainEvents.Add(orderEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}