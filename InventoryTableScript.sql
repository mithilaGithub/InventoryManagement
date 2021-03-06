USE [BPInventoryManagement]
GO
/****** Object:  Table [dbo].[ItemMaster]    Script Date: 27/01/2019 10:03:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Unit] [nvarchar](10) NOT NULL,
	[CurrentStock] [int] NOT NULL DEFAULT ((0)),
	[ReorderLevel] [int] NOT NULL,
	[TotalAdded] [int] NOT NULL DEFAULT ((0)),
	[TotalConsumpted] [int] NOT NULL DEFAULT ((0)),
	[CreatedDate] [nvarchar](50) NULL,
	[UpdatedDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_ItemMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StockTransaction]    Script Date: 27/01/2019 10:03:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockTransaction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Date] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_StockTransaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[StockTransaction]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([Id])
GO
