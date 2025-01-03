﻿using ECommerce.Domain.Entities;
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
            .HasDatabaseName("IX_Gender_Id")
            .IsUnique();

            builder
            .HasIndex(r => r.Name)
            .HasDatabaseName("IX_Gender_Name");
        }
    }
}