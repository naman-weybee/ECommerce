using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Seeders.Interfaces
{
    public interface IEntitySeeder
    {
        void Seed(ModelBuilder modelBuilder);
    }
}