/*
確認使否已有帳號使用該Email

參數:
	@TB_EMail		查詢Email
輸出:
    是否存在
*/
Select COUNT(*)
FROM userTable
WHERE( userEMail = @TB_EMail )