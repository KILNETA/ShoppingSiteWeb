using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.commodity
{
    /// <summary>
    /// 商品頁面
    /// </summary>
    public partial class Item : System.Web.UI.Page
    {

        /// <summary>
        /// 頁面加載
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //判斷連結的 commodityId 是否為有效數字
            if (
                Context.Request.QueryString["commodityId"] == null ||
                !new Regex("^[0-9]*$").IsMatch(Context.Request.QueryString["commodityId"].ToString())
            ){
                Response.Redirect("../Default.aspx");
                return;
            }

            //判斷用戶是否登入
            if (Session["UserId"] != null)
                showWelcomeUserInMenu();
            else
                showLoginInMenu();

            if (IsPostBack)
            {   //next load

            }
            else
            {   //first load
                if (Session["UserId"] != null)
                    ViewState["UserId"] = Session["UserId"].ToString();
                else
                    ViewState["UserId"] = "null";

                LoadCommodityData();
                LoadShopData();
            }
        }

        /// <summary>
        /// 取得商品資料
        /// </summary>
        private void LoadCommodityData()
        {
            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            GridView gv = new GridView();

            //調用DB 取得商品資料
            DB.connectionReader(
                "selectCommodity.sql",
                new ArrayList {
                    new DB.Parameter("CommodityId", SqlDbType.Int, Context.Request.QueryString["commodityId"].ToString())
                },
                (SqlDataReader ts) => {
                    gv.DataSource = ts;
                    gv.DataBind();
                }
            );

            //若商品不存在
            if (gv.Rows.Count == 0)
            {
                Response.Redirect("../Default.aspx");
                return;
            }

            commodityThumbnail.ImageUrl = gv.Rows[0].Cells[4].Text;
            commodityId.Text = gv.Rows[0].Cells[0].Text;
            commodityName.Text = gv.Rows[0].Cells[1].Text;
            commodityPrice.Text = $"${Int32.Parse(gv.Rows[0].Cells[2].Text).ToString("N0")}";
            commodityNum.Text = $"庫存 {gv.Rows[0].Cells[3].Text} 件";
            commodityIntroduction.Text = gv.Rows[0].Cells[5].Text;

            //紀錄商品相關資訊
            ViewState[$"commodityId"] = gv.Rows[0].Cells[0].Text;
            ViewState[$"commodityNum"] = gv.Rows[0].Cells[3].Text;

            //如果商品缺貨
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

        /// <summary>
        /// 取得商品所屬商店資料
        /// </summary>
        private void LoadShopData()
        {
            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            GridView gv = new GridView();

            //調用DB 取得商品所屬商店資料
            DB.connectionReader(
                "selectShopBasisCommodity.sql",
                new ArrayList {
                    new DB.Parameter("CommodityId", SqlDbType.Int, Context.Request.QueryString["commodityId"].ToString())
                },
                (SqlDataReader ts) => {
                    gv.DataSource = ts;
                    gv.DataBind();
                }
            );

            ViewState["shopId"] = gv.Rows[0].Cells[0].Text;
            LB_shopName.Text = gv.Rows[0].Cells[1].Text;
            LB_shopEMail.Text += gv.Rows[0].Cells[2].Text;
            LB_shopPhone.Text += gv.Rows[0].Cells[3].Text;
            //連接 商店Url
            LB_shopName.Attributes.Add("href", $"/shop/Shop.aspx?shopId={ViewState["shopId"]}");
        }

        /// <summary>
        /// 顯示用戶選單 (已登入)
        /// </summary>
        private void showWelcomeUserInMenu()
        {
            // 歡迎列表控件 (已登入)
            Label LB_Welcome = new Label();
            LinkButton LB_UserDashBoard = new LinkButton();
            Label LB_Menuseparate = new Label();
            LinkButton LB_UserSignOut = new LinkButton();

            LB_Welcome.CssClass = "menuWelcome";
            LB_UserDashBoard.CssClass = "generalLink";
            LB_Menuseparate.CssClass = "TitelMenuseparate";
            LB_UserSignOut.CssClass = "generalLink";

            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            DetailsView userTable = new DetailsView();

            //調用DB 取得用戶名稱
            DB.connectionReader(
                "selectUserName.sql",
                new ArrayList {
                    new DB.Parameter("UserID", SqlDbType.Int, Session["UserId"])
                },
                (SqlDataReader ts) => {
                    userTable.DataSource = ts;
                    userTable.DataBind();
                }
            );

            LB_Welcome.Text = "歡迎您！ ";
            LB_UserDashBoard.Text = userTable.Rows[0].Cells[1].Text;
            LB_Menuseparate.Text = "|";
            LB_UserSignOut.Text = "登出";

            //連接 個人儀表板 Url
            LB_UserDashBoard.Attributes.Add("href", "/buyer/DashBoard.aspx");

            // 掛載 "歡迎列表控件"
            Panel_TitelMenuLogin.Controls.Add(LB_Welcome);
            Panel_TitelMenuLogin.Controls.Add(LB_UserDashBoard);
            Panel_TitelMenuLogin.Controls.Add(LB_Menuseparate);
            Panel_TitelMenuLogin.Controls.Add(LB_UserSignOut);

            //登出按鍵 掛載 登出事件
            LB_UserSignOut.Click += new EventHandler(
                (object sender, EventArgs e) => {
                    Session["UserId"] = null;
                    Response.Write("<script>alert('成功登出！');window.location.reload();</script>");
                });
        }

        /// <summary>
        ///  顯示用戶選單 (未登入)
        /// </summary>
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

        /// <summary>
        /// 更改選取的商品數量 事件
        /// </summary>
        protected void TB_commodityNum_TextChanged(object sender, EventArgs e)
        {
            //判斷數量是否正確 (錯誤則執行)
            if (TB_commodityNum.Text == String.Empty ||
                !new Regex("^[0-9]*$").IsMatch(TB_commodityNum.Text) ||
                Int32.Parse(TB_commodityNum.Text) < 1)
            {
                TB_commodityNum.Text = "1";
                return;
            }
            // 判斷數量是否超過庫存
            else if (Int32.Parse(TB_commodityNum.Text) > Int32.Parse(ViewState[$"commodityNum"].ToString()))
            {
                TB_commodityNum.Text = ViewState[$"commodityNum"].ToString();
            }
        }

        /// <summary>
        /// 點擊商品搜索 (事件)
        /// </summary>
        protected void LB_runSearch_Click(object sender, EventArgs e)
        {
            if (DDL_SearchMode.SelectedIndex == 0)
            {
                Response.Redirect($"~/search/Search.aspx?commoditySearch={TB_Search.Text}");
            }
        }

        /// <summary>
        /// 加入購物車 事件
        /// </summary>
        protected void LB_JoinShoppingCart_Click(object sender, EventArgs e)
        {
            //判斷用戶狀態
            if (Session["UserId"] == null)
            {
                Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='../buyer/Login.aspx';</script>");
                return;
            }
            else if (ViewState["UserId"].ToString() != Session["UserId"].ToString())
            {
                Response.Write($"<script>alert('頁面內容與帳號不符，重新入頁面！');window.location='../commodity/Item.aspx?commodityId={Context.Request.QueryString["commodityId"]}';</script>");
                return;
            }

            //判斷輸入數量是否有誤
            if (TB_commodityNum.Text == String.Empty
                || !new Regex("^[0-9]*$").IsMatch(TB_commodityNum.Text)
                || Int32.Parse(TB_commodityNum.Text) < 1
                || Int32.Parse(TB_commodityNum.Text) > Int32.Parse(ViewState[$"commodityNum"].ToString()))
            {
                Response.Write("<script>alert('輸入數量有誤！')</script>");
                return;
            }

            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            DetailsView dv = new DetailsView();
            //調用DB 將商品加入購物車
            DB.connectionReader(
                "joinShoppingCart.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.Int, Session["UserId"].ToString()),
                    new DB.Parameter("CommodityId", SqlDbType.Int, Context.Request.QueryString["commodityId"].ToString()),
                    new DB.Parameter("CommodityNum", SqlDbType.Int, TB_commodityNum.Text)
                },
                (SqlDataReader ts) => {
                    dv.DataSource = ts;
                    dv.DataBind();
                }
            );
            //判斷回覆狀態
            switch (dv.Rows[0].Cells[1].Text)
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

        /// <summary>
        /// 直接購買 事件
        /// </summary>
        protected void LB_ToShopping_Click(object sender, EventArgs e)
        {
            //判斷用戶狀態
            if (Session["UserId"] == null)
            {
                Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='../buyer/Login.aspx';</script>");
                return;
            }
            else if (ViewState["UserId"].ToString() != Session["UserId"].ToString())
            {
                Response.Write($"<script>alert('頁面內容與帳號不符，重新入頁面！');window.location='../commodity/Item.aspx?commodityId={Context.Request.QueryString["commodityId"]}';</script>");
                return;
            }

            //判斷輸入數量是否有誤
            if (TB_commodityNum.Text == String.Empty
                || !new Regex("^[0-9]*$").IsMatch(TB_commodityNum.Text)
                || Int32.Parse(TB_commodityNum.Text) < 1
                || Int32.Parse(TB_commodityNum.Text) > Int32.Parse(ViewState[$"commodityNum"].ToString()))
            {
                Response.Write("<script>alert('輸入數量有誤！')</script>");
                return;
            }

            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            DetailsView dv = new DetailsView();

            //調用DB 購買商品，並建構訂單紀錄
            DB.connectionReader(
                "shoppingSingle.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.Int, Session["UserId"]),
                    new DB.Parameter("CommodityId", SqlDbType.Int, Context.Request.QueryString["commodityId"].ToString()),
                    new DB.Parameter("SelectNum", SqlDbType.Int, TB_commodityNum.Text)
                },
                (SqlDataReader ts) => {
                    dv.DataSource = ts;
                    dv.DataBind();
                }
            );

            //判斷回覆狀態
            switch (dv.Rows[1].Cells[1].Text)
            {
                case "1":
                    Response.Write($"<script>alert('{dv.Rows[0].Cells[1].Text}');window.location='../buyer/TransactionList.aspx';</script>");
                    break;
                case "-1":
                    Response.Write($"<script>alert('{dv.Rows[0].Cells[1].Text}');window.location='../commodity/Item.aspx?commodityId={Context.Request.QueryString["commodityId"].ToString()}';</script>");
                    break;
                default:
                    Response.Write($"<script>alert('{dv.Rows[0].Cells[1].Text}');window.location='../commodity/Item.aspx?commodityId={Context.Request.QueryString["commodityId"].ToString()}';</script>\"");
                    break;
            }
        }
    }
}