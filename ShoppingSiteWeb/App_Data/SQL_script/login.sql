/*
登入帳戶

參數:
    @SwitchField    用戶是使用 Email 或 UserName 登入
	@TB_User        用戶名稱
	@TB_Password    密碼
輸出:
    用戶ID
*/
DECLARE @SQLCommand nvarchar(200)
SET @SQLCommand = 
	'Select [userId] 
	FROM [userTable] 
	WHERE('+ QUOTENAME(@SwitchField) + '=' + QUOTENAME(@TB_User,'''') + ' COLLATE SQL_Latin1_General_CP1_CS_AS ) 
	AND ( [userPassword] = ' + QUOTENAME(@TB_Password,'''') + ' COLLATE SQL_Latin1_General_CP1_CS_AS )'
EXECUTE (@SQLCommand)