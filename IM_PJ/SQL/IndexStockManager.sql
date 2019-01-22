-- =============================================
-- Author:      Binh-TT
-- Create date: 2018-06-04
-- Description: Create index table Stock Manager
-- =============================================
CREATE INDEX [ID_StockManager] ON tbl_StockManager
(
    [ID]
    , [AgentID]
    , [ProductID]
    , [ProductVariableID]
)
GO

CREATE INDEX [ID_SKU] ON tbl_StockManager(SKU)
GO