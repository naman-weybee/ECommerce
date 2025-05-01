using ECommerce.Application.DTOs.RolePermission;

namespace ECommerce.Application.DTOs.Role
{
    public class RoleDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<RolePermissionDTO> RolePermissions { get; set; }
    }
}