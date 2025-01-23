using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Entities
{
    public class OrderItem : Base
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public Money UnitPrice { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        public void CreateOrderItem(Guid orderId, Guid productId, int quantity, Money unitPrice)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void UpdateOrderItem(Guid id, Guid orderId, Guid productId, int quantity, Money unitPrice)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public Money TotalAmount => UnitPrice.Multiply(Quantity);

        public void UpdateQuantity(int newQuantity, Money newProductPrice)
        {
            Quantity = newQuantity;
            UnitPrice = new Money(newQuantity * newProductPrice.Amount);

            StatusUpdated();
        }

        public void UpdateUnitPrice(Money newUnitPrice)
        {
            UnitPrice = newUnitPrice;
            StatusUpdated();
        }
    }
}