using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
            .Property(p => p.Price)
            .HasConversion(new MoneyConverter());

            builder
            .Property(p => p.Currency)
            .HasConversion(new CurrencyConverter());

            builder
            .HasIndex(p => p.Id)
            .HasDatabaseName("IX_Product_Id")
            .IsUnique();

            builder
            .HasIndex(p => p.Name)
            .HasDatabaseName("IX_Product_Name");

            builder
            .HasIndex(p => p.Brand)
            .HasDatabaseName("IX_Product_Brand");

            builder
            .HasIndex(p => p.SKU)
            .HasDatabaseName("IX_Product_SKU")
            .IsUnique();

            builder
            .HasIndex(p => new { p.Name, p.Brand, p.CategoryId })
            .HasDatabaseName("IX_Product_Name_Brand_CategoryId")
            .IsUnique();
        }
    }
}