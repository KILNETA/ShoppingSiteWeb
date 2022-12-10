using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.commodity
{
    public partial class Item : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
                showWelcomeUserInMenu();
            else
                showLoginInMenu();

            if (IsPostBack)
            {

            }
            else
            {
                LoadCommodityData();
                LoadShopData();
            }

        }

        private void LoadCommodityData()
        {
            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_RegisterUser.SelectParameters.Add("CommodityId", Context.Request.QueryString["commodityId"].ToString() );

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"SELECT " +
                    $"commodityId, " +
                    $"commodityName, " +
                    $"commodityPrice, " +
                    $"commodityNum, " +
                    $"commodityThumbnail, " +
                    $"commodityIntroduction " +
                $"FROM commodityTable " +
                $"WHERE commodityId = @CommodityId ";

            //執行SQL指令 .select() ==
            SqlDataSource_RegisterUser.DataSourceMode = SqlDataSourceMode.DataSet;
            //取得查找資料
            DataView dv = (DataView)SqlDataSource_RegisterUser.Select(new DataSourceSelectArguments());
            GridView gv = new GridView();
            //資料匯入表格
            gv.DataSource = dv;
            //更新表格
            gv.DataBind();
            //SqlDataSource元件釋放資源
            SqlDataSource_RegisterUser.Dispose();

            String PriceNum = gv.Rows[0].Cells[2].Text;
            if (PriceNum.Length > 3)
                PriceNum = PriceNum.Insert(PriceNum.Length - 3, ",");

            commodityThumbnail.ImageUrl = gv.Rows[0].Cells[4].Text;

            commodityId.Text = gv.Rows[0].Cells[0].Text;
            commodityName.Text = gv.Rows[0].Cells[1].Text;
            commodityPrice.Text = $"${PriceNum}";
            commodityNum.Text = $"庫存 {gv.Rows[0].Cells[3].Text} 件";
            commodityIntroduction.Text = gv.Rows[0].Cells[5].Text;

            ViewState[$"commodityId"] = gv.Rows[0].Cells[0].Text;
            ViewState[$"commodityNum"] = gv.Rows[0].Cells[3].Text;

            if (Int32.Parse(gv.Rows[0].Cells[3].Text) < 1)
            {
                TB_commodityNum.Enabled = false;
                LB_JoinShoppingCart.CssClass = "commodityBuyButton_Cant";
                LB_ToShopping.CssClass = "commodityBuyButton_Cant";
                LB_JoinShoppingCart.Enabled = false;
                LB_ToShopping.Enabled = false;
                commodityNumLack.Text = "缺貨中";
            }
        }

        private void LoadShopData()
        {
            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_RegisterUser.SelectParameters.Add("CommodityId", Context.Request.QueryString["commodityId"].ToString());

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"SELECT " +
                    $"shopTable.shopId, " +
                    $"shopName, " +
                    $"shopEMail, " +
                    $"shopPhoneNum " +
                $"FROM shopTable " +
                $"INNER JOIN shop_commodityTable " +
                    $"ON shop_commodityTable.shopId = shopTable.shopId " +
                $"WHERE shop_commodityTable.commodityId = @CommodityId ";

            //執行SQL指令 .select() ==
            SqlDataSource_RegisterUser.DataSourceMode = SqlDataSourceMode.DataSet;
            //取得查找資料
            DataView dv = (DataView)SqlDataSource_RegisterUser.Select(new DataSourceSelectArguments());
            GridView gv = new GridView();
            //資料匯入表格
            gv.DataSource = dv;
            //更新表格
            gv.DataBind();
            //SqlDataSource元件釋放資源
            SqlDataSource_RegisterUser.Dispose();

            ViewState[$"shopId"] = gv.Rows[0].Cells[0].Text;
            LB_shopName.Text = gv.Rows[0].Cells[1].Text;
            LB_shopEMail.Text += gv.Rows[0].Cells[2].Text;
            LB_shopPhone.Text += gv.Rows[0].Cells[3].Text;
        }

        private void showWelcomeUserInMenu()
        {
            Label LB_Welcome = new Label();
            LinkButton LB_UserDashBoard = new LinkButton();
            Label LB_Menuseparate = new Label();
            LinkButton LB_UserSignOut = new LinkButton();

            LB_Welcome.CssClass = "menuWelcome";
            LB_UserDashBoard.CssClass = "generalLink";
            LB_Menuseparate.CssClass = "TitelMenuseparate";
            LB_UserSignOut.CssClass = "generalLink";

            SqlDataSource SqlDataSource_LoginUser = new SqlDataSource();
            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_LoginUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_LoginUser.SelectParameters.Add("UserID", Session["UserId"].ToString());

            // SQL指令 ==
            SqlDataSource_LoginUser.SelectCommand =
                $"Select [userName] " +
                $"FROM [userTable] " +
                $"WHERE( userId = @UserID ) ";

            // 執行SQL指令 .select() ==
            SqlDataSource_LoginUser.DataSourceMode = SqlDataSourceMode.DataSet;
            DataView dv = (DataView)SqlDataSource_LoginUser.Select(new DataSourceSelectArguments());

            DetailsView userTable = new DetailsView();
            userTable.DataSource = dv;
            userTable.DataBind();

            SqlDataSource_LoginUser.Dispose();

            LB_Welcome.Text = "歡迎您！ ";
            LB_UserDashBoard.Text = userTable.Rows[0].Cells[1].Text;
            LB_Menuseparate.Text = "|";
            LB_UserSignOut.Text = "登出";

            LB_UserDashBoard.PostBackUrl = "~/buyer/DashBoard.aspx";

            Panel_TitelMenuLogin.Controls.Add(LB_Welcome);
            Panel_TitelMenuLogin.Controls.Add(LB_UserDashBoard);
            Panel_TitelMenuLogin.Controls.Add(LB_Menuseparate);
            Panel_TitelMenuLogin.Controls.Add(LB_UserSignOut);

            LB_UserSignOut.Click += new EventHandler(this.SignOutButton_Click);

        }

        protected void SignOutButton_Click(object sender, EventArgs e)
        {
            Session["UserId"] = null;
            Response.Write("<script>alert('成功登出！');window.location='Default.aspx';</script>");
        }

        private void showLoginInMenu()
        {
            LinkButton LB_Register = new LinkButton();
            LinkButton LB_Login = new LinkButton();
            Label LB_Menuseparate = new Label();


            LB_Register.CssClass = "generalLink";
            LB_Login.CssClass = "generalLink";
            LB_Menuseparate.CssClass = "TitelMenuseparate";

            LB_Register.Text = "會員註冊";
            LB_Login.Text = "會員登入";
            LB_Menuseparate.Text = "|";

            LB_Register.PostBackUrl = "~/buyer/Register.aspx";
            LB_Login.PostBackUrl = "~/buyer/Login.aspx";

            Panel_TitelMenuLogin.Controls.Add(LB_Register);
            Panel_TitelMenuLogin.Controls.Add(LB_Menuseparate);
            Panel_TitelMenuLogin.Controls.Add(LB_Login);
        }

        protected void TB_commodityNum_TextChanged(object sender, EventArgs e)
        {
            if (TB_commodityNum.Text == String.Empty ||
                !new Regex("^[0-9]*$").IsMatch(TB_commodityNum.Text) ||
                Int32.Parse(TB_commodityNum.Text) < 1)
            {
                TB_commodityNum.Text = "1";
                return;
            }
            else if (Int32.Parse(TB_commodityNum.Text) > Int32.Parse(ViewState[$"commodityNum"].ToString()))
            {
                TB_commodityNum.Text = ViewState[$"commodityNum"].ToString();
            }
            else
            {

            }
        }
    }
}