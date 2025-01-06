using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Aggregates
{
    public class OrderItemAggregate : AggregateRoot<OrderItem>
    {
        public OrderItem OrderItem { get; set; }

        public OrderItemAggregate(OrderItem entity)
            : base(entity)
        {
            OrderItem = entity;
        }

        public void CreateOrderItem(OrderItem orderItem)
        {
            OrderItem.CreateOrderItem(orderItem.OrderId, orderItem.ProductId, orderItem.Quantity, orderItem.UnitPrice);

            EventType = eEventType.OrderItemCreated;
            RaiseDomainEvent();
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            OrderItem.UpdateOrderItem(orderItem.Id, orderItem.OrderId, orderItem.ProductId, orderItem.Quantity, orderItem.UnitPrice);

            EventType = eEventType.OrderItemUpdated;
            RaiseDomainEvent();
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));

            OrderItem.UpdateQuantity(newQuantity);

            EventType = eEventType.OrderItemQuantityUpdated;
            RaiseDomainEvent();
        }

        public void UpdateUnitPrice(Money newUnitPrice)
        {
            if (newUnitPrice.Amount <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(newUnitPrice));

            OrderItem.UpdateUnitPrice(newUnitPrice);

            EventType = eEventType.OrderItemUnitPriceUpdated;
            RaiseDomainEvent();
        }

        public void DeleteOrderItem()
        {
            EventType = eEventType.OrderItemDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new OrderItemEvent(OrderItem.Id, OrderItem.OrderId, OrderItem.ProductId, EventType);
            RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}