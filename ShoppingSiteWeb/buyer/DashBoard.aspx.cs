using System;
using System.Collections.Generic;
using System.Data;
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
                    LoadUserData();
                    LoadShopData();
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

        private void LoadUserData()
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

        private void LoadShopData()
        {
            SqlDataSource SqlDataSource_LoginUser = new SqlDataSource();
            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_LoginUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_LoginUser.SelectParameters.Add("UserID", Session["UserId"].ToString());

            // SQL指令 ==
            SqlDataSource_LoginUser.SelectCommand =
                $"Select shopTable.* " +
                $"FROM [shopTable] " +
                $"Inner join [user_shopTable] " +
                $"On shopTable.shopId = user_shopTable.shopId " +
                $"Inner join [userTable] " +
                $"On user_shopTable.userId = userTable.userId " +
                $"WHERE( userTable.userId = @UserID ) ";

            // 執行SQL指令 .select() ==
            SqlDataSource_LoginUser.DataSourceMode = SqlDataSourceMode.DataSet;
            DataView dv = (DataView)SqlDataSource_LoginUser.Select(new DataSourceSelectArguments());

            GV_ShopData.DataSource = dv;
            GV_ShopData.DataBind();

            SqlDataSource_LoginUser.Dispose();
        }
    }
}