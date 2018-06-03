 
  CREATE TABLE [dbo].[UserLogin](
	[UserLoginId][int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier]  NOT NULL,
	[token] varchar(50) NOT NULL,
	[LoggedInFrom] varchar(50) NOT NULL,
	[ExpiresAt] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_UserLoginId] PRIMARY KEY CLUSTERED 
(
	[UserLoginId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],

 ) ON [PRIMARY]
 
 
   CREATE TABLE [dbo].[PurchaseReceipt](
	[PurchaseReceiptId][int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier]  NOT NULL,
	[VendorTransactionId] varchar(100) NOT NULL,
	[TransactionType] varchar(50) NOT NULL,
	[TransactionProvider]  varchar(50) NOT NULL,
	[TransactionStatus] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_PurchaseReceiptId] PRIMARY KEY CLUSTERED 
(
	[PurchaseReceiptId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],

 ) ON [PRIMARY]
 
 
    CREATE TABLE [dbo].[PurchaseReceiptData](
	[PurchaseReceiptDataId][int] IDENTITY(1,1) NOT NULL,
	[PurchaseReceiptId] [uniqueidentifier]  NOT NULL,
	[ReceiptData] varchar(1024) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_PurchaseReceiptDataId] PRIMARY KEY CLUSTERED 
(
	[PurchaseReceiptDataId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],

 ) ON [PRIMARY]
 
  
   CREATE TABLE [dbo].[Transactions](
	[TransactionId][int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ItemId] [int]  NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] decimal(4,2) NOT NULL,
	[VendorTransactionId] varchar(100) NOT NULL,
	[TransactionType] varchar(50) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_TransactionId] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],

 ) ON [PRIMARY]
 
   
   CREATE TABLE [dbo].[PurchaseItemDeliveryAddressMap](
	[PurchaseItemDeliveryAddressMapId][int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [int] NOT NULL,
	[Address] nvarchar(200) NOT NULL,
	[City] varchar(100)  NOT NULL,
	[ZipCode] varchar(50) NOT NULL,
	[State] varchar(100) NOT NULL,
	[Country] varchar(100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_PurchaseItemDeliveryAddressMapId] PRIMARY KEY CLUSTERED 
(
	[PurchaseItemDeliveryAddressMapId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],

 ) ON [PRIMARY]
 
   
   CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[Name] [int]  NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsMember] [bit] NOT NULL,
	[PrimaryContactNo] varchar(50)  NULL,
	[SecondaryContactNo] varchar(50) NULL,
	[PrimaryEmail] varchar(100)  NULL,
	[SecondaryEmail] varchar(100)  NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL
 CONSTRAINT [PK_UserId] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],

 ) ON [PRIMARY]
 
    
   CREATE TABLE [dbo].[UserAddress](
	[UserAddressId] [int] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Addresstype] [int]  NOT NULL,
	[Address] nvarchar(200) NOT NULL,
	[IsDeliveryAddress] bit NOT NULL,
	[City] varchar(100)  NOT NULL,
	[ZipCode] varchar(50) NOT NULL,
	[State] varchar(100) NOT NULL,
	[Country] varchar(100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserAddressId] PRIMARY KEY CLUSTERED 
(
	[UserAddressId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],

 ) ON [PRIMARY]