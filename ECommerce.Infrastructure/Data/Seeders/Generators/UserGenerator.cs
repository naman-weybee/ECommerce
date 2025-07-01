using Bogus;
using ECommerce.Infrastructure.Data.Seeders.SeederEntities;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class UserGenerator
    {
        public static Faker Faker { get; set; } = new();
        public static Random random { get; set; } = new();

        public static List<User> Generate(int dataCount, List<Role> roles, List<Gender> genders)
        {
            var users = new List<User>();
            var roleIds = roles?.Where(c => !c.IsDeleted)?.Select(c => c.Id)?.ToList()!;
            var genderIds = genders?.Select(c => c.Id)?.ToList()!;

            for (int i = 0; i < dataCount; i++)
            {
                var email = Faker.Internet.Email();
                if (users.Any(x => x.Email == email))
                    continue;

                var isEmailVerified = Faker.Random.Bool(0.9f);
                users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = Faker.Name.FirstName(),
                    LastName = Faker.Name.LastName(),
                    Password = "99fb7c0e0fd1601782e5e154a6d46c38",
                    Email = email,
                    PhoneNumber = Faker.Phone.PhoneNumber(),
                    RoleId = roleIds[random.Next(roleIds.Count)],
                    DateOfBirth = Faker.Date.Past(30, DateTime.Now.AddYears(-18)),
                    GenderId = genderIds[random.Next(genderIds.Count)],
                    IsActive = Faker.Random.Bool(0.9f),
                    IsEmailVerified = isEmailVerified,
                    EmailVerificationToken = isEmailVerified ? null : Faker.Random.AlphaNumeric(20),
                    IsPhoneNumberVerified = Faker.Random.Bool(0.8f),
                    IsSubscribedToNotifications = Faker.Random.Bool(0.5f)
                });
            }

            return users;
        }
    }
}