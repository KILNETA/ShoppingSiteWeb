using System.Data.SqlClient;
using System.Data;
using System.IO;
using System;
using System.Collections;
using Newtonsoft.Json;

/// <summary>
/// 網頁配置資料
/// </summary>
class WebConfig
{
    /// <summary>
    ///SQL script path
    /// </summary>
    public static string pathSQL = 
        @"D:\ShoppingSiteWeb\ShoppingSiteWeb\App_Data\SQL_script\";

    /// <summary>
    ///MS LocalDB path
    /// </summary>
    public static string pathDB = 
        @"Data Source=(LocalDB)\MSSQLLocalDB;" +
        @"AttachDbFilename=|DataDirectory|\Database_Main.mdf;" +
        @"Integrated Security=True";
}

/// <summary>
/// 資料庫公用函式庫 靜態類
/// </summary>
class DB
{
    /// <summary>
    /// 查詢資料庫
    /// </summary>
    /// <param name="SQL_script">SQL腳本檔案名稱</param>
    /// <param name="parameters">參數值 (DB.Parameter)</param>
    /// <param name="returnFunc">回傳函式</param>
    public static void connectionReader(
        string SQL_script,
        ArrayList parameters,
        Action<SqlDataReader> returnFunc
    ){
        /// <summary>
        /// SQL Server 連線
        /// </summary>
        SqlConnection connection = new SqlConnection(WebConfig.pathDB);
        /// <summary>
        /// SQL Server 指令腳本
        /// </summary>
        SqlCommand readerCmd = new SqlCommand(
            File.ReadAllText(WebConfig.pathSQL + SQL_script),
            connection
            );

        //導入傳遞的參數值給 readerCmd
        foreach (Parameter element in parameters)
        {
            readerCmd.Parameters.Add(
                element.Name,
                element.Type
            ).Value = 
                element.Value;
        }
        
        connection.Open();  // 資料庫 開啟連線  -----

        //調用回傳函式將讀取的資料回傳
        returnFunc(readerCmd.ExecuteReader());

        connection.Close(); // 資料庫 關閉連線  -----
    }

    /// <summary>
    /// ArrayList參數陣列打包Json格式，供SQL Script參數輸入。
    /// </summary>
    /// <param name="parms">參數列表</param>
    /// <returns>Json格式參數</returns>
    public static string parmsToJson(ArrayList parms)
    {
        return JsonConvert.SerializeObject(parms);
    }

    /// <summary>
    /// SQL參數型別 資料結構
    /// </summary>
    public struct Parameter
    {
        /// <summary>
        /// 參數名稱
        /// </summary>
        public string Name;
        /// <summary>
        /// 參數型別
        /// </summary>
        public SqlDbType Type;
        /// <summary>
        /// 參數值
        /// </summary>
        public dynamic Value;

        /// <summary>
        /// SQL參數型別 建構子
        /// </summary>
        /// <param name="Name">參數名稱</param>
        /// <param name="Type">參數型別</param>
        /// <param name="Value">參數值</param>
        public Parameter(
            string Name,
            SqlDbType Type,
            dynamic Value
        )
        {
            this.Name = Name;
            this.Type = Type;
            this.Value = Value;
        }
    }
}