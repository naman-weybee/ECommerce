using Bogus;
using ECommerce.Infrastructure.Data.Seeders.SeederEntities;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class CartItemGenerator
    {
        public static Faker Faker { get; set; } = new();
        private static Random random { get; set; } = new();

        public static List<CartItems> Generate(int cartItemCount, List<User> users, List<Products> products)
        {
            var cartItems = new List<CartItems>();
            var userProductPairs = new HashSet<(Guid UserId, Guid ProductId)>();

            var activeUsers = users?.Where(c => !c.IsDeleted)?.Select(c => c.Id)?.ToList()!;
            var activeProducts = products?.Where(c => !c.IsDeleted)?.Select(c => c.Id)?.ToList()!;

            for (int i = 0; i < cartItemCount; i++)
            {
                var userId = activeUsers[random.Next(activeUsers.Count)];
                var productId = activeProducts[random.Next(activeProducts.Count)];

                if (userProductPairs.Contains((userId, productId)))
                    continue;

                userProductPairs.Add((userId, productId));

                var quantity = Faker.Random.Int(1, 10);
                var productPrice = products!.Where(x => x.Id == productId).Select(x => x.Price).First();
                cartItems.Add(new CartItems
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = productPrice * quantity
                });
            }

            return cartItems;
        }
    }
}