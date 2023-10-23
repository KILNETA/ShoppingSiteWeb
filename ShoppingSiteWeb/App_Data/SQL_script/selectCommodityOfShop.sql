/*
查詢商店販賣的商品資料

參數:
    @ShopId    商店編號
輸出:
    商品資料
*/
SELECT
	CT.commodityId,
	CT.commodityName,
	CT.commodityPrice,
	CT.commodityNum,
	CT.commodityThumbnail
FROM commodityTable CT
INNER JOIN shop_commodityTable SCT
ON SCT.commodityId = CT.commodityId
WHERE SCT.shopId = @ShopId