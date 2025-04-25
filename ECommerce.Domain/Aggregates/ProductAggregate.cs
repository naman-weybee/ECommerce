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

        public void CreateProduct()
        {
            Entity.CreateProduct(Entity.CategoryId, Entity.Name, Entity.Price, Entity.Currency, Entity.Stock, Entity.SKU, Entity.Description, Entity.Brand);

            EventType = eEventType.ProductCreated;
            RaiseDomainEvent();
        }

        public void UpdateProduct()
        {
            Entity.UpdateProduct(Entity.Id, Entity.CategoryId, Entity.Name, Entity.Price, Entity.Currency, Entity.Stock, Entity.SKU, Entity.Description, Entity.Brand);

            EventType = eEventType.ProductUpdated;
            RaiseDomainEvent();

            if (Entity.Stock == 0)
                ProductStockDepletedEvent();
        }

        public void IncreaseStock(int quantity)
        {
            Entity.IncreaseStock(quantity);

            EventType = eEventType.ProductStockIncreased;
            RaiseDomainEvent();
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity > Entity.Stock)
                throw new InvalidOperationException("Not enough stock available.");

            Entity.DecreaseStock(quantity);

            EventType = eEventType.ProductStockDecreased;
            RaiseDomainEvent();

            if (Entity.Stock == 0)
                ProductStockDepletedEvent();
        }

        public void ChangePrice(Money newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("New price must be greater than zero.");

            Entity.ChangePrice(newPrice);

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
            var domainEvent = new ProductEvent(Entity.Id, Entity.Name, Entity.SKU, Entity.Price.Amount, Entity.Stock, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}