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
            var orderItems = new List<OrderItem>();
            var ordersToStore = new List<Order>();
            var cartItemsToRemove = new List<CartItems>();
            var actualCartItems = new List<CartItems>();
            var orderItemPairs = new HashSet<(Guid OrderId, Guid CartItemId)>();
            var activeOrders = orders?.Where(o => o != Guid.Empty)?.ToList()!;
            var activeCartItems = cartItems?.Where(ci => !ci.IsDeleted)?.ToList()!;
            var billingAddresses = addresses?.Where(a => !a.IsDeleted && a.AdderessType == eAddressType.Billing)?.ToList()!;
            var shippingAddresses = addresses?.Where(a => !a.IsDeleted && a.AdderessType == eAddressType.Shipping)?.ToList()!;

            if (orderItemCount > activeOrders.Count * activeCartItems.Count)
            {
                Console.WriteLine($"Cannot generate {orderItemCount} unique OrderItems — max possible is {activeOrders.Count * activeCartItems.Count}.");
                orderItemCount = activeOrders.Count * activeCartItems.Count;
            }

            for (int i = 0; i < orderItemCount; i++)
            {
                var orderId = activeOrders[random.Next(activeOrders.Count)];
                var cartItem = activeCartItems[random.Next(activeCartItems.Count)];

                if (orderItemPairs.Contains((orderId, cartItem.Id)))
                {
                    i--;
                    continue;
                }

                orderItemPairs.Add((orderId, cartItem.Id));

                if (!ordersToStore.Any(o => o.Id == orderId))
                {
                    var status = Faker.PickRandom<eOrderStatus>();
                    var orderToStore = new Order
                    {
                        Id = orderId,
                        UserId = cartItem.UserId,
                        TotalAmount = cartItem.UnitPrice * cartItem.Quantity,
                        OrderStatus = status,
                        OrderPlacedDate = status == eOrderStatus.Placed ? DateTime.UtcNow : null,
                        OrderShippedDate = status == eOrderStatus.Shipped ? DateTime.UtcNow : null,
                        OrderDeliveredDate = status == eOrderStatus.Delivered ? DateTime.UtcNow : null,
                        OrderCanceledDate = status == eOrderStatus.Canceled ? DateTime.UtcNow : null,
                        BillingAddressId = Faker.PickRandom(billingAddresses).Id,
                        ShippingAddressId = Faker.PickRandom(shippingAddresses).Id,
                        PaymentMethod = Faker.PickRandom(paymentMethods)
                    };

                    ordersToStore.Add(orderToStore);
                }

                var product = products.First(p => p.Id == cartItem.ProductId);
                if (product.Stock < cartItem.Quantity)
                {
                    Console.WriteLine($"Insufficient stock for Product: {product.Name}.");
                    continue;
                }

                product.Stock -= cartItem.Quantity;

                if (!cartItemsToRemove.Contains(cartItem))
                {
                    cartItem.DeletedDate = DateTime.UtcNow;
                    cartItem.IsDeleted = true;
                    cartItemsToRemove.Add(cartItem);
                }

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.UnitPrice
                };

                if (orderItems.Any(x =>
                        x.OrderId == orderItem.OrderId &&
                        x.ProductId == orderItem.ProductId))
                {
                    i--;
                    continue;
                }

                orderItems.Add(orderItem);
            }

            var orderTable = SeederHelper.ToDataTable(ordersToStore);
            DbContextBulkExtensions.BulkInsert(orderTable, "Orders");

            foreach (var cartItemsoRemove in cartItemsToRemove)
            {
                if (actualCartItems.Any(x => x.Id == cartItemsoRemove.Id))
                    continue;

                actualCartItems.Add(cartItemsoRemove);
            }

            foreach (var activeCartItem in activeCartItems)
            {
                if (actualCartItems.Any(x => x.Id == activeCartItem.Id))
                    continue;

                actualCartItems.Add(activeCartItem);
            }

            var cartItemTable = SeederHelper.ToDataTable(actualCartItems);
            DbContextBulkExtensions.BulkInsert(cartItemTable, "CartItems");

            return orderItems;
        }
    }
}