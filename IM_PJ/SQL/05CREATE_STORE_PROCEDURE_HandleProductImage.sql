CREATE PROCEDURE HandleProductImage
AS
BEGIN
    DECLARE @Now DateTime;

    -- Lay nhung product chua update image
    SELECT
        P.*
    INTO #ProductImage
    FROM tbl_Product AS P
    WHERE 
        P.ProductImage IS NOT NULL
        AND LEN(P.ProductImage) > 0
        AND NOT EXISTS (
            SELECT
                NULL AS DUMMY
            FROM
                tbl_ProductImage AS PI
            WHERE
                P.ID = PI.ProductID
                AND SUBSTRING(P.ProductImage, 0, PATINDEX('%.[a-z]%', P.ProductImage)) = SUBSTRING(PI.ProductImage, 0, PATINDEX('%.[a-z]%', PI.ProductImage))
        )
    ORDER BY
        P.ID
    ;

    -- Cap nhat vao trong Product Image
    SET @Now = GETDATE(); 
    INSERT INTO tbl_ProductImage (
        ProductID
    ,   ProductImage
    ,   IsHidden
    ,   CreatedDate
    ,   CreatedBy
    ,   ModifiedDate
    ,   ModifiedBy
    )
    SELECT
        #PI.ID
    ,   #PI.ProductImage
    ,   0
    ,   @Now
    ,   N'admin'
    ,   @Now
    ,   N'admin'
    FROM  #ProductImage AS #PI
    ;

    -- Cap nhat thong tin log
    INSERT INTO [ANNShopHistory].[dbo].[ResultHandleProductImage] (
        ProductID
    ,   VariationID
    ,   SKU
    ,   FromTable
    ,   FromImage
    ,   [Action]
    ,   ToTable
    ,   ToImage
    ,   Note
    ,   CreatedDate
    )
    SELECT 
        #PI.ID
    ,   0
    ,   #PI.ProductSKU
    ,   N'tbl_Product'
    ,   #PI.ProductImage
    ,   N'ADD'
    ,   N'tbl_ProductImage'
    ,   NULL
    ,   N'Thêm ảnh sản phẩm với những sản phẩm có hình ảnh nhưng lại không tồn tại trong Product Image Table'
    ,   @Now
    FROM  #ProductImage AS #PI
    ;
    
    -- Show log
    SELECT
        *
    FROM
        [ANNShopHistory].[dbo].[ResultHandleProductImage]
    WHERE
        [Action] = N'ADD'
        AND CreatedDate = @Now
    ;

    -- Lay nhung variation co image format {ProductID}-{hhmmssffff}-{Image Name}.{png|*}
    SELECT
        ProductID
    ,   ID AS VariationID
    ,   SKU
    ,   [Image] AS ImageFull
    ,   SUBSTRING(
        [Image], 
        LEN(ProductID) + 13,
        PATINDEX('%.[a-z]%', [Image]) - LEN(ProductID) - 13
    ) AS ImageName
    ,   SUBSTRING([Image], (PATINDEX('%.[a-z]%', [Image]) + 1), LEN([Image])) AS ImageType
    INTO #VariationImage
    FROM
        tbl_ProductVariable
    WHERE
        [Image] IS NOT NULL
        AND [Image] LIKE CONCAT(ProductID, '-', '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]-%')
    ORDER BY
        ProductID DESC
    ,   ID DESC
    ;

    SELECT
        PI.ProductID
    ,   VI.VariationID
    ,   VI.SKU
    ,   VI.ImageFull AS ImageRemove
    ,   PI.ProductImage AS ImageNew
    INTO #VariationImageRemove
    FROM
        tbl_ProductImage AS PI
        INNER JOIN #VariationImage AS VI
        ON PI.ProductID = VI.ProductID
        AND SUBSTRING(PI.ProductImage, 0, PATINDEX('%.[a-z]%', PI.ProductImage)) LIKE CONCAT('%', VI.ImageName)
    ;

    -- Khai bao cac bien the de dùng cho FETCH 
    DECLARE @ProductID int,
        @VariationID int,
        @SKU nvarchar(255),
        @ImageRemove nvarchar(max),
        @ImageNew nvarchar(max);  

    DECLARE variation_image_cursor CURSOR FOR  
    SELECT 
        ProductID
    ,   VariationID
    ,   SKU
    ,   ImageRemove
    ,   ImageNew
    FROM
        #VariationImageRemove
    ORDER BY 
        ProductID
    ,   VariationID
    ;  
    
    OPEN variation_image_cursor;  
        
    SET @Now = GETDATE(); 
    FETCH NEXT FROM variation_image_cursor  
    INTO @ProductID, @VariationID, @SKU, @ImageRemove, @ImageNew;  
    
    -- Check @@FETCH_STATUS to see if there are any more rows to fetch.  
    WHILE @@FETCH_STATUS = 0  
    BEGIN  
        -- Cap nhat Product Variation Table
        UPDATE tbl_ProductVariable
        SET 
            [Image] = @ImageNew
        WHERE
            ProductID = @ProductID
            AND ID = @VariationID
        ;
    
        -- Cap nhat thong tin log
        INSERT INTO [ANNShopHistory].[dbo].[ResultHandleProductImage] (
            ProductID
        ,   VariationID
        ,   SKU
        ,   FromTable
        ,   FromImage
        ,   Action
        ,   ToTable
        ,   ToImage
        ,   Note
        ,   CreatedDate
        )
        VALUES (
            @ProductID
        ,   @VariationID
        ,   @SKU
        ,   N'tbl_ProductImage'
        ,   @ImageNew
        ,   N'UPDATE'
        ,   N'tbl_ProductVariable'
        ,   @ImageRemove
        ,   N'Xoá những hình ảnh của biến thể đã tồn tại trong product image'
        ,   @Now
        );
    
    -- This is executed as long as the previous fetch succeeds.  
    FETCH NEXT FROM variation_image_cursor  
    INTO @ProductID, @VariationID, @SKU, @ImageRemove, @ImageNew;  
    END  
    
    CLOSE variation_image_cursor;  
    DEALLOCATE variation_image_cursor;

    -- Show log
    SELECT
        *
    FROM
        [ANNShopHistory].[dbo].[ResultHandleProductImage]
    WHERE
        [Action] = N'UPDATE'
        AND CreatedDate = @Now
    ;
END