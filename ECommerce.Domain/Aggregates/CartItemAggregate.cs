using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects;
using MediatR;

namespace ECommerce.Domain.Aggregates
{
    public class CartItemAggregate : AggregateRoot<CartItem>
    {
        private readonly IMediator _mediator;

        public CartItem CartItem { get; set; }

        public CartItemAggregate(CartItem entity, IMediator mediator)
            : base(entity, mediator)
        {
            CartItem = entity;
            _mediator = mediator;
        }

        public async Task CreateCartItem(CartItem cartItem)
        {
            CartItem.CreateCartItem(cartItem.UserId, cartItem.ProductId, cartItem.Quantity, cartItem.UnitPrice);

            EventType = eEventType.CartItemCreated;
            await RaiseDomainEvent();
        }

        public async Task UpdateCartItem(CartItem cartItem)
        {
            CartItem.UpdateCartItem(cartItem.Id, cartItem.UserId, cartItem.ProductId, cartItem.Quantity, cartItem.UnitPrice);

            EventType = eEventType.CartItemUpdated;
            await RaiseDomainEvent();
        }

        public async Task UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));

            CartItem.UpdateQuantity(newQuantity);

            EventType = eEventType.OrderItemQuantityUpdated;
            await RaiseDomainEvent();
        }

        public async Task UpdateUnitPrice(Money newUnitPrice)
        {
            if (newUnitPrice.Amount <= 0)
                throw new ArgumentException("Unit price must be greater than zero.", nameof(newUnitPrice));

            CartItem.UpdateUnitPrice(newUnitPrice);

            EventType = eEventType.OrderItemUnitPriceUpdated;
            await RaiseDomainEvent();
        }

        public async Task DeleteCartItem()
        {
            EventType = eEventType.CartItemDeleted;
            await RaiseDomainEvent();
        }

        private async Task RaiseDomainEvent()
        {
            var domainEvent = new CartItemEvent(CartItem.Id, CartItem.UserId, CartItem.ProductId, EventType);
            await RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}