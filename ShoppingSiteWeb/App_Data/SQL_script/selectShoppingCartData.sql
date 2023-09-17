/*
查詢購物車商品

參數:
	@UserId         用戶ID
輸出:
    購物車商品列表
*/
SELECT DISTINCT
    SCT.commodityId,
    SCT.commodityNum,
    CT.commodityName,
    CT.commodityPrice,
    CT.commodityNum,
    CT.commodityThumbnail,
    CT.commodityIntroduction
FROM shoppingCartTable SCT
INNER JOIN commodityTable CT
    ON SCT.commodityId = CT.commodityId
WHERE SCT.userId = @UserId