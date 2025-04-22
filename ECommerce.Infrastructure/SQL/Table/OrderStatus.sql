CREATE TABLE OrderStatus (
    StatusId INT PRIMARY KEY,
    StatusName NVARCHAR(50) NOT NULL);

INSERT INTO OrderStatus (StatusId, StatusName)
VALUES 
    (0, 'Pending'),
    (1, 'Placed'),
    (2, 'Shipped'),
    (3, 'Delivered'),
    (4, 'Canceled');