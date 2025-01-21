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
            .HasIndex(a => a.Id)
            .HasDatabaseName("IX_Address_Id")
            .IsUnique();

            builder
            .HasIndex(a => new { a.CountryId, a.StateId, a.CityId, a.PostalCode })
            .HasDatabaseName("IX_Address_CountryId_StateId_CityId_PostalCode")
            .IsUnique();

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