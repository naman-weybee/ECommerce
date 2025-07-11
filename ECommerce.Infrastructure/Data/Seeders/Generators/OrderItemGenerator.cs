using Bogus;
using ECommerce.Domain.Enums;
using ECommerce.Infrastructure.Data.Seeders.Helpers;
using ECommerce.Infrastructure.Data.Seeders.SeederEntities;

namespace ECommerce.Infrastructure.Data.Seeders.Generators
{
    public static class OrderItemGenerator
    {
        public static Faker Faker { get; set; } = new();
        private static Random random { get; set; } = new();

        private static readonly string[] paymentMethods = ["CreditCard", "PayPal", "BankTransfer"];

        public static List<OrderItem> Generate(int orderItemCount, List<Guid> orders, List<Addresses> addresses, List<CartItems> cartItems, List<Products> products)
        {
            var orderItems = new HashSet<OrderItem>();
            var ordersToStore = new HashSet<Order>();
            var actualCartItems = new HashSet<CartItems>();
            var orderUserPairs = new HashSet<(Guid OrderId, Guid UserId)>();

            var activeCartItems = cartItems?.Where(ci => !ci.IsDeleted)?.ToList()!;
            var billingAddresses = addresses?.Where(a => !a.IsDeleted && a.AdderessType == eAddressType.Billing)?.ToList()!;
            var shippingAddresses = addresses?.Where(a => !a.IsDeleted && a.AdderessType == eAddressType.Shipping)?.ToList()!;

            if (orderItemCount > orders.Count * activeCartItems.Count)
            {
                Console.WriteLine($"Cannot generate {orderItemCount} unique OrderItems — max possible is {orders.Count * activeCartItems.Count}.");
                orderItemCount = orders.Count * activeCartItems.Count;
            }

            foreach (var orderId in orders!)
            {
                var cartUserId = activeCartItems[random.Next(activeCartItems.Count)].UserId;
                orderUserPairs.Add((orderId, cartUserId));
            }

            foreach (var orderUser in orderUserPairs)
            {
                var orderId = orderUser.OrderId;
                var userId = orderUser.UserId;

                var userCartItems = activeCartItems.Where(ci => ci.UserId == userId).ToList();
                foreach (var userCart in userCartItems)
                {
                    var orderItem = new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        ProductId = userCart.ProductId,
                        Quantity = userCart.Quantity,
                        UnitPrice = userCart.UnitPrice
                    };

                    userCart.IsDeleted = true;
                    userCart.DeletedDate = DateTime.UtcNow;
                    actualCartItems.Add(userCart);
                    orderItems.Add(orderItem);
                }

                var status = Faker.PickRandom<eOrderStatus>();

                var now = DateTime.UtcNow;
                DateTime? placedDate = null;
                DateTime? shippedDate = null;
                DateTime? deliveredDate = null;
                DateTime? canceledDate = null;

                switch (status)
                {
                    case eOrderStatus.Placed:
                        placedDate = now;
                        break;

                    case eOrderStatus.Shipped:
                        placedDate = now.AddDays(-2);
                        shippedDate = now;
                        break;

                    case eOrderStatus.Delivered:
                        placedDate = now.AddDays(-5);
                        shippedDate = now.AddDays(-3);
                        deliveredDate = now;
                        break;

                    case eOrderStatus.Canceled:
                        placedDate = now.AddDays(-1);
                        canceledDate = now;
                        break;
                }

                var orderToStore = new Order
                {
                    Id = orderId,
                    UserId = userId,
                    TotalAmount = orderItems
                        .Where(x => x.OrderId == orderId)
                        .Sum(x => x.UnitPrice * x.Quantity),
                    OrderStatus = status,
                    OrderPlacedDate = placedDate,
                    OrderShippedDate = shippedDate,
                    OrderDeliveredDate = deliveredDate,
                    OrderCanceledDate = canceledDate,
                    BillingAddressId = Faker.PickRandom(billingAddresses).Id,
                    ShippingAddressId = Faker.PickRandom(shippingAddresses).Id,
                    PaymentMethod = Faker.PickRandom(paymentMethods)
                };


                ordersToStore.Add(orderToStore);
            }

            foreach (var cartItem in activeCartItems)
            {
                if (actualCartItems.Any(x => x.Id == cartItem.Id))
                    continue;

                actualCartItems.Add(cartItem);
            }

            var orderTable = SeederHelper.ToDataTable(ordersToStore?.ToList()!);
            DbContextBulkExtensions.BulkInsert(orderTable, "Orders");

            var cartItemTable = SeederHelper.ToDataTable(actualCartItems?.ToList()!);
            DbContextBulkExtensions.BulkInsert(cartItemTable, "CartItems");

            return orderItems?.ToList()!;
        }
    }
}