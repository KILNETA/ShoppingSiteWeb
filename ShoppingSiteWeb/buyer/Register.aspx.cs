using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.buyer
{
    public partial class Register : System.Web.UI.Page
    {

        private void initDDL_BirthdayMonth()
        {
            DDL_BirthdayMonth.Items.Clear();
            DDL_BirthdayMonth.Items.Insert(0, new ListItem("－", "0"));
        }

        private void initDDL_BirthdayDay()
        {
            DDL_BirthdayDay.Items.Clear();
            DDL_BirthdayDay.Items.Insert(0, new ListItem("－", "0"));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetNoStore();
            Response.Cache.SetNoServerCaching();

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();

            if (IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    Response.Write("<script>alert('已登入！進入個人儀錶板！');window.location='DashBoard.aspx';</script>");
                }

                if (Session["Token"] == null || TB_Token.Text != Session["Token"].ToString())
                    Response.Write("<script>alert('表單已失效，創建新註冊表單！');window.location='Register.aspx';</script>");

                TextBox Password_textbox = TB_Password;
                Password_textbox.Attributes.Add("value", Password_textbox.Text);
                TextBox PasswordCheck_textbox = TB_PasswordCheck;
                PasswordCheck_textbox.Attributes.Add("value", PasswordCheck_textbox.Text);
            }
            else
            {
                if (Session["UserId"] != null)
                {
                    Response.Redirect("DashBoard.aspx");
                }

                String Token = Path.GetRandomFileName().Replace(".", "");
                Session["Token"] = Token;

                TB_Token.Text = Token;

                ViewState["isFinish_UserName"] = "false";
                ViewState["isFinish_Password"] = "false";
                ViewState["isFinish_PasswordCheck"] = "false";
                ViewState["isFinish_EMail"] = "false";
                ViewState["isFinish_CheckEMail"] = "false";
                ViewState["isSendCheckCodeEMail"] = "false";

                for (int i = 0; i <= (DateTime.Now.Year - 1900); i++)
                {
                    DDL_BirthdayYear.Items.Insert(i + 1, new ListItem($"{DateTime.Now.Year - i}", $"{DateTime.Now.Year - i}"));
                }
            }
        }

        protected void DDL_BirthdayYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            initDDL_BirthdayMonth();
            initDDL_BirthdayDay();

            if (DDL_BirthdayYear.SelectedIndex == 0)
                return;

            for (int i = 1; i <= 12; i++)
            {
                DDL_BirthdayMonth.Items.Insert(i, new ListItem($"{i}", $"{i}"));
            }
        }

        protected void DDL_BirthdayMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[] maxDays = new int[13] { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            initDDL_BirthdayDay();

            if (DDL_BirthdayMonth.SelectedIndex == 0)
                return;

            if (
                DateTime.IsLeapYear(DDL_BirthdayYear.SelectedIndex) &&
                DDL_BirthdayMonth.SelectedIndex == 2)
                maxDays[2]++;
            for (int i = 1; i <= maxDays[DDL_BirthdayMonth.SelectedIndex]; i++)
            {
                DDL_BirthdayDay.Items.Insert(i, new ListItem($"{i}", $"{i}"));
            }
        }
        protected void TB_UserName_TextChanged(object sender, EventArgs e)
        {
            ViewState["isFinish_UserName"] = "false";

            if (!new Regex("^[a-zA-Z0-9 ]*$").IsMatch(TB_UserName.Text))
            {
                LB_ErrorMessage_UserName.Text = "用戶名包含特殊字元";
                return;
            }
            else if (TB_UserName.Text.Length > 24)
            {
                LB_ErrorMessage_UserName.Text = "用戶名過長";
                return;
            }
            else if (TB_UserName.Text.Length < 4)
            {
                LB_ErrorMessage_UserName.Text = "用戶名過短";
                return;
            }

            String DB_addressStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";
            SqlConnection dataConnection = new SqlConnection(DB_addressStr);

            String cmdStr = $"Select userName FROM userTable WHERE ( userName =\'{TB_UserName.Text}\' COLLATE SQL_Latin1_General_CP1_CS_AS )";
            SqlCommand cmd = new SqlCommand(cmdStr, dataConnection);
            dataConnection.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            useCheckUserNameTable.DataSource = dr;
            useCheckUserNameTable.DataBind();

            dataConnection.Close();

            if (0 == useCheckUserNameTable.DataItemCount)
            {
                LB_ErrorMessage_UserName.Text = "　";
                ViewState["isFinish_UserName"] = "true";
                return;
            }
            else
            {
                LB_ErrorMessage_UserName.Text = "用戶名已被使用";
                return;
            }
        }

        protected void TB_EMail_TextChanged(object sender, EventArgs e)
        {
            ViewState["isFinish_EMail"] = "false";
            ViewState["isSendCheckCodeEMail"] = "false";
            ViewState["isFinish_CheckEMail"] = "false";
            IMG_EMailCheck.ImageUrl = "picture/verify_fail.png";

            if (!Regex.IsMatch(TB_EMail.Text, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$"))
            {
                LB_ErrorMessage_EMail.Text = "格式錯誤";
                return;
            }

            String DB_addressStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";
            SqlConnection dataConnection = new SqlConnection(DB_addressStr);

            String cmdStr = $"Select userEMail FROM userTable WHERE( userEMail =\'{TB_EMail.Text}\' )";
            SqlCommand cmd = new SqlCommand(cmdStr, dataConnection);
            dataConnection.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            useCheckEmailTable.DataSource = dr;
            useCheckEmailTable.DataBind();

            dataConnection.Close();

            if (0 == useCheckEmailTable.DataItemCount)
            {
                LB_ErrorMessage_EMail.Text = "　";
                ViewState["isFinish_EMail"] = "true";
                return;
            }
            else
            {
                LB_ErrorMessage_EMail.Text = "此信箱已被使用";
                return;
            }
        }

        protected void BT_SendCheckCodeEMail_Click(object sender, EventArgs e)
        {
            ViewState["isFinish_CheckEMail"] = "false";
            IMG_EMailCheck.ImageUrl = "picture/verify_fail.png";

            if (ViewState["isFinish_EMail"].ToString() == "true")
            {
                LB_SendEMailMessage.Text = "已發送驗證信件";
                ViewState["isSendCheckCodeEMail"] = "true";
            }
            else
            {
                LB_SendEMailMessage.Text = "　";
                if (TB_EMail.Text == String.Empty)
                    LB_ErrorMessage_EMail.Text = "請輸入電子信箱";
                else
                    LB_ErrorMessage_EMail.Text = "格式錯誤";
            }
        }

        protected void TB_EMailCheckCode_TextChanged(object sender, EventArgs e)
        {
            ViewState["isFinish_CheckEMail"] = "false";
            if (TB_EMailCheckCode.Text == "1234" && 
                ViewState["isSendCheckCodeEMail"].ToString() == "true")
            {
                ViewState["isFinish_CheckEMail"] = "true";
                IMG_EMailCheck.ImageUrl = "picture/verify_confirm.png";
            }
            else
            {
                IMG_EMailCheck.ImageUrl = "picture/verify_fail.png";
            }
        }

        protected void TB_Password_TextChanged(object sender, EventArgs e)
        {
            ViewState["isFinish_Password"] = "false";
            ViewState["isFinish_PasswordCheck"] = "false";
            IMG_PasswordCheck.ImageUrl = "picture/verify_fail.png";

            if (TB_Password.Text.Length > 18)
            {
                LB_ErrorMessage_Password.Text = "密碼過長";
            }
            else if (TB_Password.Text.Length < 6)
            {
                LB_ErrorMessage_Password.Text = "密碼過短";
            }
            else
            {
                LB_ErrorMessage_Password.Text = "　";
                ViewState["isFinish_Password"] = "true";
            }
        }

        protected void TB_PasswordCheck_TextChanged(object sender, EventArgs e)
        {
            ViewState["isFinish_PasswordCheck"] = "false";
            if (TB_Password.Text.Equals(TB_PasswordCheck.Text) && 
                ViewState["isFinish_Password"].ToString() == "true")
            {
                LB_ErrorMessage_PasswordCheck.Text = "　";
                IMG_PasswordCheck.ImageUrl = "picture/verify_confirm.png";
                ViewState["isFinish_PasswordCheck"] = "true";
            }
            else
            {
                LB_ErrorMessage_PasswordCheck.Text = "確認密碼不符合";
                IMG_PasswordCheck.ImageUrl = "picture/verify_fail.png";
            }
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            if (Session["Token"] == null || TB_Token.Text != Session["Token"].ToString())
            {
                Response.Write("<script>alert('表單已失效，創建新註冊表單！');window.location='Register.aspx';</script>");
                return;
            }

            if (
                ViewState["isFinish_UserName"].ToString() != "true" ||
                ViewState["isFinish_Password"].ToString() != "true" ||
                ViewState["isFinish_PasswordCheck"].ToString() != "true" ||
                ViewState["isFinish_EMail"].ToString() != "true" ||
                ViewState["isFinish_CheckEMail"].ToString() != "true" ||
                ViewState["isSendCheckCodeEMail"].ToString() != "true" ||
                TB_RealName.Text == String.Empty ||
                TB_PhoneNum.Text == String.Empty ||
                TB_Address.Text == String.Empty ||
                DDL_BirthdayYear.SelectedIndex == 0 ||
                DDL_BirthdayMonth.SelectedIndex == 0 ||
                DDL_BirthdayDay.SelectedIndex == 0
                )
            {
                return;
            }

            String DB_addressStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";
            SqlConnection dataConnection = new SqlConnection(DB_addressStr);

            String cmdStr_CheckUserName = $"Select userName FROM userTable WHERE ( userName ='{TB_UserName.Text}' COLLATE SQL_Latin1_General_CP1_CS_AS )";
            SqlCommand cmd_CheckUserName = new SqlCommand(cmdStr_CheckUserName, dataConnection);
            String cmdStr_CheckEmail = $"Select userEMail FROM userTable WHERE( userEMail =\'{TB_EMail.Text}\' )";
            SqlCommand cmd_CheckEmail = new SqlCommand(cmdStr_CheckEmail, dataConnection);

            dataConnection.Open();
            SqlDataReader dr_CheckUserName = cmd_CheckUserName.ExecuteReader();
            useCheckUserNameTable.DataSource = dr_CheckUserName;
            useCheckUserNameTable.DataBind();
            dataConnection.Close();

            dataConnection.Open();
            SqlDataReader dr_CheckEmail = cmd_CheckEmail.ExecuteReader();
            useCheckEmailTable.DataSource = dr_CheckEmail;
            useCheckEmailTable.DataBind();
            dataConnection.Close();

            if (0 == useCheckEmailTable.DataItemCount &&
                0 == useCheckUserNameTable.DataItemCount) 
            {
                String cmdStr_RegisterUser =
                    $"INSERT INTO userTable" +
                    $"([userName],[userPhoneNum],[userEMail],[userRealName],[userBirthday],[userAddress],[userPassword])" +
                    $"VALUES ('{TB_UserName.Text}','{TB_PhoneNum.Text}','{TB_EMail.Text}',N'{TB_RealName.Text}'," +
                    $"'{DDL_BirthdayYear.Text}/{DDL_BirthdayMonth.SelectedIndex}/{DDL_BirthdayDay.SelectedIndex}'," +
                    $"N'{TB_Address.Text}','{TB_PasswordCheck.Text}') ";
                SqlCommand cmd_RegisterUser = new SqlCommand(cmdStr_RegisterUser, dataConnection);

                dataConnection.Open();
                cmd_RegisterUser.ExecuteNonQuery();
                dataConnection.Close();

                String cmdStr_CheckRegister = 
                    $"Select userId " +
                    $"FROM userTable " +
                    $"WHERE ( userName ='{TB_UserName.Text}' COLLATE SQL_Latin1_General_CP1_CS_AS )" +
                    $"AND ( userPassword=\'{TB_PasswordCheck.Text}\' COLLATE SQL_Latin1_General_CP1_CS_AS ) ";
                SqlCommand cmd_CheckRegister = new SqlCommand(cmdStr_CheckRegister, dataConnection);

                dataConnection.Open();
                SqlDataReader dr_CheckRegister = cmd_CheckRegister.ExecuteReader();
                useCheckRegisterTable.DataSource = dr_CheckRegister;
                useCheckRegisterTable.DataBind();
                dataConnection.Close();

                ViewState.Clear();
                Session.Remove("Token");

                if (1 == useCheckRegisterTable.DataItemCount)
                {
                    Session["UserId"] = useCheckRegisterTable.Rows[0].Cells[1].Text;
                    Response.Write("<script>alert('會員註冊成功！');window.location='DashBoard.aspx';</script>");
                }
                else
                {
                    Session["UserId"] = null;
                    Response.Write("<script>alert('會員註冊失敗，創建新註冊表單！');window.location='Register.aspx';</script>");
                }
            }
        }
    }
}