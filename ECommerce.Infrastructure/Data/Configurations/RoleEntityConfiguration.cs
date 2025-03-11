using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder
            .HasIndex(r => r.Id)
            .HasDatabaseName("IX_RoleEntity_Id")
            .IsUnique();

            builder
            .HasIndex(r => r.Name)
            .HasDatabaseName("IX_RoleEntity_Name")
            .IsUnique();
        }
    }
}