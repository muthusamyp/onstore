
USE [OnStore]
GO

--CREATE TABLE [dbo].[Stores](
--	[StoreId] [int] NOT NULL,
--	[StoreName] [nvarchar](128) NOT NULL,
--	[Description] [nvarchar](1024) NOT NULL,
--	[CreatedDate] [datetime] NOT NULL,
--	[UpdatedDate] [datetime] NOT NULL
	
-- CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
--(
--	[StoreId] ASC
--)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
--) ON [PRIMARY]

--GO

/****** Object:  Table [dbo].[IM_Categories]    Script Date: 05/01/2018 19:47:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Categories](
	[CategoryId] [int]  NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[IconName] [nvarchar](64) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL

 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[Items](
	[ItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](128) NOT NULL,		
	[IconName] [nvarchar](128) NULL,
	[Description] [nvarchar](1024) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[IM_ItemCategoryMap]    Script Date: 05/01/2018 19:50:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ItemCategoryMap](
	[ItemCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL  FOREIGN KEY REFERENCES Items([ItemId]),
	[CategoryId] [int] NOT NULL FOREIGN KEY REFERENCES Categories([CategoryId]),
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL
 CONSTRAINT [PK_ItemCategoryId] PRIMARY KEY CLUSTERED 
(
	[ItemCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [IX_CategoryId_ItemId] UNIQUE NONCLUSTERED 
(
	[CategoryId] ASC,
	[ItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],


) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[IM_ItemStoreAvailability]    Script Date: 05/01/2018 19:55:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ItemStoreMap](
	[ItemStoreMapId] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[StoreId] [int] NOT NULL FOREIGN KEY REFERENCES Stores(StoreId),
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ItemStoreMapId] PRIMARY KEY CLUSTERED 
(
	[ItemStoreMapId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[ItemsAvailability](
	[ItemAvailabilityID] [int] IDENTITY(1,1) NOT NULL,
	[ItemStoreMapId] [int] NOT NULL FOREIGN KEY REFERENCES ItemStoreMap(ItemStoreMapId),
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ItemStoreAvailability] PRIMARY KEY CLUSTERED 
(
	[ItemAvailabilityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PriceDiscount](
	[PriceDiscountId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](1024) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Percentage] [decimal](4, 2) NOT NULL,
	[IsPremiumUser] [bit] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL
 CONSTRAINT [PK_PriceChange] PRIMARY KEY CLUSTERED 
(
	[PriceDiscountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[ItemStorePriceDiscount](
	[ItemStorePriceDiscountId] [int] IDENTITY(1,1) NOT NULL,
	[ItemStoreMapId] [int] NOT NULL  FOREIGN KEY REFERENCES ItemStoreMap(ItemStoreMapId),
	[PriceDiscountId] [int] NOT NULL FOREIGN KEY REFERENCES PriceDiscount(PriceDiscountId),
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ItemStorePriceDiscountId] PRIMARY KEY CLUSTERED 
(
	[ItemStorePriceDiscountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [IX_ItemId_PriceDiscountId_StoreId] UNIQUE NONCLUSTERED 
(
	[ItemStoreMapId]  ASC,
	[PriceDiscountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Images](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[ImageName] nvarchar(100) NOT NULL,
	[ImageUrl] varchar(300) NOT NULL,
	[ImageHeight] [int] NOT NULL,
	[ImageWidth] [int]  NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL
	
 CONSTRAINT [PK_ImageId] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 ) ON [PRIMARY]
 
 
 CREATE TABLE [dbo].[ItemImageMap](
	[ItemImageMapId] [int] IDENTITY(1,1) NOT NULL,
	[ImageId] [int] NOT NULL FOREIGN KEY REFERENCES Images([ImageId]),
	[ItemId] [int]  NOT NULL FOREIGN KEY REFERENCES Items([ItemId]),
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_ItemImageMapId] PRIMARY KEY CLUSTERED 
(
	[ItemImageMapId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 ) ON [PRIMARY]
 
   
 CREATE TABLE [dbo].[Videos](
	[VideoId] [int] IDENTITY(1,1) NOT NULL,
	[VideoName] [int] NOT NULL,
	[Description] [int]  NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_VideoId] PRIMARY KEY CLUSTERED 
(
	[VideoId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 ) ON [PRIMARY]
 
  CREATE TABLE [dbo].[ItemVideoMap](
	[ItemVideoMapId] [int] IDENTITY(1,1) NOT NULL,
	[VideoId] [int] NOT NULL FOREIGN KEY REFERENCES Videos([VideoId]),
	[ItemId] [int]  NOT NULL FOREIGN KEY REFERENCES Items([ItemId]),
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,

 CONSTRAINT [PK_ItemVideoMapId] PRIMARY KEY CLUSTERED 
(
	[ItemVideoMapId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
CONSTRAINT [IX_ItemId_VideoId] UNIQUE NONCLUSTERED 
(
	[VideoId]  ASC,
	[ItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
 ) ON [PRIMARY]
 
 
 CREATE TABLE [dbo].[Vendors](
	[VendorId] [int]  NOT NULL,
	[VendorName] nvarchar(100) NOT NULL,
	[Description]  nvarchar(400) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	
 CONSTRAINT [PK_VendorId] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],

 ) ON [PRIMARY]
 
 
  CREATE TABLE [dbo].[ItemPrice](
    [ItemPriceMapId] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL FOREIGN KEY REFERENCES Items([ItemId]),
	[Price] [decimal](4, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,

 CONSTRAINT [PK_ItemPriceMapId] PRIMARY KEY CLUSTERED 
(
	[ItemPriceMapId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],

 ) ON [PRIMARY]
 
 
  CREATE TABLE [dbo].[ItemVendorPrice](
	[ItemVendorPriceId] [int] Identity(1,1) NOT NULL,
	[ItemId] [int] NOT NULL FOREIGN KEY REFERENCES Items([ItemId]),
	[VendorId] [int]  NOT NULL FOREIGN KEY REFERENCES Vendors([VendorId]),
	[Price] [decimal](4, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,

 CONSTRAINT [PK_ItemVendorPriceId] PRIMARY KEY CLUSTERED 
(
	[ItemVendorPriceId] ASC
)
WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],

 ) ON [PRIMARY]