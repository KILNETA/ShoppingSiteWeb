/*
刪除購物車商品

參數:
	@UserId         用戶ID
	@CommodityId	商品ID
輸出:
    交易狀態
*/
-- 參數變數
DELETE FROM shoppingCartTable
WHERE userId = @UserId
	AND commodityId = @CommodityId

SELECT @@ROWCOUNT 