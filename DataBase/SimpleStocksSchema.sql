IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'SimpleStocks')
CREATE DATABASE SimpleStocks;
GO

USE SimpleStocks;
GO

IF OBJECT_ID('SimpleStocks.dbo.Transactions', 'U') IS NOT NULL
DROP TABLE SimpleStocks.dbo.Transactions
GO

IF OBJECT_ID('SimpleStocks.dbo.UserAssets', 'U') IS NOT NULL
DROP TABLE SimpleStocks.dbo.UserAssets
GO

IF OBJECT_ID('SimpleStocks.dbo.[Order]', 'U') IS NOT NULL
DROP TABLE SimpleStocks.dbo.[Order]
GO

IF OBJECT_ID('SimpleStocks.dbo.BankAccounts', 'U') IS NOT NULL
DROP TABLE SimpleStocks.dbo.BankAccounts
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
  [Zip] int not null
)
GO

CREATE TABLE [BankAccounts] (
  [Id] integer IDENTITY PRIMARY KEY not null,
  [UserId] integer not null,
  [Balance] decimal not null,
  [AccountNumber] int not null,
  [RoutingNumber] int not null,
  [BankName] nvarchar(255) not null
)
GO

CREATE TABLE [Assets] (
  [Id] integer PRIMARY KEY not null,
  [symbol] nvarchar(255) not null,
  [Name] nvarchar(255) not null,
  [CurrentPrice] decimal not null
)
GO

CREATE TABLE [Transactions] (
  [Id] integer IDENTITY PRIMARY KEY not null,
  [UserId] integer not null,
  [TransactionType] nvarchar(255) not null,
  [Quantity] integer not null,
  [AssetId] integer not null,
  [DateTime] DateTime not null,
  [OrderId] integer not null,
  [Amount] decimal not null
)
GO

CREATE TABLE [Order] (
  [Id] integer IDENTITY PRIMARY KEY not null,
  [UserId] integer not null,
  [OrderStatus] nvarchar(255) not null,
  [Total] decimal not null
)
GO

CREATE TABLE [UserAssets] (
  [Id] integer IDENTITY PRIMARY KEY not null,
  [UserId] integer not null,
  [AssetId] integer not null,
  [Quantity] integer not null
)
GO

CREATE TABLE [Login] (
  [Id] integer IDENTITY PRIMARY KEY not null,
  [UserId] integer not null,
  [PasswordHash] nvarchar(255) not null,
  [Email] nvarchar(255) not null 
)
GO

ALTER TABLE [UserAssets] ADD FOREIGN KEY ([UserId]) REFERENCES [StockUser] ([Id])
GO

ALTER TABLE [UserAssets] ADD FOREIGN KEY ([AssetId]) REFERENCES [Assets] ([Id])
GO

ALTER TABLE [Transactions] ADD FOREIGN KEY ([UserId]) REFERENCES [StockUser] ([Id])
GO

ALTER TABLE [Transactions] ADD FOREIGN KEY ([AssetId]) REFERENCES [Assets] ([Id])
GO

ALTER TABLE [Order] ADD FOREIGN KEY ([UserId]) REFERENCES [StockUser] ([Id])
GO

ALTER TABLE [Transactions] ADD FOREIGN KEY ([OrderId]) REFERENCES [Order] ([Id])
GO

ALTER TABLE [BankAccounts] ADD FOREIGN KEY ([UserId]) REFERENCES [StockUser] ([Id])
GO

ALTER TABLE [Login] ADD FOREIGN KEY ([UserId]) REFERENCES [StockUser] ([Id])
GO

ALTER TABLE [Login] ADD FOREIGN KEY ([Email]) REFERENCES [StockUser] ([Email])
GO
