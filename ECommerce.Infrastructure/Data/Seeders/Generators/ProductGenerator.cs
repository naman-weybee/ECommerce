using Bogus;
using DataSeeder.Entities;
using ECommerce.Infrastructure.Data.Seeders.SeederEntities;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class ProductGenerator
    {
        public static Faker Faker { get; set; } = new();
        public static Random random { get; set; } = new();

        public static List<Products> Generate(int dataCount, List<Category> categories)
        {
            var items = new List<Products>();
            var activeCategories = categories?.Where(c => !c.IsDeleted)?.Select(c => c.Id)?.ToList()!;

            for (int i = 0; i < dataCount; i++)
            {
                items.Add(new Products
                {
                    Id = Guid.NewGuid(),
                    CategoryId = activeCategories[random.Next(activeCategories.Count)],
                    Name = Faker.Commerce.ProductName(),
                    Description = Faker.Commerce.ProductDescription(),
                    Price = Faker.Random.Double(10, 5000),
                    Currency = "USD",
                    Stock = Faker.PickRandom(10, 1000),
                    SKU = Faker.Random.AlphaNumeric(8).ToUpper(),
                    Brand = Faker.Company.CompanyName()
                });
            }

            return items;
        }
    }
}