using Bogus;
using DataSeeder.Entities;
using ECommerce.Infrastructure.Data.Seeders.SeederEntities;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class CityGenerator
    {
        public static Faker Faker { get; set; } = new();
        private static Random random { get; set; } = new();

        public static List<City> Generate(int dataCount, List<State> states)
        {
            var cities = new List<City>();
            var uniqueStates = states?.Where(c => !c.IsDeleted)?.Select(c => c.Id)?.ToList()!;

            for (int i = 0; i < dataCount; i++)
            {
                var city = Faker.Address.State();
                if (cities.Any(x => x.Name == city))
                    continue;

                cities.Add(new City
                {
                    Id = Guid.NewGuid(),
                    Name = city,
                    StateId = uniqueStates[random.Next(uniqueStates.Count)]
                });
            }

            return cities;
        }
    }
}