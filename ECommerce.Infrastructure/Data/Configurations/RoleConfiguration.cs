using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
            .HasIndex(r => new { r.Name, r.RoleEntity })
            .HasDatabaseName("IX_Role_Name_RoleEntity")
            .IsUnique();

            builder
            .HasIndex(r => new { r.HasViewPermission, r.HasCreateOrUpdatePermission, r.HasDeletePermission, r.HasFullPermission })
            .HasDatabaseName("IX_Role_PermissionFlags")
            .IsUnique();
        }
    }
}