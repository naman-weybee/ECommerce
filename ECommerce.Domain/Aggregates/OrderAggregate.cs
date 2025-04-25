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
            Entity.OrderItems ??= [];
            _eventCollector = eventCollector;
        }

        public void CreateOrder()
        {
            Entity.CreateOrder(Entity.Id, Entity.UserId, Entity.BillingAddressId, Entity.ShippingAddressId, Entity.OrderStatus, Entity.TotalAmount, Entity.PaymentMethod, Entity.OrderItems);

            EventType = eEventType.OrderPlaced;
            RaiseDomainEvent();
        }

        public void UpdateOrder()
        {
            Entity.UpdateOrder(Entity.Id, Entity.UserId, Entity.BillingAddressId, Entity.ShippingAddressId, Entity.OrderStatus, Entity.TotalAmount, Entity.PaymentMethod, Entity.OrderItems);

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

            Entity.AddOrderItem(item);

            EventType = eEventType.OrderItemAddedInOrder;
            RaiseDomainEvent();
        }

        public void RemoveOrderItem(Guid orderItemId)
        {
            ValidateOrderForModification();

            var item = Entity.OrderItems.FirstOrDefault(o => o.Id == orderItemId)
                ?? throw new InvalidOperationException("Order item not found.");

            Entity.RemoveOrderItem(item);

            EventType = eEventType.OrderItemRemovedFromOrder;
            RaiseDomainEvent();
        }

        public void UpdateOrderStatus(eOrderStatus newStatus)
        {
            if (!Entity.OrderItems.Any())
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
            Entity.UpdatePaymentMethod(newPaymentMethod);

            EventType = eEventType.OrderPaymentMethodUpdated;
            RaiseDomainEvent();
        }

        public void UpdateOrderBillingAddress(Guid billingAddressId)
        {
            ValidateOrderForModification();
            Entity.UpdateOrderBillingAddress(billingAddressId);

            EventType = eEventType.OrderBillingAddressUpdated;
            RaiseDomainEvent();
        }

        public void UpdateOrderShippingAddress(Guid shippingAddressId)
        {
            ValidateOrderForModification();
            Entity.UpdateOrderShippingAddress(shippingAddressId);

            EventType = eEventType.OrderShippingAddressUpdated;
            RaiseDomainEvent();
        }

        public void UpdateTotalAmount()
        {
            ValidateOrderForModification();

            Entity.UpdateTotalAmount();

            EventType = eEventType.OrderTotalAmountUpdated;
            RaiseDomainEvent();
        }

        private void ValidateOrderForModification()
        {
            if (Entity.OrderStatus == eOrderStatus.Canceled)
                throw new InvalidOperationException("Cannot modify a canceled Entity.");
        }

        private void OrderPlacedEvent()
        {
            if (Entity.OrderStatus != eOrderStatus.Pending)
                throw new InvalidOperationException("Order can only be Placed if it is Pending.");

            EventType = eEventType.OrderPlaced;
            Entity.UpdateOrderStatus(eOrderStatus.Placed);
        }

        private void OrderShippedEvent()
        {
            if (Entity.OrderStatus != eOrderStatus.Placed)
                throw new InvalidOperationException("Order can only be Shipped if it is Placed.");

            EventType = eEventType.OrderShipped;
            Entity.UpdateOrderStatus(eOrderStatus.Shipped);
        }

        private void OrderDeliveredEvent()
        {
            if (Entity.OrderStatus != eOrderStatus.Shipped)
                throw new InvalidOperationException("Order can only be Delivered if it is Shipped.");

            EventType = eEventType.OrderDelivered;
            Entity.UpdateOrderStatus(eOrderStatus.Delivered);
        }

        private void OrderCanceledEvent()
        {
            if (Entity.OrderStatus == eOrderStatus.Delivered || Entity.OrderStatus == eOrderStatus.Canceled)
                throw new InvalidOperationException("Canceled status is not allowed for Delivered or already Canceled orders.");

            EventType = eEventType.OrderCanceled;
            Entity.UpdateOrderStatus(eOrderStatus.Canceled);
        }

        public void DeleteOrder()
        {
            EventType = eEventType.OrderDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new OrderEvent(Entity.Id, Entity.UserId, Entity.BillingAddressId, Entity.ShippingAddressId, Entity.TotalAmount.Amount, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}