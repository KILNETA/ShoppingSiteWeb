/*
搜索相關的商品

參數:
	@CommoditySearch         搜索關鍵字
輸出:
    相關的商品列表
*/
SELECT
    CT.commodityId,
    CT.commodityName,
    CT.commodityPrice,
    CT.commodityNum,
    CT.commodityThumbnail
FROM commodityTable CT
WHERE CT.commodityName LIKE @CommoditySearch