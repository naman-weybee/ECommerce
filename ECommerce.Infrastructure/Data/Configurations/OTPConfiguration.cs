using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class OTPConfiguration : IEntityTypeConfiguration<OTP>
    {
        public void Configure(EntityTypeBuilder<OTP> builder)
        {
            builder
            .HasIndex(o => o.Id)
            .HasDatabaseName("IX_OTP_Id")
            .IsUnique();

            builder
            .HasIndex(o => o.UserId)
            .HasDatabaseName("IX_OTP_UserId");

            builder
            .HasIndex(o => new { o.Id, o.UserId })
            .HasDatabaseName("IX_OTP_Id_UserId")
            .IsUnique();
        }
    }
}