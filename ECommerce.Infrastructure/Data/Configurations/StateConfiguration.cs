using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder
            .HasIndex(c => c.Id)
            .HasDatabaseName("IX_State_Id")
            .IsUnique();

            builder
            .HasIndex(c => c.Name)
            .HasDatabaseName("IX_State_Name");

            builder
            .HasIndex(c => new { c.Name, c.CountryId })
            .HasDatabaseName("IX_State_Name_CountryId")
            .IsUnique();
        }
    }
}