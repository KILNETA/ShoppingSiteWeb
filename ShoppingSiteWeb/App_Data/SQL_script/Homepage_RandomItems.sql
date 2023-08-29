/*
首頁推薦隨機商品

參數:
	@ChooseNum 取出商品數量
輸出:
	商品資訊、商店資訊
*/
SELECT TOP(@ChooseNum)
	CT.commodityId, 
	CT.commodityName, 
	CT.commodityPrice, 
	CT.commodityNum, 
	CT.commodityThumbnail, 
	ST.shopId, 
	ST.shopName 
FROM commodityTable CT 
INNER JOIN shop_commodityTable SCT 
ON SCT.commodityId = CT.commodityId 
INNER JOIN shopTable ST 
ON ST.shopId = SCT.shopId 
ORDER BY NEWID()