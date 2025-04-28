using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Data.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Seeders
{
    public class RoleSeeder : IEntitySeeder
    {
        public Guid AdminRoleId = Guid.Parse("13571357-1357-1357-1357-135713571357");
        public Guid GuestRoleId = Guid.Parse("24682468-2468-2468-2468-246824682468");

        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new() { Id = AdminRoleId, Name = "Admin", CreatedDate = new DateTime(2025, 1, 1), UpdatedDate = new DateTime(2025, 1, 1) },
                new() { Id = GuestRoleId, Name = "Guest", CreatedDate = new DateTime(2025, 1, 1), UpdatedDate = new DateTime(2025, 1, 1) }
            );
        }
    }
}