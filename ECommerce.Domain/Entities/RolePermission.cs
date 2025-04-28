using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }

        public eRoleEntity RoleEntityId { get; set; }

        public bool HasViewPermission { get; set; }

        public bool HasCreateOrUpdatePermission { get; set; }

        public bool HasDeletePermission { get; set; }

        public bool HasFullPermission { get; set; }

        public virtual Role Role { get; set; }

        public virtual RoleEntity RoleEntity { get; set; }

        public void CreateRolePermission(Guid roleId, eRoleEntity roleEntityId, bool hasViewPermission, bool hasCreateOrUpdatePermission, bool hasDeletePermission, bool hasFullPermission)
        {
            RoleId = roleId;
            RoleEntityId = roleEntityId;
            HasViewPermission = hasViewPermission;
            HasCreateOrUpdatePermission = hasCreateOrUpdatePermission;
            HasDeletePermission = hasDeletePermission;
            HasFullPermission = hasFullPermission;
        }

        public void UpdateRolePermission(Guid roleId, eRoleEntity roleEntityId, bool hasViewPermission, bool hasCreateOrUpdatePermission, bool hasDeletePermission, bool hasFullPermission)
        {
            RoleId = roleId;
            RoleEntityId = roleEntityId;
            HasViewPermission = hasViewPermission;
            HasCreateOrUpdatePermission = hasCreateOrUpdatePermission;
            HasDeletePermission = hasDeletePermission;
            HasFullPermission = hasFullPermission;
        }
    }
}