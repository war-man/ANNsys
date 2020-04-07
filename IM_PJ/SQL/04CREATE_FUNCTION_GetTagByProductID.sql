IF OBJECT_ID (N'dbo.ufnGetTagByProductID', N'FN') IS NOT NULL  
    DROP FUNCTION ufnGetTagByProductID;  
GO  
CREATE FUNCTION dbo.ufnGetTagByProductID(@ProductID INT)  
RETURNS NVARCHAR(MAX)   
AS   
BEGIN  
    DECLARE @tag NVARCHAR(MAX);  

    SELECT @tag = COALESCE( @tag + ', ', '') + t.Name 
    FROM
        ProductTag AS pt
    INNER JOIN Tag AS t
    ON pt.TagID = t.ID
    WHERE pt.ProductID = @ProductID
    ;

    RETURN @tag;  
END; 
