using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.buyer
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    Response.Write("<script>alert('已登入！進入個人儀錶板！');window.location='DashBoard.aspx';</script>");
                }
            }
            else
            {
                if (Session["UserId"] != null)
                {
                    Response.Redirect("DashBoard.aspx");
                }
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(TB_User.Text) || String.IsNullOrEmpty(TB_Password.Text))
            {
                if (String.IsNullOrEmpty(TB_User.Text))
                    ErrorLB_1.Text = "請輸入此欄";
                else
                    ErrorLB_1.Text = "　";

                if (String.IsNullOrEmpty(TB_Password.Text))
                    ErrorLB_2.Text = "請輸入此欄";
                else
                    ErrorLB_2.Text = "　";
            }
            else
            {
                SqlDataSource SqlDataSource_LoginUser = new SqlDataSource();

                //連結資料庫的連接字串 ConnectionString
                SqlDataSource_LoginUser.ConnectionString =
                    "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";
                //判斷用戶是使用 Email 或 UserName 登入
                String SwitchField = (TB_User.Text.ToLower().Contains("@".ToLower())) ? "userEMail" : "userName";
                
                SqlDataSource_LoginUser.SelectParameters.Add("TB_User", TB_User.Text);
                SqlDataSource_LoginUser.SelectParameters.Add("TB_Password", TB_Password.Text);

                // SQL指令 ==
                SqlDataSource_LoginUser.SelectCommand =
                    $"Select [userId] " +
                    $"FROM [userTable] " +
                    $"WHERE( [{SwitchField}] = @TB_User COLLATE SQL_Latin1_General_CP1_CS_AS ) " +
                    $"AND ( [userPassword] = @TB_Password COLLATE SQL_Latin1_General_CP1_CS_AS ) ";

                // 執行SQL指令 .select() ==
                SqlDataSource_LoginUser.DataSourceMode = SqlDataSourceMode.DataSet;
                DataView dv = (DataView)SqlDataSource_LoginUser.Select(new DataSourceSelectArguments());

                useCheckLoginTable.DataSource = dv;
                useCheckLoginTable.DataBind();

                SqlDataSource_LoginUser.Dispose();
                
                if(1 == useCheckLoginTable.DataItemCount)
                {
                    ErrorLB_1.Text = "　";
                    Session["UserId"] = useCheckLoginTable.Rows[0].Cells[1].Text;
                    Response.Redirect("DashBoard.aspx");
                }
                else
                {
                    ErrorLB_1.Text = "登入失敗，帳號或密碼有誤！！";
                }
            }
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}