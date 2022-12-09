using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ShoppingSiteWeb
{
    public partial class Default : System.Web.UI.Page
    {
        /* C->commodity S->shop */
        private static readonly String[] dataNames =  {
            "CId",
            "CName",
            "CPrice",
            "CNum",
            "CThumbnail",
            "SId",
            "SName"
        };

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
                selectRecommendCommoditys();
            }
            showCommodityPage();
        }

        private void selectRecommendCommoditys()
        {
            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"SELECT TOP(60)" +
                    $"CT.commodityId, " +
                    $"CT.commodityName, " +
                    $"CT.commodityPrice, " +
                    $"CT.commodityNum, " +
                    $"CT.commodityThumbnail, " +
                    $"ST.shopId, " +
                    $"ST.shopName " +
                $"FROM commodityTable CT " +
                $"INNER JOIN shop_commodityTable SCT " +
                $"ON SCT.commodityId = CT.commodityId " +
                $"INNER JOIN shopTable ST " +
                $"ON ST.shopId = SCT.shopId " +
                $"ORDER BY NEWID()";

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

            saveRecommendCommoditys(gv);
        }

        private void saveRecommendCommoditys(GridView gv)
        {
            for (int cell = 0; cell < dataNames.Length ; cell++) {
                for (int row = 0; row < 36; row++) {
                    // ViewState[dataNames_Index]
                    ViewState[$"{dataNames[cell]}_{row}"] = gv.Rows[row].Cells[cell].Text;
                }
            }
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


            private void showCommodityPage()
        {
            List<Panel> commodityPage = new List<Panel>();

            for (int i = 0 ; i < 10 ; i++)
            {
                commodityPage.Add(showCommodityList( i ));
                commodityPage[i].CssClass = "CommodityList"; 
                Panel_CommodityPage.Controls.Add(commodityPage[i]);
            }
        }

        private Panel showCommodityList(int index)
        {
            Panel commodityList = new Panel();

            for (int i = 0; i < 6; i++)
            {
                commodityList.Controls.Add(showCommodityItem(index*6 + i));
            }

            return commodityList;
        }

        private Panel showCommodityItem(int index)
        {
            //測試用的
            if (index>=36)
                return new Panel();
            //測試用的

             Panel commodityItem = new Panel();
            commodityItem.CssClass = "CommodityItem";

            String PriceNum = ViewState[$"CPrice_{index}"].ToString();
            if (PriceNum.Length > 3)
                PriceNum = PriceNum.Insert(PriceNum.Length - 3, ",");

            LinkButton commodityThumbnail_Box = new LinkButton();
            Image commodityThumbnail = new Image();
            Panel commodityContent = new Panel();
            LinkButton commodityName_Box = new LinkButton();
            Label commodityName = new Label();
            Panel commodityPriceContent = new Panel();
            Label commodityPriceSymbol = new Label();
            Label commodityPrice = new Label();
            LinkButton commodityShoppingCart = new LinkButton();
            Panel commodityShoppingCart_Icon = new Panel();

            commodityThumbnail.CssClass = "CommodityIcon";
            commodityThumbnail.ImageUrl = ViewState[$"CThumbnail_{index}"].ToString();
            commodityThumbnail_Box.Controls.Add(commodityThumbnail);

            commodityContent.CssClass = "CommodityContent";

            commodityName_Box.CssClass = "CommodityNameText";
            commodityName.Text = ViewState[$"CName_{index}"].ToString();
            commodityName_Box.Controls.Add(commodityName);

            commodityPriceContent.CssClass = "CommodityPriceBox";
            commodityPriceSymbol.CssClass = "CommodityPriceSymbol";
            commodityPriceSymbol.Text = "$";
            commodityPrice.CssClass = "CommodityPriceText";
            commodityPrice.Text = PriceNum;
            commodityPriceContent.Controls.Add(commodityPriceSymbol);
            commodityPriceContent.Controls.Add(commodityPrice);

            commodityShoppingCart.CssClass = "ShoppingCart";
            commodityShoppingCart_Icon.CssClass = "ShoppingCart_Icon";
            commodityShoppingCart.Controls.Add(commodityShoppingCart_Icon);

            commodityThumbnail_Box.PostBackUrl = $"~/commodity/Item.aspx?commodityId={ViewState[$"CId_{index}"]}";
            commodityName_Box.PostBackUrl = $"~/commodity/Item.aspx?commodityId={ViewState[$"CId_{index}"]}";

            commodityContent.Controls.Add(commodityName_Box);
            commodityContent.Controls.Add(commodityPriceContent);

            commodityItem.Controls.Add(commodityThumbnail_Box);
            commodityItem.Controls.Add(commodityShoppingCart);
            commodityItem.Controls.Add(commodityContent);

            return commodityItem;
        }
    }
}