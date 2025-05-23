using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class Role : Base
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }

        public void CreateRole(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public void UpdateRole(Guid id, string name)
        {
            Id = id;
            Name = name;

            StatusUpdated();
        }
    }
}