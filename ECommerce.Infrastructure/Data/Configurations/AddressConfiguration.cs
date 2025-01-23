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
            .HasIndex(a => new { a.FirstName, a.LastName, a.UserId, a.CountryId, a.StateId, a.CityId, a.PostalCode, a.AdderessType, a.PhoneNumber })
            .HasDatabaseName("IX_Address_FirstName_LastName_UserId_CountryId_StateId_CityId_PostalCode_AdderessType_PhoneNumber")
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