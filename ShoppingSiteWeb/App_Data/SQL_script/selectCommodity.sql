/*
取得商品資料

參數:
	@CommodityId		商品ID
輸出:
    商品資料
*/
SELECT
	commodityId,
	commodityName,
	commodityPrice,
	commodityNum,
	commodityThumbnail,
	commodityIntroduction
FROM commodityTable
WHERE commodityId = @CommodityId