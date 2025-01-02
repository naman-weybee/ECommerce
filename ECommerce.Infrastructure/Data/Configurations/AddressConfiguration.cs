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
            .HasIndex(a => new { a.Street, a.City, a.State, a.PostalCode, a.Country })
            .HasDatabaseName("IX_Address_Street_City_State_PostalCode_Country")
            .IsUnique();
        }
    }
}