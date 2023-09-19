using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

///<summary>
/// 購物車組件
///</summary>
namespace Web.ShoppingCart
{
    /// <summary>
    /// 商品資料 資料結構
    /// </summary>
    [Serializable]
    class ShoppingCartCommodity
    {
        public int SCT_id;
        public int SCT_selectNum;
        public string CT_name;
        public int CT_price;
        public int CT_num;
        public string CT_thumbnail;
        public string CT_introduction;
        public bool isBuyCheck = false;

        /// <summary>
        /// 商品資料 建構子
        /// </summary>
        /// <param name="SCT_id">唯一編號</param>
        /// <param name="SCT_selectNum">選購數量</param>
        /// <param name="CT_name">名稱</param>
        /// <param name="CT_price">價格</param>
        /// <param name="CT_num">數量</param>
        /// <param name="CT_thumbnail">預覽圖</param>
        /// <param name="CT_introduction">商品簡介</param>
        public ShoppingCartCommodity(
            string SCT_id,
            string SCT_selectNum,
            string CT_name,
            string CT_price,
            string CT_num,
            string CT_thumbnail,
            string CT_introduction
        ) : base()
        {
            this.SCT_id = int.Parse(SCT_id);
            this.SCT_selectNum = int.Parse(SCT_selectNum);
            this.CT_name = CT_name;
            this.CT_price = int.Parse(CT_price);
            this.CT_num = int.Parse(CT_num);
            this.CT_thumbnail = CT_thumbnail;
            this.CT_introduction = CT_introduction;
        }
    }

    /// <summary>
    /// 購物車列表項目 UI
    /// </summary>
    class ShoppingCartCommodityUI : Panel
    {
        /// <summary>
        /// 當前布局的頁面
        /// </summary>
        ShoppingSiteWeb.buyer.ShoppingCart page;
        /// <summary>
        /// 暫存 UserId
        /// </summary>
        string UserId;
        /// <summary>
        /// 暫存 Token
        /// </summary>
        string Token;

        /// <summary>
        /// 勾選控件(框架)
        /// </summary>
        Panel check_Box = new Panel();
        /// <summary>
        /// 勾選控件
        /// </summary>
        CheckBox check = new CheckBox();

        /// <summary>
        /// 縮圖(框架)
        /// </summary>
        LinkButton thumbnail_Box = new LinkButton();
        /// <summary>
        /// 縮圖
        /// </summary>
        Image thumbnail = new Image();

        /// <summary>
        /// 商品編號
        /// </summary>
        Label commodityId = new Label();

        /// <summary>
        /// 分區線1
        /// </summary>
        Panel hr_1 = new Panel();
        /// <summary>
        /// 分區線2
        /// </summary>
        Panel hr_2 = new Panel();

        /// <summary>
        /// 內容(框架)
        /// </summary>
        Panel context_Box = new Panel();
        /// <summary>
        /// 商品名
        /// </summary>
        LinkButton name = new LinkButton();
        /// <summary>
        /// 價格
        /// </summary>
        Label price = new Label();
        /// <summary>
        /// 簡介
        /// </summary>
        Label introduction = new Label();

        /// <summary>
        /// 價錢與小計(框架)
        /// </summary>
        Panel numSubPrice_Box = new Panel();
        /// <summary>
        /// 選擇數量(框架)
        /// </summary>
        Panel num_Box = new Panel();
        /// <summary>
        /// 防止 TextBox Postback 刷新頁面
        /// </summary>
        UpdatePanel updatePanel = new UpdatePanel();
        /// <summary>
        /// 選擇數量
        /// </summary>
        TextBox num = new TextBox();
        /// <summary>
        /// 小計(框架)
        /// </summary>
        Panel subPrice_Box = new Panel();
        /// <summary>
        /// 小計內容(框架)
        /// </summary>
        Panel subPrice_Context = new Panel();
        /// <summary>
        /// 小計標頭
        /// </summary>
        Label subPriceTitle = new Label();
        /// <summary>
        /// 小計
        /// </summary>
        Label subPrice = new Label();
        /// <summary>
        /// 數量
        /// </summary>
        Panel num_Text = new Panel();
        /// <summary>
        /// 庫存標頭
        /// </summary>
        Label hasNum = new Label();
        /// <summary>
        /// 庫存
        /// </summary>
        Label hasNumLack = new Label();

        /// <summary>
        /// 刪除按鈕
        /// </summary>
        LinkButton remove = new LinkButton();
        /// <summary>
        /// 刪除圖示
        /// </summary>
        Panel remove_Icon = new Panel();

        /// <summary>
        /// UI建構子
        /// </summary>
        /// <param name="CommodityData">商品資料</param>
        /// <param name="page">頁面</param>
        /// <param name="UserId">用戶ID</param>
        /// <param name="Token">Token</param>
        public ShoppingCartCommodityUI(ShoppingCartCommodity CommodityData, ShoppingSiteWeb.buyer.ShoppingCart page,string UserId, string Token)
        {
            //暫存公用認證參數
            this.page = page;
            this.UserId = UserId;
            this.Token = Token;

            ///單件價格
            String PriceNum =
                    CommodityData.CT_price.ToString("N0");
            ///小計價格
            String SubPriceNum =
               (CommodityData.CT_price * CommodityData.SCT_selectNum).ToString("N0");

            //UI Css掛載
            this.CssClass = "SC_commodityItem";

            //缺貨情況
            if (CommodityData.CT_num < 1)
            {
                hasNum.Text = "";
                hasNumLack.Text = "缺貨中";
                num.Enabled = false;
                subPrice.CssClass = "SC_SubPrice_Lack";
                check.Enabled = false;
                check.Checked = false;
                CommodityData.isBuyCheck = false;
            }
            else
            {
                hasNum.Text = $"庫存 {CommodityData.CT_num} 件";
                hasNumLack.Text = "";
                num.Enabled = true;
                subPrice.CssClass = "SC_SubPrice";
                check.Enabled = true;
                check.Checked = CommodityData.isBuyCheck;
            }

            //勾選框
            check_Box.CssClass = "SC_Check_Box";
            check.BorderStyle = BorderStyle.None;
            check.CssClass = "SC_Check";
            check_Box.Controls.Add(check);
            check.AutoPostBack = true;
            check.CheckedChanged +=
                delegate (object sender, EventArgs e) {
                    VBT_ShoppingCartCommodityCheckChanged(
                        CommodityData
                    );
                };

            //縮圖與商品編號
            thumbnail_Box.CssClass = "SC_Thumbnail_Box";
            thumbnail.CssClass = "SC_Thumbnail";
            thumbnail.ImageUrl = CommodityData.CT_thumbnail.ToString();
            commodityId.CssClass = "SC_Id";
            commodityId.Text = $"商品編號：{CommodityData.SCT_id}";
            //子控件整合
            thumbnail_Box.Controls.Add(thumbnail);
            thumbnail_Box.Controls.Add(commodityId);

            //分區線 CSS
            hr_1.CssClass = "SC_hr";
            hr_2.CssClass = "SC_hr";

            //商品資訊
            context_Box.CssClass = "SC_Context_Box";
            name.CssClass = "SC_Name";
            name.Text = CommodityData.CT_name.ToString();
            price.CssClass = "SC_Price";
            price.Text = $"${PriceNum}";
            introduction.CssClass = "SC_Introduction";
            introduction.Text = CommodityData.CT_introduction.ToString();
            //子控件整合
            context_Box.Controls.Add(name);
            context_Box.Controls.Add(price);
            context_Box.Controls.Add(introduction);

            //商品小計
            numSubPrice_Box.CssClass = "SC_NumSubPrice_Box";
            subPrice_Box.CssClass = "SC_SubPrice_Box";
            subPrice_Context.CssClass = "SC_SubPrice_Context";
            subPriceTitle.CssClass = "SC_SubPriceTitle";
            subPriceTitle.Text = "小計：";
            subPrice.CssClass = "SC_SubPrice";
            subPrice.Text = $"${SubPriceNum}";
            //子控件整合
            subPrice_Context.Controls.Add(subPriceTitle);
            subPrice_Context.Controls.Add(subPrice);
            subPrice_Box.Controls.Add(subPrice_Context);

            //商品數量
            num_Box.CssClass = "SC_Num_Box";
            num.CssClass = "SC_Input";
            num.Text = CommodityData.SCT_selectNum.ToString();
            num_Text.CssClass = "SC_Num_Text";
            hasNum.CssClass = "SC_HasNum";
            hasNumLack.CssClass = "SC_HasNum_Lack";
            num_Text.Controls.Add(hasNum);
            num_Text.Controls.Add(hasNumLack);
            num.AutoPostBack = true;
            num.ID = $"selectNumChange_{CommodityData.SCT_id}";
            //商品數量更改事件
            num.TextChanged +=
                delegate (object sender, EventArgs e) {
                    VBT_ShoppingCartCommodityNumChanged(
                        CommodityData
                    );
                };

            //子控件整合
            num_Box.Controls.Add(num);
            num_Box.Controls.Add(num_Text);
            //子控件整合
            numSubPrice_Box.Controls.Add(subPrice_Box);
            numSubPrice_Box.Controls.Add(num_Box);

            //刪除
            remove.CssClass = "SC_Remove";
            remove_Icon.CssClass = "SC_RemoveIcon";
            remove.Controls.Add(remove_Icon);
            //刪除事件
            remove.Click +=
                delegate (object sender, EventArgs e) {
                    VBT_ShoppingCartRemove(
                        CommodityData
                    );
                };

            //連接商品頁面
            string commodityLink = $"../commodity/Item.aspx?commodityId={CommodityData.SCT_id}";
            thumbnail_Box.Attributes.Add("href", commodityLink);
            name.Attributes.Add("href", commodityLink);

            //主控件整合
            this.Controls.Add(check_Box);
            this.Controls.Add(thumbnail_Box);
            this.Controls.Add(hr_1);
            this.Controls.Add(context_Box);
            this.Controls.Add(hr_2);
            this.Controls.Add(numSubPrice_Box);
            this.Controls.Add(remove);
        }

        /// <summary>
        /// 勾選框變更
        /// </summary>
        /// <param name="CommodityData">商品資料</param>
        protected void VBT_ShoppingCartCommodityCheckChanged(
            ShoppingCartCommodity CommodityData
        ){
            if (!checkUserStateUsable())
                return;

            //更改選取
            if (check.Checked)
                CommodityData.isBuyCheck = true;
            else
                CommodityData.isBuyCheck = false;
            //重新計算總金額
            page.calculateSubTotal();
        }

        /// <summary>
        /// 選擇數量變更
        /// </summary>
        /// <param name="CommodityData">商品資料</param>
        protected void VBT_ShoppingCartCommodityNumChanged(
            ShoppingCartCommodity CommodityData
        ){
            if (!checkUserStateUsable())
                return;

            //判斷數量是否正確 (錯誤則執行)
            else if (
                    num.Text == String.Empty
                || !new Regex("^[0-9]*$").IsMatch(num.Text)
                || Int32.Parse(num.Text) > CommodityData.CT_num
                || Int32.Parse(num.Text) < 1)
            {
                num.Text = CommodityData.SCT_selectNum.ToString();
                return;
            }

            //儲存更改的商品選擇數量
            DB.connectionReader(
                "changeShoppingSelectNum.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.Int, page.Session["UserId"]),
                    new DB.Parameter("CommodityId", SqlDbType.Int, CommodityData.SCT_id),
                    new DB.Parameter("CommodityNum", SqlDbType.Int, num.Text)
                },
                (SqlDataReader ts) => {}
            );

            //儲存選取的數量
            CommodityData.SCT_selectNum = int.Parse(num.Text);
            ///小計價格
            String SubPriceNum =
                    (CommodityData.CT_price * CommodityData.SCT_selectNum).ToString("N0");
            //顯示單項小計
            subPrice.Text = $"${SubPriceNum}";
            //重新計算總金額
            page.calculateSubTotal();
        }

        /// <summary>
        /// 刪除購物車商品
        /// </summary>
        /// <param name="CommodityData">商品資料</param>
        protected void VBT_ShoppingCartRemove(
            ShoppingCartCommodity CommodityData
        ){
            if (!checkUserStateUsable())
                return;

            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            DetailsView dv = new DetailsView();

            //儲存更改的商品選擇數量
            DB.connectionReader(
                "deleteShoppingCartCommodity.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.Int, page.Session["UserId"]),
                    new DB.Parameter("CommodityId", SqlDbType.Int, CommodityData.SCT_id),
                },
                (SqlDataReader ts) => {
                    dv.DataSource = ts;
                    dv.DataBind();
                }
            );

            //操作結果提示彈窗
            if (dv.Rows[0].Cells[1].Text != "0")
                page.Response.Write("<script>alert('已從購物車移除！');window.location='ShoppingCart.aspx';</script>\"");
            else
                page.Response.Write("<script>alert('從購物車移除，失敗！');window.location='ShoppingCart.aspx';</script>\"");
        }

        /// <summary>
        /// 確認用戶狀態
        /// </summary>
        /// <returns>用戶狀態是否正常</returns>
        private bool checkUserStateUsable()
        {
            //檢驗用戶狀態
            if (page.Session["UserId"] == null)
                return false;
            else if (UserId != page.Session["UserId"].ToString())
                return false;
            //驗證Token
            if (page.Session["Token"] == null || Token != page.Session["Token"].ToString())
                return false;
            else
                return true;
        }
    }
}