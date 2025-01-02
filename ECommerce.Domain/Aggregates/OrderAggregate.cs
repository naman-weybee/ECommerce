using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Aggregates
{
    public class OrderAggregate : AggregateRoot<Order>
    {
        public Order Order { get; set; }

        public OrderAggregate(Order entity)
             : base(entity)
        {
            Order = entity;
            Order.OrderItems ??= new List<OrderItem>();
        }

        public void CreateOrder(Order order)
        {
            Order.CreateOrder(order.UserId, order.AddressId, order.OrderStatus, order.TotalAmount, order.PaymentMethod, order.OrderItems);

            EventType = eEventType.OrderCreated;
            RaiseDomainEvent();
        }

        public void UpdateOrder(Order order)
        {
            Order.UpdateOrder(order.Id, order.UserId, order.AddressId, order.OrderStatus, order.TotalAmount, order.PaymentMethod, order.OrderItems);

            EventType = eEventType.OrderUpdated;
            RaiseDomainEvent();
        }

        public void AddOrderItem(OrderItem item)
        {
            ValidateOrderForModification();

            if (item.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(item.Quantity));

            if (item.UnitPrice.Amount <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(item.UnitPrice.Amount));

            Order.AddOrderItem(item);

            EventType = eEventType.OrderItemAddedInOrder;
            RaiseDomainEvent();
        }

        public void RemoveOrderItem(Guid orderItemId)
        {
            ValidateOrderForModification();

            var item = Order.OrderItems.FirstOrDefault(o => o.Id == orderItemId)
                ?? throw new InvalidOperationException("Order item not found.");

            Order.RemoveOrderItem(item);

            EventType = eEventType.OrderItemRemovedFromOrder;
            RaiseDomainEvent();
        }

        public void UpdateOrderStatus(eOrderStatus newStatus)
        {
            if (!Order.OrderItems.Any())
                throw new InvalidOperationException("Cannot change order status of an order without items.");

            if (Order.OrderStatus == newStatus)
                throw new InvalidOperationException("Order status cannot be same as current status.");

            if (newStatus == eOrderStatus.Canceled && Order.OrderItems.Any(item => item.Quantity > 0))
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

        public void UpdateShippingAddress(Guid addressId)
        {
            ValidateOrderForModification();
            Order.UpdateShippingAddress(addressId);
            RaiseDomainEvent();
        }

        public void UpdateQuantity(Guid orderItemId, Guid productId, int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));

            var item = Order.OrderItems.FirstOrDefault(o => o.ProductId == productId && o.Id == orderItemId)
                ?? throw new InvalidOperationException("Order item not found.");

            item.UpdateQuantity(newQuantity);

            EventType = eEventType.OrderUpdated;
            RaiseDomainEvent();
        }

        public void UpdateUnitPrice(Guid orderItemId, Guid productId, Money newUnitPrice)
        {
            if (newUnitPrice.Amount <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(newUnitPrice));

            var item = Order.OrderItems.FirstOrDefault(o => o.ProductId == productId && o.Id == orderItemId)
                ?? throw new InvalidOperationException("Order item not found.");

            item.UpdateUnitPrice(newUnitPrice);

            EventType = eEventType.OrderUpdated;
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