using ECommerce.Domain.Enums;
using ECommerce.Domain.ValueObjects;
using System.Web.Providers.Entities;

namespace ECommerce.Domain.Entities
{
    public class Order : Base
    {
        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public DateTime OrderDate { get; private set; }

        public eOrderStatus OrderStatus { get; private set; }

        public Money TotalAmount { get; private set; }

        public string PaymentMethod { get; private set; }

        public Address ShippingAddress { get; private set; }

        public User User { get; private set; }

        public ICollection<OrderItem> OrderItems { get; private set; }

        public Order(Guid userId, DateTime orderDate, eOrderStatus status, Money totalAmount, string paymentMethod, Address shippingAddress)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            OrderDate = orderDate;
            OrderStatus = status;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
            ShippingAddress = shippingAddress;
            OrderItems = new List<OrderItem>();
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            if (OrderStatus == eOrderStatus.Canceled)
                throw new InvalidOperationException("Cannot add items to a canceled order.");

            OrderItems.Add(orderItem);
            UpdateTotalAmount();
            Status_Updated();
        }

        public void RemoveOrderItem(OrderItem orderItem)
        {
            if (OrderStatus == eOrderStatus.Canceled)
                throw new InvalidOperationException("Cannot remove items from a canceled order.");

            OrderItems.Remove(orderItem);
            UpdateTotalAmount();
            Status_Updated();
        }

        public void UpdateOrderStatus(eOrderStatus newStatus)
        {
            if (newStatus == eOrderStatus.Canceled && OrderItems.Any(item => item.Quantity > 0))
                throw new InvalidOperationException("Cannot cancel an order with items that are already in the order.");

            OrderStatus = newStatus;
            Status_Updated();
        }

        public void UpdatePaymentMethod(string newPaymentMethod)
        {
            PaymentMethod = newPaymentMethod;
            Status_Updated();
        }

        public void UpdateShippingAddress(Address newAddress)
        {
            ShippingAddress = newAddress;
            Status_Updated();
        }

        private void UpdateTotalAmount()
        {
            TotalAmount = OrderItems.Aggregate(new Money(0), (total, item) => total.Add(item.UnitPrice));
        }
    }
}