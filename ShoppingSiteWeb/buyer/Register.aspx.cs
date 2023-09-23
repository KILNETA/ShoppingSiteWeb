using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.buyer
{
    /// <summary>
    /// 註冊帳戶頁面
    /// </summary>
    public partial class Register : System.Web.UI.Page
    {
        /// <summary>
        /// 1~12月 最大天數
        /// </summary>
        private static int[] maxDays = new int[13] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        /// <summary>
        /// 初始化 生日月份選單
        /// </summary>
        private void initDDL_BirthdayMonth()
        {
            DDL_BirthdayMonth.Items.Clear();
            DDL_BirthdayMonth.Items.Insert(0, new ListItem("－", "0"));
        }

        /// <summary>
        /// 初始化 生日日期選單
        /// </summary>
        private void initDDL_BirthdayDay()
        {
            DDL_BirthdayDay.Items.Clear();
            DDL_BirthdayDay.Items.Insert(0, new ListItem("－", "0"));
        }

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
                //判斷是否已登入會員 
                if (Session["UserId"] != null)
                {
                    Response.Write("<script>alert('已登入！進入個人儀錶板！');window.location='DashBoard.aspx';</script>");
                }

                //判斷Token是否過期 
                if (Session["Token"] == null || TB_Token.Text != Session["Token"].ToString())
                    Response.Write("<script>alert('表單已失效，創建新註冊表單！');window.location='Register.aspx';</script>");

                //避免PostBack導致輸入的密碼消失
                TextBox Password_textbox = TB_Password;
                Password_textbox.Attributes.Add("value", Password_textbox.Text);
                TextBox PasswordCheck_textbox = TB_PasswordCheck;
                PasswordCheck_textbox.Attributes.Add("value", PasswordCheck_textbox.Text);
            }
            //首次加載
            else
            {
                //判斷是否已登入會員 
                if (Session["UserId"] != null)
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
                ViewState["isFinish_UserName"] = "false";
                ViewState["isFinish_Password"] = "false";
                ViewState["isFinish_PasswordCheck"] = "false";
                ViewState["isFinish_EMail"] = "false";
                ViewState["isFinish_CheckEMail"] = "false";
                ViewState["isSendCheckCodeEMail"] = "false";

                //印出 1900 ~ 當前年份 的年份
                for (int i = 0; i <= (DateTime.Now.Year - 1900); i++)
                {
                    DDL_BirthdayYear.Items.Insert(i + 1, new ListItem($"{DateTime.Now.Year - i}", $"{DateTime.Now.Year - i}"));
                }
            }
        }

        /// <summary>
        /// 生日選單 年份索引更改(事件)
        /// </summary>
        protected void DDL_BirthdayYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //初始化 月份、日期 選項數值
            initDDL_BirthdayMonth();
            initDDL_BirthdayDay();

            //如果未選擇年分則直接退出 (不執行下面的程式)
            if (DDL_BirthdayYear.SelectedIndex == 0)
                return;

            //印出 1 ~ 12 月份
            for (int i = 1; i <= 12; i++)
            {
                DDL_BirthdayMonth.Items.Insert(i, new ListItem($"{i}", $"{i}"));
            }
        }

        /// <summary>
        /// 生日選單 月份索引更改(事件)
        /// </summary>
        protected void DDL_BirthdayMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            //初始化 日期 選項數值
            initDDL_BirthdayDay();

            //如果未選擇月份則直接退出 (不執行下面的程式)
            if (DDL_BirthdayMonth.SelectedIndex == 0)
                return;

            //判斷閏年
            if (
                DateTime.IsLeapYear(Int32.Parse(DDL_BirthdayYear.Text)) &&
                DDL_BirthdayMonth.SelectedIndex == 2)
                maxDays[2]++;

            //印出 1 ~ maxDays[月份] 該月最大月份的各個選項
            for (int i = 1; i <= maxDays[DDL_BirthdayMonth.SelectedIndex]; i++)
            {
                DDL_BirthdayDay.Items.Insert(i, new ListItem($"{i}", $"{i}"));
            }
        }

        /// <summary>
        /// 輸入框 用戶名內文更改(事件)
        /// </summary>
        protected void TB_UserName_TextChanged(object sender, EventArgs e)
        {
            //初始化參數
            ViewState["isFinish_UserName"] = "false";
            LB_ErrorMessage_UserName.ForeColor = System.Drawing.Color.Red;

            //用戶名包含特殊字元
            if (!new Regex("^[a-zA-Z0-9 ]*$").IsMatch(TB_UserName.Text))
            {
                LB_ErrorMessage_UserName.Text = "用戶名包含特殊字元";
                return;
            }
            //用戶名過長 >24
            else if (TB_UserName.Text.Length > 24)
            {
                LB_ErrorMessage_UserName.Text = "用戶名過長";
                return;
            }
            //用戶名過短 <4
            else if (TB_UserName.Text.Length < 4)
            {
                LB_ErrorMessage_UserName.Text = "用戶名過短";
                return;
            }

            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            GridView gv = new GridView();

            //調用DB 確認用戶名是否被使用
            DB.connectionReader(
                "userNameHasBeenUsed.sql",
                new ArrayList {
                    new DB.Parameter("TB_User", SqlDbType.NVarChar, TB_UserName.Text)
                },
                (SqlDataReader ts) => {
                    gv.DataSource = ts;
                    gv.DataBind();
                }
            );

            //用戶名未被使用 顯示可用消息(綠色)
            if (0 == Int32.Parse(gv.Rows[0].Cells[0].Text))
            {
                LB_ErrorMessage_UserName.ForeColor = System.Drawing.Color.Green;
                LB_ErrorMessage_UserName.Text = "用戶名可用";
                ViewState["isFinish_UserName"] = "true";
                return;
            }
            //用戶名已被使用 顯示錯誤消息(紅色)
            else
            {
                LB_ErrorMessage_UserName.Text = "用戶名已被使用";
                return;
            }
        }

        /// <summary>
        /// 輸入框 信箱內文更改(事件)
        /// </summary>
        protected void TB_EMail_TextChanged(object sender, EventArgs e)
        {
            //初始化確認表
            ViewState["isFinish_EMail"] = "false";
            ViewState["isSendCheckCodeEMail"] = "false";
            ViewState["isFinish_CheckEMail"] = "false";
            IMG_EMailCheck.ImageUrl = "picture/verify_fail.png";
            LB_ErrorMessage_EMail.ForeColor = System.Drawing.Color.Red;

            if (!Regex.IsMatch(TB_EMail.Text, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$"))
            {
                LB_ErrorMessage_EMail.Text = "格式錯誤";
                return;
            }

            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            GridView gv = new GridView();

            //調用DB 確認Email是否被使用
            DB.connectionReader(
                "emailHasBeenUsed.sql",
                new ArrayList {
                    new DB.Parameter("TB_EMail", SqlDbType.NVarChar, TB_EMail.Text)
                },
                (SqlDataReader ts) => {
                    gv.DataSource = ts;
                    gv.DataBind();
                }
            );

            //信箱未被使用 顯示可用消息(綠色)
            if (0 == Int32.Parse(gv.Rows[0].Cells[0].Text))
            {
                LB_ErrorMessage_EMail.ForeColor = System.Drawing.Color.Green;
                LB_ErrorMessage_EMail.Text = "信箱可用";
                ViewState["isFinish_EMail"] = "true";
                return;
            }
            //信箱已被使用 顯示錯誤消息(紅色)
            else
            {
                LB_ErrorMessage_EMail.Text = "此信箱已被使用";
                return;
            }
        }

        /// <summary>
        /// 按鈕 驗證信箱被觸發(事件)
        /// </summary>
        protected void BT_SendCheckCodeEMail_Click(object sender, EventArgs e)
        {
            //初始化確認表
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
                if (TB_EMail.Text == String.Empty)
                    LB_ErrorMessage_EMail.Text = "請輸入電子信箱";
                else
                    LB_ErrorMessage_EMail.Text = "欄位有誤";
            }
        }

        /// <summary>
        /// 輸入框 信箱驗證碼更改(事件)
        /// </summary>
        protected void TB_EMailCheckCode_TextChanged(object sender, EventArgs e)
        {
            //初始化確認表
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
        /// 輸入框 密碼更改(事件)
        /// </summary>
        protected void TB_Password_TextChanged(object sender, EventArgs e)
        {
            //初始化確認表
            ViewState["isFinish_Password"] = "false";
            ViewState["isFinish_PasswordCheck"] = "false";
            IMG_PasswordCheck.ImageUrl = "picture/verify_fail.png";

            //密碼過長
            if (TB_Password.Text.Length > 18)
            {
                LB_ErrorMessage_Password.Text = "密碼過長";
            }
            //密碼過短
            else if (TB_Password.Text.Length < 6)
            {
                LB_ErrorMessage_Password.Text = "密碼過短";
            }
            else if (!new Regex("^[a-zA-Z0-9 ]*$").IsMatch(TB_Password.Text))
            {
                LB_ErrorMessage_Password.Text = "密碼包含特殊字元";
            }
            else if (new Regex("^[0-9]*$").IsMatch(TB_Password.Text))
            {
                LB_ErrorMessage_Password.Text = "密碼需包含至少一個英文字母";
            }
            //密碼輸入正確
            else
            {
                LB_ErrorMessage_Password.Text = "　";
                ViewState["isFinish_Password"] = "true";
            }
        }

        /// <summary>
        /// 輸入框 確認密碼更改(事件)
        /// </summary>
        protected void TB_PasswordCheck_TextChanged(object sender, EventArgs e)
        {
            //初始化確認表
            ViewState["isFinish_PasswordCheck"] = "false";
            //確認密碼與密碼符合
            if (TB_Password.Text.Equals(TB_PasswordCheck.Text) &&
                ViewState["isFinish_Password"].ToString() == "true")
            {
                LB_ErrorMessage_PasswordCheck.Text = "　";
                IMG_PasswordCheck.ImageUrl = "picture/verify_confirm.png";
                ViewState["isFinish_PasswordCheck"] = "true";
            }
            //確認密碼與密碼不符合
            else
            {
                LB_ErrorMessage_PasswordCheck.Text = "確認密碼不符合";
                IMG_PasswordCheck.ImageUrl = "picture/verify_fail.png";
            }
        }

        /// <summary>
        /// 檢測未填寫或有誤的警告訊息
        /// </summary>
        private bool RegisterWarningMessageCheck()
        {
            //有誤的警告
            bool complete = true;

            //用戶名包含特殊字元
            if (!new Regex("^[a-zA-Z0-9 ]*$").IsMatch(TB_UserName.Text))
            {
                complete = false;
                LB_ErrorMessage_UserName.Text = "用戶名包含特殊字元";
            }
            //用戶名過長 >24
            else if (TB_UserName.Text.Length > 24)
            {
                complete = false;
                LB_ErrorMessage_UserName.Text = "用戶名過長";
            }
            //用戶名過短 <4
            else if (TB_UserName.Text.Length < 4)
            {
                complete = false;
                LB_ErrorMessage_UserName.Text = "用戶名過短";
            }
            else
                LB_ErrorMessage_UserName.Text = "　";

            //密碼過長
            if (TB_Password.Text.Length > 18)
            {
                complete = false;
                LB_ErrorMessage_Password.Text = "密碼過長";
            }
            //密碼過短
            else if (TB_Password.Text.Length < 6)
            {
                complete = false;
                LB_ErrorMessage_Password.Text = "密碼過短";
            }
            else if (!new Regex("^[a-zA-Z0-9 ]*$").IsMatch(TB_Password.Text))
            {
                complete = false;
                LB_ErrorMessage_Password.Text = "密碼包含特殊字元";
            }
            else if (new Regex("^[0-9]*$").IsMatch(TB_Password.Text))
            {
                complete = false;
                LB_ErrorMessage_Password.Text = "密碼需包含至少一個英文字母";
            }
            else
                LB_ErrorMessage_Password.Text = "　";

            //確認密碼為空
            if (!TB_Password.Text.Equals(TB_PasswordCheck.Text) ||
                ViewState["isFinish_PasswordCheck"].ToString() != "true")
            {
                complete = false;
                LB_ErrorMessage_PasswordCheck.Text = "確認密碼不符合";
                IMG_PasswordCheck.ImageUrl = "picture/verify_fail.png";
            }
            else
            {
                LB_ErrorMessage_PasswordCheck.Text = "　";
                IMG_PasswordCheck.ImageUrl = "picture/verify_confirm.png";
                ViewState["isFinish_PasswordCheck"] = "true";
            }

            //電子信箱為空
            if (!Regex.IsMatch(TB_EMail.Text, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$"))
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

            //真實名稱為空
            if (TB_RealName.Text == String.Empty)
            {
                complete = false;
                LB_ErrorMessage_RealName.Text = "此欄不得為空";
            }
            else if (TB_Address.Text.Length > 20)
            {
                complete = false;
                LB_ErrorMessage_RealName.Text = "姓名過長";
            }
            else
                LB_ErrorMessage_RealName.Text = "　";

            //行動電話為空
            if (TB_PhoneNum.Text == String.Empty)
            {
                complete = false;
                LB_ErrorMessage_PhoneNum.Text = "此欄不得為空";
            }
            else if(TB_PhoneNum.Text.Length > 10)
            {
                complete = false;
                LB_ErrorMessage_PhoneNum.Text = "電話過長";
            }
            else
                LB_ErrorMessage_PhoneNum.Text = "　";

            //地址為空
            if (TB_Address.Text == String.Empty)
            {
                complete = false;
                LB_ErrorMessage_Address.Text = "此欄不得為空";
            }
            else if (TB_Address.Text.Length > 50)
            {
                complete = false;
                LB_ErrorMessage_Address.Text = "地址過長";
            }
            else
                LB_ErrorMessage_Address.Text = "　";

            //生日日期未填寫正確
            if (DDL_BirthdayYear.SelectedIndex == 0 ||
                DDL_BirthdayMonth.SelectedIndex == 0 ||
                DDL_BirthdayDay.SelectedIndex == 0)
            {
                complete = false;
                LB_ErrorMessage_BirthdayDate.Text = "日期填寫有誤";
            }
            else
                LB_ErrorMessage_BirthdayDate.Text = "　";

            return complete;
        }

        /// <summary>
        /// 按鈕 啟動帳號註冊程序(事件)
        /// </summary>
        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            //驗證Token
            if (Session["Token"] == null || TB_Token.Text != Session["Token"].ToString())
            {
                Response.Write("<script>alert('表單已失效，創建新註冊表單！');window.location='Register.aspx';</script>");
                return;
            }

            //確認表單是否填寫正確
            if( !RegisterWarningMessageCheck() )
                return;

            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            GridView gv = new GridView();

            //調用DB 確認用戶名是否被使用
            DB.connectionReader(
                "responseUser.sql",
                new ArrayList {
                    new DB.Parameter("TB_userName",     SqlDbType.NVarChar, TB_UserName.Text),
                    new DB.Parameter("TB_userPhoneNum", SqlDbType.NVarChar, TB_PhoneNum.Text),
                    new DB.Parameter("TB_userEMail",    SqlDbType.NVarChar, TB_EMail.Text),
                    new DB.Parameter("TB_userRealName", SqlDbType.NVarChar, TB_RealName.Text),
                    new DB.Parameter("TB_userBirthday", SqlDbType.NVarChar, $"{DDL_BirthdayYear.Text}/{DDL_BirthdayMonth.Text}/{DDL_BirthdayDay.Text}"),
                    new DB.Parameter("TB_userAddress",  SqlDbType.NVarChar, TB_Address.Text),
                    new DB.Parameter("TB_userPassword", SqlDbType.NVarChar, TB_PasswordCheck.Text),
                },
                (SqlDataReader ts) => {
                    gv.DataSource = ts;
                    gv.DataBind();
                }
            );

            //清除此頁面所有暫存資料
            ViewState.Clear();
            //清除Token
            Session.Remove("Token");

            //判斷用戶帳號註冊成功與否
            if (1 == gv.Rows.Count
                && gv.Rows[0].Cells[1].Text != "0")
            {
                Session["UserId"] = gv.Rows[0].Cells[1].Text;
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "goBackJS",
                    "alert('會員註冊成功！');" +
                    "window.location.replace('DashBoard.aspx');",
                    true);
            }
            //會員註冊失敗，用戶名或信箱已被使用
            else if (gv.Rows[0].Cells[1].Text == "0")
            {
                Session["UserId"] = null;
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "goBackJS",
                    "alert('會員註冊失敗，用戶名或信箱已被使用！');" +
                    "window.location.replace(window.location.href);",
                    true);
            }
            //表單已失效，創建新註冊表單
            else
            {
                Session["UserId"] = null;
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "goBackJS",
                    "alert('表單已失效，創建新註冊表單！');" +
                    "window.location.replace(window.location.href);",
                    true);
            }
        }
    }
}