/*
取得用戶資料

參數:
	@UserId         用戶ID
輸出:
    用戶資料
*/
Select * 
FROM userTable 
WHERE userId = @UserId
