using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Aggregates
{
    public class ProductAggregate : AggregateRoot<Product>
    {
        public Product Product { get; set; }

        public ProductAggregate(Product entity)
            : base(entity)
        {
            Product = entity;
        }

        public void CreateProduct(Product product)
        {
            Product.CreateProduct(product.Name, product.Price, product.Currency, product.Stock, product.Description, product.SKU, product.Brand, product.CategoryId);

            EventType = eEventType.ProductCreated;
            RaiseDomainEvent();
        }

        public void UpdateProduct(Product product)
        {
            Product.UpdateProduct(product.Name, product.Price, product.Currency, product.Stock, product.Description, product.SKU, product.Brand, product.CategoryId);

            EventType = eEventType.ProductUpdated;
            RaiseDomainEvent();
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
            if (Product.IsDeleted)
                throw new InvalidOperationException("Cannnot delete already deleted Product.");

            EventType = eEventType.ProductDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new ProductEvent(Product.Id, Product.Name, Product.Price.Amount, Product.Stock, EventType);
            RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}