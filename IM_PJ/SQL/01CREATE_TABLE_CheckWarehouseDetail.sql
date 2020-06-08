USE [inventorymanagement]
GO

/****** Object:  Table [dbo].[CheckWarehouseDetail]    Script Date: 6/6/2020 4:43:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CheckWarehouseDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CheckWarehouseID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[ProductVariableID] [int] NOT NULL,
	[ProductSKU] [nvarchar](50) NOT NULL,
	[QuantityOld] [int] NOT NULL,
	[QuantityNew] [int] NULL,
	[Note] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_CheckWarehouseDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[CheckWarehouseDetail] ADD  CONSTRAINT [DF_CheckWarehouseDetail_ProductID]  DEFAULT ((0)) FOR [ProductID]
GO

ALTER TABLE [dbo].[CheckWarehouseDetail] ADD  CONSTRAINT [DF_CheckWarehouseDetail_ProductVariableID]  DEFAULT ((0)) FOR [ProductVariableID]
GO


