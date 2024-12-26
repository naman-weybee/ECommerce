using MediatR;

namespace ECommerce.Domain.Events
{
    public class ProductEvent : INotification
    {
        public Guid ProductId { get; }

        public string Name { get; }

        public decimal Price { get; }

        public int Stock { get; }

        public ProductEvent(Guid productId, string name, decimal price, int stock)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}