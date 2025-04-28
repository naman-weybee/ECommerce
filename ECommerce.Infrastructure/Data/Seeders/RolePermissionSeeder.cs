using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data.Seeders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Data.Seeders
{
    public class RolePermissionSeeder : IEntitySeeder
    {
        public Guid AdminRoleId = Guid.Parse("13571357-1357-1357-1357-135713571357");
        public Guid GuestRoleId = Guid.Parse("24682468-2468-2468-2468-246824682468");

        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>().HasData(
                new() { RoleId = AdminRoleId, RoleEntityId = eRoleEntity.Full, HasFullPermission = true },
                new() { RoleId = GuestRoleId, RoleEntityId = eRoleEntity.Country, HasViewPermission = true },
                new() { RoleId = GuestRoleId, RoleEntityId = eRoleEntity.State, HasViewPermission = true },
                new() { RoleId = GuestRoleId, RoleEntityId = eRoleEntity.City, HasViewPermission = true },
                new() { RoleId = GuestRoleId, RoleEntityId = eRoleEntity.Gender, HasViewPermission = true },
                new() { RoleId = GuestRoleId, RoleEntityId = eRoleEntity.Category, HasViewPermission = true },
                new() { RoleId = GuestRoleId, RoleEntityId = eRoleEntity.Product, HasViewPermission = true },
                new() { RoleId = GuestRoleId, RoleEntityId = eRoleEntity.Order, HasViewPermission = true },
                new() { RoleId = GuestRoleId, RoleEntityId = eRoleEntity.OrderStatus, HasViewPermission = true },
                new() { RoleId = GuestRoleId, RoleEntityId = eRoleEntity.Address, HasViewPermission = true, HasCreateOrUpdatePermission = true, HasDeletePermission = true },
                new() { RoleId = GuestRoleId, RoleEntityId = eRoleEntity.CartItem, HasViewPermission = true, HasCreateOrUpdatePermission = true, HasDeletePermission = true }
            );
        }
    }
}