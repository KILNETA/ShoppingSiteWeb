/*
直接購買單個商品

參數:
	@UserId         用戶ID
	@CommodityId    商品ID
	@SelectNum      購買數量
輸出:
    交易狀態(狀態說明)
*/
DECLARE @transactionId INT = 0
DECLARE @transactionError BIT = 0
BEGIN TRANSACTION
DECLARE @commodityNum INT
DECLARE @commodityName NVARCHAR(50)
SELECT @commodityNum = commodityNum , @commodityName = commodityName
FROM commodityTable
WHERE commodityId = @CommodityId
IF(@SelectNum > @commodityNum)
BEGIN
	SELECT @commodityName +N'數量缺少', -1
	SET @transactionError = 1
END
IF(@transactionError = 0)
BEGIN
	INSERT INTO transactionTable([userId],[transactionDate])
	VALUES (@userId,GETDATE())
	SELECT @transactionId = ISNULL(successful.transactionId,0) from (SELECT SCOPE_IDENTITY() AS transactionId) successful
	IF(@transactionId !=0)
	BEGIN
		INSERT INTO transaction_recordsTable([transactionId],[commodityId],[commodityNum])
		VALUES (@transactionId,@CommodityId,@SelectNum)
		IF(@@ROWCOUNT = 0)
			SET @transactionError = 1
		UPDATE commodityTable
		SET commodityNum = @commodityNum - @SelectNum
		WHERE commodityId = @CommodityId
		IF(@@ROWCOUNT = 0)
        	SET @transactionError = 1
	END
ELSE
	SET @transactionError = 1
END
IF(@transactionError = 0)
BEGIN
	SELECT N'訂單成立！', 1
	COMMIT TRANSACTION
END
ELSE
BEGIN
	SELECT N'訂單失敗！', 0
	ROLLBACK TRANSACTION
END