CREATE TABLE HistoryOrderItem (
    Id INT PRIMARY KEY IDENTITY(1,1),
    HistoryOrderId INT NOT NULL,
    OrderId NVARCHAR(MAX) NOT NULL,
    OrderItemId NVARCHAR(MAX) NOT NULL,

    ProductId NVARCHAR(MAX) NOT NULL,
    ProductName NVARCHAR(200),
    SKU NVARCHAR(100),
    Brand NVARCHAR(100),
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    TotalPrice AS (UnitPrice * Quantity) PERSISTED,
    Currency NVARCHAR(3),	

    CategoryId NVARCHAR(MAX),
    CategoryName NVARCHAR(100),
    OperationType NVARCHAR(10),
    CreatedDate DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_HistoryOrderItem_HistoryOrder 
        FOREIGN KEY (HistoryOrderId) 
        REFERENCES HistoryOrder(Id));
