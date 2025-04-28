using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Seeders
{
    public class GenderSeeder : IEntitySeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().HasData(
                new() { Id = Guid.Parse("12345678-1234-1234-1234-123456789abc"), Name = "Male" },
                new() { Id = Guid.Parse("12345678-1234-1234-1234-123456789abd"), Name = "Female" },
                new() { Id = Guid.Parse("12345678-1234-1234-1234-123456789abe"), Name = "Other" }
            );
        }
    }
}