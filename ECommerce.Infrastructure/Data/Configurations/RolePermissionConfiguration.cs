using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder
            .HasKey(rp => new { rp.RoleId, rp.RoleEntityId });

            builder
            .HasOne(rp => rp.Role)
            .WithMany()
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

            builder
            .HasOne(rp => rp.RoleEntity)
            .WithMany()
            .HasForeignKey(rp => rp.RoleEntityId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}