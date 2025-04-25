using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects;
using ECommerce.Infrastructure.Services;

namespace ECommerce.Domain.Aggregates
{
    public class OrderItemAggregate : AggregateRoot<OrderItem>
    {
        private readonly IDomainEventCollector _eventCollector;

        public OrderItem OrderItem { get; set; }

        public OrderItemAggregate(OrderItem entity, IDomainEventCollector eventCollector)
            : base(entity, eventCollector)
        {
            OrderItem = entity;
            _eventCollector = eventCollector;
        }

        public void CreateOrderItem()
        {
            Entity.CreateOrderItem(Entity.OrderId, Entity.ProductId, Entity.Quantity, Entity.UnitPrice);

            EventType = eEventType.OrderItemCreated;
            RaiseDomainEvent();
        }

        public void UpdateOrderItem(Money unitPrice)
        {
            Entity.UpdateOrderItem(Entity.Id, Entity.OrderId, Entity.ProductId, Entity.Quantity, unitPrice);

            EventType = eEventType.OrderItemUpdated;
            RaiseDomainEvent();
        }

        public void UpdateQuantity(int newQuantity, Money newProductPrice)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));

            Entity.UpdateQuantity(newQuantity, newProductPrice);

            EventType = eEventType.OrderItemQuantityUpdated;
            RaiseDomainEvent();
        }

        public void UpdateUnitPrice(Money newUnitPrice)
        {
            if (newUnitPrice.Amount <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(newUnitPrice));

            Entity.UpdateUnitPrice(newUnitPrice);

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
            var domainEvent = new OrderItemEvent(Entity.Id, Entity.OrderId, Entity.ProductId, EventType);
            RaiseDomainEvent(domainEvent);
        }
    }
}