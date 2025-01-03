using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder
            .HasIndex(o => o.Id)
            .HasDatabaseName("IX_OrderItem_Id")
            .IsUnique();

            builder
            .Property(p => p.UnitPrice)
            .HasConversion(new MoneyConverter());

            builder
            .HasIndex(o => new { o.OrderId, o.ProductId })
            .HasDatabaseName("IX_OrderItem_OrderId_ProductId")
            .IsUnique();

            builder
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}