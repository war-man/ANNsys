CREATE NONCLUSTERED INDEX ID_Product ON tbl_ProductVariable
(
    [ProductID] ASC 
)
INCLUDE
(
    [ID]
)
;