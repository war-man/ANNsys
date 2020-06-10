USE [inventorymanagement]
GO

/****** Object:  Table [dbo].[CheckWarehouse]    Script Date: 6/6/2020 4:42:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CheckWarehouse](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Stock] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](255) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_CheckWarehouse] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CheckWarehouse] ADD  CONSTRAINT [DF_CheckWarehouse_Stock]  DEFAULT ((1)) FOR [Stock]
GO

ALTER TABLE [dbo].[CheckWarehouse] ADD  CONSTRAINT [DF_CheckWarehouse_Active]  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [dbo].[CheckWarehouse] ADD  CONSTRAINT [DF_CheckWarehouse_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[CheckWarehouse] ADD  CONSTRAINT [DF_CheckWarehouse_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO


