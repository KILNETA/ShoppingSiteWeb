/*
上架商品

參數:
	@UserID         			用戶ID
	@TB_CommodityName    		商品名稱
	@TB_CommodityPrice      	商品價格
	@TB_CommodityNum         	商品數量
	@TB_CommodityIntroduction	商品簡介
	@TB_CommodityThumbnail      商品縮圖
輸出:
    上架狀態(狀態說明)
*/
-- 開始交易
BEGIN TRANSACTION

DECLARE @shopId INT
SELECT @shopId = user_shopTable.shopId
FROM shopTable
INNER JOIN user_shopTable
ON shopTable.shopId = user_shopTable.shopId
WHERE user_shopTable.userId = @UserID
INSERT INTO commodityTable([commodityName],[commodityPrice],[commodityNum],[commodityIntroduction],[commodityThumbnail])
VALUES(
	@TB_CommodityName,
	@TB_CommodityPrice,
	@TB_CommodityNum,
	@TB_CommodityIntroduction,
	@TB_CommodityThumbnail
)
DECLARE @commodityId INT
SELECT @commodityId = ISNULL(successful.commodityId, 0) from(SELECT SCOPE_IDENTITY() AS commodityId) successful
IF (@commodityId != 0)
	BEGIN
		INSERT INTO shop_commodityTable([commodityId], [shopId])
		SELECT @commodityId, @shopId
		Where Not Exists(
			Select shop_commodityTable.commodityId
			From shop_commodityTable
			Where commodityId = @commodityId
		)
	END

IF @@ROWCOUNT = 1 AND NOT @commodityId = 0
BEGIN --上架成功
	SELECT 1
	COMMIT TRANSACTION
	RETURN
END
ELSE
BEGIN --上架失敗
	SELECT 0
	ROLLBACK TRANSACTION
	RETURN
END