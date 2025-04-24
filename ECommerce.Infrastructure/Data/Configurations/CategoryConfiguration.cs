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
            .HasIndex(c => new { c.Name, c.ParentCategoryId })
            .HasDatabaseName("IX_Category_Name_ParentCategoryId")
            .IsUnique();
        }
    }
}