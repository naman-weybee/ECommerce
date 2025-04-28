using ECommerce.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class RoleEntity
    {
        public eRoleEntity Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}