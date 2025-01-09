using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class OrderItemAggregate : AggregateRoot<OrderItem>
    {
        private readonly IMediator _mediator;

        public OrderItem OrderItem { get; set; }

        public OrderItemAggregate(OrderItem entity, IMediator mediator)
            : base(entity, mediator)
        {
            OrderItem = entity;
            _mediator = mediator;
        }

        public async Task CreateOrderItem(OrderItem orderItem)
        {
            OrderItem.CreateOrderItem(orderItem.OrderId, orderItem.ProductId, orderItem.Quantity, orderItem.UnitPrice);

            EventType = eEventType.OrderItemCreated;
            await RaiseDomainEvent();
        }

        public async Task UpdateOrderItem(OrderItem orderItem)
        {
            OrderItem.UpdateOrderItem(orderItem.Id, orderItem.OrderId, orderItem.ProductId, orderItem.Quantity, orderItem.UnitPrice);

            EventType = eEventType.OrderItemUpdated;
            await RaiseDomainEvent();
        }

        public async Task UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));

            OrderItem.UpdateQuantity(newQuantity);

            EventType = eEventType.OrderItemQuantityUpdated;
            await RaiseDomainEvent();
        }

        public async Task UpdateUnitPrice(Money newUnitPrice)
        {
            if (newUnitPrice.Amount <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(newUnitPrice));

            OrderItem.UpdateUnitPrice(newUnitPrice);

            EventType = eEventType.OrderItemUnitPriceUpdated;
            await RaiseDomainEvent();
        }

        public async Task DeleteOrderItem()
        {
            EventType = eEventType.OrderItemDeleted;
            await RaiseDomainEvent();
        }

        private async Task RaiseDomainEvent()
        {
            var domainEvent = new OrderItemEvent(OrderItem.Id, OrderItem.OrderId, OrderItem.ProductId, EventType);
            await RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}