using ECommerce.Domain.Enums;

namespace ECommerce.Infrastructure.Data.Seeders.SeederEntities
{
    public class RolePermission
    {
        public Guid RoleId { get; set; }

        public eRoleEntity RoleEntityId { get; set; }

        public bool HasViewPermission { get; set; }

        public bool HasCreateOrUpdatePermission { get; set; }

        public bool HasDeletePermission { get; set; }

        public bool HasFullPermission { get; set; }
    }
}