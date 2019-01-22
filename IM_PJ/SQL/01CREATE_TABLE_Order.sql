CREATE NONCLUSTERED INDEX ID_Customer ON tbl_Order
(
    [AgentID] ASC
,   [CustomerID] ASC
,   [CreatedDate] ASC
)
INCLUDE
(
    [ID]
)
;