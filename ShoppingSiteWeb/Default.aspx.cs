using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
using Web.Commodity;

namespace ShoppingSiteWeb
{
    /// <summary>
    /// 首頁
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {

        /// <summary>
        /// 頁面加載
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //設置右上角的用戶狀態
            if (Session["UserId"] != null)
                showWelcomeUserInMenu();
            else
                showLoginInMenu();

            if (IsPostBack)
            {   //next load

            }
            else
            {   //first load
                selectRecommendCommoditys();
            }

            //加載推薦的商品目錄
            showCommodityPage();
        }

        /// <summary>
        /// 選擇推薦商品
        /// </summary>
        private void selectRecommendCommoditys()
        {
            /// <summary>
            ///隨機商品數
            /// </summary>
            int chooseNum = 36;
            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            GridView gv = new GridView();

            //調用DB 隨機取出商品 作為推薦
            DB.connectionReader(
                "Homepage_RandomItems.sql",
                new ArrayList {
                    new DB.Parameter("ChooseNum", SqlDbType.Int, chooseNum)
                },
                (SqlDataReader ts) => { 
                    gv.DataSource = ts;
                    gv.DataBind();
                }
            );
            //將 推薦商品 數據存儲至 網頁暫存
            saveRecommendCommoditys(gv);
        }

        /// <summary>
        /// 將 推薦商品(商品資訊) 數據存儲至 網頁暫存
        /// </summary>
        private void saveRecommendCommoditys(GridView gv)
        {
            ArrayList CommodityList = new ArrayList();
            for (int row = 0; row < 36; row++) {
                CommodityList.Add(new Commodity(
                    gv.Rows[row].Cells[0].Text,
                    gv.Rows[row].Cells[1].Text,
                    gv.Rows[row].Cells[2].Text,
                    gv.Rows[row].Cells[3].Text,
                    gv.Rows[row].Cells[4].Text
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
                    Response.Write("<script>alert('成功登出！');window.location='Default.aspx';</script>");
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
            /// 商品列數量
            /// </summary>
            int column = 6;
            /// <summary>
            /// 商品頁(框架)
            /// </summary>
            List<Panel> commodityPage = new List<Panel>();

            for (int i = 0 ; i < column; i++)
            {
                commodityPage.Add(showCommodityRow( i ));
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
                commodityRow.Controls.Add(
                    new CommodityUI(
                        (Commodity)CommodityList[colIndex * rowCount + i], 
                        this
                    )
                );
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