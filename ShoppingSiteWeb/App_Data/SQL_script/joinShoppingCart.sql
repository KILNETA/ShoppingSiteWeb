/*
將商品加入用戶購物車

參數:
	@UserId         用戶ID
	@CommodityId    欲購買商品ID
	@CommodityNum   欲購買商品數量
輸出:
    狀態碼
     1:已加入購物車
    -1:已存在購物車中
*/

--流程變數:紀錄是否已存在購物車中--
DECLARE @hasInCart INT

--查詢是否已存在購物車中--
IF EXISTS(
    SELECT 1
    From shoppingCartTable
    Where  userId = @UserId
    AND commodityId = @CommodityId
)
    SET @hasInCart = 1
ELSE
    SET @hasInCart = 0

IF(@hasInCart = 0)
    --(不存在購物車中)--
    BEGIN
        --將商品加入購物車--
        INSERT INTO shoppingCartTable([joinDate],[userId],[commodityId],[commodityNum])
        Select
            GETDATE(),
            @UserId,
            @CommodityId,
            @CommodityNum
        Where Not Exists(
            Select userId,commodityId
            From shoppingCartTable
            Where userId = @UserId
                AND commodityId = @CommodityId
        )
        --(插入的Row數) 回傳狀態值(1)--
        Select @@ROWCOUNT
    END
ELSE
    --(已存在購物車中) 回傳狀態值(-1)--
    Select '-1'