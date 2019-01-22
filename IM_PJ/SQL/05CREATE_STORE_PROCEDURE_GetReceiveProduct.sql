-- =============================================
-- Author:      Binh-TT
-- Create date: 2018-06-07
-- Description: Get Receive Product
-- =============================================
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE GetReceiveProduct
    @UserName NVARCHAR(MAX)
,   @AgentID INT
,   @CustomerPhone NVARCHAR(15)
,   @SKU NVARCHAR(MAX)
,   @RowIndex INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @DateNow AS DATETIME
    DECLARE @FeeRefund AS FLOAT
    DECLARE @DateToChangeProduct AS FLOAT
    DECLARE @ProductCanChange AS FLOAT
    DECLARE @MSG AS NVARCHAR(MAX)

    SET @DateNow = GetDate()
    SET @FeeRefund = 0
    SET @DateToChangeProduct = 0
    SET @ProductCanChange = 0
    SET @MSG = ''

    SELECT TOP 1
            *
    INTO #Customer
    FROM
            tbl_Customer AS CTM
    WHERE
            CTM.CustomerPhone = @CustomerPhone
    ;

    IF @@ROWCOUNT = 0
    BEGIN
        SET @MSG = N'Không tìm thấy khách hàng với sdt: ' + @CustomerPhone;
        THROW 50010, @MSG, 1;
    END

    -- Get infor system
    SELECT
            @FeeRefund = FeeChangeProduct
    ,       @DateToChangeProduct = NumOfDateToChangeProduct
    ,       @ProductCanChange = NumOfProductCanChange
    FROM
            tbl_Config
    WHERE
        ID = 1
    ;

    -- Update infor with user of group
    SELECT TOP 1
            @FeeRefund = FeeRefund
    ,       @DateToChangeProduct = NumOfDateToChangeProduct
    ,       @ProductCanChange = NumOfProductCanChange
    FROM
            #Customer AS OWN
    INNER JOIN tbl_DiscountCustomer AS CTM
        ON  OWN.ID = CTM.UID
        AND CTM.IsHidden = 0
    INNER JOIN tbl_DiscountGroup AS GRP
        ON  CTM.DiscountGroupID = GRP.ID
    ORDER BY GRP.DiscountAmount DESC
    ;

    -- Get RefundGoods
    SELECT
            RFG.AgentID
    ,       RFG.CustomerID
    ,       ISNULL(RFG.TotalQuantity, 0) AS TotalQuantity
    ,       ISNULL(RFG.TotalRefundFee, 0) AS TotalRefundFee
    ,       RFG.ID AS RefundGoodID
    ,       RGD.ID AS RefundGoodDetailID
    ,       CAST(RGD.SKU AS NVARCHAR(20)) AS SKU
    ,       ISNULL(RGD.QuantityMax, 0) AS QuantityMax
    ,       ISNULL(RGD.Quantity, 0) AS Quantity
    INTO #RefundGoods
    FROM
            #Customer AS CTM
    INNER JOIN tbl_RefundGoods AS RFG
        ON  RFG.AgentID = @AgentID
        AND CTM.ID = RFG.CustomerID
    INNER JOIN tbl_RefundGoodsDetails AS RGD
        ON  RFG.AgentID = RGD.AgentID
        AND RFG.CustomerID = RFG.CustomerID
        AND RFG.ID = RGD.RefundGoodsID
    WHERE
            RFG.CreatedDate BETWEEN DATEADD(DAY, (-1) * @DateToChangeProduct, @DateNow) AND @DateNow
    ORDER BY
            RGD.AgentID
    ,       RGD.CustomerID
    ,       RGD.SKU
    ;

    SELECT
            @ProductCanChange = @ProductCanChange - SUM(Quantity)
    FROM
            #RefundGoods
    GROUP BY
            AgentID
    ,       CustomerID
    ;

    IF @ProductCanChange < 1
    BEGIN
        SET @MSG = N'Khách hàng đã đôi trã quá số lượng';
        THROW 50020, @MSG, 1;
    END

    SELECT
            ROW_NUMBER() OVER(ORDER BY ODD.CreatedDate DESC, ORD.ID DESC) AS RowIndex
    ,       ORD.AgentID
    ,       ORD.CustomerID
    ,       ORD.ID AS OrderID
    ,       ODD.ID AS OrderDetailID
    ,       PRD.ProductTitle AS ProductName
    ,       ODD.ProductType AS ProductType
    ,       ODD.SKU
    ,       ISNULL(ODD.Price, 0) AS Price
    ,       (ISNULL(ODD.Price, 0) - ISNULL(ORD.DiscountPerProduct, 0)) AS ReducedPrice
    ,       ISNULL(ORD.DiscountPerProduct, 0) AS DiscountPerProduct
    ,       ISNULL(ODD.Quantity, 0) AS Quantity
    INTO #ReceiveProduct
    FROM
            #Customer AS CTM
    INNER JOIN tbl_Order AS ORD
        ON  ORD.AgentID = @AgentID
        AND CTM.ID = ORD.CustomerID
    INNER JOIN tbl_OrderDetail AS ODD
        ON  ORD.AgentID = ODD.AgentID
        AND ORD.ID = ODD.OrderID
    INNER JOIN (
            SELECT
                    PRO.ID AS ProductID
            ,       PRV.ID AS ProductVariableID
            ,       PRO.ProductTitle
            FROM
                    tbl_Product AS PRO
            LEFT JOIN tbl_ProductVariable AS PRV
                ON PRO.ID = PRV.ProductID
        ) PRD
        ON (
            ODD.ProductType = 1
            AND ODD.ProductID = PRD.ProductID
        )
        OR (
            ODD.ProductType = 2
            AND ODD.ProductVariableID = PRD.ProductVariableID
        )
    WHERE
            ORD.CreatedDate BETWEEN DATEADD(DAY, (-1) * @DateToChangeProduct, @DateNow) AND @DateNow
    AND     ORD.PaymentStatus = 3
    AND     ORD.ExcuteStatus = 2
    AND     UPPER(ODD.SKU) = UPPER(@SKU)
    ORDER BY
            ORD.CreatedDate DESC
    ,       ORD.ID
    ,       ODD.SKU
    ;

    SELECT
            PRD.OrderID
    ,       PRD.OrderDetailID
    ,       CAST(PRD.RowIndex AS INT) AS RowIndex
    ,       PRD.ProductName
    ,       PRD.ProductType
    ,       PRD.SKU
    ,       PRD.Price
    ,       PRD.ReducedPrice
    ,       PRD.DiscountPerProduct
    ,       PRD.Quantity AS QuantityOrder
    ,       (PRD.Quantity - ISNULL(RFG.QuantityRefund, 0)) AS QuantityLeft
    ,       @FeeRefund AS FeeRefund
    INTO #Result
    FROM
            #ReceiveProduct AS PRD
    LEFT JOIN (
                SELECT
                        AgentID
                ,       CustomerID
                ,       SKU
                ,       SUM(ISNULL(Quantity, 0)) AS QuantityRefund
                FROM
                        #RefundGoods
                GROUP BY
                        AgentID
                ,       CustomerID
                ,       SKU
        ) AS RFG
        ON  PRD.AgentID = RFG.AgentID
        AND PRD.CustomerID = RFG.CustomerID
        AND PRD.SKU = RFG.SKU
    WHERE
            RowIndex = @RowIndex
    ;

    IF @@RowCount > 0
    BEGIN
        IF (SELECT QuantityLeft FROM #Result) < 1
        BEGIN
            SET @MSG = N'Số lượng đổi trả nhỏ hơn 1';
            THROW 50030, @MSG, 1;
        END
        ELSE
        BEGIN
            SELECT
                    OrderID
            ,       OrderDetailID
            ,       RowIndex
            ,       ProductName
            ,       ProductType
            ,       SKU
            ,       Price
            ,       ReducedPrice
            ,       DiscountPerProduct
            ,       QuantityOrder
            ,       QuantityLeft
            ,       FeeRefund
            FROM
                    #Result
        END
    END
END
