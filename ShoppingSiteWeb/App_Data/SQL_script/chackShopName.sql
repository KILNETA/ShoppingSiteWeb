/*
取得商店名稱是否被使用

參數:
	@TB_ShopName		商店名稱
輸出:
    商店名稱是否被使用
*/
Select COUNT(*)
FROM shopTable
WHERE ( shopName = @TB_ShopName COLLATE SQL_Latin1_General_CP1_CS_AS )