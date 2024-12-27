using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Aggregates
{
    public class OrderAggregate : AggregateRoot<Order>
    {
        public Order Order { get; set; }

        public List<OrderItem>? OrderItems { get; set; }

        public OrderAggregate(Order entity)
             : base(entity)
        {
            Order = entity;
            OrderItems = entity.OrderItems.ToList() ?? new List<OrderItem>();
        }

        public void AddOrderItem(Guid productId, int quantity, Money unitPrice)
        {
            ValidateOrderForModification();

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            if (unitPrice.Amount <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(unitPrice));

            var item = new OrderItem(Order.Id, productId, quantity, unitPrice);

            Order.AddOrderItem(item);
            RaiseDomainEvent();
        }

        public void RemoveOrderItem(Guid orderItemId, Guid productId)
        {
            ValidateOrderForModification();

            var item = OrderItems.Find(o => o.OrderId == Order.Id && o.ProductId == productId && o.Id == orderItemId);
            if (item == null)
                throw new InvalidOperationException("Order item not found.");

            Order.RemoveOrderItem(item);
            RaiseDomainEvent();
        }

        public void UpdateOrderStatus(eOrderStatus newStatus)
        {
            if (!OrderItems.Any())
                throw new InvalidOperationException("Cannot change order status of an order without items.");

            if (Order.OrderStatus == newStatus)
                throw new InvalidOperationException("Order status cannot be same as current status.");

            if (newStatus == eOrderStatus.Canceled && OrderItems.Any(item => item.Quantity > 0))
                throw new InvalidOperationException("Cannot cancel an order with items that are already in the order.");

            switch (newStatus)
            {
                case eOrderStatus.Placed:
                    OrderPlacedEvent();
                    break;

                case eOrderStatus.Shipped:
                    OrderShippedEvent();
                    break;

                case eOrderStatus.Delivered:
                    OrderDeliveredEvent();
                    break;

                case eOrderStatus.Canceled:
                    OrderCanceledEvent();
                    break;
            }
        }

        public void UpdatePaymentMethod(string newPaymentMethod)
        {
            ValidateOrderForModification();
            Order.UpdatePaymentMethod(newPaymentMethod);
            RaiseDomainEvent();
        }

        public void UpdateShippingAddress(Address newAddress)
        {
            ValidateOrderForModification();
            Order.UpdateShippingAddress(newAddress);
            RaiseDomainEvent();
        }

        public void UpdateQuantity(Guid orderItemId, Guid productId, int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));

            var item = OrderItems.Find(o => o.OrderId == Order.Id && o.ProductId == productId && o.Id == orderItemId);
            if (item == null)
                throw new InvalidOperationException("Order item not found.");

            item.UpdateQuantity(newQuantity);
            RaiseDomainEvent();
        }

        public void UpdateUnitPrice(Guid orderItemId, Guid productId, Money newUnitPrice)
        {
            if (newUnitPrice.Amount <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(newUnitPrice));

            var item = OrderItems.Find(o => o.OrderId == Order.Id && o.ProductId == productId && o.Id == orderItemId);
            if (item == null)
                throw new InvalidOperationException("Order item not found.");

            item.UpdateUnitPrice(newUnitPrice);
            RaiseDomainEvent();
        }

        private void ValidateOrderForModification()
        {
            if (Order.OrderStatus == eOrderStatus.Canceled)
                throw new InvalidOperationException("Cannot modify a canceled order.");
        }

        private void OrderPlacedEvent()
        {
            if (Order.OrderStatus != eOrderStatus.Pending)
                throw new InvalidOperationException("Order can only be Placed if it is Pending.");

            Order.UpdateOrderStatus(eOrderStatus.Placed);
        }

        private void OrderShippedEvent()
        {
            if (Order.OrderStatus != eOrderStatus.Placed)
                throw new InvalidOperationException("Order can only be Shipped if it is Placed.");

            Order.UpdateOrderStatus(eOrderStatus.Shipped);
        }

        private void OrderDeliveredEvent()
        {
            if (Order.OrderStatus != eOrderStatus.Shipped)
                throw new InvalidOperationException("Order can only be Delivered if it is Shipped.");

            Order.UpdateOrderStatus(eOrderStatus.Delivered);
        }

        private void OrderCanceledEvent()
        {
            if (Order.OrderStatus == eOrderStatus.Delivered || Order.OrderStatus == eOrderStatus.Canceled)
                throw new InvalidOperationException("Canceled status is not allowed for Delivered or already Canceled orders.");

            Order.UpdateOrderStatus(eOrderStatus.Canceled);
        }

        public void DeleteOrder()
        {
            if (Order.IsDeleted)
                throw new InvalidOperationException("Cannnot delete already deleted Order.");

            Order.DeleteOrder();

            EventType = eEventType.OrderDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new OrderEvent(Order.Id, Order.UserId, Order.TotalAmount.Amount, EventType);
            RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}