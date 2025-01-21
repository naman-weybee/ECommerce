using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder
            .HasIndex(c => c.Id)
            .HasDatabaseName("IX_Country_Id")
            .IsUnique();

            builder
            .HasIndex(c => c.Name)
            .HasDatabaseName("IX_Country_Name");
        }
    }
}