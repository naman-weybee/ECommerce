using ECommerce.Domain.Entities;
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
            .HasIndex(o => new { o.OrderId, o.ProductId })
            .HasDatabaseName("IX_OrderItem_OrderId_ProductId")
            .IsUnique();
        }
    }
}