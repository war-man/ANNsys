CREATE PROCEDURE ClearStock
AS
BEGIN
    SELECT
        MAX(ID) AS IDLAST
    INTO #stockLast
    FROM
        tbl_StockManager AS STM
    GROUP BY
        STM.SKU;

    DELETE tbl_StockManager
    WHERE NOT Exists (
        SELECT
            NULL AS DUMMY
        FROM
            #stockLast as STL
        WHERE
            ID = IDLAST
    );
END
