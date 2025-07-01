using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data.Seeders.SeederEntities;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class RoleEntityGenerator
    {
        public static List<RoleEntity> Generate()
        {
            return
            [
                new() { Id = eRoleEntity.Unknown, Name = "Unknown" },
                new() { Id = eRoleEntity.Full, Name = "Full" },
                new() { Id = eRoleEntity.Country, Name = "Country" },
                new() { Id = eRoleEntity.State, Name = "State" },
                new() { Id = eRoleEntity.City, Name = "City" },
                new() { Id = eRoleEntity.Role, Name = "Role" },
                new() { Id = eRoleEntity.RolePermission, Name = "RolePermission" },
                new() { Id = eRoleEntity.RoleEntity, Name = "RoleEntity" },
                new() { Id = eRoleEntity.Gender, Name = "Gender" },
                new() { Id = eRoleEntity.Address, Name = "Addresses" },
                new() { Id = eRoleEntity.Category, Name = "Category" },
                new() { Id = eRoleEntity.Product, Name = "Product" },
                new() { Id = eRoleEntity.User, Name = "User" },
                new() { Id = eRoleEntity.CartItem, Name = "CartItem" },
                new() { Id = eRoleEntity.Order, Name = "Order" },
                new() { Id = eRoleEntity.OrderStatus, Name = "OrderStatus" },
                new() { Id = eRoleEntity.OrderItem, Name = "OrderItem" },
                new() { Id = eRoleEntity.RefreshToken, Name = "RefreshToken" },
                new() { Id = eRoleEntity.OTP, Name = "OTP" }
            ];
        }
    }
}