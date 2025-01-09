using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class ProductAggregate : AggregateRoot<Product>
    {
        private readonly IMediator _mediator;

        public Product Product { get; set; }

        public ProductAggregate(Product entity, IMediator mediator)
            : base(entity, mediator)
        {
            Product = entity;
            _mediator = mediator;
        }

        public async Task CreateProduct(Product product)
        {
            Product.CreateProduct(product.CategoryId, product.Name, product.Price, product.Currency, product.Stock, product.Description, product.SKU, product.Brand);

            EventType = eEventType.ProductCreated;
            await RaiseDomainEvent();
        }

        public async Task UpdateProduct(Product product)
        {
            Product.UpdateProduct(product.Id, product.CategoryId, product.Name, product.Price, product.Currency, product.Stock, product.Description, product.SKU, product.Brand);

            EventType = eEventType.ProductUpdated;
            await RaiseDomainEvent();

            if (Product.Stock == 0)
                await ProductStockDepletedEvent();
        }

        public async Task IncreaseStock(int quantity)
        {
            Product.IncreaseStock(quantity);

            EventType = eEventType.ProductStockIncreased;
            await RaiseDomainEvent();
        }

        public async Task DecreaseStock(int quantity)
        {
            if (quantity > Product.Stock)
                throw new InvalidOperationException("Not enough stock available.");

            Product.DecreaseStock(quantity);

            EventType = eEventType.ProductStockDecreased;
            await RaiseDomainEvent();

            if (Product.Stock == 0)
                await ProductStockDepletedEvent();
        }

        public async Task ChangePrice(Money newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("New price must be greater than zero.");

            Product.ChangePrice(newPrice);

            EventType = eEventType.ProductPriceChanged;
            await RaiseDomainEvent();
        }

        public async Task DeleteProduct()
        {
            EventType = eEventType.ProductDeleted;
            await RaiseDomainEvent();
        }

        private async Task ProductStockDepletedEvent()
        {
            EventType = eEventType.ProductStockDepleted;
            await RaiseDomainEvent();
        }

        private async Task RaiseDomainEvent()
        {
            var domainEvent = new ProductEvent(Product.Id, Product.Name, Product.SKU, Product.Price.Amount, Product.Stock, EventType);
            await RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}