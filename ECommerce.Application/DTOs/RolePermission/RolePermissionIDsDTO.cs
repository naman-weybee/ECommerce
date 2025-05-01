using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.RolePermission
{
    public class RolePermissionIDsDTO
    {
        public Guid RoleId { get; set; }

        public eRoleEntity RoleEntityId { get; set; }

        public virtual RoleEntityDTO RoleEntity { get; set; }
    }
}