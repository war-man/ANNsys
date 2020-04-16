CREATE PROCEDURE BackupOrder
    @date NVARCHAR(10)
AS
BEGIN
    -- Lay du lieu don hang tai main table
    SELECT
        *
    INTO #order
    FROM
        inventorymanagement.dbo.tbl_Order
    WHERE
        CONVERT(nvarchar(10), CreatedDate, 121) < @date
    ORDER BY CreatedDate;

    -- Lay du lieu chi tiet don hang tai main table
    SELECT
        BODY.*
    INTO #orderDetail
    FROM
        inventorymanagement.dbo.tbl_OrderDetail AS BODY
    INNER JOIN #order AS HEADER
    ON HEADER.ID = BODY.OrderID
    ORDER BY BODY.CreatedDate;

    -- Backup du lieu vao database hien tai
    SET IDENTITY_INSERT dbo.tbl_Order ON;
    INSERT INTO dbo.tbl_Order (
        [ID]
        ,[AgentID]
        ,[OrderType]
        ,[AdditionFee]
        ,[DisCount]
        ,[CustomerID]
        ,[CustomerName]
        ,[CustomerPhone]
        ,[CustomerAddress]
        ,[CustomerEmail]
        ,[TotalPrice]
        ,[PaymentStatus]
        ,[ExcuteStatus]
        ,[IsHidden]
        ,[WayIn]
        ,[CreatedDate]
        ,[CreatedBy]
        ,[ModifiedDate]
        ,[ModifiedBy]
        ,[DiscountPerProduct]
        ,[TotalDiscount]
        ,[FeeShipping]
        ,[TotalPriceNotDiscount]
        ,[GuestPaid]
        ,[GuestChange]
        ,[FeeRefund]
        ,[PaymentType]
        ,[ShippingType]
        ,[OrderNote]
        ,[DateDone]
        ,[RefundsGoodsID]
        ,[ShippingCode]
        ,[TransportCompanyID]
        ,[TransportCompanySubID]
        ,[OtherFeeName]
        ,[OtherFeeValue]
        ,[PostalDeliveryType]
        ,[CustomerNewPhone]
        ,[UserHelp]
        ,[VerifiedCOD]
        ,[VerifiedBy]
        ,[CouponID]
        ,[CouponValue]
    )
    SELECT
        MAIN.[ID]
        ,MAIN.[AgentID]
        ,MAIN.[OrderType]
        ,MAIN.[AdditionFee]
        ,MAIN.[DisCount]
        ,MAIN.[CustomerID]
        ,MAIN.[CustomerName]
        ,MAIN.[CustomerPhone]
        ,MAIN.[CustomerAddress]
        ,MAIN.[CustomerEmail]
        ,MAIN.[TotalPrice]
        ,MAIN.[PaymentStatus]
        ,MAIN.[ExcuteStatus]
        ,MAIN.[IsHidden]
        ,MAIN.[WayIn]
        ,MAIN.[CreatedDate]
        ,MAIN.[CreatedBy]
        ,MAIN.[ModifiedDate]
        ,MAIN.[ModifiedBy]
        ,MAIN.[DiscountPerProduct]
        ,MAIN.[TotalDiscount]
        ,MAIN.[FeeShipping]
        ,MAIN.[TotalPriceNotDiscount]
        ,MAIN.[GuestPaid]
        ,MAIN.[GuestChange]
        ,MAIN.[FeeRefund]
        ,MAIN.[PaymentType]
        ,MAIN.[ShippingType]
        ,MAIN.[OrderNote]
        ,MAIN.[DateDone]
        ,MAIN.[RefundsGoodsID]
        ,MAIN.[ShippingCode]
        ,MAIN.[TransportCompanyID]
        ,MAIN.[TransportCompanySubID]
        ,MAIN.[OtherFeeName]
        ,MAIN.[OtherFeeValue]
        ,MAIN.[PostalDeliveryType]
        ,MAIN.[CustomerNewPhone]
        ,MAIN.[UserHelp]
        ,MAIN.[VerifiedCOD]
        ,MAIN.[VerifiedBy]
        ,MAIN.[CouponID]
        ,MAIN.[CouponValue]
    FROM
        #order AS MAIN
    WHERE NOT EXISTS (
        SELECT
            NULL AS DUMMY
        FROM
            dbo.tbl_Order AS SUB
        WHERE
            MAIN.ID = SUB.ID
    );
    SET IDENTITY_INSERT dbo.tbl_Order OFF;
    SELECT COUNT(*) AS RowOrder FROM dbo.tbl_Order;


    SET IDENTITY_INSERT dbo.tbl_OrderDetail ON;
    INSERT INTO dbo.tbl_OrderDetail (
        [ID]
        ,[AgentID]
        ,[OrderID]
        ,[SKU]
        ,[ProductID]
        ,[ProductVariableID]
        ,[ProductVariableDescrition]
        ,[Quantity]
        ,[Price]
        ,[Status]
        ,[DiscountPrice]
        ,[ProductType]
        ,[CreatedDate]
        ,[CreatedBy]
        ,[ModifiedDate]
        ,[ModifiedBy]
        ,[IsCount]
    )
    SELECT
        MAIN.[ID]
        ,MAIN.[AgentID]
        ,MAIN.[OrderID]
        ,MAIN.[SKU]
        ,MAIN.[ProductID]
        ,MAIN.[ProductVariableID]
        ,MAIN.[ProductVariableDescrition]
        ,MAIN.[Quantity]
        ,MAIN.[Price]
        ,MAIN.[Status]
        ,MAIN.[DiscountPrice]
        ,MAIN.[ProductType]
        ,MAIN.[CreatedDate]
        ,MAIN.[CreatedBy]
        ,MAIN.[ModifiedDate]
        ,MAIN.[ModifiedBy]
        ,MAIN.[IsCount]
    FROM
        #orderDetail AS MAIN
    WHERE NOT EXISTS (
        SELECT
            NULL AS DUMMY
        FROM
            dbo.tbl_OrderDetail AS SUB
        WHERE
            MAIN.ID = SUB.ID
    );
    SET IDENTITY_INSERT dbo.tbl_OrderDetail ON;
    SELECT COUNT(*) AS RowOrderDetail FROM dbo.tbl_OrderDetail;

    -- Remove nhung data da duoc backup
    SELECT COUNT(*) AS RowOrderDetailBefore FROM inventorymanagement.dbo.tbl_OrderDetail;
    DELETE inventorymanagement.dbo.tbl_OrderDetail
    FROM inventorymanagement.dbo.tbl_OrderDetail AS MAIN
    WHERE Exists (
        SELECT
            NULL AS DUMMY
        FROM
            #orderDetail as SUB
        WHERE
            MAIN.ID = SUB.ID
    );
    SELECT COUNT(*) AS RowOrderDetailAfter FROM inventorymanagement.dbo.tbl_OrderDetail;

    SELECT COUNT(*) AS RowOrderBefore FROM inventorymanagement.dbo.tbl_Order;
    DELETE inventorymanagement.dbo.tbl_Order
    FROM inventorymanagement.dbo.tbl_Order AS MAIN
    WHERE Exists (
        SELECT
            NULL AS DUMMY
        FROM
            #order as SUB
        WHERE
            MAIN.ID = SUB.ID
    );
    SELECT COUNT(*) AS RowOrderAfter FROM inventorymanagement.dbo.tbl_Order;
END
