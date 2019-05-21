CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SKU] VARCHAR(30) NOT NULL, 
    [Description] NVARCHAR(200) NOT NULL, 
    [Unit] VARCHAR(5) NOT NULL, 
    [Price] DECIMAL(18, 2) NULL
)
GO

CREATE TABLE [dbo].[Stock]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FiProduct] INT NOT NULL, 
    [Shelf] CHAR(3) NOT NULL, 
    [Quantity] NUMERIC(7, 2) NOT NULL, 
    CONSTRAINT [FK_Stock_Product] FOREIGN KEY ([FiProduct]) REFERENCES [Product]([Id])
)
GO

CREATE TABLE [dbo].[Service]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nr] VARCHAR(10) NOT NULL, 
    [Description] NVARCHAR(200) NOT NULL, 
    [Unit] VARCHAR(5) NOT NULL, 
    [Price] DECIMAL NULL
)
GO
