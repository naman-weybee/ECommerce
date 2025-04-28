using ECommerce.Domain.Entities.HelperEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Configurations
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder
            .HasKey(x => new { x.StatusId, x.Name });

            builder
            .HasIndex(x => new { x.StatusId, x.Name })
            .HasDatabaseName("IX_OrderStatus_StatusId_Name");
        }
    }
}