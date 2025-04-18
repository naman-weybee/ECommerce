using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder
            .HasIndex(c => c.Id)
            .HasDatabaseName("IX_City_Id")
            .IsUnique();

            builder
            .HasIndex(c => c.Name)
            .HasDatabaseName("IX_City_Name");

            builder
            .HasIndex(c => new { c.Name, c.StateId })
            .HasDatabaseName("IX_City_Name_StateId")
            .IsUnique();
        }
    }
}