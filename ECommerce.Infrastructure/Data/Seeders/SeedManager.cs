using ECommerce.Infrastructure.Data.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Seeders
{
    public static class SeedManager
    {
        private static readonly List<IEntitySeeder> Seeders =
        [
            new GenderSeeder(),
            new OrderStatusSeeder(),
            new RoleEntitySeeder(),
            new RoleSeeder(),
            new RolePermissionSeeder()
        ];

        public static void SeedAll(ModelBuilder modelBuilder)
        {
            foreach (var seeder in Seeders)
                seeder.Seed(modelBuilder);
        }
    }
}