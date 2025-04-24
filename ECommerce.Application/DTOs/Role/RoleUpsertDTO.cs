using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.Role
{
    public class RoleUpsertDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public eRoleEntity RoleEntity { get; set; }

        public bool HasViewPermission { get; set; }

        public bool HasCreateOrUpdatePermission { get; set; }

        public bool HasDeletePermission { get; set; }

        public bool HasFullPermission { get; set; }
    }
}