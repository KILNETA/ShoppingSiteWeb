/*
取得商品所屬商店資料

參數:
	@CommodityId		商品ID
輸出:
    商品所屬商店資料
*/
SELECT
	shopTable.shopId,
	shopName,
	shopEMail,
	shopPhoneNum
FROM shopTable
INNER JOIN shop_commodityTable
ON shop_commodityTable.shopId = shopTable.shopId
WHERE shop_commodityTable.commodityId = @CommodityId