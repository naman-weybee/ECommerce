using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class ProductAggregate : AggregateRoot<Product>
    {
        private readonly IDomainEventCollector _eventCollector;

        public Product Product { get; set; }

        public ProductAggregate(Product entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            Product = entity;
            _eventCollector = eventCollector;
        }

        public void CreateProduct(Product product)
        {
            Product.CreateProduct(product.CategoryId, product.Name, product.Price, product.Currency, product.Stock, product.Description, product.SKU, product.Brand);

            EventType = eEventType.ProductCreated;
            RaiseDomainEvent();
        }

        public void UpdateProduct(Product product)
        {
            Product.UpdateProduct(product.Id, product.CategoryId, product.Name, product.Price, product.Currency, product.Stock, product.Description, product.SKU, product.Brand);

            EventType = eEventType.ProductUpdated;
            RaiseDomainEvent();

            if (Product.Stock == 0)
                ProductStockDepletedEvent();
        }

        public void IncreaseStock(int quantity)
        {
            Product.IncreaseStock(quantity);

            EventType = eEventType.ProductStockIncreased;
            RaiseDomainEvent();
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity > Product.Stock)
                throw new InvalidOperationException("Not enough stock available.");

            Product.DecreaseStock(quantity);

            EventType = eEventType.ProductStockDecreased;
            RaiseDomainEvent();

            if (Product.Stock == 0)
                ProductStockDepletedEvent();
        }

        public void ChangePrice(Money newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("New price must be greater than zero.");

            Product.ChangePrice(newPrice);

            EventType = eEventType.ProductPriceChanged;
            RaiseDomainEvent();
        }

        public void DeleteProduct()
        {
            EventType = eEventType.ProductDeleted;
            RaiseDomainEvent();
        }

        private void ProductStockDepletedEvent()
        {
            EventType = eEventType.ProductStockDepleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new ProductEvent(Product.Id, Product.Name, Product.SKU, Product.Price.Amount, Product.Stock, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}