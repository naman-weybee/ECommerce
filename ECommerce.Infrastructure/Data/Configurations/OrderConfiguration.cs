using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
            .HasIndex(o => o.UserId)
            .HasDatabaseName("IX_Order_UserId");

            builder
            .HasIndex(o => o.BillingAddressId)
            .HasDatabaseName("IX_Order_BillingAddressId");

            builder
            .HasIndex(o => o.ShippingAddressId)
            .HasDatabaseName("IX_Order_ShippingAddressId");

            builder
            .Property(p => p.TotalAmount)
            .HasConversion(new MoneyConverter());

            builder
            .HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(o => o.BillingAddress)
            .WithMany()
            .HasForeignKey(o => o.BillingAddressId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(o => o.ShippingAddress)
            .WithMany()
            .HasForeignKey(o => o.ShippingAddressId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}