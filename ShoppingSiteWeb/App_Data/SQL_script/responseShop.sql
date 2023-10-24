/*
註冊商店

參數:
	@TB_ShopName        商店名稱
    @TB_ShopEMail       Email
    @TB_ShopPhoneNum    電話
    @TB_ShopAddress     地址
    @UserId             用戶ID
輸出:
    是否註冊成功
*/
-- 開始交易
BEGIN TRANSACTION

/**/
INSERT INTO shopTable([shopName],[shopEMail],[shopPhoneNum],[shopAddress])
SELECT
    @TB_ShopName,
    @TB_ShopEMail,
    @TB_ShopPhoneNum,
    @TB_ShopAddress
WHERE Not Exists(
    SELECT shopTable.shopName, shopTable.shopEMail
    FROM shopTable
    WHERE shopName = @TB_ShopName COLLATE SQL_Latin1_General_CP1_CS_AS
    OR shopEMail = @TB_ShopEMail
)

DECLARE @shopId INT
SELECT @shopId = ISNULL(successful.shopId, 0)  from(SELECT SCOPE_IDENTITY() AS shopId) successful
IF NOT @shopId = 0
BEGIN
    INSERT INTO user_shopTable([userId], [shopId])
    SELECT @UserId, @shopId
    WHERE Not Exists(
      SELECT user_shopTable.userId
      FROM user_shopTable
      WHERE userId = @UserId
    )
END

IF @@ROWCOUNT = 1 AND NOT @shopId = 0
BEGIN --註冊成功
	SELECT 1
	COMMIT TRANSACTION
	RETURN
END
ELSE
BEGIN --註冊失敗
	SELECT 0
	ROLLBACK TRANSACTION
	RETURN
END
