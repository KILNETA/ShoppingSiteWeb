using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Web.Commodity;

namespace ShoppingSiteWeb.shop
{
    /// <summary>
    /// 商店頁面
    /// </summary>
    public partial class Shop : System.Web.UI.Page
    {

        /// <summary>
        /// 頁面加載
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //檢查 Request 是否有 shopId 參數
            //檢查 shopId 是否為 數字編號
            if (Context.Request.QueryString["shopId"] == null || 
                !new Regex("^[0-9]*$").IsMatch(Context.Request.QueryString["shopId"].ToString()))
            {
                //返回首頁
                Response.Redirect("../Default.aspx");
                return;
            }
            //顯示商店資料
            showShopData();

            if (IsPostBack)
            {   //next load

            }
            else
            {   //first load
                //加載商品資料
                selectRecommendCommoditys();
            }
            //加載商品目錄
            showCommodityPage();

            //設置右上角的用戶狀態
            if (Session["UserId"] != null)
                showWelcomeUserInMenu();
            else
                showLoginInMenu();
        }

        /// <summary>
        /// 顯示商店資料
        /// </summary>
        private void showShopData()
        {
            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            GridView gv = new GridView();

            //調用DB 取得商店資料
            DB.connectionReader(
                "selectShopData.sql",
                new ArrayList {
                    new DB.Parameter("ShopId", SqlDbType.Int, Context.Request.QueryString["shopId"].ToString())
                },
                (SqlDataReader ts) => {
                    gv.DataSource = ts;
                    gv.DataBind();
                }
            );

            //查找結果存在商店
            if (gv.Rows.Count != 0) {
                LB_shopName.Text = gv.Rows[0].Cells[0].Text;
                LB_shopEMail.Text += gv.Rows[0].Cells[1].Text;
                LB_shopPhone.Text += gv.Rows[0].Cells[2].Text;
                LB_shopAddress.Text += gv.Rows[0].Cells[3].Text;
            }
            //找不到商店返回首頁
            else
            {
                Response.Redirect("../Default.aspx");
            }
        }

        /// <summary>
        /// 加載商品資料
        /// </summary>
        private void selectRecommendCommoditys()
        {
            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            GridView gv = new GridView();

            //調用DB 取出商店販賣的商品
            DB.connectionReader(
                "selectCommodityOfShop.sql",
                new ArrayList {
                    new DB.Parameter("ShopId", SqlDbType.Int, Context.Request.QueryString["shopId"].ToString())
                },
                (SqlDataReader ts) => {
                    gv.DataSource = ts;
                    gv.DataBind();
                }
            );

            //將 商品數據 存儲至 網頁暫存
            saveRecommendCommoditys(gv);
        }

        /// <summary>
        /// 暫存商品數據
        /// </summary>
        /// <param name="gv">商品數據</param>
        private void saveRecommendCommoditys(GridView gv)
        {
            /// <summary>
            /// 商品列表
            /// </summary>
            ArrayList CommodityList = new ArrayList();
            //將 商品數據 根據資料結構 定序後暫存
            foreach (GridViewRow commodity in gv.Rows)
            {
                CommodityList.Add(new Commodity(
                    commodity.Cells[0].Text,
                    commodity.Cells[1].Text,
                    commodity.Cells[2].Text,
                    commodity.Cells[3].Text,
                    commodity.Cells[4].Text
                ));
            }
            ViewState["CommodityList"] = CommodityList;
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

            // 調用DB 取得用戶名稱
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

            // 連接 個人儀表板 Url
            LB_UserDashBoard.Attributes.Add("href", "/buyer/DashBoard.aspx");

            // 掛載 "歡迎列表控件"
            Panel_TitelMenuLogin.Controls.Add(LB_Welcome);
            Panel_TitelMenuLogin.Controls.Add(LB_UserDashBoard);
            Panel_TitelMenuLogin.Controls.Add(LB_Menuseparate);
            Panel_TitelMenuLogin.Controls.Add(LB_UserSignOut);

            // 登出按鍵 掛載 登出事件
            LB_UserSignOut.Click += new EventHandler(
                (object sender, EventArgs e) => {
                    Session["UserId"] = null;
                    Response.Write("<script>alert('成功登出！');window.location.reload();</script>");
                });
        }

        /// <summary>
        /// 顯示用戶選單 (未登入)
        /// </summary>
        private void showLoginInMenu()
        {
            // 歡迎列表控件 (未登入)
            LinkButton LB_Register = new LinkButton();
            LinkButton LB_Login = new LinkButton();
            Label LB_Menuseparate = new Label();

            LB_Register.CssClass = "generalLink";
            LB_Login.CssClass = "generalLink";
            LB_Menuseparate.CssClass = "TitelMenuseparate";

            LB_Register.Text = "會員註冊";
            LB_Login.Text = "會員登入";
            LB_Menuseparate.Text = "|";

            //連接 會員註冊、會員登入 Url
            LB_Register.Attributes.Add("href", "/buyer/Register.aspx");
            LB_Login.Attributes.Add("href", "/buyer/Login.aspx");

            // 掛載 "歡迎列表控件"
            Panel_TitelMenuLogin.Controls.Add(LB_Register);
            Panel_TitelMenuLogin.Controls.Add(LB_Menuseparate);
            Panel_TitelMenuLogin.Controls.Add(LB_Login);
        }

        /// <summary>
        /// 顯示商品頁面
        /// </summary>
        private void showCommodityPage()
        {
            /// <summary>
            /// 商品列表
            /// </summary>
            var CommodityList = (ArrayList)ViewState["CommodityList"];
            /// <summary>
            /// 商品列數量
            /// </summary>
            int column = CommodityList.Count / 6 + (CommodityList.Count % 6 == 0 ? 0 : 1);
            /// <summary>
            /// 商品頁(框架)
            /// </summary>
            List<Panel> commodityPage = new List<Panel>();

            for (int i = 0; i < column; i++)
            {
                commodityPage.Add(showCommodityRow(i));
                commodityPage[i].CssClass = "CommodityList";
                Panel_CommodityPage.Controls.Add(commodityPage[i]);
            }
        }

        /// <summary>
        /// 展示商品列
        /// </summary>
        /// <param name="colIndex">所在column</param>
        /// <returns></returns>
        private Panel showCommodityRow(int colIndex)
        {
            /// <summary>
            /// 商品列展示數量
            /// </summary>
            int rowCount = 6;
            /// <summary>
            /// 商品列(框架)
            /// </summary>
            Panel commodityRow = new Panel();
            /// <summary>
            /// 商品列表
            /// </summary>
            var CommodityList = (ArrayList)ViewState["CommodityList"];

            for (int i = 0; i < rowCount; i++)
            {
                if (colIndex * rowCount + i < CommodityList.Count)
                {   //顯示商品控件
                    commodityRow.Controls.Add(
                        new CommodityUI(
                            (Commodity)CommodityList[colIndex * rowCount + i],
                            this
                        )
                    );
                }
                else
                {   //商品佔位符
                    Panel commodityItem = new Panel();
                    commodityItem.CssClass = "CommodityItem_none";
                    commodityRow.Controls.Add(commodityItem);
                }
            }

            return commodityRow;
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
    }
}