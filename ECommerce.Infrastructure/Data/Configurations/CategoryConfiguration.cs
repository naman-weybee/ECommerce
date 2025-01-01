using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
            .HasIndex(p => p.Id)
            .HasDatabaseName("IX_Category_Id")
            .IsUnique();

            builder
            .HasIndex(p => p.Name)
            .HasDatabaseName("IX_Category_Name")
            .IsUnique();
        }
    }
}