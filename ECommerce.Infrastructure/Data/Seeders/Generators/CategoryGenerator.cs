using Bogus;
using DataSeeder.Entities;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class CategoryGenerator
    {
        public static Faker Faker { get; set; } = new();

        public static List<Category> Generate(int dataCount)
        {
            var categoryIds = new List<Guid>();
            var categories = new List<Category>();

            for (int i = 0; i < dataCount; i++)
            {
                var id = Guid.NewGuid();
                Guid? parentId = null;
                categoryIds.Add(id);

                if (i > 0 && Faker.Random.Bool(0.2f))
                {
                    var parentIndex = Faker.Random.Int(0, categories.Count - 2);
                    parentId = categoryIds[parentIndex];
                }

                categories.Add(new Category
                {
                    Id = id,
                    ParentCategoryId = parentId,
                    Name = Faker.Commerce.Categories(1)[0] + $"_{i}",
                    Description = Faker.Commerce.ProductDescription()
                });
            }

            return categories;
        }
    }
}