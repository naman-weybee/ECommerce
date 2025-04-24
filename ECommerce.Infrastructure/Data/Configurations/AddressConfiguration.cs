using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder
            .HasIndex(c => c.UserId)
            .HasDatabaseName("IX_Address_UserId");

            builder
            .HasIndex(c => new { c.CountryId, c.StateId, c.CityId })
            .HasDatabaseName("IX_Address_CountryId_StateId_CityId");

            builder
            .HasOne(o => o.Country)
            .WithMany()
            .HasForeignKey(o => o.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(o => o.State)
            .WithMany()
            .HasForeignKey(o => o.StateId)
            .OnDelete(DeleteBehavior.Restrict);

            builder
            .HasOne(o => o.City)
            .WithMany()
            .HasForeignKey(o => o.CityId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}