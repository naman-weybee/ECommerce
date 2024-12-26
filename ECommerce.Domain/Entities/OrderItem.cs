using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Entities
{
    public class OrderItem : Base
    {
        public Guid Id { get;set;}

        public Guid OrderId { get;set;}

        public Guid ProductId { get;set;}

        public int Quantity { get;set;}

        public Money UnitPrice { get;set;}

        public Order Order { get;set;}

        public Product Product { get;set;}

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
            Quantity = newQuantity;
            StatusUpdated();
        }

        public void UpdateUnitPrice(Money newUnitPrice)
        {
            UnitPrice = newUnitPrice;
            StatusUpdated();
        }
    }
}