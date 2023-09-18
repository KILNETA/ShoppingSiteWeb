/*
查詢交易紀錄

參數:
	@UserId         用戶ID
輸出:
    用戶交易紀錄
*/
Select TT.transactionId,TT.transactionDate,CT.commodityName,CT.commodityId,CT.commodityPrice,CT.commodityThumbnail,TRT.commodityNum
FROM transaction_recordsTable TRT
INNER JOIN commodityTable CT
ON TRT.commodityId = CT.commodityId
INNER JOIN transactionTable TT
ON TRT.commodityId = CT.commodityId
WHERE TRT.transactionId = TT.transactionId AND TT.userId = @UserId
ORDER BY TT.transactionDate desc, TT.transactionId desc