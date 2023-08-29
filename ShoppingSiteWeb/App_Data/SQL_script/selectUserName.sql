/*
查詢用戶名稱

參數:
	@UserId         用戶ID
輸出:
    用戶名稱
*/
Select [userName]
FROM [userTable]
WHERE( userId = @UserID )