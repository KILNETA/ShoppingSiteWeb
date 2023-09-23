/*
確認使否已有帳號使用該用戶名

參數:
	@TB_User		查詢用戶名
輸出:
    是否存在
*/
Select COUNT(0)
FROM userTable
WHERE ( userName = @TB_User COLLATE SQL_Latin1_General_CP1_CS_AS )