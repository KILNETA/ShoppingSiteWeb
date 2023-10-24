/*
檢查該用戶是否已擁有商店

參數:
    @UserId    用戶編號
輸出:
    是否已擁有商店
*/
Select COUNT(*)
FROM user_shopTable
WHERE ( userId = @UserId )