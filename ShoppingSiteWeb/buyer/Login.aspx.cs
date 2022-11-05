using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.buyer
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                String DB_addressStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";
                SqlConnection dataConnection = new SqlConnection(DB_addressStr);

                String cmdStr = $"Select userName,userPassword FROM userTable WHERE(userName=\'{TB_User.Text}\' AND userPassword=\'{TB_Password.Text}\')";
                SqlCommand cmd = new SqlCommand(cmdStr, dataConnection);
                dataConnection.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                useCheckLoginTable.DataSource = dr;
                useCheckLoginTable.DataBind();

                dataConnection.Close();

                if(1 == useCheckLoginTable.DataItemCount)
                {
                    ErrorLB_1.Text = "　";
                    Response.Redirect("DashBoard.aspx");
                }
                else
                {
                    ErrorLB_1.Text = "登入失敗，帳號或密碼有誤！！";
                }
            }
        }
    }
}