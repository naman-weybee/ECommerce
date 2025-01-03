using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;

namespace ECommerce.Domain.Aggregates
{
    public class CartItemAggregate : AggregateRoot<CartItem>
    {
        public CartItem CartItem { get; private set; }

        public CartItemAggregate(CartItem entity)
            : base(entity)
        {
            CartItem = entity;
        }

        public void CreateCartItem(CartItem cartItem)
        {
            CartItem.CreateCartItem(cartItem.UserId, cartItem.ProductId, cartItem.Quantity, cartItem.UnitPrice);

            EventType = eEventType.CartItemCreated;
            RaiseDomainEvent();
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            CartItem.UpdateCartItem(cartItem.Id, cartItem.UserId, cartItem.ProductId, cartItem.Quantity, cartItem.UnitPrice);

            EventType = eEventType.CartItemUpdated;
            RaiseDomainEvent();
        }

        public void DeleteCartItem()
        {
            EventType = eEventType.CartItemDeleted;
            RaiseDomainEvent();
        }

        private void RaiseDomainEvent()
        {
            var domainEvent = new CartItemEvent(CartItem.Id, CartItem.UserId, CartItem.ProductId, EventType);
            RaiseDomainEvent(domainEvent);
        }

        public new void ClearDomainEvents()
        {
            base.ClearDomainEvents();
        }
    }
}