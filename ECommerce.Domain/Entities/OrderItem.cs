using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Entities
{
    public class OrderItem : Base
    {
        public Guid Id { get; private set; }

        public Guid OrderId { get; private set; }

        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }

        public Money UnitPrice { get; private set; }

        public Order Order { get; private set; }

        public Product Product { get; private set; }

        public OrderItem(Guid orderId, Guid productId, int quantity, Money unitPrice)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public Money TotalAmount => UnitPrice.Multiply(Quantity);

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 0)
                throw new InvalidOperationException("Quantity cannot be negative.");

            Quantity = newQuantity;
            Status_Updated();
        }

        public void UpdateUnitPrice(Money newUnitPrice)
        {
            UnitPrice = newUnitPrice;
            Status_Updated();
        }
    }
}