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
        }
    }
}