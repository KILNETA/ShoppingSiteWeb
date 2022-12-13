using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.shop
{
    public partial class Shop : System.Web.UI.Page
    {
        /* C->commodity S->shop */
        private static readonly String[] dataNames =  {
            "CId",
            "CName",
            "CPrice",
            "CNum",
            "CThumbnail"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!new Regex("^[0-9]*$").IsMatch(Context.Request.QueryString["shopId"].ToString()))
            {
                Response.Redirect("../Default.aspx");
                return;
            }

            if (Session["UserId"] != null)
                showWelcomeUserInMenu();
            else
                showLoginInMenu();

            if (IsPostBack)
            {

            }
            else
            {
                selectRecommendCommoditys();
            }
            showShopData();
            showCommodityPage();
        }

        private void showShopData()
        {
            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_RegisterUser.SelectParameters.Add("ShopId", Context.Request.QueryString["shopId"].ToString());

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"SELECT " +
                    $"shopName, " +
                    $"shopEMail, " +
                    $"shopPhoneNum, " +
                    $"shopAddress " +
                $"FROM shopTable " +
                $"WHERE shopId = @ShopId";

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

            if (gv.Rows.Count != 0) {
                LB_shopName.Text = gv.Rows[0].Cells[0].Text;
                LB_shopEMail.Text += gv.Rows[0].Cells[1].Text;
                LB_shopPhone.Text += gv.Rows[0].Cells[2].Text;
                LB_shopAddress.Text += gv.Rows[0].Cells[3].Text;
            }
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }

        private void selectRecommendCommoditys()
        {
            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_RegisterUser.SelectParameters.Add("ShopId", Context.Request.QueryString["shopId"].ToString());

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"SELECT " +
                    $"CT.commodityId, " +
                    $"CT.commodityName, " +
                    $"CT.commodityPrice, " +
                    $"CT.commodityNum, " +
                    $"CT.commodityThumbnail " +
                $"FROM commodityTable CT " +
                $"INNER JOIN shop_commodityTable SCT " +
                $"ON SCT.commodityId = CT.commodityId " +
                $"WHERE SCT.shopId = @ShopId";

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

            ViewState["commodityNum"] = gv.Rows.Count;

            saveRecommendCommoditys(gv);
        }

        private void saveRecommendCommoditys(GridView gv)
        {
            for (int cell = 0; cell < dataNames.Length; cell++)
            {
                for (int row = 0; row < Int32.Parse(ViewState["commodityNum"].ToString()); row++)
                {
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
            Response.Write("<script>alert('成功登出！');window.location='../Default.aspx';</script>");
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

            int RowNum = Int32.Parse(ViewState["commodityNum"].ToString()) / 6;

            if (Int32.Parse(ViewState["commodityNum"].ToString()) % 6 != 0)
                RowNum++;

            for (int i = 0; i < RowNum; i++)
            {
                commodityPage.Add(showCommodityList(i));
                commodityPage[i].CssClass = "CommodityList";
                Panel_CommodityPage.Controls.Add(commodityPage[i]);
            }
        }

        private Panel showCommodityList(int index)
        {
            Panel commodityList = new Panel();

            for (int i = 0; i < 6; i++)
            {
                commodityList.Controls.Add(showCommodityItem(index * 6 + i));
            }

            return commodityList;
        }

        private Panel showCommodityItem(int index)
        {
            Panel commodityItem = new Panel();
            commodityItem.CssClass = "CommodityItem";

            if(index >= Int32.Parse(ViewState["commodityNum"].ToString()))
            {
                commodityItem.CssClass = "CommodityItem_none";
                return commodityItem;
            }

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
            commodityShoppingCart.Click +=
                delegate (object sender1, EventArgs e1) {
                    VBT_ShoppingCart(
                        new object(),
                        new EventArgs(),
                        ViewState[$"CId_{index}"].ToString()
                    );
                };

            commodityThumbnail_Box.PostBackUrl = $"~/commodity/Item.aspx?commodityId={ViewState[$"CId_{index}"]}";
            commodityName_Box.PostBackUrl = $"~/commodity/Item.aspx?commodityId={ViewState[$"CId_{index}"]}";

            commodityContent.Controls.Add(commodityName_Box);
            commodityContent.Controls.Add(commodityPriceContent);

            commodityItem.Controls.Add(commodityThumbnail_Box);
            commodityItem.Controls.Add(commodityShoppingCart);
            commodityItem.Controls.Add(commodityContent);

            return commodityItem;
        }

        protected void VBT_ShoppingCart(object sender, EventArgs e, String commodityId)
        {

            if (Session["UserId"] == null)
            {
                Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='../buyer/Login.aspx';</script>");
                return;
            }

            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_RegisterUser.SelectParameters.Add("UserId", Session["UserId"].ToString());
            SqlDataSource_RegisterUser.SelectParameters.Add("CommodityId", commodityId);
            SqlDataSource_RegisterUser.SelectParameters.Add("CommodityNum", "1");

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"DECLARE @hasInCart INT " +

                $"IF EXISTS( " +
                    $"SELECT 1 " +
                    $"From shoppingCartTable " +
                    $"Where  userId = @UserId " +
                    $"AND commodityId = @CommodityId " +
                $") " +
                    $"SET @hasInCart = 1 " +
                $"ELSE " +
                    $"SET @hasInCart = 0 " +

                $"IF(@hasInCart = 0) " +
                    $"BEGIN " +
                        $"DECLARE @successful INT " +

                        $"INSERT INTO shoppingCartTable([joinDate],[userId],[commodityId],[commodityNum]) " +
                        $"Select " +
                            $"GETDATE(), " +
                            $"@UserId, " +
                            $"@CommodityId, " +
                            $"@CommodityNum " +
                        $"Where Not Exists( " +
                            $"Select userId,commodityId " +
                            $"From shoppingCartTable " +
                            $"Where userId = @UserId " +
                                $"AND commodityId = @CommodityId " +
                        $") " +
                            $"Select @@ROWCOUNT " +
                    $"END " +
                $"ELSE " +
                    $"Select '-1' ";

            //執行SQL指令 .select() ==
            SqlDataSource_RegisterUser.DataSourceMode = SqlDataSourceMode.DataSet;
            //取得查找資料
            DataView dv = (DataView)SqlDataSource_RegisterUser.Select(new DataSourceSelectArguments());
            DetailsView gv = new DetailsView();
            //資料匯入表格
            gv.DataSource = dv;
            //更新表格
            gv.DataBind();
            //SqlDataSource元件釋放資源
            SqlDataSource_RegisterUser.Dispose();

            switch (gv.Rows[0].Cells[1].Text)
            {
                case "1":
                    Response.Write("<script>alert('已加入購物車！')</script>");
                    break;
                case "-1":
                    Response.Write("<script>alert('已存在購物車中！')</script>");
                    break;
                default:
                    Response.Write("<script>alert('加入購物車失敗！')</script>");
                    break;
            }
        }

        protected void LB_runSearch_Click(object sender, EventArgs e)
        {
            if (DDL_SearchMode.SelectedIndex == 0)
            {
                Response.Redirect($"~/search/Search.aspx?commoditySearch={TB_Search.Text}");
            }
        }
    }
}