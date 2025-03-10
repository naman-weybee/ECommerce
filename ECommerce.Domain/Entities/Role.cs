using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Role : Base
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string EntityName { get; set; }

        public bool HasViewPermission { get; set; }

        public bool HasCreateOrUpdatePermission { get; set; }

        public bool HasDeletePermission { get; set; }

        public bool HasFullPermission { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public void CreateRole(string name, string entityName, bool hasViewPermission, bool hasCreateOrUpdatePermission, bool hasDeletePermission, bool hasFullPermission)
        {
            Id = Guid.NewGuid();
            Name = name;
            EntityName = entityName;
            HasViewPermission = hasViewPermission;
            HasCreateOrUpdatePermission = hasCreateOrUpdatePermission;
            HasDeletePermission = hasDeletePermission;
            HasFullPermission = hasFullPermission;
        }

        public void UpdateRole(Guid id, string name, string entityName, bool hasViewPermission, bool hasCreateOrUpdatePermission, bool hasDeletePermission, bool hasFullPermission)
        {
            Id = id;
            Name = name;
            EntityName = entityName;
            HasViewPermission = hasViewPermission;
            HasCreateOrUpdatePermission = hasCreateOrUpdatePermission;
            HasDeletePermission = hasDeletePermission;
            HasFullPermission = hasFullPermission;

            StatusUpdated();
        }
    }
}