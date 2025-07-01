using Bogus;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data.Seeders.SeederEntities;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class AddressGenerator
    {
        public static Faker Faker { get; set; } = new();
        private static Random random { get; set; } = new();

        public static List<Addresses> Generate(int AddressesCount, List<User> users, List<Country> countries, List<State> states, List<City> cities)
        {
            var Addresses = new List<Addresses>();
            var activeUsers = users?.Where(c => !c.IsDeleted)?.Select(c => c.Id)?.ToList()!;
            var activeCountries = countries?.Where(c => !c.IsDeleted)?.Select(c => c.Id)?.ToList()!;
            var activeStates = states?.Where(c => !c.IsDeleted)?.Select(c => c.Id)?.ToList()!;
            var activeCities = cities?.Where(c => !c.IsDeleted)?.Select(c => c.Id)?.ToList()!;

            for (int i = 0; i < AddressesCount; i++)
            {
                Addresses.Add(new Addresses()
                {
                    Id = Guid.NewGuid(),
                    FirstName = Faker.Name.FirstName(),
                    LastName = Faker.Name.LastName(),
                    UserId = activeUsers[random.Next(activeUsers.Count)],
                    CountryId = activeCountries[random.Next(activeCountries.Count)],
                    StateId = activeStates[random.Next(activeStates.Count)],
                    CityId = activeCities[random.Next(activeCities.Count)],
                    PostalCode = Faker.Address.ZipCode(),
                    AdderessType = Faker.PickRandom<eAddressType>(),
                    AddressLine = Faker.Address.StreetAddress(),
                    PhoneNumber = Faker.Phone.PhoneNumber()
                });
            }

            return Addresses;
        }
    }
}