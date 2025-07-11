using ECommerce.Infrastructure.Data.Seeders.Generators;
using ECommerce.Infrastructure.Data.Seeders.Helpers;
using ECommerce.Shared.Constants;

namespace ECommerce.Infrastructure.Data.Seeders
{
    public static class SeederManager
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Roles.Any())
            {
                Console.WriteLine("Database already seeded.");
                return;
            }

            var roles = RoleGenerator.Generate();
            var roleTable = SeederHelper.ToDataTable(roles);
            DbContextBulkExtensions.BulkInsert(roleTable, "Roles");

            var roleEntities = RoleEntityGenerator.Generate();
            var roleEntityTable = SeederHelper.ToDataTable(roleEntities);
            DbContextBulkExtensions.BulkInsert(roleEntityTable, "RoleEntities");

            var rolePermissions = RolePermissionGenerator.Generate(Constants.RolePermissionCount, roles);
            var rolePermissionsTable = SeederHelper.ToDataTable(rolePermissions);
            DbContextBulkExtensions.BulkInsert(rolePermissionsTable, "RolePermissions");

            var categories = CategoryGenerator.Generate(Constants.CategoryCount);
            var categoryTable = SeederHelper.ToDataTable(categories);
            DbContextBulkExtensions.BulkInsert(categoryTable, "Categories");

            var products = ProductGenerator.Generate(Constants.ProductCount, categories);
            var productTable = SeederHelper.ToDataTable(products);
            DbContextBulkExtensions.BulkInsert(productTable, "Products");

            var countries = CountryGenerator.Generate(Constants.CountryCount);
            var countryTable = SeederHelper.ToDataTable(countries);
            DbContextBulkExtensions.BulkInsert(countryTable, "Countries");

            var states = StateGenerator.Generate(Constants.StateCount, countries);
            var stateTable = SeederHelper.ToDataTable(states);
            DbContextBulkExtensions.BulkInsert(stateTable, "States");

            var cities = CityGenerator.Generate(Constants.CityCount, states);
            var cityTable = SeederHelper.ToDataTable(cities);
            DbContextBulkExtensions.BulkInsert(cityTable, "Cities");

            var genders = GenderGenerator.Generate();
            var genderTable = SeederHelper.ToDataTable(genders);
            DbContextBulkExtensions.BulkInsert(genderTable, "Gender");

            var users = UserGenerator.Generate(Constants.UserCount, roles, genders);
            var userTable = SeederHelper.ToDataTable(users);
            DbContextBulkExtensions.BulkInsert(userTable, "Users");

            var addresses = AddressGenerator.Generate(Constants.AddressCount, users, countries, states, cities);
            var addressTable = SeederHelper.ToDataTable(addresses);
            DbContextBulkExtensions.BulkInsert(addressTable, "Address");

            var cartItems = CartItemGenerator.Generate(Constants.CartItemCount, users, products);
            var orderIds = OrderGenerator.Generate(Constants.OrderCount);

            var orderItems = OrderItemGenerator.Generate(Constants.OrderItemCount, orderIds, addresses, cartItems, products);
            var orderItemTable = SeederHelper.ToDataTable(orderItems);
            DbContextBulkExtensions.BulkInsert(orderItemTable, "OrderItems");
        }
    }
}