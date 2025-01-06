namespace ECommerce.Domain.Enums
{
    public enum eEventType
    {
        Unknown = 0,

        OrderCreated,        //Order Events
        OrderUpdated,
        OrderPlaced,
        OrderShipped,
        OrderDelivered,
        OrderCanceled,
        OrderStatusUpdated,
        OrderPaymentMethodUpdated,
        OrderShippingAddressUpdated,
        OrderDeleted,
        OrderItemAddedInOrder,
        OrderItemRemovedFromOrder,

        OrderItemCreated,     //Order Item Evenets
        OrderItemUpdated,
        OrderItemDeleted,
        OrderItemQuantityUpdated,
        OrderItemUnitPriceUpdated,

        CartItemCreated,     //Cart Item Evenets
        CartItemUpdated,
        CartItemDeleted,

        ProductCreated,     //Product Evenets
        ProductUpdated,
        ProductStockIncreased,
        ProductStockDecreased,
        ProductPriceChanged,
        ProductDeleted,

        CategoryCreated,    //Category Evenets
        CategoryUpdated,
        CategoryDeleted,
        ProductAddedInCategory,
        ProductRemovedFromCategory,
        SubCategoryAddedInCategory,
        SubCategoryRemovedFromCategory,

        UserCreated,        //User Events
        UserUpdated,
        UserDeleted,
        UserEmailChanged,
        UserPhoneNumberChanged,
        UserPasswordChanged,
        UserRoleChanged,
        UserAddressChanged,
        UserIsActiveStatusChanged,
        UserIsEmailVerifiedStatusChanged,
        UserIsPhoneNumberVerifiedStatusChanged,
        UserIsSubscribedToNotificationsStatusChanged,

        AddressCreated,     //Address Events
        AddressUpdated,
        AddressDeleted,

        GenderCreated,     //Gender Events
        GenderUpdated,
        GenderDeleted,

        RoleCreated,     //Role Events
        RoleUpdated,
        RoleDeleted
    }
}