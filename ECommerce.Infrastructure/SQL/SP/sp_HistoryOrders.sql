USE [ECommerce]
GO
/****** Object:  StoredProcedure [dbo].[sp_HistoryOrders]    Script Date: 22-04-2025 18:57:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_HistoryOrders]
    @orderId NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO HistoryOrders (
        OrderId,
        UserId,
        UserEmail,
        UserFirstName,
        UserLastName,
        UserPhoneNumber,

        BillingCountry,
        BillingState,
        BillingCity,
        BillingPostalCode,
        BillingAddressLine,
        BillingFirstName,
        BillingLastName,
        BillingPhoneNumber,

        ShippingCountry,
        ShippingState,
        ShippingCity,
        ShippingPostalCode,
        ShippingAddressLine,
        ShippingFirstName,
        ShippingLastName,
        ShippingPhoneNumber,

        OrderStatus,
        OrderPlacedDate,
        OrderShippedDate,
        OrderDeliveredDate,
        OrderCanceledDate,
        TotalAmount,
        PaymentMethod)
    SELECT
        o.Id AS OrderId,
        o.UserId,
        u.Email,
        u.FirstName,
        u.LastName,
        u.PhoneNumber,

        bCountry.Name,
        bState.Name,
        bCity.Name,
        ba.PostalCode,
        ba.AddressLine,
        ba.FirstName,
        ba.LastName,
        ba.PhoneNumber,

        sCountry.Name,
        sCity.Name,
        sState.Name,
        sa.PostalCode,
        sa.AddressLine,
        sa.FirstName,
        sa.LastName,
        sa.PhoneNumber,

        os.StatusName,
        o.OrderPlacedDate,
        o.OrderShippedDate,
        o.OrderDeliveredDate,
        o.OrderCanceledDate,
        o.TotalAmount,
        o.PaymentMethod
    FROM Orders o
    LEFT JOIN Users u ON o.UserId = u.Id
    LEFT JOIN Address ba ON o.BillingAddressId = ba.Id
    LEFT JOIN Cities bCity ON ba.CityId = bCity.Id
    LEFT JOIN States bState ON ba.StateId = bState.Id
    LEFT JOIN Countries bCountry ON ba.CountryId = bCountry.Id
    LEFT JOIN Address sa ON o.ShippingAddressId = sa.Id
    LEFT JOIN Cities sCity ON sa.CityId = sCity.Id
    LEFT JOIN States sState ON sa.StateId = sState.Id
    LEFT JOIN Countries sCountry ON sa.CountryId = sCountry.Id
    LEFT JOIN OrderStatus os ON o.OrderStatus = os.StatusId
    WHERE o.Id = @orderId;
END;
