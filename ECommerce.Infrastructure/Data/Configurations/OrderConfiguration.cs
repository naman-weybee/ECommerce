using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

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
            .Property(p => p.TotalAmount)
            .HasConversion(new MoneyConverter());

            builder
            .HasIndex(o => o.AddressId)
            .HasDatabaseName("IX_Order_AddressId");

            builder
            .HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(o => o.ShippingAddress)
            .WithMany()
            .HasForeignKey(o => o.AddressId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}