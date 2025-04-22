CREATE TRIGGER [dbo].[trg_HistoryOrder]
ON [dbo].[Orders]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @orderId NVARCHAR(MAX);
    DECLARE @opType NVARCHAR(10);

    IF EXISTS (SELECT 1 FROM inserted) AND NOT EXISTS (SELECT 1 FROM deleted)
    BEGIN
        SET @opType = 'INSERT';
        DECLARE insert_cursor CURSOR FOR SELECT Id FROM inserted;
        OPEN insert_cursor;
        FETCH NEXT FROM insert_cursor INTO @orderId;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            EXEC sp_CreateOrderHistory @orderId, @opType;
            FETCH NEXT FROM insert_cursor INTO @orderId;
        END
        CLOSE insert_cursor;
        DEALLOCATE insert_cursor;
    END

    IF EXISTS (SELECT 1 FROM inserted) AND EXISTS (SELECT 1 FROM deleted)
    BEGIN
        SET @opType = 'UPDATE';
        DECLARE update_cursor CURSOR FOR SELECT Id FROM inserted;
        OPEN update_cursor;
        FETCH NEXT FROM update_cursor INTO @orderId;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            EXEC sp_CreateOrderHistory @orderId, @opType;
            FETCH NEXT FROM update_cursor INTO @orderId;
        END
        CLOSE update_cursor;
        DEALLOCATE update_cursor;
    END

    IF EXISTS (SELECT 1 FROM deleted) AND NOT EXISTS (SELECT 1 FROM inserted)
    BEGIN
        SET @opType = 'DELETE';
        DECLARE delete_cursor CURSOR FOR SELECT Id FROM deleted;
        OPEN delete_cursor;
        FETCH NEXT FROM delete_cursor INTO @orderId;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            EXEC sp_CreateOrderHistory @orderId, @opType;
            FETCH NEXT FROM delete_cursor INTO @orderId;
        END
        CLOSE delete_cursor;
        DEALLOCATE delete_cursor;
    END
END;
