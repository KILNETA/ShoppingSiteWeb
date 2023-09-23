/*
註冊使用者帳號

參數:
	@TB_userName        用戶名稱
    @TB_userPhoneNum    電話
    @TB_userEMail       Email
    @TB_userRealName    名稱
    @TB_userBirthday    生日
    @TB_userAddress     地址
    @TB_userPassword    密碼
輸出:
    是否註冊成功,用戶ID
*/
-- 開始交易
BEGIN TRANSACTION
INSERT INTO userTable([userName],[userPhoneNum],[userEMail],[userRealName],[userBirthday],[userAddress],[userPassword])
SELECT
    @TB_userName,
    @TB_userPhoneNum,
    @TB_userEMail,
    @TB_userRealName,
    @TB_userBirthday,
    @TB_userAddress,
    @TB_userPassword
WHERE Not Exists(
    SELECT userTable.userName,userTable.userEMail
    FROM userTable
    WHERE userName = @TB_userName COLLATE SQL_Latin1_General_CP1_CS_AS
    OR userEMail = @TB_userEMail
    )

DECLARE 
	@RSuccessful AS BIT,
	@RUserId AS INT
SELECT @RSuccessful = ISNULL(successful.userId,0) ,@RUserId = userId
FROM (SELECT SCOPE_IDENTITY() AS userId)
successful

IF @RSuccessful = 1 AND NOT @RUserId = 0
BEGIN --註冊成功
	SELECT @RSuccessful,@RUserId
	COMMIT TRANSACTION
	RETURN
END
ELSE
BEGIN --註冊失敗
	SELECT @RSuccessful,0
	ROLLBACK TRANSACTION
	RETURN
END
