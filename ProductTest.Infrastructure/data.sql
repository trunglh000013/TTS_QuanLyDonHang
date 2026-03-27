USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = N'ProductTestDb')
BEGIN
    DROP DATABASE [ProductTestDb];
END
GO

CREATE DATABASE [ProductTestDb];
GO

USE [ProductTestDb];
GO

-----------------------------
-- SECTION 0: SEQUENCES (Id: 1 letter + 9 digits, Code: unique prefix per entity)
-----------------------------
CREATE SEQUENCE dbo.Seq_Supplier       AS BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999 NO CYCLE;
CREATE SEQUENCE dbo.Seq_Customer      AS BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999 NO CYCLE;
CREATE SEQUENCE dbo.Seq_Product       AS BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999 NO CYCLE;
CREATE SEQUENCE dbo.Seq_Cart          AS BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999 NO CYCLE;
CREATE SEQUENCE dbo.Seq_CartItem      AS BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999 NO CYCLE;
CREATE SEQUENCE dbo.Seq_Order         AS BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999 NO CYCLE;
CREATE SEQUENCE dbo.Seq_OrderItem     AS BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999 NO CYCLE;
CREATE SEQUENCE dbo.Seq_ProductRating AS BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999 NO CYCLE;
GO

-----------------------------
-- SECTION 1: CREATE TABLES
-----------------------------
-- Supplier Table
CREATE TABLE Suppliers (
    Id CHAR(10) NOT NULL PRIMARY KEY,
    Code VARCHAR(20) NOT NULL UNIQUE,
    Name NVARCHAR(255) NOT NULL,
    Address NVARCHAR(500) NULL,
    Phone NVARCHAR(50) NULL,
    Email NVARCHAR(255) NULL,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1
);

-- Product Table
CREATE TABLE Products (
    Id CHAR(10) NOT NULL PRIMARY KEY,
    Code VARCHAR(20) NOT NULL UNIQUE,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(1000) NULL,
    Category NVARCHAR(255) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    TaxRate DECIMAL(3,2) NOT NULL DEFAULT 1.00,
    Stock INT NOT NULL,
    SupplierId CHAR(10) NULL,
    SupplierName NVARCHAR(255) NULL,
    SupplierCode VARCHAR(20) NULL,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1,
    ExpiredDT DATETIME NOT NULL
);

-- Customer Table
CREATE TABLE Customers (
    Id CHAR(10) NOT NULL PRIMARY KEY,
    Code VARCHAR(20) NOT NULL UNIQUE,
    UserId CHAR(10) NULL,
    Name NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(50) NULL,
    Address NVARCHAR(500) NULL,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1
);

-- Carts and CartItems
CREATE TABLE Carts (
    Id CHAR(10) NOT NULL PRIMARY KEY,
    Code VARCHAR(20) NOT NULL UNIQUE,
    CustomerId CHAR(10) NOT NULL,
    CustomerName NVARCHAR(255) NULL,
    CustomerCode VARCHAR(20) NULL,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1
);

CREATE TABLE CartItems (
    Id CHAR(10) NOT NULL PRIMARY KEY,
    Code VARCHAR(20) NOT NULL UNIQUE,
    CartId CHAR(10) NOT NULL,
    ProductId CHAR(10) NOT NULL,
    ProductName NVARCHAR(255) NULL,
    ProductCode VARCHAR(20) NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1
);

-- Orders and OrderItems
CREATE TABLE Orders (
    Id CHAR(10) NOT NULL PRIMARY KEY,
    Code VARCHAR(20) NOT NULL UNIQUE,
    CustomerId CHAR(10) NOT NULL,
    CustomerName NVARCHAR(255) NULL,
    CustomerCode VARCHAR(20) NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT N'Placed',
    SubTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
    TaxTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1
);

CREATE TABLE OrderItems (
    Id CHAR(10) NOT NULL PRIMARY KEY,
    Code VARCHAR(20) NOT NULL UNIQUE,
    OrderId CHAR(10) NOT NULL,
    ProductId CHAR(10) NOT NULL,
    ProductName NVARCHAR(255) NULL,
    ProductCode VARCHAR(20) NULL,
    SupplierId CHAR(10) NOT NULL,
    SupplierName NVARCHAR(255) NULL,
    SupplierCode VARCHAR(20) NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    TaxRate DECIMAL(3,2) NOT NULL DEFAULT 1.00,
    LineSubTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
    TaxAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    LineTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1
);

-- ProductRatings Table
CREATE TABLE ProductRatings (
    Id CHAR(10) NOT NULL PRIMARY KEY,
    Code VARCHAR(20) NOT NULL UNIQUE,
    ProductId CHAR(10) NOT NULL,
    ProductName NVARCHAR(255) NULL,
    ProductCode VARCHAR(20) NULL,
    CustomerId CHAR(10) NOT NULL,
    OrderId CHAR(10) NOT NULL,
    OrderName NVARCHAR(255) NULL,
    OrderCode VARCHAR(20) NULL,
    Stars INT NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1
);

-- Constraints
ALTER TABLE Products
ADD CONSTRAINT FK_Products_Supplier FOREIGN KEY (SupplierId) REFERENCES Suppliers(Id);
GO

ALTER TABLE Carts
ADD CONSTRAINT FK_Carts_Customer FOREIGN KEY (CustomerId) REFERENCES Customers(Id);
GO

ALTER TABLE CartItems
ADD CONSTRAINT FK_CartItems_Cart FOREIGN KEY (CartId) REFERENCES Carts(Id);
GO

ALTER TABLE CartItems
ADD CONSTRAINT FK_CartItems_Product FOREIGN KEY (ProductId) REFERENCES Products(Id);
GO

ALTER TABLE Orders
ADD CONSTRAINT FK_Orders_Customer FOREIGN KEY (CustomerId) REFERENCES Customers(Id);
GO

ALTER TABLE OrderItems
ADD CONSTRAINT FK_OrderItems_Order FOREIGN KEY (OrderId) REFERENCES Orders(Id);
GO

ALTER TABLE OrderItems
ADD CONSTRAINT FK_OrderItems_Product FOREIGN KEY (ProductId) REFERENCES Products(Id);
GO

ALTER TABLE ProductRatings
ADD CONSTRAINT FK_ProductRatings_Product FOREIGN KEY (ProductId) REFERENCES Products(Id);
GO

ALTER TABLE ProductRatings
ADD CONSTRAINT FK_ProductRatings_Customer FOREIGN KEY (CustomerId) REFERENCES Customers(Id);
GO

ALTER TABLE ProductRatings
ADD CONSTRAINT FK_ProductRatings_Order FOREIGN KEY (OrderId) REFERENCES Orders(Id);
GO

ALTER TABLE ProductRatings
ADD CONSTRAINT CK_ProductRatings_Stars CHECK (Stars BETWEEN 1 AND 5);
GO

ALTER TABLE Products
ADD CONSTRAINT CK_Products_ExpiredDT CHECK (ExpiredDT > GETUTCDATE());
GO

ALTER TABLE Products
ADD CONSTRAINT CK_Products_TaxRate CHECK (TaxRate >= 0 AND TaxRate <= 100);
GO

-----------------------------
-- SECTION 2: TRIGGERS (as original)
-----------------------------
CREATE TRIGGER dbo.trg_Suppliers_InsteadOfInsert
ON dbo.Suppliers
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Suppliers (Id, Code, Name, Address, Phone, Email, CreatedAt, UpdatedAt, IsActive)
    SELECT
        N'S' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Supplier AS NVARCHAR(9)), 9),
        N'SUP-' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Supplier AS NVARCHAR(9)), 9),
        i.Name, i.Address, i.Phone, i.Email,
        ISNULL(i.CreatedAt, SYSUTCDATETIME()), i.UpdatedAt, ISNULL(i.IsActive, CAST(1 AS BIT))
    FROM inserted AS i;
END
GO

CREATE TRIGGER dbo.trg_Customers_InsteadOfInsert
ON dbo.Customers
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Customers (Id, Code, UserId, Name, Email, Phone, Address, CreatedAt, UpdatedAt, IsActive)
    SELECT
        N'C' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Customer AS NVARCHAR(9)), 9),
        N'CUS-' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Customer AS NVARCHAR(9)), 9),
        i.UserId,
        i.Name, i.Email, i.Phone, i.Address,
        ISNULL(i.CreatedAt, SYSUTCDATETIME()), i.UpdatedAt, ISNULL(i.IsActive, CAST(1 AS BIT))
    FROM inserted AS i;
END
GO

CREATE TRIGGER dbo.trg_Products_InsteadOfInsert
ON dbo.Products
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Products (Id, Code, Name, Description, Category, Price, TaxRate, Stock, SupplierId, SupplierName, SupplierCode, CreatedAt, UpdatedAt, IsActive, ExpiredDT)
    SELECT
        N'P' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Product AS NVARCHAR(9)), 9),
        N'PRD-' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Product AS NVARCHAR(9)), 9),
        i.Name, i.Description, i.Category, i.Price, ISNULL(i.TaxRate, CAST(1.00 AS DECIMAL(3,2))), i.Stock, i.SupplierId,
        i.SupplierName, i.SupplierCode,
        ISNULL(i.CreatedAt, SYSUTCDATETIME()), i.UpdatedAt, ISNULL(i.IsActive, CAST(1 AS BIT)), i.ExpiredDT
    FROM inserted AS i;
END
GO

CREATE TRIGGER dbo.trg_Carts_InsteadOfInsert
ON dbo.Carts
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Carts (Id, Code, CustomerId, CustomerName, CustomerCode, CreatedAt, UpdatedAt, IsActive)
    SELECT
        N'K' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Cart AS NVARCHAR(9)), 9),
        N'CRT-' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Cart AS NVARCHAR(9)), 9),
        i.CustomerId,
        i.CustomerName,
        i.CustomerCode,
        ISNULL(i.CreatedAt, SYSUTCDATETIME()), i.UpdatedAt, ISNULL(i.IsActive, CAST(1 AS BIT))
    FROM inserted AS i;
END
GO

-- Trigger for INSERT
CREATE TRIGGER dbo.trg_CartItems_InsteadOfInsert
ON dbo.CartItems
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.CartItems (Id, Code, CartId, ProductId, ProductName, ProductCode, Quantity, Price, CreatedAt, UpdatedAt, IsActive)
    SELECT
        N'I' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_CartItem AS NVARCHAR(9)), 9),
        N'CIT-' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_CartItem AS NVARCHAR(9)), 9),
        i.CartId, i.ProductId, i.ProductName, i.ProductCode, i.Quantity,
        i.Price,
        ISNULL(i.CreatedAt, SYSUTCDATETIME()), i.UpdatedAt, ISNULL(i.IsActive, CAST(1 AS BIT))
    FROM inserted AS i;
END
GO

-- Trigger for UPDATE
CREATE TRIGGER dbo.trg_CartItems_InsteadOfUpdate
ON dbo.CartItems
INSTEAD OF UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE c
    SET
        c.CartId = i.CartId,
        c.ProductId = i.ProductId,
        c.ProductName = i.ProductName,
        c.ProductCode = i.ProductCode,
        c.Quantity = i.Quantity,
        c.Price = i.Price,
        c.UpdatedAt = ISNULL(i.UpdatedAt, SYSUTCDATETIME()),
        c.IsActive = ISNULL(i.IsActive, c.IsActive)
    FROM dbo.CartItems c
    INNER JOIN inserted AS i ON c.Id = i.Id;
END
GO

-- Trigger for INSERT
CREATE TRIGGER dbo.trg_Orders_InsteadOfInsert
ON dbo.Orders
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    -- When an order is inserted, overwrite SubTotal, TaxTotal, TotalAmount by recalculating from corresponding OrderItems (if any)
    INSERT INTO dbo.Orders (Id, Code, CustomerId, CustomerName, CustomerCode, Status, SubTotal, TaxTotal, TotalAmount, CreatedAt, UpdatedAt, IsActive)
    SELECT
        N'O' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Order AS NVARCHAR(9)), 9),
        N'ORD-' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Order AS NVARCHAR(9)), 9),
        i.CustomerId, i.CustomerName, i.CustomerCode, ISNULL(i.Status, N'Placed'),
        COALESCE(oi.OrderSubTotal, CAST(0 AS DECIMAL(18,2))),
        COALESCE(oi.OrderTaxTotal, CAST(0 AS DECIMAL(18,2))),
        COALESCE(oi.OrderTotalAmount, CAST(0 AS DECIMAL(18,2))),
        ISNULL(i.CreatedAt, SYSUTCDATETIME()),
        i.UpdatedAt,
        ISNULL(i.IsActive, CAST(1 AS BIT))
    FROM inserted AS i
    OUTER APPLY (
        SELECT
            SUM(LineSubTotal) AS OrderSubTotal,
            SUM(TaxAmount) AS OrderTaxTotal,
            SUM(LineTotal) AS OrderTotalAmount
        FROM dbo.OrderItems oi
        WHERE oi.OrderId = i.Id
    ) oi;
END
GO

-- Trigger for UPDATE
CREATE TRIGGER dbo.trg_Orders_InsteadOfUpdate
ON dbo.Orders
INSTEAD OF UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    -- When an order is updated, overwrite SubTotal, TaxTotal, TotalAmount by recalculating from corresponding OrderItems (if any)
    UPDATE o
    SET
        o.CustomerId = i.CustomerId,
        o.CustomerName = i.CustomerName,
        o.CustomerCode = i.CustomerCode,
        o.Status = ISNULL(i.Status, o.Status),
        o.SubTotal = COALESCE(oi.OrderSubTotal, CAST(0 AS DECIMAL(18,2))),
        o.TaxTotal = COALESCE(oi.OrderTaxTotal, CAST(0 AS DECIMAL(18,2))),
        o.TotalAmount = COALESCE(oi.OrderTotalAmount, CAST(0 AS DECIMAL(18,2))),
        o.CreatedAt = ISNULL(i.CreatedAt, o.CreatedAt),
        o.UpdatedAt = ISNULL(i.UpdatedAt, SYSUTCDATETIME()),
        o.IsActive = ISNULL(i.IsActive, o.IsActive)
    FROM dbo.Orders o
    INNER JOIN inserted AS i ON o.Id = i.Id
    OUTER APPLY (
        SELECT
            SUM(LineSubTotal) AS OrderSubTotal,
            SUM(TaxAmount) AS OrderTaxTotal,
            SUM(LineTotal) AS OrderTotalAmount
        FROM dbo.OrderItems oi
        WHERE oi.OrderId = i.Id
    ) oi;
END
GO

CREATE TRIGGER dbo.trg_OrderItems_InsteadOfInsert
ON dbo.OrderItems
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.OrderItems (
        Id, Code, OrderId, ProductId, ProductName, ProductCode, SupplierId, SupplierName, SupplierCode, Quantity, UnitPrice, TaxRate,
        LineSubTotal, TaxAmount, LineTotal, CreatedAt, UpdatedAt, IsActive
    )
    SELECT
        N'L' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_OrderItem AS NVARCHAR(9)), 9),
        N'OLI-' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_OrderItem AS NVARCHAR(9)), 9),
        i.OrderId,
        i.ProductId,
        i.ProductName,
        i.ProductCode,
        i.SupplierId,
        i.SupplierName,
        i.SupplierCode,
        i.Quantity,
        i.UnitPrice,
        i.TaxRate,
        i.LineSubTotal,
        i.TaxAmount,
        i.LineTotal,
        ISNULL(i.CreatedAt, SYSUTCDATETIME()),
        i.UpdatedAt,
        ISNULL(i.IsActive, CAST(1 AS BIT))
    FROM inserted AS i;

    UPDATE o
    SET
        o.SubTotal = x.SubTotal,
        o.TaxTotal = x.TaxTotal,
        o.TotalAmount = x.TotalAmount,
        o.UpdatedAt = SYSUTCDATETIME()
    FROM Orders o
    JOIN (
        SELECT
            oi.OrderId,
            CAST(COALESCE(SUM(oi.LineSubTotal), 0) AS DECIMAL(18,2)) AS SubTotal,
            CAST(COALESCE(SUM(oi.TaxAmount), 0) AS DECIMAL(18,2)) AS TaxTotal,
            CAST(COALESCE(SUM(oi.LineTotal), 0) AS DECIMAL(18,2)) AS TotalAmount
        FROM OrderItems oi
        WHERE oi.IsActive = 1
        GROUP BY oi.OrderId
    ) x ON x.OrderId = o.Id
    WHERE o.Id IN (SELECT DISTINCT OrderId FROM inserted);
END
GO

-- Trigger for UPDATE
CREATE TRIGGER dbo.trg_OrderItems_InsteadOfUpdate
ON dbo.OrderItems
INSTEAD OF UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE oi
    SET
        oi.OrderId = i.OrderId,
        oi.ProductId = i.ProductId,
        oi.ProductName = i.ProductName,
        oi.ProductCode = i.ProductCode,
        oi.SupplierId = i.SupplierId,
        oi.SupplierName = i.SupplierName,
        oi.SupplierCode = i.SupplierCode,
        oi.Quantity = i.Quantity,
        oi.UnitPrice = i.UnitPrice,
        oi.TaxRate = i.TaxRate,
        oi.LineSubTotal = i.LineSubTotal,
        oi.TaxAmount = i.TaxAmount,
        oi.LineTotal = i.LineTotal,
        oi.UpdatedAt = ISNULL(i.UpdatedAt, SYSUTCDATETIME()),
        oi.IsActive = ISNULL(i.IsActive, oi.IsActive)
    FROM dbo.OrderItems oi
    INNER JOIN inserted AS i ON oi.Id = i.Id;

    UPDATE o
    SET
        o.SubTotal = x.SubTotal,
        o.TaxTotal = x.TaxTotal,
        o.TotalAmount = x.TotalAmount,
        o.UpdatedAt = SYSUTCDATETIME()
    FROM Orders o
    JOIN (
        SELECT
            oi.OrderId,
            CAST(COALESCE(SUM(oi.LineSubTotal), 0) AS DECIMAL(18,2)) AS SubTotal,
            CAST(COALESCE(SUM(oi.TaxAmount), 0) AS DECIMAL(18,2)) AS TaxTotal,
            CAST(COALESCE(SUM(oi.LineTotal), 0) AS DECIMAL(18,2)) AS TotalAmount
        FROM OrderItems oi
        WHERE oi.IsActive = 1
        GROUP BY oi.OrderId
    ) x ON x.OrderId = o.Id
    WHERE o.Id IN (
        SELECT DISTINCT OrderId FROM inserted
        UNION
        SELECT DISTINCT OrderId FROM deleted
    );
END
GO

CREATE TRIGGER dbo.trg_ProductRatings_InsteadOfInsert
ON dbo.ProductRatings
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.ProductRatings (Id, Code, ProductId, ProductName, ProductCode, CustomerId, OrderId, OrderName, OrderCode, Stars, Content, CreatedAt, UpdatedAt, IsActive)
    SELECT
        N'R' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_ProductRating AS NVARCHAR(9)), 9),
        N'RTG-' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_ProductRating AS NVARCHAR(9)), 9),
        i.ProductId,
        COALESCE(i.ProductName, p.Name),
        COALESCE(i.ProductCode, p.Code),
        o.CustomerId,
        i.OrderId,
        COALESCE(i.OrderName, o.Status),
        COALESCE(i.OrderCode, o.Code),
        i.Stars,
        i.Content,
        ISNULL(i.CreatedAt, SYSUTCDATETIME()),
        i.UpdatedAt,
        ISNULL(i.IsActive, CAST(1 AS BIT))
    FROM inserted AS i
    INNER JOIN dbo.Products p ON p.Id = i.ProductId
    INNER JOIN dbo.Orders o ON o.Id = i.OrderId;
END
GO

-- =========================================
-- SECTION 1.1: SEED DATA (at least 5 records per table, linked via FKs)
-- =========================================

-- Seed Suppliers (at least 5)
IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE Name = N'Alpha Supplies')
BEGIN
    INSERT INTO Suppliers(Name, Address, Phone, Email)
    VALUES (N'Alpha Supplies', N'123 Alpha Rd', N'0901234567', N'alpha@supplies.com');
END
IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE Name = N'Bravo Partners')
BEGIN
    INSERT INTO Suppliers(Name, Address, Phone, Email)
    VALUES (N'Bravo Partners', N'456 Bravo Blvd', N'0902345678', N'bravo@partners.com');
END
IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE Name = N'Charlie Wholesale')
BEGIN
    INSERT INTO Suppliers(Name, Address, Phone, Email)
    VALUES (N'Charlie Wholesale', N'789 Charlie St', N'0903456789', N'charlie@wholesale.com');
END
IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE Name = N'Delta Traders')
BEGIN
    INSERT INTO Suppliers(Name, Address, Phone, Email)
    VALUES (N'Delta Traders', N'101 Delta Ave', N'0904567890', N'delta@traders.com');
END
IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE Name = N'Echo Imports')
BEGIN
    INSERT INTO Suppliers(Name, Address, Phone, Email)
    VALUES (N'Echo Imports', N'202 Echo Pkwy', N'0905678901', N'echo@imports.com');
END

-- Seed Customers (at least 5)
IF NOT EXISTS (SELECT 1 FROM Customers WHERE Name = N'Jane Doe')
BEGIN
    INSERT INTO Customers(Name, Email, Phone, Address)
    VALUES (N'Jane Doe', N'jane.doe@email.com', N'0911111111', N'1 Main St');
END
IF NOT EXISTS (SELECT 1 FROM Customers WHERE Name = N'John Smith')
BEGIN
    INSERT INTO Customers(Name, Email, Phone, Address)
    VALUES (N'John Smith', N'john.smith@email.com', N'0922222222', N'2 Second St');
END
IF NOT EXISTS (SELECT 1 FROM Customers WHERE Name = N'Anna Lee')
BEGIN
    INSERT INTO Customers(Name, Email, Phone, Address)
    VALUES (N'Anna Lee', N'anna.lee@email.com', N'0933333333', N'3 Third St');
END
IF NOT EXISTS (SELECT 1 FROM Customers WHERE Name = N'Chris Tran')
BEGIN
    INSERT INTO Customers(Name, Email, Phone, Address)
    VALUES (N'Chris Tran', N'chris.tran@email.com', N'0944444444', N'4 Fourth St');
END
IF NOT EXISTS (SELECT 1 FROM Customers WHERE Name = N'Sam Nguyen')
BEGIN
    INSERT INTO Customers(Name, Email, Phone, Address)
    VALUES (N'Sam Nguyen', N'sam.nguyen@email.com', N'0955555555', N'5 Fifth St');
END

-- Seed Products (at least 5, spread across suppliers)
DECLARE @SupplierId1 CHAR(10), @SupplierId2 CHAR(10), @SupplierId3 CHAR(10), @SupplierId4 CHAR(10), @SupplierId5 CHAR(10);
SELECT TOP 1 @SupplierId1 = Id FROM Suppliers WHERE Name = N'Alpha Supplies';
SELECT TOP 1 @SupplierId2 = Id FROM Suppliers WHERE Name = N'Bravo Partners';
SELECT TOP 1 @SupplierId3 = Id FROM Suppliers WHERE Name = N'Charlie Wholesale';
SELECT TOP 1 @SupplierId4 = Id FROM Suppliers WHERE Name = N'Delta Traders';
SELECT TOP 1 @SupplierId5 = Id FROM Suppliers WHERE Name = N'Echo Imports';

IF NOT EXISTS (SELECT 1 FROM Products WHERE Name = N'Product A')
BEGIN
    INSERT INTO Products(Name, Description, Category, Price, Stock, SupplierId, ExpiredDT)
    VALUES (N'Product A', N'First product', N'Category1', 100.00, 50, @SupplierId1, DATEADD(DAY,60,GETUTCDATE()));
END
IF NOT EXISTS (SELECT 1 FROM Products WHERE Name = N'Product B')
BEGIN
    INSERT INTO Products(Name, Description, Category, Price, Stock, SupplierId, ExpiredDT)
    VALUES (N'Product B', N'Second product', N'Category2', 200.00, 30, @SupplierId2, DATEADD(DAY,90,GETUTCDATE()));
END
IF NOT EXISTS (SELECT 1 FROM Products WHERE Name = N'Product C')
BEGIN
    INSERT INTO Products(Name, Description, Category, Price, Stock, SupplierId, ExpiredDT)
    VALUES (N'Product C', N'Third product', N'Category1', 150.00, 60, @SupplierId3, DATEADD(DAY,120,GETUTCDATE()));
END
IF NOT EXISTS (SELECT 1 FROM Products WHERE Name = N'Product D')
BEGIN
    INSERT INTO Products(Name, Description, Category, Price, Stock, SupplierId, ExpiredDT)
    VALUES (N'Product D', N'Fourth product', N'Category3', 300.00, 25, @SupplierId4, DATEADD(DAY,150,GETUTCDATE()));
END
IF NOT EXISTS (SELECT 1 FROM Products WHERE Name = N'Product E')
BEGIN
    INSERT INTO Products(Name, Description, Category, Price, Stock, SupplierId, ExpiredDT)
    VALUES (N'Product E', N'Fifth product', N'Category2', 80.00, 40, @SupplierId5, DATEADD(DAY,180,GETUTCDATE()));
END
GO

-- Seed Carts (each for a different customer)
DECLARE @CustomerId1 CHAR(10), @CustomerId2 CHAR(10), @CustomerId3 CHAR(10), @CustomerId4 CHAR(10), @CustomerId5 CHAR(10);
SELECT TOP 1 @CustomerId1 = Id FROM Customers WHERE Name = N'Jane Doe';
SELECT TOP 1 @CustomerId2 = Id FROM Customers WHERE Name = N'John Smith';
SELECT TOP 1 @CustomerId3 = Id FROM Customers WHERE Name = N'Anna Lee';
SELECT TOP 1 @CustomerId4 = Id FROM Customers WHERE Name = N'Chris Tran';
SELECT TOP 1 @CustomerId5 = Id FROM Customers WHERE Name = N'Sam Nguyen';

IF NOT EXISTS (SELECT 1 FROM Carts WHERE CustomerId = @CustomerId1)
BEGIN
    INSERT INTO Carts(CustomerId) VALUES (@CustomerId1);
END
IF NOT EXISTS (SELECT 1 FROM Carts WHERE CustomerId = @CustomerId2)
BEGIN
    INSERT INTO Carts(CustomerId) VALUES (@CustomerId2);
END
IF NOT EXISTS (SELECT 1 FROM Carts WHERE CustomerId = @CustomerId3)
BEGIN
    INSERT INTO Carts(CustomerId) VALUES (@CustomerId3);
END
IF NOT EXISTS (SELECT 1 FROM Carts WHERE CustomerId = @CustomerId4)
BEGIN
    INSERT INTO Carts(CustomerId) VALUES (@CustomerId4);
END
IF NOT EXISTS (SELECT 1 FROM Carts WHERE CustomerId = @CustomerId5)
BEGIN
    INSERT INTO Carts(CustomerId) VALUES (@CustomerId5);
END

-- Seed CartItems (at least 5, referencing valid cart and product)
DECLARE @CartId1 CHAR(10), @CartId2 CHAR(10), @CartId3 CHAR(10), @CartId4 CHAR(10), @CartId5 CHAR(10);
DECLARE @ProdId1 CHAR(10), @ProdId2 CHAR(10), @ProdId3 CHAR(10), @ProdId4 CHAR(10), @ProdId5 CHAR(10);
SELECT TOP 1 @CartId1 = Id FROM Carts WHERE CustomerId = @CustomerId1;
SELECT TOP 1 @CartId2 = Id FROM Carts WHERE CustomerId = @CustomerId2;
SELECT TOP 1 @CartId3 = Id FROM Carts WHERE CustomerId = @CustomerId3;
SELECT TOP 1 @CartId4 = Id FROM Carts WHERE CustomerId = @CustomerId4;
SELECT TOP 1 @CartId5 = Id FROM Carts WHERE CustomerId = @CustomerId5;
SELECT TOP 1 @ProdId1 = Id FROM Products WHERE Name = N'Product A';
SELECT TOP 1 @ProdId2 = Id FROM Products WHERE Name = N'Product B';
SELECT TOP 1 @ProdId3 = Id FROM Products WHERE Name = N'Product C';
SELECT TOP 1 @ProdId4 = Id FROM Products WHERE Name = N'Product D';
SELECT TOP 1 @ProdId5 = Id FROM Products WHERE Name = N'Product E';

IF NOT EXISTS (SELECT 1 FROM CartItems WHERE CartId = @CartId1 AND ProductId = @ProdId1)
BEGIN
    INSERT INTO CartItems(CartId, ProductId, Quantity, Price) VALUES (@CartId1, @ProdId1, 2, 100.00);
END
IF NOT EXISTS (SELECT 1 FROM CartItems WHERE CartId = @CartId2 AND ProductId = @ProdId2)
BEGIN
    INSERT INTO CartItems(CartId, ProductId, Quantity, Price) VALUES (@CartId2, @ProdId2, 1, 200.00);
END
IF NOT EXISTS (SELECT 1 FROM CartItems WHERE CartId = @CartId3 AND ProductId = @ProdId3)
BEGIN
    INSERT INTO CartItems(CartId, ProductId, Quantity, Price) VALUES (@CartId3, @ProdId3, 3, 150.00);
END
IF NOT EXISTS (SELECT 1 FROM CartItems WHERE CartId = @CartId4 AND ProductId = @ProdId4)
BEGIN
    INSERT INTO CartItems(CartId, ProductId, Quantity, Price) VALUES (@CartId4, @ProdId4, 2, 300.00);
END
IF NOT EXISTS (SELECT 1 FROM CartItems WHERE CartId = @CartId5 AND ProductId = @ProdId5)
BEGIN
    INSERT INTO CartItems(CartId, ProductId, Quantity, Price) VALUES (@CartId5, @ProdId5, 1, 80.00);
END

-- Seed Orders (at least 5, each for a different customer)
DECLARE @OrderCustomer1 CHAR(10), @OrderCustomer2 CHAR(10), @OrderCustomer3 CHAR(10), @OrderCustomer4 CHAR(10), @OrderCustomer5 CHAR(10);
SELECT TOP 1 @OrderCustomer1 = Id FROM Customers WHERE Name = N'Jane Doe';
SELECT TOP 1 @OrderCustomer2 = Id FROM Customers WHERE Name = N'John Smith';
SELECT TOP 1 @OrderCustomer3 = Id FROM Customers WHERE Name = N'Anna Lee';
SELECT TOP 1 @OrderCustomer4 = Id FROM Customers WHERE Name = N'Chris Tran';
SELECT TOP 1 @OrderCustomer5 = Id FROM Customers WHERE Name = N'Sam Nguyen';

IF NOT EXISTS (SELECT 1 FROM Orders WHERE CustomerId = @OrderCustomer1)
BEGIN
    INSERT INTO Orders(CustomerId, Status, SubTotal, TaxTotal, TotalAmount) VALUES (@OrderCustomer1, N'Placed', 200.00, 20.00, 220.00);
END
IF NOT EXISTS (SELECT 1 FROM Orders WHERE CustomerId = @OrderCustomer2)
BEGIN
    INSERT INTO Orders(CustomerId, Status, SubTotal, TaxTotal, TotalAmount) VALUES (@OrderCustomer2, N'Placed', 100.00, 10.00, 110.00);
END
IF NOT EXISTS (SELECT 1 FROM Orders WHERE CustomerId = @OrderCustomer3)
BEGIN
    INSERT INTO Orders(CustomerId, Status, SubTotal, TaxTotal, TotalAmount) VALUES (@OrderCustomer3, N'Placed', 300.00, 30.00, 330.00);
END
IF NOT EXISTS (SELECT 1 FROM Orders WHERE CustomerId = @OrderCustomer4)
BEGIN
    INSERT INTO Orders(CustomerId, Status, SubTotal, TaxTotal, TotalAmount) VALUES (@OrderCustomer4, N'Placed', 150.00, 15.00, 165.00);
END
IF NOT EXISTS (SELECT 1 FROM Orders WHERE CustomerId = @OrderCustomer5)
BEGIN
    INSERT INTO Orders(CustomerId, Status, SubTotal, TaxTotal, TotalAmount) VALUES (@OrderCustomer5, N'Placed', 400.00, 40.00, 440.00);
END

-- Seed OrderItems (at least 5, linking to above orders and products)
DECLARE @OrderId1 CHAR(10), @OrderId2 CHAR(10), @OrderId3 CHAR(10), @OrderId4 CHAR(10), @OrderId5 CHAR(10);
SELECT TOP 1 @OrderId1 = Id FROM Orders WHERE CustomerId = @OrderCustomer1;
SELECT TOP 1 @OrderId2 = Id FROM Orders WHERE CustomerId = @OrderCustomer2;
SELECT TOP 1 @OrderId3 = Id FROM Orders WHERE CustomerId = @OrderCustomer3;
SELECT TOP 1 @OrderId4 = Id FROM Orders WHERE CustomerId = @OrderCustomer4;
SELECT TOP 1 @OrderId5 = Id FROM Orders WHERE CustomerId = @OrderCustomer5;

IF NOT EXISTS (SELECT 1 FROM OrderItems WHERE OrderId = @OrderId1 AND ProductId = @ProdId1)
BEGIN
    INSERT INTO OrderItems(OrderId, ProductId, SupplierId, Quantity, UnitPrice, TaxRate, LineSubTotal, TaxAmount, LineTotal)
    SELECT @OrderId1, @ProdId1, SupplierId, 2, 100.00, 1.00, 200.00, 2.00, 202.00
    FROM Products WHERE Id = @ProdId1;
END
IF NOT EXISTS (SELECT 1 FROM OrderItems WHERE OrderId = @OrderId2 AND ProductId = @ProdId2)
BEGIN
    INSERT INTO OrderItems(OrderId, ProductId, SupplierId, Quantity, UnitPrice, TaxRate, LineSubTotal, TaxAmount, LineTotal)
    SELECT @OrderId2, @ProdId2, SupplierId, 1, 200.00, 1.00, 200.00, 2.00, 202.00
    FROM Products WHERE Id = @ProdId2;
END
IF NOT EXISTS (SELECT 1 FROM OrderItems WHERE OrderId = @OrderId3 AND ProductId = @ProdId3)
BEGIN
    INSERT INTO OrderItems(OrderId, ProductId, SupplierId, Quantity, UnitPrice, TaxRate, LineSubTotal, TaxAmount, LineTotal)
    SELECT @OrderId3, @ProdId3, SupplierId, 2, 150.00, 1.00, 300.00, 3.00, 303.00
    FROM Products WHERE Id = @ProdId3;
END
IF NOT EXISTS (SELECT 1 FROM OrderItems WHERE OrderId = @OrderId4 AND ProductId = @ProdId4)
BEGIN
    INSERT INTO OrderItems(OrderId, ProductId, SupplierId, Quantity, UnitPrice, TaxRate, LineSubTotal, TaxAmount, LineTotal)
    SELECT @OrderId4, @ProdId4, SupplierId, 1, 300.00, 1.00, 300.00, 3.00, 303.00
    FROM Products WHERE Id = @ProdId4;
END
IF NOT EXISTS (SELECT 1 FROM OrderItems WHERE OrderId = @OrderId5 AND ProductId = @ProdId5)
BEGIN
    INSERT INTO OrderItems(OrderId, ProductId, SupplierId, Quantity, UnitPrice, TaxRate, LineSubTotal, TaxAmount, LineTotal)
    SELECT @OrderId5, @ProdId5, SupplierId, 4, 80.00, 1.00, 320.00, 3.20, 323.20
    FROM Products WHERE Id = @ProdId5;
END

-- Seed ProductRatings (linked to product + order; customer is on Orders, not on ProductRatings)
IF NOT EXISTS (SELECT 1 FROM ProductRatings WHERE ProductId = @ProdId1 AND OrderId = @OrderId1)
BEGIN
    INSERT INTO ProductRatings(ProductId, OrderId, Stars, Content)
    VALUES (@ProdId1, @OrderId1, 5, N'Excellent product!');
END
IF NOT EXISTS (SELECT 1 FROM ProductRatings WHERE ProductId = @ProdId2 AND OrderId = @OrderId2)
BEGIN
    INSERT INTO ProductRatings(ProductId, OrderId, Stars, Content)
    VALUES (@ProdId2, @OrderId2, 4, N'Good value money');
END
IF NOT EXISTS (SELECT 1 FROM ProductRatings WHERE ProductId = @ProdId3 AND OrderId = @OrderId3)
BEGIN
    INSERT INTO ProductRatings(ProductId, OrderId, Stars, Content)
    VALUES (@ProdId3, @OrderId3, 3, N'It''s okay');
END
IF NOT EXISTS (SELECT 1 FROM ProductRatings WHERE ProductId = @ProdId4 AND OrderId = @OrderId4)
BEGIN
    INSERT INTO ProductRatings(ProductId, OrderId, Stars, Content)
    VALUES (@ProdId4, @OrderId4, 5, N'Outstanding!');
END
IF NOT EXISTS (SELECT 1 FROM ProductRatings WHERE ProductId = @ProdId5 AND OrderId = @OrderId5)
BEGIN
    INSERT INTO ProductRatings(ProductId, OrderId, Stars, Content)
    VALUES (@ProdId5, @OrderId5, 4, N'Would buy again!');
END
GO

-----------------------------
-- SECTION: FUNCTIONS
-----------------------------

-- Tax functions used by stored procedures.
CREATE FUNCTION dbo.fn_ApplyTax
(
    @Price DECIMAL(18,2) = 0,
    @TaxRate DECIMAL(3,2) = 1.00
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    RETURN @Price * (1 + (@TaxRate / 100));
END;
GO

CREATE FUNCTION dbo.fn_GetTaxAmount
(
    @Price DECIMAL(18,2) = 0,
    @TaxRate DECIMAL(3,2) = 1.00
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    RETURN @Price * (@TaxRate / 100);
END;
GO

-----------------------------
-- SECTION 3: UNIVERSAL CRUD PROCEDURES FOR ALL TABLES
-----------------------------

------------------------------------------------
-- SUPPLIERS CRUD PROCEDURES
------------------------------------------------

CREATE PROCEDURE dbo.usp_SupplierGetAll
    @PageNumber NVARCHAR(50) = N'1',
    @PageSize NVARCHAR(50) = N'10'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PageNumberValue INT = TRY_CONVERT(INT, @PageNumber);
    DECLARE @PageSizeValue INT = TRY_CONVERT(INT, @PageSize);
    IF @PageNumberValue IS NULL OR @PageSizeValue IS NULL
    BEGIN
        RAISERROR(N'Invalid paging input', 16, 1);
        RETURN;
    END
    SELECT Id, Code, Name, Address, Phone, Email, CreatedAt, UpdatedAt, IsActive
    FROM Suppliers
    WHERE IsActive = 1
    ORDER BY CreatedAt DESC
    OFFSET (@PageNumberValue - 1) * @PageSizeValue ROWS FETCH NEXT @PageSizeValue ROWS ONLY;
END
GO

-- EXEC dbo.usp_SupplierGetAll @PageNumber = 1, @PageSize = 5;

CREATE PROCEDURE dbo.usp_SupplierGetById
    @Id NVARCHAR(50)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Supplier with the given Id does not exist or is not active', 16, 1);
        RETURN;
    END

    SELECT Id, Code, Name, Address, Phone, Email, CreatedAt, UpdatedAt, IsActive
    FROM Suppliers
    WHERE Id = @Id AND IsActive = 1;
END
GO

-- DECLARE @SampleId CHAR(10); SELECT TOP 1 @SampleId = Id FROM Suppliers; EXEC dbo.usp_SupplierGetById @Id=@SampleId;

CREATE PROCEDURE dbo.usp_SupplierGetByCode
    @Code NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE Code = @Code AND IsActive = 1)
    BEGIN
        RAISERROR(N'Supplier with the given Code does not exist or is not active', 16, 1);
        RETURN;
    END

    SELECT Id, Code, Name, Address, Phone, Email, CreatedAt, UpdatedAt, IsActive
    FROM Suppliers
    WHERE Code = @Code AND IsActive = 1;
END
GO

-- DECLARE @SampleCode VARCHAR(20); SELECT TOP 1 @SampleCode = Code FROM Suppliers; EXEC dbo.usp_GetSupplierByCode @Code=@SampleCode;

CREATE PROCEDURE dbo.usp_SupplierCreate
    @Name NVARCHAR(MAX),
    @Address NVARCHAR(MAX) = NULL,
    @Phone NVARCHAR(MAX) = NULL,
    @Email NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Suppliers WHERE Name = @Name AND IsActive = 1)
    BEGIN
        RAISERROR(N'Supplier name already exists', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Suppliers(Name, Address, Phone, Email) VALUES (@Name, @Address, @Phone, @Email);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- EXEC dbo.usp_SupplierCreate @Name=N'Test Supplier', @Address=N'11 Test Lane', @Phone=N'0930001000', @Email=N'test@supplier.com';

CREATE PROCEDURE dbo.usp_SupplierUpdate
    @Id NVARCHAR(50),
    @Name NVARCHAR(MAX),
    @Address NVARCHAR(MAX) = NULL,
    @Phone NVARCHAR(MAX) = NULL,
    @Email NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Supplier not found or already deleted', 16, 1);
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM Suppliers WHERE Name = @Name AND Id <> @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Supplier name already exists', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Suppliers
            SET Name = @Name, Address = @Address, Phone = @Phone, Email = @Email, UpdatedAt = SYSUTCDATETIME()
            WHERE Id = @Id;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- DECLARE @SampleSupplierId CHAR(10); SELECT TOP 1 @SampleSupplierId=Id FROM Suppliers; EXEC dbo.usp_SupplierUpdate @Id=@SampleSupplierId, @Name=N'New Name', @Address=N'Edited Addr', @Phone=N'0909999999', @Email=N'edited@domain.com';

CREATE PROCEDURE dbo.usp_SupplierDelete
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Suppliers WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Supplier not found or already deleted', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Suppliers SET IsActive = 0, UpdatedAt = SYSUTCDATETIME() WHERE Id = @Id;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- DECLARE @ToDeleteId CHAR(10); SELECT TOP 1 @ToDeleteId=Id FROM Suppliers WHERE IsActive=1; EXEC dbo.usp_DeleteSupplier @Id=@ToDeleteId;

------------------------------------------------
-- CUSTOMERS CRUD PROCEDURES
------------------------------------------------

CREATE PROCEDURE dbo.usp_CustomerGetAll
    @PageNumber NVARCHAR(50) = N'1',
    @PageSize NVARCHAR(50) = N'10'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PageNumberValue INT = TRY_CONVERT(INT, @PageNumber);
    DECLARE @PageSizeValue INT = TRY_CONVERT(INT, @PageSize);
    IF @PageNumberValue IS NULL OR @PageSizeValue IS NULL
    BEGIN
        RAISERROR(N'Invalid paging input', 16, 1);
        RETURN;
    END
    SELECT Id, Code, Name, Email, Phone, Address, CreatedAt, UpdatedAt, IsActive
    FROM Customers
    WHERE IsActive = 1
    ORDER BY CreatedAt DESC
    OFFSET (@PageNumberValue - 1) * @PageSizeValue ROWS FETCH NEXT @PageSizeValue ROWS ONLY;
END
GO

CREATE PROCEDURE dbo.usp_CustomerGetById
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Code, Name, Email, Phone, Address, CreatedAt, UpdatedAt, IsActive
    FROM Customers
    WHERE Id = @Id AND IsActive = 1;
END
GO

-- DECLARE @CustomerSampleId CHAR(10); SELECT TOP 1 @CustomerSampleId=Id FROM Customers; EXEC dbo.usp_CustomerGetById @Id=@CustomerSampleId;

CREATE PROCEDURE dbo.usp_CustomerGetByCode
    @Code NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Code, Name, Email, Phone, Address, CreatedAt, UpdatedAt, IsActive
    FROM Customers
    WHERE Code = @Code AND IsActive = 1;
END
GO

-- DECLARE @CustomerSampleCode VARCHAR(20); SELECT TOP 1 @CustomerSampleCode=Code FROM Customers; EXEC dbo.usp_CustomerGetByCode @Code=@CustomerSampleCode;

CREATE PROCEDURE dbo.usp_CustomerCreate
    @Name NVARCHAR(MAX),
    @Email NVARCHAR(MAX),
    @Phone NVARCHAR(MAX) = NULL,
    @Address NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Customers WHERE Email = @Email AND IsActive = 1)
    BEGIN
        RAISERROR(N'Customer email already exists', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;        
            INSERT INTO Customers(Name, Email, Phone, Address) VALUES (@Name, @Email, @Phone, @Address);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- EXEC dbo.usp_CustomerCreate @Name=N'Test User', @Email=N'testuser@email.com', @Phone=N'0999888777', @Address=N'Testville';

CREATE PROCEDURE dbo.usp_CustomerUpdate
    @Id NVARCHAR(50),
    @Name NVARCHAR(MAX),
    @Email NVARCHAR(MAX),
    @Phone NVARCHAR(MAX) = NULL,
    @Address NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Customers WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Customer not found or already deleted', 16, 1);
        RETURN;
    END
    IF EXISTS (SELECT 1 FROM Customers WHERE Email = @Email AND Id <> @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Customer email already exists', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Customers
            SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address, UpdatedAt = SYSUTCDATETIME()
            WHERE Id = @Id;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- DECLARE @CusSampleId CHAR(10); SELECT TOP 1 @CusSampleId=Id FROM Customers; EXEC dbo.usp_CustomerUpdate @Id=@CusSampleId, @Name=N'Jane X', @Email=N'janex@email.com', @Phone=N'0908888888', @Address=N'New Addr';

CREATE PROCEDURE dbo.usp_CustomerDelete
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Customers WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Customer not found or already deleted', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Customers SET IsActive = 0, UpdatedAt = SYSUTCDATETIME() WHERE Id = @Id;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- DECLARE @DelCusId CHAR(10); SELECT TOP 1 @DelCusId=Id FROM Customers WHERE IsActive=1; EXEC dbo.usp_CustomerDelete @Id=@DelCusId;

------------------------------------------------
-- PRODUCTS STORED PROCEDURES PER ENUM
------------------------------------------------

-- 1. usp_CreateProduct
CREATE PROCEDURE dbo.usp_ProductCreate
    @Name NVARCHAR(MAX),
    @Description NVARCHAR(MAX) = NULL,
    @Category NVARCHAR(MAX),
    @Price NVARCHAR(50),
    @TaxRate NVARCHAR(50) = N'1.00',
    @Stock NVARCHAR(50),
    @SupplierId NVARCHAR(50) = NULL,
    @ExpiredDT NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PriceValue DECIMAL(18,2) = TRY_CONVERT(DECIMAL(18,2), @Price);
    DECLARE @TaxRateValue DECIMAL(3,2) = TRY_CONVERT(DECIMAL(3,2), @TaxRate);
    DECLARE @StockValue INT = TRY_CONVERT(INT, @Stock);
    DECLARE @ExpiredDTValue DATETIME = TRY_CONVERT(DATETIME, @ExpiredDT, 103);
    IF @PriceValue IS NULL OR @TaxRateValue IS NULL OR @StockValue IS NULL
    BEGIN
        RAISERROR(N'Invalid numeric input for Price/TaxRate/Stock', 16, 1);
        RETURN;
    END;
    IF @ExpiredDTValue IS NULL
    BEGIN
        RAISERROR(N'Invalid ExpiredDT format, expected dd/MM/yyyy', 16, 1);
        RETURN;
    END;

    IF EXISTS (
        SELECT 1 FROM Products WHERE Name = @Name AND Category = @Category AND IsActive = 1
    )
    BEGIN
        RAISERROR(N'Product with that name and category already exists', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Products(Name, Description, Category, Price, TaxRate, Stock, SupplierId, ExpiredDT)
            VALUES (@Name, @Description, @Category, @PriceValue, @TaxRateValue, @StockValue, @SupplierId, @ExpiredDTValue);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- 2. usp_UpdateProduct
CREATE PROCEDURE dbo.usp_ProductUpdate
    @Id NVARCHAR(50),
    @Name NVARCHAR(MAX),
    @Description NVARCHAR(MAX) = NULL,
    @Category NVARCHAR(MAX),
    @Price NVARCHAR(50),
    @TaxRate NVARCHAR(50) = N'1.00',
    @Stock NVARCHAR(50),
    @SupplierId NVARCHAR(50) = NULL,
    @ExpiredDT NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PriceValue DECIMAL(18,2) = TRY_CONVERT(DECIMAL(18,2), @Price);
    DECLARE @TaxRateValue DECIMAL(3,2) = TRY_CONVERT(DECIMAL(3,2), @TaxRate);
    DECLARE @StockValue INT = TRY_CONVERT(INT, @Stock);
    DECLARE @ExpiredDTValue DATETIME = TRY_CONVERT(DATETIME, @ExpiredDT, 103);
    IF @PriceValue IS NULL OR @TaxRateValue IS NULL OR @StockValue IS NULL
    BEGIN
        RAISERROR(N'Invalid numeric input for Price/TaxRate/Stock', 16, 1);
        RETURN;
    END;
    IF @ExpiredDTValue IS NULL
    BEGIN
        RAISERROR(N'Invalid ExpiredDT format, expected dd/MM/yyyy', 16, 1);
        RETURN;
    END;

    IF NOT EXISTS (SELECT 1 FROM Products WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Product not found or already deleted', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Products WHERE Name=@Name AND Category=@Category AND Id<>@Id AND IsActive=1)
    BEGIN
        RAISERROR(N'Product with that name and category already exists', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE p
            SET Name = @Name,
                Description = @Description,
                Category = @Category,
                Price = @PriceValue,
                TaxRate = @TaxRateValue,
                Stock = @StockValue,
                SupplierId = @SupplierId,
                SupplierName = s.Name,
                SupplierCode = s.Code,
                ExpiredDT = @ExpiredDTValue,
                UpdatedAt = SYSUTCDATETIME()
            FROM Products p
            LEFT JOIN Suppliers s ON s.Id = @SupplierId
            WHERE p.Id = @Id;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- 3. usp_SearchProducts
CREATE PROCEDURE dbo.usp_ProductSearch
    @SearchTerm NVARCHAR(MAX),
    @PageNumber NVARCHAR(50) = N'1',
    @PageSize NVARCHAR(50) = N'10'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PageNumberValue INT = TRY_CONVERT(INT, @PageNumber);
    DECLARE @PageSizeValue INT = TRY_CONVERT(INT, @PageSize);
    IF @PageNumberValue IS NULL OR @PageSizeValue IS NULL
    BEGIN
        RAISERROR(N'Invalid paging input', 16, 1);
        RETURN;
    END
    SELECT Id, Code, Name, Description, Category, Price, TaxRate, Stock, SupplierId, CreatedAt, UpdatedAt, IsActive, ExpiredDT
    FROM Products
    WHERE IsActive = 1 AND
          (
            Name LIKE N'%' + @SearchTerm + N'%'
            OR Description LIKE N'%' + @SearchTerm + N'%'
            OR Category LIKE N'%' + @SearchTerm + N'%'
            OR Code LIKE '%' + @SearchTerm + '%'
          )
    ORDER BY CreatedAt DESC
    OFFSET (@PageNumberValue - 1) * @PageSizeValue ROWS FETCH NEXT @PageSizeValue ROWS ONLY;
END
GO

-- 4. usp_FilterProducts
CREATE PROCEDURE dbo.usp_ProductFilter
    @Category NVARCHAR(MAX) = NULL,
    @MinPrice NVARCHAR(50) = NULL,
    @MaxPrice NVARCHAR(50) = NULL,
    @SupplierId NVARCHAR(50) = NULL,
    @PageNumber NVARCHAR(50) = N'1',
    @PageSize NVARCHAR(50) = N'10'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @MinPriceValue DECIMAL(18,2) = TRY_CONVERT(DECIMAL(18,2), @MinPrice);
    DECLARE @MaxPriceValue DECIMAL(18,2) = TRY_CONVERT(DECIMAL(18,2), @MaxPrice);
    DECLARE @PageNumberValue INT = TRY_CONVERT(INT, @PageNumber);
    DECLARE @PageSizeValue INT = TRY_CONVERT(INT, @PageSize);
    IF (@MinPrice IS NOT NULL AND @MinPriceValue IS NULL)
       OR (@MaxPrice IS NOT NULL AND @MaxPriceValue IS NULL)
       OR @PageNumberValue IS NULL OR @PageSizeValue IS NULL
    BEGIN
        RAISERROR(N'Invalid filter input', 16, 1);
        RETURN;
    END
    SELECT Id, Code, Name, Description, Category, Price, TaxRate, Stock, SupplierId, CreatedAt, UpdatedAt, IsActive, ExpiredDT
    FROM Products
    WHERE IsActive = 1
        AND (@Category IS NULL OR Category = @Category)
        AND (@MinPriceValue IS NULL OR Price >= @MinPriceValue)
        AND (@MaxPriceValue IS NULL OR Price <= @MaxPriceValue)
        AND (@SupplierId IS NULL OR SupplierId = @SupplierId)
    ORDER BY CreatedAt DESC
    OFFSET (@PageNumberValue - 1) * @PageSizeValue ROWS FETCH NEXT @PageSizeValue ROWS ONLY;
END
GO

-- 5. usp_GetProductById
CREATE PROCEDURE dbo.usp_ProductGetById
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Code, Name, Description, Category, Price, TaxRate, Stock, SupplierId, CreatedAt, UpdatedAt, IsActive, ExpiredDT
    FROM Products
    WHERE Id = @Id AND IsActive = 1;
END
GO

-- 6. usp_GetProductByName
CREATE PROCEDURE dbo.usp_ProductGetByName
    @Name NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Code, Name, Description, Category, Price, TaxRate, Stock, SupplierId, CreatedAt, UpdatedAt, IsActive, ExpiredDT
    FROM Products
    WHERE Name = @Name AND IsActive = 1;
END
GO

-- 7. usp_GetProductByCategory
CREATE PROCEDURE dbo.usp_ProductGetByCategory
    @Category NVARCHAR(MAX),
    @PageNumber NVARCHAR(50) = N'1',
    @PageSize NVARCHAR(50) = N'10'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PageNumberValue INT = TRY_CONVERT(INT, @PageNumber);
    DECLARE @PageSizeValue INT = TRY_CONVERT(INT, @PageSize);
    IF @PageNumberValue IS NULL OR @PageSizeValue IS NULL
    BEGIN
        RAISERROR(N'Invalid paging input', 16, 1);
        RETURN;
    END
    SELECT Id, Code, Name, Description, Category, Price, TaxRate, Stock, SupplierId, CreatedAt, UpdatedAt, IsActive, ExpiredDT
    FROM Products
    WHERE Category = @Category AND IsActive = 1
    ORDER BY CreatedAt DESC
    OFFSET (@PageNumberValue - 1) * @PageSizeValue ROWS FETCH NEXT @PageSizeValue ROWS ONLY;
END
GO

-- 8. usp_DeleteProduct
CREATE PROCEDURE dbo.usp_ProductDelete
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Products WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Product not found or already deleted', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Products SET IsActive = 0, UpdatedAt = SYSUTCDATETIME() WHERE Id = @Id;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- DECLARE @ProdDeleteId CHAR(10); SELECT TOP 1 @ProdDeleteId=Id FROM Products WHERE IsActive=1; EXEC dbo.usp_ProductDelete @Id=@ProdDeleteId;

------------------------------------------------
-- CARTS CRUD PROCEDURES
------------------------------------------------
CREATE PROCEDURE dbo.usp_CartGetAllItemByCustomerId
    @CustomerId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Customers WHERE Id = @CustomerId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Customer does not exist', 16, 1);
        RETURN;
    END

    SELECT
        ci.Id AS Id,
        ci.Code AS Code,
        ci.CartId,
        ci.ProductId,
        ci.Quantity,
        ci.Price,
        ci.CreatedAt,
        ci.UpdatedAt,
        ci.IsActive
    FROM CartItems ci
    LEFT JOIN Carts c ON ci.CartId = c.Id
    LEFT JOIN Customers cu ON c.CustomerId = cu.Id
    WHERE ci.IsActive = 1
    AND cu.Id = @CustomerId AND cu.IsActive = 1;
END
GO

CREATE PROCEDURE dbo.usp_CartCreate
    @CustomerId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Customers WHERE Id = @CustomerId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Customer does not exist', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Carts(CustomerId) VALUES (@CustomerId);
            UPDATE c
            SET c.CustomerName = cu.Name,
                c.CustomerCode = cu.Code
            FROM Carts c
            JOIN Customers cu ON cu.Id = c.CustomerId
            WHERE c.Id = (SELECT TOP 1 Id FROM Carts WHERE CustomerId = @CustomerId ORDER BY CreatedAt DESC);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- DECLARE @CartForCusId CHAR(10); SELECT TOP 1 @CartForCusId=Id FROM Customers; EXEC dbo.usp_CartCreate @CustomerId=@CartForCusId;

CREATE PROCEDURE dbo.usp_CartAddItem
    @CartId NVARCHAR(50),
    @ProductId NVARCHAR(50),
    @Quantity NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @QuantityValue INT = TRY_CONVERT(INT, @Quantity);
    IF @QuantityValue IS NULL
    BEGIN
        RAISERROR(N'Invalid quantity input', 16, 1);
        RETURN;
    END

    -- Validate Cart
    IF NOT EXISTS (SELECT 1 FROM Carts WHERE Id = @CartId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Cart does not exist', 16, 1);
        RETURN;
    END

    -- Validate Product
    IF NOT EXISTS (SELECT 1 FROM Products WHERE Id = @ProductId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Product does not exist', 16, 1);
        RETURN;
    END

    -- Business check: stock available
    IF @QuantityValue > (SELECT Stock FROM Products WHERE Id = @ProductId)
    BEGIN
        RAISERROR(N'Not enough stock for this product', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            DECLARE @UnitPrice DECIMAL(18,2);
            DECLARE @TaxRate DECIMAL(3,2);
            DECLARE @LineSubTotal DECIMAL(18,2);
            DECLARE @LinePrice DECIMAL(18,2);

            SELECT
                @UnitPrice = p.Price,
                @TaxRate = p.TaxRate
            FROM Products p
            WHERE p.Id = @ProductId AND p.IsActive = 1;

            SET @LineSubTotal = CAST(@QuantityValue * @UnitPrice AS DECIMAL(18,2));
            SET @LinePrice = dbo.fn_ApplyTax(@LineSubTotal, @TaxRate);

            -- If item exists in the cart, update quantity, else insert new
            IF EXISTS (SELECT 1 FROM CartItems WHERE CartId = @CartId AND ProductId = @ProductId AND IsActive = 1)
            BEGIN
                DECLARE @CurrentQty INT = 0;
                SELECT TOP 1 @CurrentQty = Quantity
                FROM CartItems
                WHERE CartId = @CartId AND ProductId = @ProductId AND IsActive = 1;

                DECLARE @NewQty INT = @CurrentQty + @QuantityValue;
                DECLARE @NewLineSubTotal DECIMAL(18,2) = CAST(@NewQty * @UnitPrice AS DECIMAL(18,2));
                DECLARE @NewLinePrice DECIMAL(18,2) = dbo.fn_ApplyTax(@NewLineSubTotal, @TaxRate);

                UPDATE ci
                SET ci.Quantity = @NewQty,
                    ci.ProductName = p.Name,
                    ci.ProductCode = p.Code,
                    ci.Price = @NewLinePrice,
                    ci.UpdatedAt = SYSUTCDATETIME()
                FROM CartItems ci
                JOIN Products p ON p.Id = ci.ProductId
                WHERE ci.CartId = @CartId AND ci.ProductId = @ProductId AND ci.IsActive = 1;
            END
            ELSE
            BEGIN
                INSERT INTO CartItems (CartId, ProductId, ProductName, ProductCode, Quantity, Price)
                SELECT @CartId, @ProductId, p.Name, p.Code, @QuantityValue, @LinePrice
                FROM Products p
                WHERE p.Id = @ProductId;
            END

            UPDATE Products
            SET Stock = Stock - @QuantityValue, UpdatedAt = SYSUTCDATETIME()
            WHERE Id = @ProductId AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_CartDeleteItem
    @CartId NVARCHAR(50),
    @ProductId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validate Cart
    IF NOT EXISTS (SELECT 1 FROM Carts WHERE Id = @CartId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Cart does not exist', 16, 1);
        RETURN;
    END
    -- Validate Product
    IF NOT EXISTS (SELECT 1 FROM Products WHERE Id = @ProductId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Product does not exist', 16, 1);
        RETURN;
    END
    -- See if the item exists in cart
    IF NOT EXISTS (SELECT 1 FROM CartItems WHERE CartId = @CartId AND ProductId = @ProductId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Item does not exist in the cart', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            DELETE FROM CartItems WHERE ProductId = @ProductId;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- DECLARE @DCId CHAR(10), @DPId CHAR(10); SELECT TOP 1 @DCId=Id FROM Carts; SELECT TOP 1 @DPId=Id FROM Products; EXEC dbo.usp_CartDeleteItem @CartId=@DCId, @ProductId=@DPId;

CREATE PROCEDURE dbo.usp_CartDelete
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Carts WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Cart not found or already deleted', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Carts SET IsActive = 0, UpdatedAt = SYSUTCDATETIME() WHERE Id = @Id;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

------------------------------------------------
-- ORDERS CRUD PROCEDURES
------------------------------------------------
CREATE PROCEDURE dbo.usp_OrderGetAll
    @PageNumber NVARCHAR(50) = N'1',
    @PageSize NVARCHAR(50) = N'10'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PageNumberValue INT = TRY_CONVERT(INT, @PageNumber);
    DECLARE @PageSizeValue INT = TRY_CONVERT(INT, @PageSize);
    IF @PageNumberValue IS NULL OR @PageSizeValue IS NULL
    BEGIN
        RAISERROR(N'Invalid paging input', 16, 1);
        RETURN;
    END
    SELECT 
        o.Id,
        o.Code,
        o.CustomerId,
        o.Status,
        o.SubTotal,
        o.TaxTotal,
        o.TotalAmount,
        o.CreatedAt,
        o.UpdatedAt,
        o.IsActive
    FROM Orders o
    WHERE o.IsActive = 1
    ORDER BY o.CreatedAt DESC
    OFFSET (@PageNumberValue - 1) * @PageSizeValue ROWS FETCH NEXT @PageSizeValue ROWS ONLY;
END
GO

-- Get order by id
CREATE PROCEDURE dbo.usp_OrderGetById
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        o.Id,
        o.Code,
        o.CustomerId,
        o.Status,
        o.SubTotal,
        o.TaxTotal,
        o.TotalAmount,
        o.CreatedAt,
        o.UpdatedAt,
        o.IsActive
    FROM Orders o
    WHERE o.Id = @Id AND o.IsActive = 1;
END
GO

-- Get order by code
CREATE PROCEDURE dbo.usp_OrderGetByCode
    @Code NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        o.Id,
        o.Code,
        o.CustomerId,
        o.Status,
        o.SubTotal,
        o.TaxTotal,
        o.TotalAmount,
        o.CreatedAt,
        o.UpdatedAt,
        o.IsActive
    FROM Orders o
    WHERE o.Code = @Code AND o.IsActive = 1;
END
GO

-- DECLARE @OrderTestCode VARCHAR(20); SELECT TOP 1 @OrderTestCode=Code FROM Orders; EXEC dbo.usp_OrderGetByCode @Code=@OrderTestCode;

CREATE PROCEDURE dbo.usp_OrderGetByCustomerId
    @CustomerId NVARCHAR(50),
    @PageNumber NVARCHAR(50) = N'1',
    @PageSize NVARCHAR(50) = N'10'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PageNumberValue INT = TRY_CONVERT(INT, @PageNumber);
    DECLARE @PageSizeValue INT = TRY_CONVERT(INT, @PageSize);
    IF @PageNumberValue IS NULL OR @PageSizeValue IS NULL
    BEGIN
        RAISERROR(N'Invalid paging input', 16, 1);
        RETURN;
    END
    SELECT 
        o.Id,
        o.Code,
        o.CustomerId,
        o.Status,
        o.SubTotal,
        o.TaxTotal,
        o.TotalAmount,
        o.CreatedAt,
        o.UpdatedAt,
        o.IsActive
    FROM Orders o
    WHERE o.CustomerId = @CustomerId AND o.IsActive = 1
    ORDER BY o.CreatedAt DESC
    OFFSET (@PageNumberValue - 1) * @PageSizeValue ROWS FETCH NEXT @PageSizeValue ROWS ONLY;
END
GO

CREATE PROCEDURE dbo.usp_OrderGetDetail
    @OrderId NVARCHAR(50),
    @PageNumber NVARCHAR(50) = N'1',
    @PageSize NVARCHAR(50) = N'10'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PageNumberValue INT = TRY_CONVERT(INT, @PageNumber);
    DECLARE @PageSizeValue INT = TRY_CONVERT(INT, @PageSize);
    IF @PageNumberValue IS NULL OR @PageSizeValue IS NULL
    BEGIN
        RAISERROR(N'Invalid paging input', 16, 1);
        RETURN;
    END
    SELECT 
        oi.Id,
        oi.Code,
        oi.ProductId,
        oi.Quantity,
        oi.UnitPrice,
        oi.TaxRate,
        oi.LineSubTotal,
        oi.TaxAmount,
        oi.LineTotal,
        oi.CreatedAt,
        oi.UpdatedAt,
        oi.IsActive
    FROM OrderItems oi
    WHERE oi.OrderId = @OrderId AND oi.IsActive = 1
    ORDER BY oi.CreatedAt DESC
    OFFSET (@PageNumberValue - 1) * @PageSizeValue ROWS FETCH NEXT @PageSizeValue ROWS ONLY;
END
GO

CREATE TYPE dbo.OrderItemType AS TABLE
(
    ProductId CHAR(10),
    Quantity INT
);
GO

CREATE PROCEDURE dbo.usp_OrderCreate
    @CustomerId NVARCHAR(50),
    @CreateOrderBody dbo.OrderItemType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Customers WHERE Id = @CustomerId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Customer does not exist', 16, 1);
        RETURN;
    END

    -- Validate product existence
    IF EXISTS (
        SELECT 1
        FROM @CreateOrderBody oitm
        WHERE NOT EXISTS (SELECT 1 FROM Products WHERE Id = oitm.ProductId AND IsActive = 1)
    )
    BEGIN
        RAISERROR(N'One or more products do not exist or are inactive', 16, 1);
        RETURN;
    END

    -- Validate quantity
    IF EXISTS (SELECT 1 FROM @CreateOrderBody oitm WHERE oitm.Quantity <= 0)
    BEGIN
        RAISERROR(N'One or more items have invalid quantity (must be > 0)', 16, 1);
        RETURN;
    END

    -- Business check: stock available for all items
    IF EXISTS (
        SELECT 1
        FROM (
            SELECT ProductId, SUM(Quantity) AS RequestedQty
            FROM @CreateOrderBody
            GROUP BY ProductId
        ) rq
        JOIN Products p ON p.Id = rq.ProductId AND p.IsActive = 1
        WHERE p.Stock < rq.RequestedQty
    )
    BEGIN
        RAISERROR(N'Not enough stock for one or more products', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Orders(CustomerId, CustomerName, CustomerCode)
            SELECT @CustomerId, c.Name, c.Code
            FROM Customers c
            WHERE c.Id = @CustomerId;

            DECLARE @OrderId CHAR(10);
            SELECT TOP 1 @OrderId = Id FROM Orders WHERE CustomerId = @CustomerId ORDER BY CreatedAt DESC;

            INSERT INTO OrderItems(OrderId, ProductId, ProductName, ProductCode, SupplierId, SupplierName, SupplierCode, Quantity, UnitPrice, TaxRate, LineSubTotal, TaxAmount, LineTotal)
            SELECT
                @OrderId,
                oi.ProductId,
                p.Name,
                p.Code,
                p.SupplierId,
                s.Name,
                s.Code,
                oi.Quantity,
                p.Price,
                p.TaxRate,
                CAST(oi.Quantity * p.Price AS DECIMAL(18,2)),
                dbo.fn_GetTaxAmount(CAST(oi.Quantity * p.Price AS DECIMAL(18,2)), p.TaxRate),
                dbo.fn_ApplyTax(CAST(oi.Quantity * p.Price AS DECIMAL(18,2)), p.TaxRate)
            FROM @CreateOrderBody oi
            JOIN Products p ON p.Id = oi.ProductId AND p.IsActive = 1
            LEFT JOIN Suppliers s ON s.Id = p.SupplierId;

            UPDATE p
            SET p.Stock = p.Stock - rq.RequestedQty,
                p.UpdatedAt = SYSUTCDATETIME()
            FROM Products p
            JOIN (
                SELECT ProductId, SUM(Quantity) AS RequestedQty
                FROM @CreateOrderBody
                GROUP BY ProductId
            ) rq ON rq.ProductId = p.Id;

            -- Ensure order header totals are synchronized.
            UPDATE o
            SET
                o.SubTotal = x.SubTotal,
                o.TaxTotal = x.TaxTotal,
                o.TotalAmount = x.TotalAmount,
                o.UpdatedAt = SYSUTCDATETIME()
            FROM Orders o
            JOIN (
                SELECT
                    oi.OrderId,
                    CAST(COALESCE(SUM(oi.LineSubTotal), 0) AS DECIMAL(18,2)) AS SubTotal,
                    CAST(COALESCE(SUM(oi.TaxAmount), 0) AS DECIMAL(18,2)) AS TaxTotal,
                    CAST(COALESCE(SUM(oi.LineTotal), 0) AS DECIMAL(18,2)) AS TotalAmount
                FROM OrderItems oi
                WHERE oi.IsActive = 1
                GROUP BY oi.OrderId
            ) x ON x.OrderId = o.Id
            WHERE o.Id = @OrderId;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_OrderCreateByCartId
    @CartId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validation: Cart must exist and be active
    IF NOT EXISTS (SELECT 1 FROM Carts WHERE Id = @CartId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Cart does not exist', 16, 1);
        RETURN;
    END

    DECLARE @CustomerId NVARCHAR(50);
    SELECT TOP 1 @CustomerId = CustomerId FROM Carts WHERE Id = @CartId;

    IF @CustomerId IS NULL OR NOT EXISTS (SELECT 1 FROM Customers WHERE Id = @CustomerId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Customer does not exist for this cart', 16, 1);
        RETURN;
    END

    -- Validate Cart has items
    IF NOT EXISTS (SELECT 1 FROM CartItems WHERE CartId = @CartId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Cart has no items', 16, 1);
        RETURN;
    END

    -- Business check: stock available for all cart items
    IF EXISTS (
        SELECT 1
        FROM (
            SELECT ProductId, SUM(Quantity) AS RequestedQty
            FROM CartItems
            WHERE CartId = @CartId AND IsActive = 1
            GROUP BY ProductId
        ) rq
        JOIN Products p ON p.Id = rq.ProductId AND p.IsActive = 1
        WHERE p.Stock < rq.RequestedQty
    )
    BEGIN
        RAISERROR(N'Not enough stock for one or more products', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

            INSERT INTO Orders(CustomerId, CustomerName, CustomerCode)
            SELECT @CustomerId, c.Name, c.Code
            FROM Customers c
            WHERE c.Id = @CustomerId;

            DECLARE @OrderId NVARCHAR(50);
            SELECT TOP 1 @OrderId = Id FROM Orders WHERE CustomerId = @CustomerId ORDER BY CreatedAt DESC;

            INSERT INTO OrderItems(OrderId, ProductId, ProductName, ProductCode, SupplierId, SupplierName, SupplierCode, Quantity, UnitPrice, TaxRate, LineSubTotal, TaxAmount, LineTotal)
            SELECT
                @OrderId,
                ci.ProductId,
                p.Name,
                p.Code,
                p.SupplierId,
                s.Name,
                s.Code,
                ci.Quantity,
                p.Price,
                p.TaxRate,
                CAST(ci.Quantity * p.Price AS DECIMAL(18,2)),
                dbo.fn_GetTaxAmount(CAST(ci.Quantity * p.Price AS DECIMAL(18,2)), p.TaxRate),
                dbo.fn_ApplyTax(CAST(ci.Quantity * p.Price AS DECIMAL(18,2)), p.TaxRate)
            FROM CartItems ci
            JOIN Products p ON p.Id = ci.ProductId AND p.IsActive = 1
            LEFT JOIN Suppliers s ON s.Id = p.SupplierId
            WHERE ci.CartId = @CartId AND ci.IsActive = 1;

            UPDATE p
            SET p.Stock = p.Stock - rq.RequestedQty,
                p.UpdatedAt = SYSUTCDATETIME()
            FROM Products p
            JOIN (
                SELECT ProductId, SUM(Quantity) AS RequestedQty
                FROM CartItems
                WHERE CartId = @CartId AND IsActive = 1
                GROUP BY ProductId
            ) rq ON rq.ProductId = p.Id;

            -- Sync order totals after item insert.
            UPDATE o
            SET
                o.SubTotal = x.SubTotal,
                o.TaxTotal = x.TaxTotal,
                o.TotalAmount = x.TotalAmount,
                o.UpdatedAt = SYSUTCDATETIME()
            FROM Orders o
            JOIN (
                SELECT
                    oi.OrderId,
                    CAST(COALESCE(SUM(oi.LineSubTotal), 0) AS DECIMAL(18,2)) AS SubTotal,
                    CAST(COALESCE(SUM(oi.TaxAmount), 0) AS DECIMAL(18,2)) AS TaxTotal,
                    CAST(COALESCE(SUM(oi.LineTotal), 0) AS DECIMAL(18,2)) AS TotalAmount
                FROM OrderItems oi
                WHERE oi.IsActive = 1
                GROUP BY oi.OrderId
            ) x ON x.OrderId = o.Id
            WHERE o.Id = @OrderId;

            -- Optionally, mark cart as inactive (if a cart can only be ordered once)
            UPDATE Carts SET IsActive = 0 WHERE Id = @CartId;

            -- Optionally, mark cart items as inactive (or delete them)
            UPDATE CartItems SET IsActive = 0 WHERE CartId = @CartId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- Update an existing order items by TVP (upsert by ProductId)
CREATE PROCEDURE dbo.usp_OrderUpdate
    @Id NVARCHAR(50),
    @CustomerId NVARCHAR(50),
    @Status NVARCHAR(MAX) = N'Placed',
    @UpdateOrderBody dbo.OrderItemType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @InputItems TABLE
    (
        ProductId CHAR(10) PRIMARY KEY,
        Quantity INT NOT NULL
    );

    -- Validate duplicate product rows in TVP
    IF EXISTS (
        SELECT 1
        FROM @UpdateOrderBody
        GROUP BY ProductId
        HAVING COUNT(*) > 1
    )
    BEGIN
        RAISERROR(N'Duplicate ProductId in update order body', 16, 1);
        RETURN;
    END

    INSERT INTO @InputItems(ProductId, Quantity)
    SELECT
        ProductId,
        Quantity
    FROM @UpdateOrderBody;

    -- Validate order existence
    IF NOT EXISTS (SELECT 1 FROM Orders WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Order not found or already deleted', 16, 1);
        RETURN;
    END

    -- Validate customer existence
    IF NOT EXISTS (SELECT 1 FROM Customers WHERE Id = @CustomerId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Customer does not exist', 16, 1);
        RETURN;
    END

    -- Validate update body has data
    IF NOT EXISTS (SELECT 1 FROM @InputItems)
    BEGIN
        RAISERROR(N'Update order body is empty', 16, 1);
        RETURN;
    END

    -- Validate product existence (separate)
    IF EXISTS (
        SELECT 1
        FROM @InputItems oitm
        WHERE NOT EXISTS (SELECT 1 FROM Products WHERE Id = oitm.ProductId AND IsActive = 1)
    )
    BEGIN
        RAISERROR(N'One or more products do not exist or are inactive', 16, 1);
        RETURN;
    END

    -- Validate quantity (separate)
    IF EXISTS (SELECT 1 FROM @InputItems oitm WHERE oitm.Quantity <= 0)
    BEGIN
        RAISERROR(N'One or more items have invalid quantity (must be > 0)', 16, 1);
        RETURN;
    END

    IF EXISTS (
        SELECT 1
        FROM @InputItems i
        OUTER APPLY (
            SELECT COALESCE(SUM(oi.Quantity), 0) AS CurrentQty
            FROM OrderItems oi
            WHERE oi.OrderId = @Id
              AND oi.IsActive = 1
              AND oi.ProductId = i.ProductId
        ) cur
        JOIN Products p ON p.Id = i.ProductId AND p.IsActive = 1
        WHERE (i.Quantity - cur.CurrentQty) > 0
          AND p.Stock < (i.Quantity - cur.CurrentQty)
    )
    BEGIN
        RAISERROR(N'Not enough stock for one or more products', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;

            DECLARE @Delta TABLE
            (
                ProductId CHAR(10) PRIMARY KEY,
                DeltaQty INT NOT NULL
            );

            INSERT INTO @Delta(ProductId, DeltaQty)
            SELECT
                i.ProductId,
                i.Quantity - COALESCE(cur.CurrentQty, 0) AS DeltaQty
            FROM @InputItems i
            OUTER APPLY (
                SELECT COALESCE(SUM(oi.Quantity), 0) AS CurrentQty
                FROM OrderItems oi
                WHERE oi.OrderId = @Id
                  AND oi.IsActive = 1
                  AND oi.ProductId = i.ProductId
            ) cur;

            -- Update existing active order items that appear in TVP.
            UPDATE oi
            SET
                oi.Quantity = i.Quantity,
                oi.ProductName = p.Name,
                oi.ProductCode = p.Code,
                oi.SupplierId = p.SupplierId,
                oi.SupplierName = s.Name,
                oi.SupplierCode = s.Code,
                oi.UnitPrice = p.Price,
                oi.TaxRate = p.TaxRate,
                oi.LineSubTotal = CAST(i.Quantity * p.Price AS DECIMAL(18,2)),
                oi.TaxAmount = dbo.fn_GetTaxAmount(CAST(i.Quantity * p.Price AS DECIMAL(18,2)), p.TaxRate),
                oi.LineTotal = dbo.fn_ApplyTax(CAST(i.Quantity * p.Price AS DECIMAL(18,2)), p.TaxRate),
                oi.UpdatedAt = SYSUTCDATETIME()
            FROM OrderItems oi
            JOIN @InputItems i ON i.ProductId = oi.ProductId
            JOIN Products p ON p.Id = i.ProductId AND p.IsActive = 1
            LEFT JOIN Suppliers s ON s.Id = p.SupplierId
            WHERE oi.OrderId = @Id AND oi.IsActive = 1;

            -- Insert missing order items from TVP.
            INSERT INTO OrderItems(OrderId, ProductId, ProductName, ProductCode, SupplierId, SupplierName, SupplierCode, Quantity, UnitPrice, TaxRate, LineSubTotal, TaxAmount, LineTotal)
            SELECT
                @Id,
                i.ProductId,
                p.Name,
                p.Code,
                p.SupplierId,
                s.Name,
                s.Code,
                i.Quantity,
                p.Price,
                p.TaxRate,
                CAST(i.Quantity * p.Price AS DECIMAL(18,2)),
                dbo.fn_GetTaxAmount(CAST(i.Quantity * p.Price AS DECIMAL(18,2)), p.TaxRate),
                dbo.fn_ApplyTax(CAST(i.Quantity * p.Price AS DECIMAL(18,2)), p.TaxRate)
            FROM @InputItems i
            JOIN Products p ON p.Id = i.ProductId AND p.IsActive = 1
            LEFT JOIN Suppliers s ON s.Id = p.SupplierId
            LEFT JOIN OrderItems oi
                ON oi.OrderId = @Id
               AND oi.ProductId = i.ProductId
               AND oi.IsActive = 1
            WHERE oi.Id IS NULL;

            -- Apply stock delta: stock = stock - (NewQty - CurrentQty).
            UPDATE p
            SET p.Stock = p.Stock - d.DeltaQty,
                p.UpdatedAt = SYSUTCDATETIME()
            FROM Products p
            JOIN @Delta d ON d.ProductId = p.Id
            WHERE d.DeltaQty <> 0;

            UPDATE o
            SET
                o.CustomerId = @CustomerId,
                o.CustomerName = c.Name,
                o.CustomerCode = c.Code,
                o.Status = @Status,
                o.SubTotal = x.SubTotal,
                o.TaxTotal = x.TaxTotal,
                o.TotalAmount = x.TotalAmount,
                o.UpdatedAt = SYSUTCDATETIME()
            FROM Orders o
            JOIN Customers c ON c.Id = @CustomerId
            JOIN (
                SELECT
                    oi.OrderId,
                    CAST(COALESCE(SUM(oi.LineSubTotal), 0) AS DECIMAL(18,2)) AS SubTotal,
                    CAST(COALESCE(SUM(oi.TaxAmount), 0) AS DECIMAL(18,2)) AS TaxTotal,
                    CAST(COALESCE(SUM(oi.LineTotal), 0) AS DECIMAL(18,2)) AS TotalAmount
                FROM OrderItems oi
                WHERE oi.OrderId = @Id
                  AND oi.IsActive = 1
                GROUP BY oi.OrderId
            ) x ON x.OrderId = o.Id
            WHERE o.Id = @Id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- Soft delete order (set IsActive = 0), also mark its items as not active
CREATE PROCEDURE dbo.usp_OrderDelete
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Orders WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Order not found or already deleted', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Orders SET IsActive = 0, UpdatedAt = SYSUTCDATETIME() WHERE Id = @Id;
            UPDATE OrderItems SET IsActive = 0, UpdatedAt = SYSUTCDATETIME() WHERE OrderId = @Id AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- DECLARE @OIDToDel CHAR(10); SELECT TOP 1 @OIDToDel=Id FROM Orders WHERE IsActive=1; EXEC dbo.usp_OrderDelete @Id=@OIDToDel;

------------------------------------------------
-- PRODUCT RATINGS CRUD PROCEDURES
------------------------------------------------
CREATE PROCEDURE dbo.usp_ProductRatingGetAll
    @PageNumber NVARCHAR(50) = N'1',
    @PageSize NVARCHAR(50) = N'10'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PageNumberValue INT = TRY_CONVERT(INT, @PageNumber);
    DECLARE @PageSizeValue INT = TRY_CONVERT(INT, @PageSize);
    IF @PageNumberValue IS NULL OR @PageSizeValue IS NULL
    BEGIN
        RAISERROR(N'Invalid paging input', 16, 1);
        RETURN;
    END
    SELECT Id, Code, ProductId, CustomerId, Stars, Content, CreatedAt, UpdatedAt, IsActive
    FROM ProductRatings
    WHERE IsActive = 1
    ORDER BY CreatedAt DESC
    OFFSET (@PageNumberValue - 1) * @PageSizeValue ROWS FETCH NEXT @PageSizeValue ROWS ONLY;
END
GO

-- EXEC dbo.usp_GetAllProductRatings @PageNumber=1, @PageSize=3;

CREATE PROCEDURE dbo.usp_ProductRatingGetById
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Code, ProductId, CustomerId, Stars, Content, CreatedAt, UpdatedAt, IsActive
    FROM ProductRatings
    WHERE Id = @Id AND IsActive = 1;
END
GO

-- DECLARE @RatingSampleId CHAR(10); SELECT TOP 1 @RatingSampleId=Id FROM ProductRatings; EXEC dbo.usp_GetProductRatingById @Id=@RatingSampleId;

CREATE PROCEDURE dbo.usp_ProductRatingGetByCode
    @Code NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Code, ProductId, CustomerId, Stars, Content, CreatedAt, UpdatedAt, IsActive
    FROM ProductRatings
    WHERE Code = @Code AND IsActive = 1;
END
GO

CREATE PROCEDURE dbo.usp_ProductRatingCreate
    @ProductId NVARCHAR(50),
    @OrderId NVARCHAR(50),
    @Stars NVARCHAR(50),
    @Content NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @StarsValue INT = TRY_CONVERT(INT, @Stars);
    IF @StarsValue IS NULL
    BEGIN
        RAISERROR(N'Invalid Stars input', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Products WHERE Id = @ProductId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Product does not exist', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Orders WHERE Id = @OrderId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Order does not exist', 16, 1);
        RETURN;
    END
    
    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO ProductRatings(ProductId, ProductName, ProductCode, CustomerId, OrderId, OrderName, OrderCode, Stars, Content)
            SELECT
                @ProductId,
                p.Name,
                p.Code,
                o.CustomerId,
                @OrderId,
                o.Status,
                o.Code,
                @StarsValue,
                @Content
            FROM Products p
            JOIN Orders o ON o.Id = @OrderId
            WHERE p.Id = @ProductId;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_ProductRatingUpdate
    @Id NVARCHAR(50),
    @Stars NVARCHAR(50),
    @Content NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @StarsValue INT = TRY_CONVERT(INT, @Stars);
    IF @StarsValue IS NULL
    BEGIN
        RAISERROR(N'Invalid Stars input', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM ProductRatings WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'ProductRating not found or already deleted', 16, 1);
        RETURN;
    END

    -- Validate stars (should be 1-5)
    IF @StarsValue < 1 OR @StarsValue > 5
    BEGIN
        RAISERROR(N'Invalid Stars rating. Value must be between 1 and 5.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE pr
            SET Stars = @StarsValue,
                Content = @Content,
                ProductName = p.Name,
                ProductCode = p.Code,
                CustomerId = o.CustomerId,
                OrderName = o.Status,
                OrderCode = o.Code,
                UpdatedAt = SYSUTCDATETIME()
            FROM ProductRatings pr
            LEFT JOIN Products p ON p.Id = pr.ProductId
            LEFT JOIN Orders o ON o.Id = pr.OrderId
            WHERE pr.Id = @Id;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_ProductRatingDelete
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM ProductRatings WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'ProductRating not found or already deleted', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE ProductRatings SET IsActive = 0, UpdatedAt = SYSUTCDATETIME() WHERE Id = @Id;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

------------------------------------------------
-- SECTION AUTH: USERS / ROLES / PERMISSIONS
------------------------------------------------

-- Sequences
CREATE SEQUENCE dbo.Seq_User AS BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999 NO CYCLE;
GO
CREATE SEQUENCE dbo.Seq_Role AS BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999 NO CYCLE;
GO
CREATE SEQUENCE dbo.Seq_Permission AS BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999 NO CYCLE;
GO

CREATE SEQUENCE dbo.Seq_UserToken AS BIGINT START WITH 1 INCREMENT BY 1 MINVALUE 1 MAXVALUE 999999999 NO CYCLE;
GO

-- Users Table
CREATE TABLE Users (
    Id CHAR(10) NOT NULL PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1
);
GO

ALTER TABLE Customers
    ADD CONSTRAINT FK_Customers_Users FOREIGN KEY (UserId) REFERENCES Users(Id);
GO

-- Roles Table
CREATE TABLE Roles (
    Id CHAR(10) NOT NULL PRIMARY KEY,
    Code VARCHAR(20) NOT NULL UNIQUE,
    Name NVARCHAR(255) NOT NULL UNIQUE,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1
);
GO

-- Permissions Table
CREATE TABLE Permissions (
    Id CHAR(10) NOT NULL PRIMARY KEY,
    Code VARCHAR(20) NOT NULL UNIQUE,
    Name NVARCHAR(255) NOT NULL UNIQUE,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1
);
GO

-- UserRoles Table (join)
CREATE TABLE UserRoles (
    UserId CHAR(10) NOT NULL,
    RoleId CHAR(10) NOT NULL,
    RoleCode VARCHAR(20) NOT NULL,
    RoleName NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1,
    CONSTRAINT PK_UserRoles PRIMARY KEY (UserId, RoleId)
);
GO

-- UserPermissions Table (join)
CREATE TABLE UserPermissions (
    UserId CHAR(10) NOT NULL,
    PermissionId CHAR(10) NOT NULL,
    PermissionCode VARCHAR(20) NOT NULL,
    PermissionName NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1,
    CONSTRAINT PK_UserPermissions PRIMARY KEY (UserId, PermissionId)
);
GO

-- RolePermissions Table (join)
CREATE TABLE RolePermissions (
    RoleId CHAR(10) NOT NULL,
    PermissionId CHAR(10) NOT NULL,
    PermissionCode VARCHAR(20) NOT NULL,
    PermissionName NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME NULL,
    IsActive BIT DEFAULT 1,
    CONSTRAINT PK_RolePermissions PRIMARY KEY (RoleId, PermissionId)
);
GO

-- UserToken (auth token store)
CREATE TABLE UserToken (
    Id CHAR(10) NOT NULL PRIMARY KEY,
    UserId CHAR(10) NOT NULL,
    RefreshToken NVARCHAR(2000) NOT NULL,
    IssuedAt DATETIME NULL,
    ExpiresAt DATETIME NULL,
    RevokedAt DATETIME NULL,
    IsActive BIT DEFAULT 1
);
GO

-- FK Constraints
ALTER TABLE UserRoles
    ADD CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserId) REFERENCES Users(Id);
GO

ALTER TABLE UserRoles
    ADD CONSTRAINT FK_UserRoles_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id);
GO

ALTER TABLE UserPermissions
    ADD CONSTRAINT FK_UserPermissions_Users FOREIGN KEY (UserId) REFERENCES Users(Id);
GO

ALTER TABLE UserPermissions
    ADD CONSTRAINT FK_UserPermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES Permissions(Id);
GO

ALTER TABLE RolePermissions
    ADD CONSTRAINT FK_RolePermissions_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id);
GO

ALTER TABLE RolePermissions
    ADD CONSTRAINT FK_RolePermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES Permissions(Id);
GO

ALTER TABLE UserToken
    ADD CONSTRAINT FK_UserToken_Users FOREIGN KEY (UserId) REFERENCES Users(Id);
GO

-- TVP: batch id lists (user-role, user-permission, role-permission grant/revoke)
CREATE TYPE dbo.Tvp_IdList AS TABLE (
    Id NVARCHAR(50) NOT NULL
);
GO

CREATE TRIGGER dbo.trg_UserToken_InsteadOfInsert
ON dbo.UserToken
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.UserToken (Id, UserId, RefreshToken, IssuedAt, ExpiresAt, RevokedAt, IsActive)
    SELECT
        N'W' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_UserToken AS NVARCHAR(9)), 9),
        i.UserId,
        i.RefreshToken,
        i.IssuedAt,
        i.ExpiresAt,
        i.RevokedAt,
        ISNULL(i.IsActive, CAST(1 AS BIT))
    FROM inserted AS i;
END
GO

-- Triggers: generate Id for Users, and Id+Code for Roles/Permissions
CREATE TRIGGER dbo.trg_Users_InsteadOfInsert
ON dbo.Users
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Users (Id, Username, Email, PasswordHash, CreatedAt, UpdatedAt, IsActive)
    SELECT
        N'U' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_User AS NVARCHAR(9)), 9),
        i.Username,
        i.Email,
        i.PasswordHash,
        ISNULL(i.CreatedAt, SYSUTCDATETIME()),
        i.UpdatedAt,
        ISNULL(i.IsActive, CAST(1 AS BIT))
    FROM inserted AS i;
END
GO

CREATE TRIGGER dbo.trg_Roles_InsteadOfInsert
ON dbo.Roles
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Roles (Id, Code, Name, CreatedAt, UpdatedAt, IsActive)
    SELECT
        N'R' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Role AS NVARCHAR(9)), 9),
        N'ROLE-' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Role AS NVARCHAR(9)), 9),
        i.Name,
        ISNULL(i.CreatedAt, SYSUTCDATETIME()),
        i.UpdatedAt,
        ISNULL(i.IsActive, CAST(1 AS BIT))
    FROM inserted AS i;
END
GO

CREATE TRIGGER dbo.trg_Permissions_InsteadOfInsert
ON dbo.Permissions
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Permissions (Id, Code, Name, CreatedAt, UpdatedAt, IsActive)
    SELECT
        N'P' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Permission AS NVARCHAR(9)), 9),
        N'PERM-' + RIGHT(REPLICATE(N'0', 9) + CAST(NEXT VALUE FOR dbo.Seq_Permission AS NVARCHAR(9)), 9),
        i.Name,
        ISNULL(i.CreatedAt, SYSUTCDATETIME()),
        i.UpdatedAt,
        ISNULL(i.IsActive, CAST(1 AS BIT))
    FROM inserted AS i;
END
GO

-- ============================================================
-- SEED DATA: Users, Roles, Permissions, RolePermission, UserRole, UserPermission, UserToken
-- Đăng nhập mẫu (cùng mật khẩu): Admin@123
--   admin@producttest.local  -> role Administrator
--   manager@producttest.local -> role Manager
--   demo@producttest.local    -> role Customer + trực tiếp users.write
-- ============================================================

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = N'Administrator')
BEGIN
    INSERT INTO dbo.Roles (Name) VALUES (N'Administrator'), (N'Manager'), (N'Customer');
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE Name = N'users.read')
BEGIN
    INSERT INTO dbo.Permissions (Name) VALUES
        (N'users.read'),
        (N'users.write'),
        (N'products.read'),
        (N'orders.manage'),
        (N'admin.roles');
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Username = N'admin')
BEGIN
    INSERT INTO dbo.Users (Username, Email, PasswordHash) VALUES
        (N'admin', N'admin@producttest.local', N'$2a$11$Yv82yhISBl.DysEh1SyXYehe9cC33T4gXGlhqQyqqSEtiJ.wmCxJe'),
        (N'manager', N'manager@producttest.local', N'$2a$11$Yv82yhISBl.DysEh1SyXYehe9cC33T4gXGlhqQyqqSEtiJ.wmCxJe'),
        (N'demo', N'demo@producttest.local', N'$2a$11$Yv82yhISBl.DysEh1SyXYehe9cC33T4gXGlhqQyqqSEtiJ.wmCxJe');
END
GO

IF NOT EXISTS (
    SELECT 1 FROM dbo.UserRoles ur
    INNER JOIN dbo.Users u ON u.Id = ur.UserId AND u.Username = N'admin'
    INNER JOIN dbo.Roles r ON r.Id = ur.RoleId AND r.Name = N'Administrator'
)
BEGIN
    INSERT INTO dbo.UserRoles (UserId, RoleId, RoleCode, RoleName)
    SELECT u.Id, r.Id, r.Code, r.Name FROM dbo.Users u CROSS JOIN dbo.Roles r
    WHERE u.Username = N'admin' AND r.Name = N'Administrator';
END
GO

IF NOT EXISTS (
    SELECT 1 FROM dbo.UserRoles ur
    INNER JOIN dbo.Users u ON u.Id = ur.UserId AND u.Username = N'manager'
    INNER JOIN dbo.Roles r ON r.Id = ur.RoleId AND r.Name = N'Manager'
)
BEGIN
    INSERT INTO dbo.UserRoles (UserId, RoleId, RoleCode, RoleName)
    SELECT u.Id, r.Id, r.Code, r.Name FROM dbo.Users u CROSS JOIN dbo.Roles r
    WHERE u.Username = N'manager' AND r.Name = N'Manager';
END
GO

IF NOT EXISTS (
    SELECT 1 FROM dbo.UserRoles ur
    INNER JOIN dbo.Users u ON u.Id = ur.UserId AND u.Username = N'demo'
    INNER JOIN dbo.Roles r ON r.Id = ur.RoleId AND r.Name = N'Customer'
)
BEGIN
    INSERT INTO dbo.UserRoles (UserId, RoleId, RoleCode, RoleName)
    SELECT u.Id, r.Id, r.Code, r.Name FROM dbo.Users u CROSS JOIN dbo.Roles r
    WHERE u.Username = N'demo' AND r.Name = N'Customer';
END
GO

IF NOT EXISTS (
    SELECT 1 FROM dbo.RolePermissions rp
    INNER JOIN dbo.Roles r ON r.Id = rp.RoleId AND r.Name = N'Administrator'
)
BEGIN
    INSERT INTO dbo.RolePermissions (RoleId, PermissionId, PermissionCode, PermissionName)
    SELECT r.Id, p.Id, p.Code, p.Name
    FROM dbo.Roles r CROSS JOIN dbo.Permissions p
    WHERE r.Name = N'Administrator';
END
GO

IF NOT EXISTS (
    SELECT 1 FROM dbo.RolePermissions rp
    INNER JOIN dbo.Roles r ON r.Id = rp.RoleId AND r.Name = N'Manager'
)
BEGIN
    INSERT INTO dbo.RolePermissions (RoleId, PermissionId, PermissionCode, PermissionName)
    SELECT r.Id, p.Id, p.Code, p.Name
    FROM dbo.Roles r INNER JOIN dbo.Permissions p ON p.Name IN (N'users.read', N'products.read', N'orders.manage')
    WHERE r.Name = N'Manager';
END
GO

IF NOT EXISTS (
    SELECT 1 FROM dbo.RolePermissions rp
    INNER JOIN dbo.Roles r ON r.Id = rp.RoleId AND r.Name = N'Customer'
)
BEGIN
    INSERT INTO dbo.RolePermissions (RoleId, PermissionId, PermissionCode, PermissionName)
    SELECT r.Id, p.Id, p.Code, p.Name
    FROM dbo.Roles r INNER JOIN dbo.Permissions p ON p.Name = N'products.read'
    WHERE r.Name = N'Customer';
END
GO

IF NOT EXISTS (
    SELECT 1 FROM dbo.UserPermissions up
    INNER JOIN dbo.Users u ON u.Id = up.UserId AND u.Username = N'demo'
    INNER JOIN dbo.Permissions p ON p.Id = up.PermissionId AND p.Name = N'users.write'
)
BEGIN
    INSERT INTO dbo.UserPermissions (UserId, PermissionId, PermissionCode, PermissionName)
    SELECT u.Id, p.Id, p.Code, p.Name
    FROM dbo.Users u CROSS JOIN dbo.Permissions p
    WHERE u.Username = N'demo' AND p.Name = N'users.write';
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.UserToken WHERE RefreshToken = N'seed-refresh-token-admin-001')
BEGIN
    INSERT INTO dbo.UserToken (UserId, RefreshToken, IssuedAt, ExpiresAt)
    SELECT u.Id, N'seed-refresh-token-admin-001', SYSUTCDATETIME(), DATEADD(DAY, 7, SYSUTCDATETIME())
    FROM dbo.Users u WHERE u.Username = N'admin' AND u.IsActive = 1;
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.UserToken WHERE RefreshToken = N'seed-refresh-token-demo-001')
BEGIN
    INSERT INTO dbo.UserToken (UserId, RefreshToken, IssuedAt, ExpiresAt)
    SELECT u.Id, N'seed-refresh-token-demo-001', SYSUTCDATETIME(), DATEADD(DAY, 30, SYSUTCDATETIME())
    FROM dbo.Users u WHERE u.Username = N'demo' AND u.IsActive = 1;
END
GO

-- ============================================================
-- USERS CRUD
-- ============================================================
CREATE PROCEDURE dbo.usp_UserGetAll
    @PageNumber NVARCHAR(50) = N'1',
    @PageSize NVARCHAR(50) = N'10'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PageNumberValue INT = TRY_CONVERT(INT, @PageNumber);
    DECLARE @PageSizeValue INT = TRY_CONVERT(INT, @PageSize);
    IF @PageNumberValue IS NULL OR @PageSizeValue IS NULL
    BEGIN
        RAISERROR(N'Invalid paging input', 16, 1);
        RETURN;
    END

    SELECT
        u.Id,
        u.Username,
        u.Email,
        u.IsActive,
        u.CreatedAt,
        u.UpdatedAt
    FROM Users u
    WHERE u.IsActive = 1
    ORDER BY u.CreatedAt DESC
    OFFSET (@PageNumberValue - 1) * @PageSizeValue ROWS
    FETCH NEXT @PageSizeValue ROWS ONLY;
END
GO

CREATE PROCEDURE dbo.usp_UserGetById
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        u.Id,
        u.Username,
        u.Email,
        u.PasswordHash,
        u.IsActive,
        u.CreatedAt,
        u.UpdatedAt
    FROM Users u
    WHERE u.Id = @Id AND u.IsActive = 1;
END
GO

CREATE PROCEDURE dbo.usp_UserGetByEmail
    @Email NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        u.Id,
        u.Username,
        u.Email,
        u.PasswordHash,
        u.IsActive,
        u.CreatedAt,
        u.UpdatedAt
    FROM Users u
    WHERE u.Email = @Email AND u.IsActive = 1;
END
GO

CREATE PROCEDURE dbo.usp_UserCreate
    @Username NVARCHAR(100),
    @Email NVARCHAR(255),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Users WHERE (Username = @Username OR Email = @Email) AND IsActive = 1)
    BEGIN
        RAISERROR(N'User already exists (username or email).', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Users(Username, Email, PasswordHash)
            VALUES (@Username, @Email, @PasswordHash);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserUpdate
    @Id NVARCHAR(50),
    @Username NVARCHAR(100),
    @Email NVARCHAR(255),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'User not found or already deleted.', 16, 1);
        RETURN;
    END

    IF EXISTS (
        SELECT 1 FROM Users
        WHERE (Username = @Username OR Email = @Email)
          AND Id <> @Id
          AND IsActive = 1
    )
    BEGIN
        RAISERROR(N'Username or email already in use.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Users
            SET Username = @Username,
                Email = @Email,
                PasswordHash = @PasswordHash,
                UpdatedAt = SYSUTCDATETIME()
            WHERE Id = @Id AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserDelete
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'User not found or already deleted.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Users
            SET IsActive = 0,
                UpdatedAt = SYSUTCDATETIME()
            WHERE Id = @Id AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserLogin
    @UserId NVARCHAR(50),
    @RefreshToken NVARCHAR(2000),
    @IssuedAt DATETIME,
    @ExpiresAt DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId AND IsActive = 1)
    BEGIN
        RAISERROR(N'User not found or inactive.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO UserToken (UserId, RefreshToken, IssuedAt, ExpiresAt)
            VALUES (@UserId, @RefreshToken, @IssuedAt, @ExpiresAt);

            UPDATE Users
            SET UpdatedAt = SYSUTCDATETIME()
            WHERE Id = @UserId AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserLogout
    @RefreshToken NVARCHAR(2000)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE UserToken
            SET IsActive = 0,
                RevokedAt = SYSUTCDATETIME()
            WHERE RefreshToken = @RefreshToken
              AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserRefreshToken
    @RefreshToken NVARCHAR(2000)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE UserToken
            SET IsActive = 0,
                RevokedAt = SYSUTCDATETIME()
            WHERE RefreshToken = @RefreshToken
              AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserRegister
    @Username NVARCHAR(100),
    @Email NVARCHAR(255),
    @Password NVARCHAR(255),
    @RoleIds dbo.Tvp_IdList READONLY
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Users WHERE (Username = @Username OR Email = @Email) AND IsActive = 1)
    BEGIN
        RAISERROR(N'User already exists (username or email).', 16, 1);
        RETURN;
    END

    DECLARE @EffectiveRoleIds TABLE (Id NVARCHAR(50) NOT NULL PRIMARY KEY);
    INSERT INTO @EffectiveRoleIds(Id)
    SELECT DISTINCT t.Id
    FROM @RoleIds t
    WHERE NULLIF(LTRIM(RTRIM(t.Id)), N'') IS NOT NULL;

    IF NOT EXISTS (SELECT 1 FROM @EffectiveRoleIds)
    BEGIN
        INSERT INTO @EffectiveRoleIds(Id)
        SELECT TOP 1 r.Id
        FROM Roles r
        WHERE r.Name = N'Customer' AND r.IsActive = 1;

        IF NOT EXISTS (SELECT 1 FROM @EffectiveRoleIds)
        BEGIN
            RAISERROR(N'Default role ''Customer'' does not exist or is inactive.', 16, 1);
            RETURN;
        END
    END

    IF EXISTS (
        SELECT 1
        FROM @EffectiveRoleIds e
        WHERE NOT EXISTS (
            SELECT 1
            FROM Roles r
            WHERE r.Id = e.Id AND r.IsActive = 1
        )
    )
    BEGIN
        RAISERROR(N'One or more roles were not found or are inactive.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Users(Username, Email, PasswordHash)
            VALUES (@Username, @Email, @Password);

            DECLARE @NewUserId CHAR(10);
            SELECT TOP 1 @NewUserId = u.Id
            FROM Users u
            WHERE u.Username = @Username AND u.Email = @Email AND u.IsActive = 1
            ORDER BY u.CreatedAt DESC;

            INSERT INTO UserRoles (UserId, RoleId, RoleCode, RoleName)
            SELECT @NewUserId, r.Id, r.Code, r.Name
            FROM @EffectiveRoleIds e
            INNER JOIN Roles r ON r.Id = e.Id AND r.IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserGrantRole
    @UserId NVARCHAR(50),
    @RoleIds dbo.Tvp_IdList READONLY
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM @RoleIds)
    BEGIN
        RAISERROR(N'RoleIds cannot be empty.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId AND IsActive = 1)
    BEGIN
        RAISERROR(N'User not found or already deleted.', 16, 1);
        RETURN;
    END

    IF EXISTS (
        SELECT 1
        FROM @RoleIds t
        WHERE NOT EXISTS (SELECT 1 FROM Roles r WHERE r.Id = t.Id AND r.IsActive = 1)
    )
    BEGIN
        RAISERROR(N'One or more roles were not found or are inactive.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO UserRoles (UserId, RoleId, RoleCode, RoleName)
            SELECT @UserId, t.Id, r.Code, r.Name
            FROM @RoleIds t
            INNER JOIN Roles r ON r.Id = t.Id AND r.IsActive = 1
            WHERE NOT EXISTS (
                SELECT 1 FROM UserRoles ur
                WHERE ur.UserId = @UserId AND ur.RoleId = t.Id AND ur.IsActive = 1
            );

            ;WITH PermissionSnapshot AS
            (
                SELECT DISTINCT
                    @UserId AS UserId,
                    rp.PermissionId,
                    rp.PermissionCode,
                    rp.PermissionName
                FROM @RoleIds t
                INNER JOIN RolePermissions rp
                    ON rp.RoleId = t.Id
                   AND rp.IsActive = 1
            )
            UPDATE up
            SET up.IsActive = 1,
                up.PermissionCode = ps.PermissionCode,
                up.PermissionName = ps.PermissionName,
                up.UpdatedAt = SYSUTCDATETIME()
            FROM UserPermissions up
            INNER JOIN PermissionSnapshot ps
                ON ps.UserId = up.UserId
               AND ps.PermissionId = up.PermissionId
            WHERE up.IsActive = 0;

            INSERT INTO UserPermissions (UserId, PermissionId, PermissionCode, PermissionName)
            SELECT
                ps.UserId,
                ps.PermissionId,
                ps.PermissionCode,
                ps.PermissionName
            FROM PermissionSnapshot ps
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM UserPermissions up
                WHERE up.UserId = ps.UserId
                  AND up.PermissionId = ps.PermissionId
            );
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserRevokeRole
    @UserId NVARCHAR(50),
    @RoleIds dbo.Tvp_IdList READONLY
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM @RoleIds)
    BEGIN
        RAISERROR(N'RoleIds cannot be empty.', 16, 1);
        RETURN;
    END

    IF EXISTS (
        SELECT 1
        FROM @RoleIds t
        WHERE NOT EXISTS (
            SELECT 1 FROM UserRoles ur
            WHERE ur.UserId = @UserId AND ur.RoleId = t.Id AND ur.IsActive = 1
        )
    )
    BEGIN
        RAISERROR(N'One or more UserRole assignments were not found or are inactive.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE ur
            SET IsActive = 0,
                UpdatedAt = SYSUTCDATETIME()
            FROM UserRoles ur
            INNER JOIN @RoleIds t ON ur.RoleId = t.Id
            WHERE ur.UserId = @UserId AND ur.IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserGrantPermission
    @UserId NVARCHAR(50),
    @PermissionIds dbo.Tvp_IdList READONLY
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM @PermissionIds)
    BEGIN
        RAISERROR(N'PermissionIds cannot be empty.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId AND IsActive = 1)
    BEGIN
        RAISERROR(N'User not found or already deleted.', 16, 1);
        RETURN;
    END

    IF EXISTS (
        SELECT 1
        FROM @PermissionIds t
        WHERE NOT EXISTS (SELECT 1 FROM Permissions p WHERE p.Id = t.Id AND p.IsActive = 1)
    )
    BEGIN
        RAISERROR(N'One or more permissions were not found or are inactive.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO UserPermissions (UserId, PermissionId, PermissionCode, PermissionName)
            SELECT @UserId, t.Id, p.Code, p.Name
            FROM @PermissionIds t
            INNER JOIN Permissions p ON p.Id = t.Id AND p.IsActive = 1
            WHERE NOT EXISTS (
                SELECT 1 FROM UserPermissions up
                WHERE up.UserId = @UserId AND up.PermissionId = t.Id AND up.IsActive = 1
            );
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserRevokePermission
    @UserId NVARCHAR(50),
    @PermissionIds dbo.Tvp_IdList READONLY
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM @PermissionIds)
    BEGIN
        RAISERROR(N'PermissionIds cannot be empty.', 16, 1);
        RETURN;
    END

    IF EXISTS (
        SELECT 1
        FROM @PermissionIds t
        WHERE NOT EXISTS (
            SELECT 1 FROM UserPermissions up
            WHERE up.UserId = @UserId AND up.PermissionId = t.Id AND up.IsActive = 1
        )
    )
    BEGIN
        RAISERROR(N'One or more UserPermission assignments were not found or are inactive.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE up
            SET IsActive = 0,
                UpdatedAt = SYSUTCDATETIME()
            FROM UserPermissions up
            INNER JOIN @PermissionIds t ON up.PermissionId = t.Id
            WHERE up.UserId = @UserId AND up.IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- ============================================================
-- USER TOKENS (auth token store)
-- ============================================================
CREATE PROCEDURE dbo.usp_UserTokenGetByRefreshToken
    @RefreshToken NVARCHAR(2000)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ut.Id,
        ut.UserId,
        ut.RefreshToken,
        ut.IssuedAt,
        ut.ExpiresAt,
        ut.RevokedAt,
        ut.IsActive
    FROM UserToken ut
    WHERE ut.RefreshToken = @RefreshToken
      AND ut.IsActive = 1;
END
GO

CREATE PROCEDURE dbo.usp_UserTokenGetByUserId
    @UserId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ut.Id,
        ut.UserId,
        ut.RefreshToken,
        ut.IssuedAt,
        ut.ExpiresAt,
        ut.RevokedAt,
        ut.IsActive
    FROM UserToken ut
    WHERE ut.UserId = @UserId
      AND ut.IsActive = 1
    ORDER BY ut.IssuedAt DESC;
END
GO

CREATE PROCEDURE dbo.usp_UserTokenRevokeByRefreshToken
    @RefreshToken NVARCHAR(2000)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE UserToken
            SET IsActive = 0,
                RevokedAt = SYSUTCDATETIME()
            WHERE RefreshToken = @RefreshToken
              AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserTokenLogoutByRefreshToken
    @RefreshToken NVARCHAR(2000)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;
            -- Logout: remove refresh token record.
            DELETE FROM UserToken
            WHERE RefreshToken = @RefreshToken
              AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- ============================================================
-- ROLES CRUD
-- ============================================================
CREATE PROCEDURE dbo.usp_RoleGetAll
    @PageNumber NVARCHAR(50) = N'1',
    @PageSize NVARCHAR(50) = N'10'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PageNumberValue INT = TRY_CONVERT(INT, @PageNumber);
    DECLARE @PageSizeValue INT = TRY_CONVERT(INT, @PageSize);
    IF @PageNumberValue IS NULL OR @PageSizeValue IS NULL
    BEGIN
        RAISERROR(N'Invalid paging input', 16, 1);
        RETURN;
    END

    SELECT
        r.Id,
        r.Code,
        r.Name,
        r.IsActive,
        r.CreatedAt,
        r.UpdatedAt
    FROM Roles r
    WHERE r.IsActive = 1
    ORDER BY r.CreatedAt DESC
    OFFSET (@PageNumberValue - 1) * @PageSizeValue ROWS
    FETCH NEXT @PageSizeValue ROWS ONLY;
END
GO

CREATE PROCEDURE dbo.usp_RoleGetById
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        r.Id,
        r.Code,
        r.Name,
        r.IsActive,
        r.CreatedAt,
        r.UpdatedAt
    FROM Roles r
    WHERE r.Id = @Id AND r.IsActive = 1;
END
GO

CREATE PROCEDURE dbo.usp_RoleGetByCode
    @Code NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        r.Id,
        r.Code,
        r.Name,
        r.IsActive,
        r.CreatedAt,
        r.UpdatedAt
    FROM Roles r
    WHERE r.Code = @Code AND r.IsActive = 1;
END
GO

CREATE PROCEDURE dbo.usp_RoleCreate
    @Name NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Roles WHERE Name = @Name AND IsActive = 1)
    BEGIN
        RAISERROR(N'Role name already exists.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Roles(Name)
            VALUES (@Name);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_RoleUpdate
    @Id NVARCHAR(50),
    @Name NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Role not found or already deleted.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Roles WHERE Name = @Name AND Id <> @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Role name already in use.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Roles
            SET Name = @Name,
                UpdatedAt = SYSUTCDATETIME()
            WHERE Id = @Id AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_RoleDelete
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Role not found or already deleted.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Roles
            SET IsActive = 0,
                UpdatedAt = SYSUTCDATETIME()
            WHERE Id = @Id AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_RoleGrantPermission
    @RoleId NVARCHAR(50),
    @PermissionIds dbo.Tvp_IdList READONLY
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM @PermissionIds)
    BEGIN
        RAISERROR(N'PermissionIds cannot be empty.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = @RoleId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Role not found or already deleted.', 16, 1);
        RETURN;
    END

    IF EXISTS (
        SELECT 1
        FROM @PermissionIds t
        WHERE NOT EXISTS (SELECT 1 FROM Permissions p WHERE p.Id = t.Id AND p.IsActive = 1)
    )
    BEGIN
        RAISERROR(N'One or more permissions were not found or are inactive.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO RolePermissions (RoleId, PermissionId, PermissionCode, PermissionName)
            SELECT @RoleId, t.Id, p.Code, p.Name
            FROM @PermissionIds t
            INNER JOIN Permissions p ON p.Id = t.Id AND p.IsActive = 1
            WHERE NOT EXISTS (
                SELECT 1 FROM RolePermissions rp
                WHERE rp.RoleId = @RoleId AND rp.PermissionId = t.Id AND rp.IsActive = 1
            );
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_RoleRevokePermission
    @RoleId NVARCHAR(50),
    @PermissionIds dbo.Tvp_IdList READONLY
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM @PermissionIds)
    BEGIN
        RAISERROR(N'PermissionIds cannot be empty.', 16, 1);
        RETURN;
    END

    IF EXISTS (
        SELECT 1
        FROM @PermissionIds t
        WHERE NOT EXISTS (
            SELECT 1 FROM RolePermissions rp
            WHERE rp.RoleId = @RoleId AND rp.PermissionId = t.Id AND rp.IsActive = 1
        )
    )
    BEGIN
        RAISERROR(N'One or more RolePermission assignments were not found or are inactive.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE rp
            SET IsActive = 0,
                UpdatedAt = SYSUTCDATETIME()
            FROM RolePermissions rp
            INNER JOIN @PermissionIds t ON rp.PermissionId = t.Id
            WHERE rp.RoleId = @RoleId AND rp.IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- ============================================================
-- PERMISSIONS CRUD
-- ============================================================
CREATE PROCEDURE dbo.usp_PermissionGetAll
    @PageNumber NVARCHAR(50) = N'1',
    @PageSize NVARCHAR(50) = N'10'
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PageNumberValue INT = TRY_CONVERT(INT, @PageNumber);
    DECLARE @PageSizeValue INT = TRY_CONVERT(INT, @PageSize);
    IF @PageNumberValue IS NULL OR @PageSizeValue IS NULL
    BEGIN
        RAISERROR(N'Invalid paging input', 16, 1);
        RETURN;
    END

    SELECT
        p.Id,
        p.Code,
        p.Name,
        p.IsActive,
        p.CreatedAt,
        p.UpdatedAt
    FROM Permissions p
    WHERE p.IsActive = 1
    ORDER BY p.CreatedAt DESC
    OFFSET (@PageNumberValue - 1) * @PageSizeValue ROWS
    FETCH NEXT @PageSizeValue ROWS ONLY;
END
GO

CREATE PROCEDURE dbo.usp_PermissionGetById
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        p.Id,
        p.Code,
        p.Name,
        p.IsActive,
        p.CreatedAt,
        p.UpdatedAt
    FROM Permissions p
    WHERE p.Id = @Id AND p.IsActive = 1;
END
GO

CREATE PROCEDURE dbo.usp_PermissionGetByCode
    @Code NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        p.Id,
        p.Code,
        p.Name,
        p.IsActive,
        p.CreatedAt,
        p.UpdatedAt
    FROM Permissions p
    WHERE p.Code = @Code AND p.IsActive = 1;
END
GO

CREATE PROCEDURE dbo.usp_PermissionCreate
    @Name NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Permissions WHERE Name = @Name AND IsActive = 1)
    BEGIN
        RAISERROR(N'Permission name already exists.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO Permissions(Name)
            VALUES (@Name);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_PermissionUpdate
    @Id NVARCHAR(50),
    @Name NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Permissions WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Permission not found or already deleted.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM Permissions WHERE Name = @Name AND Id <> @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Permission name already in use.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Permissions
            SET Name = @Name,
                UpdatedAt = SYSUTCDATETIME()
            WHERE Id = @Id AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_PermissionDelete
    @Id NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Permissions WHERE Id = @Id AND IsActive = 1)
    BEGIN
        RAISERROR(N'Permission not found or already deleted.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE Permissions
            SET IsActive = 0,
                UpdatedAt = SYSUTCDATETIME()
            WHERE Id = @Id AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- ============================================================
-- USER ROLES (join) CRUD
-- ============================================================
CREATE PROCEDURE dbo.usp_UserRoleCreate
    @UserId NVARCHAR(50),
    @RoleId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId AND IsActive = 1)
    BEGIN
        RAISERROR(N'User not found or already deleted.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = @RoleId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Role not found or already deleted.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM UserRoles WHERE UserId = @UserId AND RoleId = @RoleId AND IsActive = 1)
    BEGIN
        RAISERROR(N'UserRole already exists.', 16, 1);
        RETURN;
    END

    DECLARE @RoleCode VARCHAR(20);
    DECLARE @RoleName NVARCHAR(255);

    SELECT @RoleCode = Code, @RoleName = Name FROM Roles WHERE Id = @RoleId AND IsActive = 1;

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO UserRoles(UserId, RoleId, RoleCode, RoleName) VALUES (@UserId, @RoleId, @RoleCode, @RoleName);

            ;WITH PermissionSnapshot AS
            (
                SELECT DISTINCT
                    @UserId AS UserId,
                    rp.PermissionId,
                    rp.PermissionCode,
                    rp.PermissionName
                FROM RolePermissions rp
                WHERE rp.RoleId = @RoleId
                  AND rp.IsActive = 1
            )
            UPDATE up
            SET up.IsActive = 1,
                up.PermissionCode = ps.PermissionCode,
                up.PermissionName = ps.PermissionName,
                up.UpdatedAt = SYSUTCDATETIME()
            FROM UserPermissions up
            INNER JOIN PermissionSnapshot ps
                ON ps.UserId = up.UserId
               AND ps.PermissionId = up.PermissionId
            WHERE up.IsActive = 0;

            INSERT INTO UserPermissions(UserId, PermissionId, PermissionCode, PermissionName)
            SELECT
                ps.UserId,
                ps.PermissionId,
                ps.PermissionCode,
                ps.PermissionName
            FROM PermissionSnapshot ps
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM UserPermissions up
                WHERE up.UserId = ps.UserId
                  AND up.PermissionId = ps.PermissionId
            );
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserRoleDelete
    @UserId NVARCHAR(50),
    @RoleId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM UserRoles WHERE UserId = @UserId AND RoleId = @RoleId AND IsActive = 1)
    BEGIN
        RAISERROR(N'UserRole not found or already deleted.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE UserRoles
            SET IsActive = 0,
                UpdatedAt = SYSUTCDATETIME()
            WHERE UserId = @UserId AND RoleId = @RoleId AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserRoleGetByUserId
    @UserId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        ur.UserId,
        ur.RoleId,
        ur.RoleCode,
        ur.RoleName,
        ur.IsActive,
        ur.CreatedAt,
        ur.UpdatedAt
    FROM UserRoles ur
    JOIN Roles r ON r.Id = ur.RoleId
    JOIN Users u ON u.Id = ur.UserId
    WHERE ur.UserId = @UserId
      AND ur.IsActive = 1
      AND r.IsActive = 1
      AND u.IsActive = 1;
END
GO

CREATE PROCEDURE dbo.usp_UserRoleGetByRoleId
    @RoleId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        ur.UserId,
        ur.RoleId,
        ur.RoleCode,
        ur.RoleName,
        ur.IsActive,
        ur.CreatedAt,
        ur.UpdatedAt
    FROM UserRoles ur
    JOIN Roles r ON r.Id = ur.RoleId
    JOIN Users u ON u.Id = ur.UserId
    WHERE ur.RoleId = @RoleId
      AND ur.IsActive = 1
      AND r.IsActive = 1
      AND u.IsActive = 1;
END
GO

-- ============================================================
-- USER PERMISSIONS (join) CRUD
-- ============================================================
CREATE PROCEDURE dbo.usp_UserPermissionCreate
    @UserId NVARCHAR(50),
    @PermissionId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId AND IsActive = 1)
    BEGIN
        RAISERROR(N'User not found or already deleted.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Permissions WHERE Id = @PermissionId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Permission not found or already deleted.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM UserPermissions WHERE UserId = @UserId AND PermissionId = @PermissionId AND IsActive = 1)
    BEGIN
        RAISERROR(N'UserPermission already exists.', 16, 1);
        RETURN;
    END

    DECLARE @PermissionCode VARCHAR(20);
    DECLARE @PermissionName NVARCHAR(255);

    SELECT @PermissionCode = Code, @PermissionName = Name FROM Permissions WHERE Id = @PermissionId AND IsActive = 1;

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO UserPermissions(UserId, PermissionId, PermissionCode, PermissionName) VALUES (@UserId, @PermissionId, @PermissionCode, @PermissionName);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserPermissionDelete
    @UserId NVARCHAR(50),
    @PermissionId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM UserPermissions WHERE UserId = @UserId AND PermissionId = @PermissionId AND IsActive = 1)
    BEGIN
        RAISERROR(N'UserPermission not found or already deleted.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE UserPermissions
            SET IsActive = 0,
                UpdatedAt = SYSUTCDATETIME()
            WHERE UserId = @UserId AND PermissionId = @PermissionId AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_UserPermissionGetByUserId
    @UserId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        up.UserId,
        up.PermissionId,
        up.PermissionCode,
        up.PermissionName,
        up.IsActive,
        up.CreatedAt,
        up.UpdatedAt
    FROM UserPermissions up
    JOIN Permissions p ON p.Id = up.PermissionId
    JOIN Users u ON u.Id = up.UserId
    WHERE up.UserId = @UserId
      AND up.IsActive = 1
      AND p.IsActive = 1
      AND u.IsActive = 1;
END
GO

CREATE PROCEDURE dbo.usp_UserPermissionGetByPermissionId
    @PermissionId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        up.UserId,
        up.PermissionId,
        up.PermissionCode,
        up.PermissionName,
        up.IsActive,
        up.CreatedAt,
        up.UpdatedAt
    FROM UserPermissions up
    JOIN Permissions p ON p.Id = up.PermissionId
    JOIN Users u ON u.Id = up.UserId
    WHERE up.PermissionId = @PermissionId
      AND up.IsActive = 1
      AND p.IsActive = 1
      AND u.IsActive = 1;
END
GO

-- ============================================================
-- ROLE PERMISSIONS (join) CRUD
-- ============================================================
CREATE PROCEDURE dbo.usp_RolePermissionCreate
    @RoleId NVARCHAR(50),
    @PermissionId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Roles WHERE Id = @RoleId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Role not found or already deleted.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Permissions WHERE Id = @PermissionId AND IsActive = 1)
    BEGIN
        RAISERROR(N'Permission not found or already deleted.', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM RolePermissions WHERE RoleId = @RoleId AND PermissionId = @PermissionId AND IsActive = 1)
    BEGIN
        RAISERROR(N'RolePermission already exists.', 16, 1);
        RETURN;
    END

    DECLARE @PermissionCode VARCHAR(20);
    DECLARE @PermissionName NVARCHAR(255);

    SELECT @PermissionCode = Code, @PermissionName = Name FROM Permissions WHERE Id = @PermissionId AND IsActive = 1;

    BEGIN TRY
        BEGIN TRANSACTION;
            INSERT INTO RolePermissions(RoleId, PermissionId, PermissionCode, PermissionName) VALUES (@RoleId, @PermissionId, @PermissionCode, @PermissionName);
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_RolePermissionDelete
    @RoleId NVARCHAR(50),
    @PermissionId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM RolePermissions WHERE RoleId = @RoleId AND PermissionId = @PermissionId AND IsActive = 1)
    BEGIN
        RAISERROR(N'RolePermission not found or already deleted.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
            UPDATE RolePermissions
            SET IsActive = 0,
                UpdatedAt = SYSUTCDATETIME()
            WHERE RoleId = @RoleId AND PermissionId = @PermissionId AND IsActive = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE dbo.usp_RolePermissionGetByRoleId
    @RoleId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        rp.RoleId,
        rp.PermissionId,
        rp.PermissionCode,
        rp.PermissionName,
        rp.IsActive,
        rp.CreatedAt,
        rp.UpdatedAt
    FROM RolePermissions rp
    JOIN Permissions p ON p.Id = rp.PermissionId
    JOIN Roles r ON r.Id = rp.RoleId
    WHERE rp.RoleId = @RoleId
      AND rp.IsActive = 1
      AND p.IsActive = 1
      AND r.IsActive = 1;
END
GO

CREATE PROCEDURE dbo.usp_RolePermissionGetByPermissionId
    @PermissionId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        rp.RoleId,
        rp.PermissionId,
        rp.PermissionCode,
        rp.PermissionName,
        rp.IsActive,
        rp.CreatedAt,
        rp.UpdatedAt
    FROM RolePermissions rp
    JOIN Permissions p ON p.Id = rp.PermissionId
    JOIN Roles r ON r.Id = rp.RoleId
    WHERE rp.PermissionId = @PermissionId
      AND rp.IsActive = 1
      AND p.IsActive = 1
      AND r.IsActive = 1;
END
GO