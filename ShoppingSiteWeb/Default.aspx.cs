using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ShoppingSiteWeb
{
    public partial class Default : System.Web.UI.Page
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
            else {
                showCommodityPage();
            }
        }

        private void showWelcomeUserInMenu()
        {
            Label LB_Welcome = new Label();
            LinkButton LB_UserDashBoard = new LinkButton();

            LB_Welcome.CssClass = "menuWelcome";
            LB_UserDashBoard.CssClass = "generalLink";

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

            LB_UserDashBoard.PostBackUrl = "~/buyer/DashBoard.aspx";

            Panel_TitelMenuLogin.Controls.Add(LB_Welcome);
            Panel_TitelMenuLogin.Controls.Add(LB_UserDashBoard);
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


            private void showCommodityPage()
        {
            List<Panel> commodityPage = new List<Panel>();

            for (int i = 0 ; i < 10 ; i++)
            {
                commodityPage.Add(showCommodityList());
                commodityPage[i].CssClass = "CommodityList"; 
                Panel_CommodityPage.Controls.Add(commodityPage[i]);
            }
        }

        private Panel showCommodityList()
        {
            Panel commodityList = new Panel();

            for (int i = 0; i < 6; i++)
            {
                commodityList.Controls.Add(showCommodityItem());
            }

            return commodityList;
        }

        private Panel showCommodityItem()
        {
            Panel commodityItem = new Panel();
            commodityItem.CssClass = "CommodityItem";

            LinkButton commodityThumbnail_Box = new LinkButton();
            Image commodityThumbnail = new Image();
            Label commodityName = new Label();
            Label commodityPrice = new Label();
            LinkButton commodityShoppingCart = new LinkButton();
            Panel commodityShoppingCart_Icon = new Panel();

            commodityThumbnail.CssClass = "CommodityIcon";
            commodityThumbnail.ImageUrl = "https://lh3.googleusercontent.com/u/3/drive-viewer/AFDK6gMhmaGzT_G4RLZC5mD8yXhhn63LTTxMcchothMGZTst97tuswoUDSTWdk-cluiHqhFWlAgxPC20KnRkMR33dPtHG_Ku=w1920-h937";
            commodityThumbnail_Box.Controls.Add(commodityThumbnail);

            commodityName.Text = "好康的";
            commodityName.Font.Size = 12;
            commodityPrice.Text = "1,200";
            commodityPrice.Font.Size = 12;

            commodityShoppingCart.CssClass = "ShoppingCart";
            commodityShoppingCart_Icon.CssClass = "ShoppingCart_Icon";
            commodityShoppingCart.Controls.Add(commodityShoppingCart_Icon);

            commodityItem.Controls.Add(commodityThumbnail_Box);
            commodityItem.Controls.Add(commodityName);
            commodityItem.Controls.Add(commodityPrice);
            commodityItem.Controls.Add(commodityShoppingCart);

            return commodityItem;
        }
    }
}