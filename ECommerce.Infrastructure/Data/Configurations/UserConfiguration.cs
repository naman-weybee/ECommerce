using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
            .HasIndex(u => u.Id)
            .HasDatabaseName("IX_User_Id")
            .IsUnique();

            builder
            .HasIndex(u => u.Email)
            .HasDatabaseName("IX_User_Email")
            .IsUnique();
        }
    }
}