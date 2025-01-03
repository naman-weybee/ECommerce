using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
            .HasIndex(o => o.Id)
            .HasDatabaseName("IX_Order_Id")
            .IsUnique();

            builder
            .HasIndex(o => o.UserId)
            .HasDatabaseName("IX_Order_UserId");

            builder
            .HasIndex(o => o.AddressId)
            .HasDatabaseName("IX_Order_AddressId");
        }
    }
}