using Bogus;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data.Seeders.SeederEntities;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class RolePermissionGenerator
    {
        public static Faker Faker { get; set; } = new();
        public static Random random { get; set; } = new();

        public static List<RolePermission> Generate(int dataCount, List<Role> roles)
        {
            var items = new List<RolePermission>();
            var usedPairs = new HashSet<(Guid RoleId, eRoleEntity RoleEntityId)>();

            var adminRole = roles.FirstOrDefault(r => r.Name == "Admin" && !r.IsDeleted);
            if (adminRole != null)
            {
                var adminPair = (adminRole.Id, eRoleEntity.Full);
                items.Add(new RolePermission
                {
                    RoleId = adminRole.Id,
                    RoleEntityId = eRoleEntity.Full,
                    HasViewPermission = true,
                    HasCreateOrUpdatePermission = true,
                    HasDeletePermission = true,
                    HasFullPermission = true
                });

                usedPairs.Add(adminPair);
            }

            var roleIds = roles
                .Where(r => !r.IsDeleted && (adminRole == null || r.Id != adminRole.Id))
                .Select(r => r.Id)
                .ToList();

            var possibleEntities = System.Enum.GetValues<eRoleEntity>().Cast<eRoleEntity>().ToList();
            var maxPossible = (roleIds.Count * possibleEntities.Count) + (adminRole != null ? 1 : 0);

            if (items.Count + dataCount > maxPossible)
                Console.WriteLine($"Cannot generate {dataCount} unique RolePermissions — max possible is {maxPossible}.");

            while (items.Count < dataCount + (adminRole != null ? 1 : 0) && usedPairs.Count < maxPossible)
            {
                var roleId = roleIds[random.Next(roleIds.Count)];
                var roleEntityId = possibleEntities[random.Next(possibleEntities.Count)];

                var pair = (roleId, roleEntityId);
                if (usedPairs.Contains(pair))
                    continue;

                var hasView = Faker.Random.Bool(0.8f);
                var hasCreateOrUpdate = Faker.Random.Bool(0.5f);
                var hasDelete = Faker.Random.Bool(0.3f);

                var newItem = new RolePermission
                {
                    RoleId = roleId,
                    RoleEntityId = roleEntityId,
                    HasViewPermission = hasView,
                    HasCreateOrUpdatePermission = hasCreateOrUpdate,
                    HasDeletePermission = hasDelete,
                    HasFullPermission = hasView && hasCreateOrUpdate && hasDelete
                };

                items.Add(newItem);
                usedPairs.Add(pair);
            }

            return items;
        }
    }
}