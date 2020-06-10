CREATE NONCLUSTERED INDEX ID_RefundGoods ON tbl_RefundGoodsDetails
(
    [AgentID] ASC
,   [CustomerID] ASC
,   [RefundGoodsID] ASC 
)
INCLUDE
(
    [ID]
,   [SKU]
)
;