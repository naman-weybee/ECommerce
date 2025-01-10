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
            .HasIndex(c => c.Id)
            .HasDatabaseName("IX_CartItem_Id")
            .IsUnique();

            builder
            .Property(p => p.UnitPrice)
            .HasConversion(new MoneyConverter());
        }
    }
}