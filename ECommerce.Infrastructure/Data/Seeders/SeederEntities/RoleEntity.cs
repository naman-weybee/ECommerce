using ECommerce.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Infrastructure.Data.Seeders.SeederEntities
{
    public class RoleEntity
    {
        public eRoleEntity Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}