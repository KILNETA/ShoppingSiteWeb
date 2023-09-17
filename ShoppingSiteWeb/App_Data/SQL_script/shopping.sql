/*
在購物車執行商品交易

參數:
	@json_str		參數陣列 Json格式
	@UserId         用戶ID
輸出:
    交易狀態(狀態說明)
*/
-- 參數變數
DECLARE  
	@commodityTable AS TABLE(
		_index INT IDENTITY(0, 1),
		Id int,
		Num int
		)
-- 如果不是合格的Json格式
IF((SELECT IsJson(@json_str) as isjson)=0)
BEGIN
	PRINT(N'參數Json格式有誤!')
	RETURN
END

-- 讀取Json格式變數列表
INSERT INTO 
    @commodityTable(Id,Num)
SELECT 
    JSON_VALUE(value,'$.Id') as IdList,
    JSON_VALUE(value,'$.Num') as NumList
FROM
    openJson(@json_str)

-- 變量宣告
DECLARE
    @commodityId AS INT,
    @selectNum AS INT;
DECLARE 
	@transactionId INT = 0,
	@transactionError BIT = 0
DECLARE
	@tableCount INT,
	@tableIndex INT = 0

SELECT @tableCount=COUNT(*) 
FROM @commodityTable

-- 開始交易
BEGIN TRANSACTION
WHILE @tableIndex < @tableCount 
	AND @transactionError = 0
BEGIN
	--表格取值
	SELECT @commodityId=Id, @selectNum=Num 
	FROM @commodityTable
	WHERE @tableIndex=_index

	DECLARE @commodityNum INT
    DECLARE @commodityName NVARCHAR(50)
	SELECT @commodityNum = commodityNum , @commodityName = commodityName
    FROM commodityTable
    WHERE commodityId = @commodityId
    IF(@selectNum > @commodityNum)
    BEGIN
        SELECT @commodityName +N'數量缺少', -1
        SET @transactionError = 1
		RETURN
    END

	--遞增索引
	SET @tableIndex += 1
END

INSERT INTO transactionTable([userId],[transactionDate])
VALUES (@UserId,GETDATE())

SELECT @transactionId = ISNULL(successful.transactionId,0) 
FROM (SELECT SCOPE_IDENTITY() AS transactionId) successful

IF(@transactionId = 0)
	RETURN

--重置索引
SET @tableIndex = 0
WHILE @tableIndex < @tableCount 
	AND @transactionError = 0
BEGIN
	--表格取值
	SELECT @commodityId=Id, @selectNum=Num 
	FROM @commodityTable
	WHERE @tableIndex=_index

	-- 新增商品訂單
	INSERT INTO transaction_recordsTable([transactionId],[commodityId],[commodityNum])
    VALUES (@transactionId,@commodityId,@selectNum)
    IF(@@ROWCOUNT = 0)
		SET @transactionError = 1
	-- 更新商品存貨
	UPDATE commodityTable
	SET commodityNum = @commodityNum - @selectNum
	WHERE commodityId = @commodityId
	IF(@@ROWCOUNT = 0)
		SET @transactionError = 1
	--刪除購物車項目
	DELETE FROM shoppingCartTable
	WHERE userId = @UserId
		AND commodityId = @commodityId
	IF(@@ROWCOUNT = 0)
		SET @transactionError = 1

	--遞增index
	SET @tableIndex += 1
END

IF(@transactionError = 0)
BEGIN --訂單成立
	SELECT N'訂單成立！', 1
	COMMIT TRANSACTION
	RETURN
END
ELSE
BEGIN --訂單失敗
	SELECT N'訂單失敗！', 0
	ROLLBACK TRANSACTION
	RETURN
END