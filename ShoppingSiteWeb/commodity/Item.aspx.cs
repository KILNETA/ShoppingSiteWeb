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
            if(!new Regex("^[0-9]*$").IsMatch(Context.Request.QueryString["commodityId"].ToString())){
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
                if (Session["UserId"] != null)
                    ViewState["UserId"] = Session["UserId"].ToString();
                else
                    ViewState["UserId"] = "null";

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

            if (gv.Rows.Count == 0)
            {
                Response.Redirect("../Default.aspx");
                return;
            }

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

            ViewState["shopId"] = gv.Rows[0].Cells[0].Text;
            LB_shopName.Text = gv.Rows[0].Cells[1].Text;
            LB_shopEMail.Text += gv.Rows[0].Cells[2].Text;
            LB_shopPhone.Text += gv.Rows[0].Cells[3].Text;

            LB_shopName.PostBackUrl = $"~/shop/Shop.aspx?shopId={ViewState["shopId"]}";
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

        protected void LB_runSearch_Click(object sender, EventArgs e)
        {
            if (DDL_SearchMode.SelectedIndex == 0)
            {
                Response.Redirect($"~/search/Search.aspx?commoditySearch={TB_Search.Text}");
            }

        }

        protected void LB_JoinShoppingCart_Click(object sender, EventArgs e)
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


            if (    TB_commodityNum.Text == String.Empty 
                ||  !new Regex("^[0-9]*$").IsMatch(TB_commodityNum.Text)
                ||  Int32.Parse(TB_commodityNum.Text) < 1
                ||  Int32.Parse(TB_commodityNum.Text) > Int32.Parse(ViewState[$"commodityNum"].ToString()) )
            {
                Response.Write("<script>alert('輸入數量有誤！')</script>");
                return;
            }

            SqlDataSource_RegisterUser.SelectParameters.Add("UserId", Session["UserId"].ToString());
            SqlDataSource_RegisterUser.SelectParameters.Add("CommodityId", Context.Request.QueryString["commodityId"].ToString());
            SqlDataSource_RegisterUser.SelectParameters.Add("CommodityNum", TB_commodityNum.Text);

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

        protected void LB_ToShopping_Click(object sender, EventArgs e)
        {

            if (Session["UserId"] == null)
            {
                Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='../buyer/Login.aspx';</script>");
                return;
            }
            else if (ViewState["UserId"].ToString() != Session["UserId"].ToString())
            {
                Response.Write($"<script>alert('頁面內容與帳號不符，重新入頁面！');window.location='../commodity/Item.aspx?commodityId={Context.Request.QueryString["commodityId"].ToString()}';</script>");
                return;
            }

            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";


            if (TB_commodityNum.Text == String.Empty
                || !new Regex("^[0-9]*$").IsMatch(TB_commodityNum.Text)
                || Int32.Parse(TB_commodityNum.Text) < 1
                || Int32.Parse(TB_commodityNum.Text) > Int32.Parse(ViewState[$"commodityNum"].ToString()))
            {
                Response.Write("<script>alert('輸入數量有誤！')</script>");
                return;
            }

            SqlDataSource_RegisterUser.SelectParameters.Add("userId", Session["UserId"].ToString());
            SqlDataSource_RegisterUser.SelectParameters.Add("shoppingCart_commodityId", Context.Request.QueryString["commodityId"].ToString());
            SqlDataSource_RegisterUser.SelectParameters.Add("shoppingCartNum", TB_commodityNum.Text);

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"DECLARE @transactionId INT = 0 " +
                $"DECLARE @transactionError BIT = 0 " +

                $"BEGIN TRANSACTION " +

                $"DECLARE @commodityNum INT " +
                $"DECLARE @commodityName NVARCHAR(50) " +
                $"SELECT @commodityNum = commodityNum , @commodityName = commodityName " +
                $"FROM commodityTable " +
                $"WHERE commodityId = @shoppingCart_commodityId " +
                $"IF(@shoppingCartNum > @commodityNum) " +
                $"BEGIN " +
                    $"SELECT @commodityName +N'數量缺少', -1 " +
                    $"SET @transactionError = 1 " +
                $"END " +

                $"IF(@transactionError = 0) " +
                $"BEGIN " +

                    $"INSERT INTO transactionTable([userId],[transactionDate]) " +
                    $"VALUES (@userId,GETDATE()) " +
                    $"SELECT @transactionId = ISNULL(successful.transactionId,0) from (SELECT SCOPE_IDENTITY() AS transactionId) successful " +

                    $"IF(@transactionId !=0) " +
                    $"BEGIN " +

                        $"INSERT INTO transaction_recordsTable([transactionId],[commodityId],[commodityNum]) " +
                        $"VALUES (@transactionId,@shoppingCart_commodityId,@shoppingCartNum) " +
                        $"IF(@@ROWCOUNT = 0) " +
                            $"SET @transactionError = 1 " +
                        $"UPDATE commodityTable " +
                        $"SET commodityNum = @commodityNum - @shoppingCartNum " +
                        $"WHERE commodityId = @shoppingCart_commodityId " +
                        $"IF(@@ROWCOUNT = 0) " +
                            $"SET @transactionError = 1 " +
                    $"END " +
                $"ELSE " +
                    $"SET @transactionError = 1 " +
                $"END " +

                $"IF(@transactionError = 0) " +
                $"BEGIN " +
                    $"SELECT N'訂單成立！', 1 " +
                    $"COMMIT TRANSACTION " +
                $"END " +
                $"ELSE " +
                $"BEGIN " +
                    $"SELECT N'訂單失敗！', 0 " +
                    $"ROLLBACK TRANSACTION " +
                $"END ";

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

            switch (gv.Rows[1].Cells[1].Text)
            {
                case "1":
                    Response.Write($"<script>alert('{gv.Rows[0].Cells[1].Text}');window.location='../buyer/TransactionList.aspx';</script>");
                    break;
                case "-1":
                    Response.Write($"<script>alert('{gv.Rows[0].Cells[1].Text}');window.location='../commodity/Item.aspx?commodityId={Context.Request.QueryString["commodityId"].ToString()}';</script>");
                    break;
                default:
                    Response.Write($"<script>alert('{gv.Rows[0].Cells[1].Text}');window.location='../commodity/Item.aspx?commodityId={Context.Request.QueryString["commodityId"].ToString()}';</script>\"");
                    break;
            }
        }
    }
}