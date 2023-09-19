/*
取得商店資料

參數:
	@UserId         用戶ID
輸出:
    商店資料
*/
Select shopTable.*
FROM [shopTable]
Inner join [user_shopTable]
On shopTable.shopId = user_shopTable.shopId
Inner join [userTable]
On user_shopTable.userId = userTable.userId
WHERE( userTable.userId = @UserID )
