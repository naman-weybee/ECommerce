USE [ECommerce]
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateOrderItemHistory]    Script Date: 22-04-2025 18:57:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_CreateOrderItemHistory]
    @orderItemId NVARCHAR(MAX),
    @operationType NVARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO HistoryOrderItem (
        HistoryOrderId,
        OrderId,
        OrderItemId,
        ProductId,
        ProductName,
        SKU,
        Brand,
        Quantity,
        UnitPrice,
        Currency,
        CategoryId,
        CategoryName,
        OperationType)
    SELECT
        ho.Id,
        oi.OrderId,
        oi.Id,
        p.Id,
        p.Name,
        p.SKU,
        p.Brand,
        oi.Quantity,
        oi.UnitPrice,
        p.Currency,
        p.CategoryId,
        c.Name,
        @operationType
    FROM OrderItems oi
    INNER JOIN HistoryOrder ho ON oi.OrderId = ho.OrderId
    INNER JOIN Products p ON oi.ProductId = p.Id
    LEFT JOIN Categories c ON p.CategoryId = c.Id
    WHERE oi.Id = @orderItemId;
END;
