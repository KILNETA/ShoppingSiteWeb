using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.shop
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Write("<script>alert('已登出！返回登入頁面！');window.location='~/buyer/Login.aspx';</script>");
                }
            }
            else
            {
                if (Session["UserId"] != null)
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

                    GV_UserData.DataSource = dv;
                    GV_UserData.DataBind();

                    SqlDataSource_LoginUser.Dispose();

                    if (GV_UserData.Rows.Count == 0)
                        Response.Write("<script>alert('尚未註冊商店！進入商店註冊頁面！');window.location='Register.aspx';</script>");
                    else
                        BT_GoShop.PostBackUrl = $"Shop.aspx?shopId={GV_UserData.Rows[0].Cells[0].Text}";
                }
                else
                {
                    Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='../buyer/Login.aspx';</script>");
                }
            }
        }

        protected void SignOutButton_Click(object sender, EventArgs e)
        {
            Session["UserId"] = null;
            Response.Write("<script>alert('成功登出！返回登入頁面！');window.location='../buyer/Login.aspx';</script>");
        }
    }
}