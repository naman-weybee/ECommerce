namespace ECommerce.Domain.Enums
{
    public enum eEventType
    {
        Unknown = 0,

        OrderPlaced,        //Order Events
        OrderUpdated,
        OrderShipped,
        OrderDelivered,
        OrderCanceled,
        OrderStatusUpdated,
        OrderPaymentMethodUpdated,
        OrderAddressUpdated,
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
        ProductStockDepleted,
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
        UserEmailVarified,
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
        RoleDeleted,

        RefreshTokenCreated,     //Refresh Token Events
        RefreshTokenUpdated,
        RefreshTokenDeleted,
        RefreshTokenRevoked,
        RefreshTokenRegenerated,

        CountryCreated,     //Country Events
        CountryUpdated,
        CountryDeleted,

        StateCreated,       //State Events
        StateUpdated,
        StateDeleted,

        CityCreated,        //City Events
        CityUpdated,
        CityDeleted,

        SetAsDefaultAddress,    //Address Type Events
        SetAsBillingAddress,
        SetAsShippingAddress,
    }
}