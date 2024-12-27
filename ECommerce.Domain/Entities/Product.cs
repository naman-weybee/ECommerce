using ECommerce.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Product : Base
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public Money Price { get; set; }

        public Currency Currency { get; set; }

        public int Stock { get; set; }

        public string? SKU { get; set; }

        public string? Brand { get; set; }

        public Category CategoryId { get; set; }

        public Product(string name, Money price, Currency currency, int stock, string? description = null, string? sku = null, string? brand = null, Category? categoryId = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Currency = currency;
            Stock = stock;
            Description = description;
            SKU = sku;
            Brand = brand;
            CategoryId = categoryId;
        }

        public void CreateProduct(string name, Money price, Currency currency, int stock, string? description = null, string? sku = null, string? brand = null, Category? categoryId = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Currency = currency;
            Stock = stock;
            Description = description;
            SKU = sku;
            Brand = brand;
            CategoryId = categoryId;
        }

        public void UpdateProduct(string name, Money price, Currency currency, int stock, string? description = null, string? sku = null, string? brand = null, Category? categoryId = null)
        {
            Name = name;
            Price = price;
            Currency = currency;
            Stock = stock;
            Description = description;
            SKU = sku;
            Brand = brand;
            CategoryId = categoryId;

            StatusUpdated();
        }

        public void IncreaseStock(int quantity)
        {
            Stock += quantity;
            StatusUpdated();
        }

        public void DecreaseStock(int quantity)
        {
            Stock -= quantity;
            StatusUpdated();
        }

        public void ChangePrice(Money newPrice)
        {
            Price = newPrice;
            StatusUpdated();
        }

        public void DeleteProduct()
        {
            StatusDeleted();
        }
    }
}