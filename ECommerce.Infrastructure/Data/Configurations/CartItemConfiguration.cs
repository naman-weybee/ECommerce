using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder
            .HasIndex(c => c.UserId)
            .HasDatabaseName("IX_CartItem_UserId");

            builder
            .HasIndex(c => c.ProductId)
            .HasDatabaseName("IX_CartItem_ProductId");

            builder
            .Property(p => p.UnitPrice)
            .HasConversion(new MoneyConverter());
        }
    }
}