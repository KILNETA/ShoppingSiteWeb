/*
更改在購物車商品數量

參數:
	@UserId         用戶ID
	@CommodityId	商品ID
	@CommodityNum	商品數量
輸出:
    交易狀態
*/
-- 參數變數
UPDATE shoppingCartTable
SET commodityNum = @CommodityNum
WHERE userId = @UserId
	AND commodityId = @CommodityId