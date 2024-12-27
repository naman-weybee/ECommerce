namespace ECommerce.Domain.Enums
{
    public enum eEventType
    {
        Unknown = 0,

        OrderPlaced,        //Order Events
        OrderShipped,
        OrderDelivered,
        OrderCanceled,
        OrderStatusUpdated,
        OrderPaymentMethodUpdated,
        OrderShippingAddressUpdated,
        OrderDeleted,

        ProductCreated,     //Product Evenets
        ProductUpdated,
        ProductStockIncreased,
        ProductStockDecreased,
        ProductPriceChanged,
        ProductDeleted,

        CategoryCreated,    //Category Evenets
        CategoryUpdated,
        CategoryDeleted
    }
}