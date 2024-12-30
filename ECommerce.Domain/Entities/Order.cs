using ECommerce.Domain.Enums;
using ECommerce.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class Order : Base
    {
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Address")]
        public Guid AddressId { get; set; }

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

        public Order(Guid userId, Guid addressId, eOrderStatus status, Money totalAmount, string paymentMethod)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            AddressId = addressId;
            OrderStatus = status;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
            OrderItems = new List<OrderItem>();
        }

        public void CreateOrder(Guid userId, Guid addressId, eOrderStatus status, Money totalAmount, string paymentMethod)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            AddressId = addressId;
            OrderStatus = status;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
            OrderItems = new List<OrderItem>();
        }

        public void UpdateOrder(Guid id, Guid userId, Guid addressId, eOrderStatus status, Money totalAmount, string paymentMethod)
        {
            Id = id;
            UserId = userId;
            AddressId = addressId;
            OrderStatus = status;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
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

        public void UpdateShippingAddress(Guid addressId)
        {
            AddressId = addressId;
            StatusUpdated();
        }

        private void UpdateTotalAmount()
        {
            TotalAmount = OrderItems.Aggregate(new Money(0), (total, item) => total.Add(item.UnitPrice));
        }
    }
}