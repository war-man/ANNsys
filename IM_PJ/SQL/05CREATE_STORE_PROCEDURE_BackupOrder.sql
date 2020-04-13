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
    FROM
        #order
    ;
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
    FROM
        #orderDetail
    ;
    SET IDENTITY_INSERT dbo.tbl_OrderDetail ON;
    SELECT COUNT(*) AS RowOrderDetail FROM dbo.tbl_OrderDetail;

    -- Remove nhung data da duoc backup
    SELECT COUNT(*) AS RowOrderDetailBefore FROM inventorymanagement.dbo.tbl_OrderDetail;
    DELETE inventorymanagement.dbo.tbl_OrderDetail
    FROM inventorymanagement.dbo.tbl_OrderDetail AS MAIN
    WHERE NOT Exists (
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
    WHERE NOT Exists (
        SELECT
            NULL AS DUMMY
        FROM
            #order as SUB
        WHERE
            MAIN.ID = SUB.ID
    );
    SELECT COUNT(*) AS RowOrderAfter FROM inventorymanagement.dbo.tbl_Order;
END
