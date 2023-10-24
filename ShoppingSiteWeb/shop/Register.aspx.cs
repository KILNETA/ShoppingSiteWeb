using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.shop
{
    /// <summary>
    /// 註冊商店頁面
    /// </summary>
    public partial class Register : System.Web.UI.Page
    {

        /// <summary>
        /// 頁面加載
        /// </summary>
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

        /// <summary>
        /// 確認是否已存在商店
        /// </summary>
        /// <returns></returns>
        private bool CheakHasShop()
        {
            /// <summary>
            /// 暫存資料表
            /// </summary>
            DetailsView dv = new DetailsView();

            //調用DB 確認是否已存在商店
            DB.connectionReader(
                "chackHasShop.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.NVarChar,  Session["UserId"].ToString())
                },
                (SqlDataReader ts) => {
                    dv.DataSource = ts;
                    dv.DataBind();
                }
            );

            //是否已存在商店
            if (dv.Rows[0].Cells[1].Text == "1")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 輸入框 商店名稱 更改
        /// </summary>
        protected void TB_ShopName_TextChanged(object sender, EventArgs e)
        {
            //重置表單狀態
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

            /// <summary>
            /// 暫存資料表
            /// </summary>
            DetailsView dv = new DetailsView();

            //調用DB 確認是否已存在商店
            DB.connectionReader(
                "chackShopName.sql",
                new ArrayList {
                    new DB.Parameter("TB_ShopName", SqlDbType.NVarChar,  TB_ShopName.Text)
                },
                (SqlDataReader ts) => {
                    dv.DataSource = ts;
                    dv.DataBind();
                }
            );

            //商店名是否可用
            if (dv.Rows[0].Cells[1].Text == "0")
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

        /// <summary>
        /// 輸入框 Email 更改
        /// </summary>
        protected void TB_ShopEMail_TextChanged(object sender, EventArgs e)
        {
            //重置表單狀態
            ViewState["isFinish_EMail"] = "false";
            ViewState["isSendCheckCodeEMail"] = "false";
            ViewState["isFinish_CheckEMail"] = "false";
            IMG_EMailCheck.ImageUrl = "picture/verify_fail.png";
            LB_ErrorMessage_EMail.ForeColor = System.Drawing.Color.Red;

            //Email 格式檢查
            if (!Regex.IsMatch(TB_ShopEMail.Text, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$"))
            {
                LB_ErrorMessage_EMail.Text = "格式錯誤";
                return;
            }

            /// <summary>
            /// 暫存資料表
            /// </summary>
            DetailsView dv = new DetailsView();

            //調用DB 確認信箱是否被使用
            DB.connectionReader(
                "chackShopEmail.sql",
                new ArrayList {
                    new DB.Parameter("TB_ShopEMail", SqlDbType.NVarChar,  TB_ShopEMail.Text)
                },
                (SqlDataReader ts) => {
                    dv.DataSource = ts;
                    dv.DataBind();
                }
            );

            //信箱未被使用 顯示可用消息(綠色)
            if (dv.Rows[0].Cells[1].Text == "0")
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

        /// <summary>
        /// 輸入框 驗證碼 更改
        /// </summary>
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

        /// <summary>
        /// 按鈕 驗證 按下
        /// </summary>
        protected void BT_SendCheckCodeEMail_Click(object sender, EventArgs e)
        {
            //重置表單狀態
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

        /// <summary>
        /// 檢測未填寫或有誤的警告訊息
        /// </summary>
        /// <returns>填寫狀態</returns>
        private bool RegisterWarningMessageCheck()
        {
            // 填寫狀態
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

        /// <summary>
        /// 按鈕 註冊 按下
        /// </summary>
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

            /// <summary>
            /// 暫存資料表
            /// </summary>
            DetailsView dv = new DetailsView();

            //調用DB 註冊商店
            DB.connectionReader(
                "responseShop.sql",
                new ArrayList {
                    new DB.Parameter("TB_ShopName",     SqlDbType.NVarChar,  TB_ShopName.Text           ),
                    new DB.Parameter("TB_ShopEMail",    SqlDbType.NVarChar,  TB_ShopEMail.Text          ),
                    new DB.Parameter("TB_ShopPhoneNum", SqlDbType.NVarChar,  TB_ShopPhoneNum.Text       ),
                    new DB.Parameter("TB_ShopAddress",  SqlDbType.NVarChar,  TB_ShopAddress.Text        ),
                    new DB.Parameter("UserId",          SqlDbType.NVarChar,  Session["UserId"].ToString())
                },
                (SqlDataReader ts) => {
                    dv.DataSource = ts;
                    dv.DataBind();
                }
            );

            //清除此頁面所有暫存資料
            ViewState.Clear();
            //清除Token
            Session.Remove("Token");

            //判斷用戶帳號註冊成功與否
            if (dv.Rows[0].Cells[1].Text == "1")
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "goBackJS",
                    "alert('商店註冊成功！');" +
                    "window.location='DashBoard.aspx';",
                    true);
            }
            //會員註冊失敗，用戶名或信箱已被使用
            else if (dv.Rows[0].Cells[1].Text == "0")
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "goBackJS",
                    "alert('商店註冊失敗，商店名或信箱已被使用！');" +
                    "window.location.replace(window.location.href);",
                    true);
            }
            //表單已失效，創建新註冊表單
            else
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "goBackJS",
                    "alert('表單已失效，創建新商店註冊表單！');" +
                    "window.location.replace(window.location.href);",
                    true);
            }
        }
    }
}