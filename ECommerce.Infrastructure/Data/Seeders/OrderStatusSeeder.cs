using ECommerce.Domain.Entities.HelperEntities;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Seeders
{
    internal class OrderStatusSeeder : IEntitySeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderStatus>().HasData(
                new() { StatusId = eOrderStatus.Pending, Name = "Pending" },
                new() { StatusId = eOrderStatus.Placed, Name = "Placed" },
                new() { StatusId = eOrderStatus.Shipped, Name = "Shipped" },
                new() { StatusId = eOrderStatus.Delivered, Name = "Delivered" },
                new() { StatusId = eOrderStatus.Canceled, Name = "Canceled" }
            );
        }
    }
}