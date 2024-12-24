using ECommerce.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Product : Base
    {
        public Guid Id { get; private set; }

        [MaxLength(100)]
        public string Name { get; private set; }

        [MaxLength(500)]
        public string? Description { get; private set; }

        public Money Price { get; private set; }

        public Currency Currency { get; private set; }

        public int Stock { get; private set; }

        public string? SKU { get; private set; }

        public string? Brand { get; private set; }

        public Category CategoryId { get; private set; }

        public Product(Guid id, string name, Money price, Currency currency, int stock, string? description = null, string? sku = null, string? brand = null, Category categoryId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty.");

            if (stock < 0)
                throw new ArgumentException("Stock cannot be negative.");

            Id = id;
            Name = name;
            Price = price;
            Currency = currency;
            Stock = stock;
            Description = description;
            SKU = sku;
            Brand = brand;
            CategoryId = categoryId;
        }

        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            Stock += quantity;
            Status_Updated();
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            if (quantity > Stock)
                throw new InvalidOperationException("Not enough stock available.");

            Stock -= quantity;
            Status_Updated();
        }

        public void ChangePrice(Money newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("New price must be greater than zero.");

            Price = newPrice;
            Status_Updated();
        }
    }
}