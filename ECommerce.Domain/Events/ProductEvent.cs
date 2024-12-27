using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Events
{
    public class ProductEvent : BaseEvent
    {
        public Guid ProductId { get; }

        public string Name { get; }

        public decimal Price { get; }

        public int Stock { get; }

        public ProductEvent(Guid productId, string name, decimal price, int stock, eEventType eEventType)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty.", nameof(productId));

            ProductId = productId;
            Name = name;
            Price = price;
            Stock = stock;
            EventType = eEventType;
        }
    }
}