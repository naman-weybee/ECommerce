-- Insert RoleEntities
INSERT INTO [ECommerce].[dbo].[RoleEntities] ([Name])
VALUES 
    ('Full'),
    ('Address'),
    ('Base'),
    ('CartItem'),
    ('Category'),
    ('City'),
    ('Country'),
    ('Gender'),
    ('Order'),
    ('OrderItem'),
    ('OTP'),
    ('Product'),
    ('RefreshToken'),
    ('Role'),
    ('RoleEntity'),
    ('State'),
    ('User');

-- Insert Role
INSERT INTO [ECommerce].[dbo].[Roles] (
    [Id],
    [Name],
    [RoleEntity],
    [HasViewPermission],
    [HasCreateOrUpdatePermission],
    [HasDeletePermission],
    [HasFullPermission],
    [CreatedDate],
    [UpdatedDate],
    [DeletedDate],
    [IsDeleted]
)
VALUES (
    NEWID(),
    N'Admin',
    1,
    0,
    0,
    0,
    0,
    GETDATE(),
    GETDATE(),
    GETDATE(),
    0
);

-- Insert Gender values
INSERT INTO [ECommerce].[dbo].[Gender] (
    [Id],
    [Name],
    [CreatedDate],
    [UpdatedDate],
    [DeletedDate],
    [IsDeleted]
)
VALUES 
    (NEWID(), 'Male', GETDATE(), NULL, NULL, 0),
    (NEWID(), 'Female', GETDATE(), NULL, NULL, 0),
    (NEWID(), 'Other', GETDATE(), NULL, NULL, 0);

-- Insert OrderStatus
INSERT INTO OrderStatus (StatusId, StatusName)
VALUES 
    (0, 'Pending'),
    (1, 'Placed'),
    (2, 'Shipped'),
    (3, 'Delivered'),
    (4, 'Canceled');