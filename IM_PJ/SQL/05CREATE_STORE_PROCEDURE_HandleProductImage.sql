CREATE PROCEDURE HandleProductImage
AS
BEGIN
    DECLARE @Now DateTime;
	SET @Now = GETDATE(); 

    -- Lay nhung product chua update image
    SELECT
		P.ID AS ProductID
    ,   0 AS VariationID
	,	P.ProductSKU AS SKU
	,	N'tbl_Product' AS FromTable
	,	P.ProductImage AS FromImage
	,	N'DELETE' AS [Action]
	,	N'tbl_ProductImage' AS ToTable
	,	PI.ProductImage AS ToImage
	,	N'Xóa những hình ảnh avatar của product có tồn tại trong product image' AS Note
	,	@Now AS CreatedDate
    INTO #ProductImage
    FROM tbl_Product AS P
	INNER JOIN tbl_ProductImage AS PI
	ON P.ID = PI.ProductID
	AND P.ProductImage = PI.ProductImage
    WHERE 
        P.ProductImage IS NOT NULL
        AND LEN(P.ProductImage) > 0
    ORDER BY
        P.ID
    ;

    -- Cap nhat vao trong Product Image
    DELETE tbl_ProductImage 
	FROM tbl_ProductImage AS PI
	WHERE EXISTS (
		SELECT
			NULL AS DUMMY
		FROM
			#ProductImage AS #PI
		WHERE
			PI.ProductID = #PI.ProductID
			AND PI.ProductImage = #PI.ToImage
	)
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
        #PI.ProductID
    ,   #PI.VariationID
    ,   #PI.SKU
    ,   #PI.FromTable
    ,   #PI.FromImage
    ,   #PI.[Action]
    ,   #PI.ToTable
    ,   #PI.ToImage
    ,   #PI.Note
    ,   #PI.CreatedDate
    FROM  #ProductImage AS #PI
    ;
    
    -- Show log
    SELECT
        *
    FROM
        [ANNShopHistory].[dbo].[ResultHandleProductImage]
    WHERE
        [Action] = N'DELETE'
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