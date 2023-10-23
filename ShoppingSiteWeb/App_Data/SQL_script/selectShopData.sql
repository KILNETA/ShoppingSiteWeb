/*
查詢商店資料

參數:
    @ShopId    商店編號
輸出:
    商店資料
*/
SELECT
	shopName,
	shopEMail,
	shopPhoneNum,
	shopAddress
FROM shopTable
WHERE shopId = @ShopId