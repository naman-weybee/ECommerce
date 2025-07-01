using Bogus;
using ECommerce.Infrastructure.Data.Seeders.SeederEntities;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class CountryGenerator
    {
        public static Faker Faker { get; set; } = new();

        public static List<Country> Generate(int dataCount)
        {
            var countries = new List<Country>();
            for (int i = 0; i < dataCount; i++)
            {
                var country = Faker.Address.Country();
                if (countries.Any(x => x.Name == country))
                {
                    dataCount++;
                    continue;
                }

                countries.Add(new Country
                {
                    Id = Guid.NewGuid(),
                    Name = country,
                });
            }

            return countries;
        }
    }
}