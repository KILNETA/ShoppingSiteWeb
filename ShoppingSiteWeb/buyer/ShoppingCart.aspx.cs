using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using Web.ShoppingCart;
using System.Web.UI;

namespace ShoppingSiteWeb.buyer
{

    /// <summary>
    /// 購物車頁面
    /// </summary>
    public partial class ShoppingCart : System.Web.UI.Page
    {
        /// <summary>
        /// 購物車列表
        /// </summary>
        ArrayList shoppingCartList = new ArrayList();

        /// <summary>
        /// 頁面加載
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {   //next load
                if (!checkUserStateUsable())
                    return;
                //取出緩存資料
                shoppingCartList = (ArrayList)ViewState["shoppingCartList"];

                calculateSubTotal();
            }
            else
            {   //first load
                //已登入
                if (Session["UserId"] != null)
                {
                    //創建Token許可證
                    String Token = Path.GetRandomFileName().Replace(".", "");
                    //用於判斷表單是否被認證(存儲用戶與server互動的數據) 
                    Session["Token"] = Token;
                    //在表單存入Token許可證
                    TB_Token.Text = Token;
                    //紀錄User (驗證身分使用)
                    ViewState["UserId"] = Session["UserId"].ToString();

                    LoadUserData();
                    selectShoppingCartData();
                }
                else
                {
                    Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='Login.aspx';</script>");
                    return;
                }
            }
            if (Session["UserId"] != null)
            {
                if (shoppingCartList.Count > 0)
                {   //購物車不為空
                    LB_ToShopping.CssClass = "SignOutButton";
                    LB_ToShopping.Enabled = true;
                    showShoppingCartList();
                }
                else
                {   //購物車為空
                    LB_ToShopping.CssClass = "SignOutButton_Lack";
                    LB_ToShopping.Enabled = false;
                    Panel_ShoppingCartBox.ContentTemplateContainer.Controls.Add(
                        shoppingCartIsEmptyUI());
                }
            }
        }

        /// <summary>
        /// 確認用戶狀態
        /// </summary>
        /// <returns>用戶狀態是否正常</returns>
        private bool checkUserStateUsable()
        {
            //檢驗用戶狀態
            if (Session["UserId"] == null)
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "goBackJS",
                    "alert('尚未登入！進入登入頁面！');" +
                    "window.location.replace(window.location.href);",
                    true);
                return false;
            }
            else if (ViewState["UserId"].ToString() != Session["UserId"].ToString())
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "goBackJS",
                    "alert('頁面內容與帳號不符，重新入頁面！');" +
                    "window.location.replace(window.location.href);",
                    true);
                return false;
            }
            //驗證Token
            else if (Session["Token"] == null || TB_Token.Text != Session["Token"].ToString())
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "goBackJS",
                    "alert('頁面閒置過久，重新載入！');" +
                    "window.location.replace(window.location.href);",
                    true);
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// 購物車是空的UI布局
        /// </summary>
        /// <returns>Label UI布局</returns>
        private Label shoppingCartIsEmptyUI()
        {
            Label ShoppingCartIsEmpty = new Label();
            ShoppingCartIsEmpty.CssClass = "SC_ShoppingCartIsEmpty";
            ShoppingCartIsEmpty.Text = "購物車內暫無商品！";
            return ShoppingCartIsEmpty;
        }

        /// <summary>
        /// 計算總計
        /// </summary>
        public void calculateSubTotal()
        {
            ///小計金額
            long subTotalNum = 0;

            foreach (ShoppingCartCommodity item in shoppingCartList)
            {
                if (item.isBuyCheck)
                    subTotalNum += item.CT_price * item.SCT_selectNum;
            }
            subTotal.Text = $"${subTotalNum.ToString("N0")}";
        }

        /// <summary>
        /// 載入用戶資料
        /// </summary>
        private void LoadUserData()
        {
            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            DetailsView userTable = new DetailsView();

            //調用DB 取得用戶名稱
            DB.connectionReader(
                "selectUserName.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.Int, Session["UserId"])
                },
                (SqlDataReader ts) => {
                    userTable.DataSource = ts;
                    userTable.DataBind();
                }
            );
            //輸出歡迎語的用戶名稱
            userRealName.Text = userTable.Rows[0].Cells[1].Text;
        }

        /// <summary>
        /// 取得購物車資料
        /// </summary>
        private void selectShoppingCartData()
        {

            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            GridView gv = new GridView();

            //調用DB 取得購物車資料
            DB.connectionReader(
                "selectShoppingCartData.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.Int, Session["UserId"])
                },
                (SqlDataReader ts) => {
                    gv.DataSource = ts;
                    gv.DataBind();
                }
            );

            //暫存購物車資料
            foreach (TableRow item in gv.Rows)
            {
                shoppingCartList.Add(
                    new ShoppingCartCommodity(
                        item.Cells[0].Text,
                        item.Cells[1].Text,
                        item.Cells[2].Text,
                        item.Cells[3].Text,
                        item.Cells[4].Text,
                        item.Cells[5].Text,
                        item.Cells[6].Text
                        )
                    );
            }
            ViewState["shoppingCartList"] = shoppingCartList;
        }

        /// <summary>
        /// 顯示購物車內容
        /// </summary>
        private void showShoppingCartList()
        {
            ///統一暫存 Token
            string Token = TB_Token.Text;
            ///統一暫存 UserId
            string UserId = ViewState["UserId"].ToString();
            foreach (ShoppingCartCommodity item in shoppingCartList)
            {
                Panel_ShoppingCartBox.ContentTemplateContainer.Controls.Add(
                    new ShoppingCartCommodityUI(item, this, UserId, Token)
                );
            }
        }

        /// <summary>
        /// 購物按鈕 事件
        /// </summary>
        protected void LB_ToShopping_Click(object sender, EventArgs e)
        {
            if (!checkUserStateUsable())
                return;

            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            DetailsView dv = new DetailsView();

            /// <summary>
            /// SQL 參數陣列
            /// </summary>
            ArrayList Parameter = new ArrayList();
            foreach (ShoppingCartCommodity item in shoppingCartList)
            {
                if (item.isBuyCheck)
                {
                    Parameter.Add(new parms(item.SCT_id, item.SCT_selectNum));
                }
            }
            
            //調用DB 購買選取的商品，並建構訂單紀錄
            DB.connectionReader(
                "shopping.sql",
                new ArrayList {
                    new DB.Parameter("json_str", SqlDbType.NVarChar, DB.parmsToJson(Parameter)),
                    new DB.Parameter("UserId", SqlDbType.Int, Session["UserId"])
                },
                (SqlDataReader ts) => {
                    dv.DataSource = ts;
                    dv.DataBind();
                }
            );

            //狀態回報
            switch (dv.Rows[1].Cells[1].Text)
            {
                case "1":
                    Response.Write($"<script>alert('{dv.Rows[0].Cells[1].Text}');window.location='TransactionList.aspx';</script>");
                    break;
                case "-1":
                    Response.Write($"<script>alert('{dv.Rows[0].Cells[1].Text}');window.location='ShoppingCart.aspx';</script>");
                    break;
                default:
                    Response.Write($"<script>alert('{dv.Rows[0].Cells[1].Text}');window.location='ShoppingCart.aspx';</script>\"");
                    break;
            }
        }

        /// <summary>
        /// SQL Server 參數陣列 數據結構
        /// </summary>
        struct parms
        {
            public int Id;
            public int Num;
            public parms(int Id, int Num)
            {
                this.Id = Id;
                this.Num = Num;
            }
        }
    }
}