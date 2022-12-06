using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.shop
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //使網頁不被塊取記憶體存取 (但重整頁面仍能讀取 仍需搭配Token判斷表單是否被認證)
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();

            //網頁PostBack
            if (IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='../buyer/Login.aspx';</script>");
                }
                //判斷是否已有商店
                else if (CheakHasShop())
                {
                    Response.Write("<script>alert('已有商店！進入商店儀錶板！');window.location='DashBoard.aspx';</script>");
                }

                //判斷Token是否過期 
                if (Session["Token"] == null || TB_Token.Text != Session["Token"].ToString())
                    Response.Write("<script>alert('表單已失效，創建新商店註冊表單！');window.location='Register.aspx';</script>");
            }
            //首次加載
            else
            {
                if (Session["UserId"] == null)
                {
                    Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='../buyer/Login.aspx';</script>");
                }
                //判斷是否已有商店
                else if (CheakHasShop())
                {
                    Response.Redirect("DashBoard.aspx");
                }

                //創建Token許可證
                String Token = Path.GetRandomFileName().Replace(".", "");
                //用於判斷表單是否被認證(存儲用戶與server互動的數據) 
                Session["Token"] = Token;

                //在表單存入Token許可證
                TB_Token.Text = Token;

                //初始化各項 (只存儲於該頁面的數據) 用於判斷表單是否填寫正確
                ViewState["isFinish_ShopName"] = "false";
                ViewState["isFinish_EMail"] = "false";
                ViewState["isFinish_CheckEMail"] = "false";
                ViewState["isSendCheckCodeEMail"] = "false";
            }
        }

        private bool CheakHasShop()
        {
            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_CheckUserName = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_CheckUserName.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            //新增SQL參數
            SqlDataSource_CheckUserName.SelectParameters.Add("UserId", Session["UserId"].ToString());

            //SQL指令 ==
            SqlDataSource_CheckUserName.SelectCommand =
                $"Select shopId " +
                $"FROM user_shopTable " +
                $"WHERE ( userId = @UserId )";

            //執行SQL指令 .select() ==
            SqlDataSource_CheckUserName.DataSourceMode = SqlDataSourceMode.DataSet;
            //取得查找資料
            DataView dv = (DataView)SqlDataSource_CheckUserName.Select(new DataSourceSelectArguments());
            DetailsView dvTable = new DetailsView();

            dvTable.DataSource = dv;
            dvTable.DataBind();
            //SqlDataSource元件釋放資源
            SqlDataSource_CheckUserName.Dispose();

            if (1 == dvTable.DataItemCount)
                return true;
            else
                return false;
        }

        protected void TB_ShopName_TextChanged(object sender, EventArgs e)
        {
            ViewState["isFinish_ShopName"] = "false";
            LB_ErrorMessage_ShopName.ForeColor = System.Drawing.Color.Red;
            //用戶名過長 >24
            if (TB_ShopName.Text.Length > 24)
            {
                LB_ErrorMessage_ShopName.Text = "商店名過長";
                return;
            }
            //用戶名過短 <4
            else if (TB_ShopName.Text.Length < 4)
            {
                LB_ErrorMessage_ShopName.Text = "商店名過短";
                return;
            }

            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_CheckUserName = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_CheckUserName.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            //新增SQL參數
            SqlDataSource_CheckUserName.SelectParameters.Add("TB_ShopName", TB_ShopName.Text);

            //SQL指令 ==
            SqlDataSource_CheckUserName.SelectCommand =
                $"Select shopName " +
                $"FROM shopTable " +
                $"WHERE ( shopName = @TB_ShopName COLLATE SQL_Latin1_General_CP1_CS_AS )";

            //執行SQL指令 .select() ==
            SqlDataSource_CheckUserName.DataSourceMode = SqlDataSourceMode.DataSet;
            //取得查找資料
            DataView dv = (DataView)SqlDataSource_CheckUserName.Select(new DataSourceSelectArguments());
            DetailsView dvTable = new DetailsView();
            //資料匯入表格
            dvTable.DataSource = dv;
            //更新表格
            dvTable.DataBind();
            //SqlDataSource元件釋放資源
            SqlDataSource_CheckUserName.Dispose();

            if (0 == dvTable.DataItemCount)
            {
                LB_ErrorMessage_ShopName.ForeColor = System.Drawing.Color.Green;
                LB_ErrorMessage_ShopName.Text = "商店名可用";
                ViewState["isFinish_UserName"] = "true";
                return;
            }
            else
            {
                LB_ErrorMessage_ShopName.Text = "商店名已被使用";
                return;
            }
        }

        protected void TB_ShopEMail_TextChanged(object sender, EventArgs e)
        {
            ViewState["isFinish_EMail"] = "false";
            ViewState["isSendCheckCodeEMail"] = "false";
            ViewState["isFinish_CheckEMail"] = "false";
            IMG_EMailCheck.ImageUrl = "picture/verify_fail.png";

            if (!Regex.IsMatch(TB_ShopEMail.Text, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$"))
            {
                LB_ErrorMessage_EMail.Text = "格式錯誤";
                return;
            }

            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_CheckEmail = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_CheckEmail.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            //新增SQL參數
            SqlDataSource_CheckEmail.SelectParameters.Add("TB_ShopEMail", TB_ShopEMail.Text);

            //SQL指令 ==
            SqlDataSource_CheckEmail.SelectCommand =
                $"Select shopEMail " +
                $"FROM shopTable " +
                $"WHERE( shopEMail = @TB_ShopEMail )";

            //執行SQL指令 .select() ==
            SqlDataSource_CheckEmail.DataSourceMode = SqlDataSourceMode.DataSet;
            //取得查找資料
            DataView dv = (DataView)SqlDataSource_CheckEmail.Select(new DataSourceSelectArguments());

            DetailsView dvTable = new DetailsView();
            //資料匯入表格
            dvTable.DataSource = dv;
            //更新表格
            dvTable.DataBind();
            //SqlDataSource元件釋放資源
            SqlDataSource_CheckEmail.Dispose();

            //信箱未被使用 顯示可用消息(綠色)
            if (0 == dvTable.DataItemCount)
            {
                LB_ErrorMessage_EMail.ForeColor = System.Drawing.Color.Green;
                LB_ErrorMessage_EMail.Text = "信箱可用";
                ViewState["isFinish_EMail"] = "true";
                return;
            }
            //信箱已被使用 顯示錯誤消息(紅色)
            else
            {
                LB_ErrorMessage_EMail.ForeColor = System.Drawing.Color.Red;
                LB_ErrorMessage_EMail.Text = "此信箱已被使用";
                return;
            }
        }

        protected void TB_EMailCheckCode_TextChanged(object sender, EventArgs e)
        {
            ViewState["isFinish_CheckEMail"] = "false";
            //驗證 信箱驗證碼 成功
            if (TB_EMailCheckCode.Text == "1234" &&
                ViewState["isSendCheckCodeEMail"].ToString() == "true")
            {
                ViewState["isFinish_CheckEMail"] = "true";
                IMG_EMailCheck.ImageUrl = "picture/verify_confirm.png";
            }
            //驗證 信箱驗證碼 失敗
            else
            {
                IMG_EMailCheck.ImageUrl = "picture/verify_fail.png";
            }
        }

        protected void BT_SendCheckCodeEMail_Click(object sender, EventArgs e)
        {
            ViewState["isFinish_CheckEMail"] = "false";
            IMG_EMailCheck.ImageUrl = "picture/verify_fail.png";

            //信箱填寫正確 發送驗證信件
            if (ViewState["isFinish_EMail"].ToString() == "true")
            {
                LB_SendEMailMessage.Text = "已發送驗證信件";
                ViewState["isSendCheckCodeEMail"] = "true";
            }
            //信箱填寫有誤 顯示錯誤訊息
            else
            {
                LB_SendEMailMessage.Text = "　";
                if (TB_ShopEMail.Text == String.Empty)
                    LB_ErrorMessage_EMail.Text = "請輸入電子信箱";
                else
                    LB_ErrorMessage_EMail.Text = "欄位有誤";
            }
        }

        /**
         * 檢測未填寫或有誤的警告訊息
         */
        private bool RegisterWarningMessageCheck()
        {

            bool complete = true;

            //用戶名過長 >24
            if (TB_ShopName.Text.Length > 24)
            {
                complete = false;
                LB_ErrorMessage_ShopName.Text = "用戶名過長";
            }
            //用戶名過短 <4
            else if (TB_ShopName.Text.Length < 4)
            {
                complete = false;
                LB_ErrorMessage_ShopName.Text = "用戶名過短";
            }
            else
                LB_ErrorMessage_ShopName.Text = "　";

            //電子信箱為空
            if (!Regex.IsMatch(TB_ShopEMail.Text, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$"))
            {
                complete = false;
                LB_ErrorMessage_EMail.ForeColor = System.Drawing.Color.Red;
                LB_ErrorMessage_EMail.Text = "格式錯誤";
            }
            //電子信箱未驗證
            else if (ViewState["isFinish_CheckEMail"].ToString() != "true" ||
                ViewState["isSendCheckCodeEMail"].ToString() != "true")
            {
                complete = false;
                LB_ErrorMessage_EMail.ForeColor = System.Drawing.Color.Red;
                LB_ErrorMessage_EMail.Text = "電子信箱尚未認證";
            }
            else
                LB_ErrorMessage_EMail.Text = "　";

            //行動電話為空
            if (TB_ShopPhoneNum.Text == String.Empty)
            {
                complete = false;
                LB_ErrorMessage_ShopPhoneNum.Text = "此欄不得為空";
            }
            else if (TB_ShopPhoneNum.Text.Length > 10)
            {
                complete = false;
                LB_ErrorMessage_ShopPhoneNum.Text = "電話過長";
            }
            else
                LB_ErrorMessage_ShopPhoneNum.Text = "　";

            //地址為空
            if (TB_ShopAddress.Text == String.Empty)
            {
                complete = false;
                LB_ErrorMessage_Address.Text = "此欄不得為空";
            }
            else if (TB_ShopAddress.Text.Length > 50)
            {
                complete = false;
                LB_ErrorMessage_Address.Text = "地址過長";
            }
            else
                LB_ErrorMessage_Address.Text = "　";

            return complete;
        }

        protected void ShopRegisterButton_Click(object sender, EventArgs e)
        {
            //驗證Token
            if (Session["Token"] == null || TB_Token.Text != Session["Token"].ToString())
            {
                Response.Write("<script>alert('表單已失效，創建新註冊表單！');window.location='Register.aspx';</script>");
                return;
            }

            //確認表單是否填寫正確
            if (!RegisterWarningMessageCheck())
                return;

            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            //新增SQL參數
            SqlDataSource_RegisterUser.SelectParameters.Add("TB_ShopName", TB_ShopName.Text);
            SqlDataSource_RegisterUser.SelectParameters.Add("TB_ShopEMail", TB_ShopEMail.Text);
            SqlDataSource_RegisterUser.SelectParameters.Add("TB_ShopPhoneNum", TB_ShopPhoneNum.Text);
            SqlDataSource_RegisterUser.SelectParameters.Add("TB_ShopAddress", TB_ShopAddress.Text);

            SqlDataSource_RegisterUser.SelectParameters.Add("UserId", Session["UserId"].ToString());

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"INSERT INTO shopTable([shopName],[shopEMail],[shopPhoneNum],[shopAddress]) " +
                $"SELECT " +
                    $"@TB_ShopName, " +
                    $"@TB_ShopEMail, " +
                    $"@TB_ShopPhoneNum, " +
                    $"@TB_ShopAddress " +
                $"WHERE Not Exists(" +
                    $"SELECT shopTable.shopName, shopTable.shopEMail " +
                    $"FROM shopTable " +
                    $"WHERE shopName = @TB_ShopName COLLATE SQL_Latin1_General_CP1_CS_AS " +
                    $"OR shopEMail = @TB_ShopEMail " +
                    $") " +
                $"if (@@ROWCOUNT != 0) " +
                    $"BEGIN " +
                        $"DECLARE @shopId INT " +
                        $"SELECT @shopId = ISNULL(successful.shopId, 0)  from(SELECT SCOPE_IDENTITY() AS shopId) successful " +

                        $"INSERT INTO user_shopTable([userId], [shopId]) " +
                        $"SELECT @UserId, @shopId " +
                        $"WHERE Not Exists( " +
                            $"SELECT user_shopTable.userId " +
                            $"FROM user_shopTable " +
                            $"WHERE userId = @UserId " +
                        $") " +
                    $"END " +
                $"SELECT @@ROWCOUNT ";

            //執行SQL指令 .select() ==
            SqlDataSource_RegisterUser.DataSourceMode = SqlDataSourceMode.DataSet;
            //取得查找資料
            DataView dv = (DataView)SqlDataSource_RegisterUser.Select(new DataSourceSelectArguments());
            DetailsView dvTable = new DetailsView();
            //資料匯入表格
            dvTable.DataSource = dv;
            //更新表格
            dvTable.DataBind();
            //SqlDataSource元件釋放資源
            SqlDataSource_RegisterUser.Dispose();

            //清除此頁面所有暫存資料
            ViewState.Clear();
            //清除Token
            Session.Remove("Token");

            //判斷用戶帳號註冊成功與否
            if (1 == dvTable.DataItemCount
                && dvTable.Rows[0].Cells[1].Text != "0")
            {
                Response.Write("<script>alert('商店註冊成功！');window.location='DashBoard.aspx';</script>");
            }
            //會員註冊失敗，用戶名或信箱已被使用
            else if (dvTable.Rows[0].Cells[1].Text == "0")
            {
                Session["UserId"] = null;
                Response.Write("<script>alert('商店註冊失敗，商店名或信箱已被使用！');window.location='Register.aspx';</script>");
            }
            //表單已失效，創建新註冊表單
            else
            {
                Session["UserId"] = null;
                Response.Write("<script>alert('表單已失效，創建新商店註冊表單！');window.location='Register.aspx';</script>");
            }
        }
    }
}