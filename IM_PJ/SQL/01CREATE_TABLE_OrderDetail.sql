CREATE NONCLUSTERED INDEX ID_Order ON tbl_OrderDetail
(
    [AgentID] ASC
,   [OrderID] ASC 
)
INCLUDE
(
    [ID]
,   [SKU]
)
;

CREATE INDEX ID_SKU ON tbl_OrderDetail(SKU)