using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder
            .HasIndex(g => g.Id)
            .HasDatabaseName("IX_Gender_Id")
            .IsUnique();

            builder
            .HasIndex(g => g.Name)
            .HasDatabaseName("IX_Gender_Name");
        }
    }
}