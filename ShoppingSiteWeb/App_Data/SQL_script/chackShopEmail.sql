/*
確認使否已有帳號使用該Email

參數:
	@TB_ShopEMail	查詢Email
輸出:
    是否存在
*/
Select COUNT(*)
FROM shopTable
WHERE( shopEMail = @TB_ShopEMail )