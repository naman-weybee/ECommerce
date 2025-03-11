using ECommerce.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Role : Base
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public eRoleEntity RoleEntity { get; set; }

        public bool HasViewPermission { get; set; }

        public bool HasCreateOrUpdatePermission { get; set; }

        public bool HasDeletePermission { get; set; }

        public bool HasFullPermission { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public void CreateRole(string name, eRoleEntity roleEntity, bool hasViewPermission, bool hasCreateOrUpdatePermission, bool hasDeletePermission, bool hasFullPermission)
        {
            Id = Guid.NewGuid();
            Name = name;
            RoleEntity = roleEntity;
            HasViewPermission = hasViewPermission;
            HasCreateOrUpdatePermission = hasCreateOrUpdatePermission;
            HasDeletePermission = hasDeletePermission;
            HasFullPermission = hasFullPermission;
        }

        public void UpdateRole(Guid id, string name, eRoleEntity roleEntity, bool hasViewPermission, bool hasCreateOrUpdatePermission, bool hasDeletePermission, bool hasFullPermission)
        {
            Id = id;
            Name = name;
            RoleEntity = roleEntity;
            HasViewPermission = hasViewPermission;
            HasCreateOrUpdatePermission = hasCreateOrUpdatePermission;
            HasDeletePermission = hasDeletePermission;
            HasFullPermission = hasFullPermission;

            StatusUpdated();
        }
    }
}