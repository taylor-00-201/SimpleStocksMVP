IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'SimpleStocks')
CREATE DATABASE SimpleStocks;
GO

USE SimpleStocks;
GO

IF OBJECT_ID('SimpleStocks.dbo.Transactions', 'U') IS NOT NULL
DROP TABLE SimpleStocks.dbo.Transactions
GO

IF OBJECT_ID('SimpleStocks.dbo.Login', 'U') IS NOT NULL
DROP TABLE SimpleStocks.dbo.Login
GO

IF OBJECT_ID('SimpleStocks.dbo.Assets', 'U') IS NOT NULL
DROP TABLE SimpleStocks.dbo.Assets
GO

IF OBJECT_ID('SimpleStocks.dbo.StockUser', 'U') IS NOT NULL
DROP TABLE SimpleStocks.dbo.StockUser
GO


CREATE TABLE [StockUser] (
  [Id] integer IDENTITY PRIMARY KEY not null,
  [UserName] nvarchar(255) UNIQUE not null,
  [Email] nvarchar(255) UNIQUE not null,
  [FirstName] nvarchar(255) not null,
  [LastName] nvarchar(255) not null,
  [IsAdmin] bit not null,
  [AddressLineOne] varchar(255) not null,
  [AddressLineTwo] varchar(255) not null,
  [City] varchar(255) not null,
  [State] varchar(255) not null,
  [Zip] varchar(15) not null,
  [Balance] decimal not null
)
GO

CREATE TABLE [Assets] (
  [Id] integer IDENTITY PRIMARY KEY not null,
  [symbol] nvarchar(255) UNIQUE not null,
  [Name] nvarchar(255) UNIQUE not null,
  [CurrentPrice] decimal UNIQUE not null
)
GO

CREATE TABLE [Transactions] (
  [Id] integer IDENTITY PRIMARY KEY not null,
  [UserId] integer not null,
  [TransactionType] nvarchar(255) not null,
  [Quantity] integer not null,
  [AssetId] integer not null,
  [DateTime] DateTime not null,
  [OrderId] integer UNIQUE not null,
  [Amount] decimal not null
)
GO

CREATE TABLE [Login] (
  [Id] integer IDENTITY PRIMARY KEY not null,
  [UserId] integer UNIQUE not null,
  [PasswordHash] nvarchar(255) not null,
  [Email] nvarchar(255) UNIQUE not null 
)
GO


ALTER TABLE [Transactions] ADD FOREIGN KEY ([UserId]) REFERENCES [StockUser] ([Id])
GO

ALTER TABLE [Transactions] ADD FOREIGN KEY ([AssetId]) REFERENCES [Assets] ([Id])
GO

ALTER TABLE [Login] ADD FOREIGN KEY ([UserId]) REFERENCES [StockUser] ([Id])
GO

--ALTER TABLE [Login] ADD FOREIGN KEY ([Email]) REFERENCES [StockUser] ([Email])
--GO
