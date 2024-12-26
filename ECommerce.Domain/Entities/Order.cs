﻿using ECommerce.Domain.Enums;
using ECommerce.Domain.ValueObjects;
using System.Web.Providers.Entities;

namespace ECommerce.Domain.Entities
{
    public class Order : Base
    {
        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public eOrderStatus OrderStatus { get; private set; } = eOrderStatus.Pending;

        public DateTime? OrderPlacedDate { get; private set; }

        public DateTime? OrderShippedDate { get; private set; }

        public DateTime? OrderDeliveredDate { get; private set; }

        public DateTime? OrderCanceledDate { get; private set; }

        public Money TotalAmount { get; private set; }

        public string PaymentMethod { get; private set; }

        public Address ShippingAddress { get; private set; }

        public User User { get; private set; }

        public ICollection<OrderItem> OrderItems { get; private set; }

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
    }
}