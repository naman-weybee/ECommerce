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
        AddressDeleted
    }
}