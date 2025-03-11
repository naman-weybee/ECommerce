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
            .HasIndex(r => r.EntityName)
            .HasDatabaseName("IX_Role_EntityName");

            builder
            .HasIndex(r => new { r.Name, r.EntityName, r.HasViewPermission, r.HasCreateOrUpdatePermission, r.HasFullPermission })
            .HasDatabaseName("IX_Role_Name_EntityName_HasViewPermission_HasCreateOrUpdatePermission_HasFullPermission")
            .IsUnique();
        }
    }
}