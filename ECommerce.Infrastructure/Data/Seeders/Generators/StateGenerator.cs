using Bogus;
using ECommerce.Infrastructure.Data.Seeders.SeederEntities;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class StateGenerator
    {
        public static Faker Faker { get; set; } = new();
        public static Random random { get; set; } = new();

        public static List<State> Generate(int dataCount, List<Country> countries)
        {
            var states = new List<State>();
            var activeCountries = countries?.Where(c => !c.IsDeleted)?.Select(c => c.Id)?.ToList()!;

            for (int i = 0; i < dataCount; i++)
            {
                var state = Faker.Address.State();
                if (states.Any(x => x.Name == state))
                    continue;

                states.Add(new State
                {
                    Id = Guid.NewGuid(),
                    Name = state,
                    CountryId = activeCountries[random.Next(activeCountries.Count)]
                });
            }

            return states;
        }
    }
}