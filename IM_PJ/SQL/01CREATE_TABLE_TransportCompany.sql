-- =============================================
-- Author:      Binh-TT
-- Create date: 2018-06-06
-- Description: Create table Transport Company
-- =============================================
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (
    SELECT
            NULL AS DUMMY
    FROM
            sys.objects
    WHERE
            object_id = OBJECT_ID(N'[dbo].[tbl_TransportCompany]')
    AND     type in (N'U')
)
BEGIN
    DROP TABLE [dbo].[tbl_TransportCompany]
END

CREATE TABLE [dbo].[tbl_TransportCompany](
    [ID] [INT] NOT NULL,
    [SubID] [INT] NOT NULL,
    [CompanyName] [NVARCHAR](MAX) NULL,
    [CompanyPhone] [NVARCHAR](MAX) NULL,
    [CompanyAddress] [NVARCHAR](MAX) NULL,
    [ShipTo] [NVARCHAR](MAX) NULL,
    [Address] [NVARCHAR](MAX) NULL,
    [Prepay] [BIT] NOT NULL DEFAULT(0),
    [COD] [BIT] NOT NULL DEFAULT(0),
    [Note] [NVARCHAR](MAX) NULL,
    [CreatedDate] [DATETIME] NULL,
    [CreatedBy] [NVARCHAR](MAX) NULL,
    [ModifiedDate] [DATETIME] NULL,
    [ModifiedBy] [NVARCHAR](MAX) NULL,
    CONSTRAINT [PK_tbl_TransportCompany]
        PRIMARY KEY CLUSTERED 
        (
            [ID] ASC,
            [SubID] ASC
        ) WITH (
            PAD_INDEX = OFF
        ,   STATISTICS_NORECOMPUTE = OFF
        ,   IGNORE_DUP_KEY = OFF
        ,   ALLOW_ROW_LOCKS = ON
        ,   ALLOW_PAGE_LOCKS = ON
        ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
