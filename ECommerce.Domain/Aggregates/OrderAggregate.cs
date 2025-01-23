using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class OrderAggregate : AggregateRoot<Order>
    {
        private readonly IDomainEventCollector _eventCollector;

        public Order Order { get; set; }

        public OrderAggregate(Order entity, IDomainEventCollector eventCollector)
             : base(entity, eventCollector)
        {
            Order = entity;
            Order.OrderItems ??= new List<OrderItem>();
            _eventCollector = eventCollector;
        }

        public void CreateOrder(Order order)
        {
            Order.CreateOrder(order.Id, order.UserId, order.AddressId, order.OrderStatus, order.TotalAmount, order.PaymentMethod, order.OrderItems);

            EventType = eEventType.OrderPlaced;
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

            RaiseDomainEvent();
        }

        public void UpdatePaymentMethod(string newPaymentMethod)
        {
            ValidateOrderForModification();
            Order.UpdatePaymentMethod(newPaymentMethod);

            EventType = eEventType.OrderPaymentMethodUpdated;
            RaiseDomainEvent();
        }

        public void UpdateOrderAddress(Guid addressId)
        {
            ValidateOrderForModification();
            Order.UpdateOrderAddress(addressId);

            EventType = eEventType.OrderAddressUpdated;
            RaiseDomainEvent();
        }

        public void UpdateTotalAmount()
        {
            ValidateOrderForModification();

            Order.UpdateTotalAmount();

            EventType = eEventType.OrderTotalAmountUpdated;
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

            EventType = eEventType.OrderPlaced;
            Order.UpdateOrderStatus(eOrderStatus.Placed);
        }

        private void OrderShippedEvent()
        {
            if (Order.OrderStatus != eOrderStatus.Placed)
                throw new InvalidOperationException("Order can only be Shipped if it is Placed.");

            EventType = eEventType.OrderShipped;
            Order.UpdateOrderStatus(eOrderStatus.Shipped);
        }

        private void OrderDeliveredEvent()
        {
            if (Order.OrderStatus != eOrderStatus.Shipped)
                throw new InvalidOperationException("Order can only be Delivered if it is Shipped.");

            EventType = eEventType.OrderDelivered;
            Order.UpdateOrderStatus(eOrderStatus.Delivered);
        }

        private void OrderCanceledEvent()
        {
            if (Order.OrderStatus == eOrderStatus.Delivered || Order.OrderStatus == eOrderStatus.Canceled)
                throw new InvalidOperationException("Canceled status is not allowed for Delivered or already Canceled orders.");

            EventType = eEventType.OrderCanceled;
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
    }
}