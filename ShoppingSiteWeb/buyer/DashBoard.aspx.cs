using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.buyer
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Write("<script>alert('已登出！返回登入頁面！');window.location='Login.aspx';</script>");
                }
            }
            else {
                if (Session["UserId"] != null)
                {
                    String DB_addressStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";
                    SqlConnection dataConnection = new SqlConnection(DB_addressStr);

                    String cmdStr = $"Select * FROM userTable WHERE( userId = {Session["UserId"]} )";
                    SqlCommand cmd = new SqlCommand(cmdStr, dataConnection);

                    dataConnection.Open();
                    SqlDataReader dr_CheckUserName = cmd.ExecuteReader();
                    GV_UserData.DataSource = dr_CheckUserName;
                    GV_UserData.DataBind();
                    dataConnection.Close();

                    userRealName.Text = GV_UserData.Rows[0].Cells[4].Text;
                }
                else
                {
                    Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='Login.aspx';</script>");
                }
            }
        }

        protected void SignOutButton_Click(object sender, EventArgs e)
        {
            Session["UserId"] = null;
            Response.Write("<script>alert('成功登出！返回登入頁面！');window.location='Login.aspx';</script>");
        }
    }
}