using ECommerce.Infrastructure.Data.Seeders.SeederEntities;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class RoleGenerator
    {
        public static List<Role> Generate()
        {
            return
            [
                new() { Id = Guid.NewGuid(), Name = "Admin" },
                new() { Id = Guid.NewGuid(), Name = "Moderator" },
                new() { Id = Guid.NewGuid(), Name = "Support" },
                new() { Id = Guid.NewGuid(), Name = "Viewer" },
                new() { Id = Guid.NewGuid(), Name = "Guest" }
            ];
        }
    }
}