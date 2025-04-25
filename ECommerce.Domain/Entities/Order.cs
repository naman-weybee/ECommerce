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

        [ForeignKey("BillingAddress")]
        public Guid BillingAddressId { get; set; }

        [ForeignKey("ShippingAddress")]
        public Guid ShippingAddressId { get; set; }

        public eOrderStatus OrderStatus { get; set; } = eOrderStatus.Placed;

        public DateTime? OrderPlacedDate { get; set; } = DateTime.UtcNow;

        public DateTime? OrderShippedDate { get; set; }

        public DateTime? OrderDeliveredDate { get; set; }

        public DateTime? OrderCanceledDate { get; set; }

        public Money TotalAmount { get; set; }

        public string PaymentMethod { get; set; }

        public virtual Address BillingAddress { get; set; }

        public virtual Address ShippingAddress { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public void CreateOrder(Guid id, Guid userId, Guid billingAddressId, Guid shippingAddressId, eOrderStatus status, Money totalAmount, string paymentMethod, ICollection<OrderItem> orderItems)
        {
            Id = id;
            UserId = userId;
            BillingAddressId = billingAddressId;
            ShippingAddressId = shippingAddressId;
            OrderStatus = status;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
            OrderItems = orderItems ?? [];
        }

        public void UpdateOrder(Guid id, Guid userId, Guid billingAddressId, Guid shippingAddressId, eOrderStatus status, Money totalAmount, string paymentMethod, ICollection<OrderItem> orderItems)
        {
            Id = id;
            UserId = userId;
            BillingAddressId = billingAddressId;
            ShippingAddressId = shippingAddressId;
            OrderStatus = status;
            TotalAmount = totalAmount;
            PaymentMethod = paymentMethod;
            OrderItems = orderItems ?? [];

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

        public void UpdateOrderBillingAddress(Guid billingAddressId)
        {
            BillingAddressId = billingAddressId;
            StatusUpdated();
        }

        public void UpdateOrderShippingAddress(Guid shippingAddressId)
        {
            ShippingAddressId = shippingAddressId;
            StatusUpdated();
        }

        public void UpdateTotalAmount()
        {
            TotalAmount = OrderItems.Aggregate(new Money(0), (total, item) => total.Add(item.UnitPrice));
        }
    }
}