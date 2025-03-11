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
            .HasIndex(r => r.Id)
            .HasDatabaseName("IX_Role_Id")
            .IsUnique();

            builder
            .HasIndex(r => r.Name)
            .HasDatabaseName("IX_Role_Name")
            .IsUnique();

            builder
            .HasIndex(r => r.RoleEntity)
            .HasDatabaseName("IX_Role_RoleEntityId");

            builder
            .HasIndex(r => new { r.Name, r.RoleEntity, r.HasViewPermission, r.HasCreateOrUpdatePermission, r.HasDeletePermission, r.HasFullPermission })
            .HasDatabaseName("IX_Role_Name_RoleEntityId_HasViewPermission_HasCreateOrUpdatePermission_HasDeletePermission_HasFullPermission")
            .IsUnique();
        }
    }
}