using ECommerce.Domain.Enums;
using ECommerce.Domain.ValueObjects;
using System.Web.Providers.Entities;

namespace ECommerce.Domain.Entities
{
    public class Order : Base
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public eOrderStatus OrderStatus { get; set; } = eOrderStatus.Pending;

        public DateTime? OrderPlacedDate { get; set; }

        public DateTime? OrderShippedDate { get; set; }

        public DateTime? OrderDeliveredDate { get; set; }

        public DateTime? OrderCanceledDate { get; set; }

        public Money TotalAmount { get; set; }

        public string PaymentMethod { get; set; }

        public Address ShippingAddress { get; set; }

        public User User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public Order(Guid userId, eOrderStatus status, Money totalAmount, string paymentMethod, Address shippingAddress)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            OrderStatus = status;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
            ShippingAddress = shippingAddress;
            OrderItems = new List<OrderItem>();
        }

        public void CreateOrder(Guid userId, eOrderStatus status, Money totalAmount, string paymentMethod, Address shippingAddress)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            OrderStatus = status;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
            ShippingAddress = shippingAddress;
            OrderItems = new List<OrderItem>();
        }

        public void UpdateOrder(Guid userId, eOrderStatus status, Money totalAmount, string paymentMethod, Address shippingAddress)
        {
            UserId = userId;
            OrderStatus = status;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
            ShippingAddress = shippingAddress;
            OrderItems = new List<OrderItem>();

            StatusUpdated();
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            OrderItems.Add(orderItem);
            UpdateTotalAmount();
            StatusUpdated();
        }

        public void RemoveOrderItem(OrderItem orderItem)
        {
            OrderItems.Remove(orderItem);
            UpdateTotalAmount();
            StatusUpdated();
        }

        public void UpdateOrderStatus(eOrderStatus newStatus)
        {
            OrderStatus = newStatus;

            UpdateOrderStatusDate();
            StatusUpdated();
        }

        public void UpdateOrderStatusDate()
        {
            switch (OrderStatus)
            {
                case eOrderStatus.Placed:
                    OrderPlacedDate = DateTime.UtcNow;
                    break;

                case eOrderStatus.Shipped:
                    OrderShippedDate = DateTime.UtcNow;
                    break;

                case eOrderStatus.Delivered:
                    OrderDeliveredDate = DateTime.UtcNow;
                    break;

                case eOrderStatus.Canceled:
                    OrderCanceledDate = DateTime.UtcNow;
                    break;
            }
        }

        public void UpdatePaymentMethod(string newPaymentMethod)
        {
            PaymentMethod = newPaymentMethod;
            StatusUpdated();
        }

        public void UpdateShippingAddress(Address newAddress)
        {
            ShippingAddress = newAddress;
            StatusUpdated();
        }

        private void UpdateTotalAmount()
        {
            TotalAmount = OrderItems.Aggregate(new Money(0), (total, item) => total.Add(item.UnitPrice));
        }

        public void DeleteOrder()
        {
            StatusDeleted();
        }
    }
}