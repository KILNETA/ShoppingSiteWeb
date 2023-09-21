using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System;

///<summary>
/// 商品組件
///</summary>
namespace Web.Commodity
{

    /// <summary>
    /// 商品資料 資料結構
    /// </summary>
    [Serializable]
    class Commodity
    {
        /// <summary>
        /// 商品資料 資料結構
        /// </summary>
        public int id;
        public string name;
        public int price;
        public int num;
        public string thumbnail;

        /// <summary>
        /// 商品資料 建構子
        /// </summary>
        /// <param name="id">唯一編號</param>
        /// <param name="name">名稱</param>
        /// <param name="price">價格</param>
        /// <param name="num">數量</param>
        /// <param name="thumbnail">預覽圖</param>
        /// <param name="shopId">商店唯一編號</param>
        /// <param name="shopName">商店名稱</param>
        public Commodity(
            string id,
            string name,
            string price,
            string num,
            string thumbnail
        )
        {
            this.id = int.Parse(id);
            this.name = name;
            this.price = int.Parse(price);
            this.num = int.Parse(num);
            this.thumbnail = thumbnail;
        }
    }

    /// <summary>
    /// 商品項目UI
    /// </summary>
    class CommodityUI : Panel
    {
        /// <summary>
        /// 縮圖(框架)
        /// </summary>
        private LinkButton thumbnailFrame = new LinkButton();
        /// <summary>
        /// 縮圖
        /// </summary>
        private Image thumbnail = new Image();

        /// <summary>
        /// 商品文字資訊(框架)
        /// </summary>
        private Panel contentFrame = new Panel();
        /// <summary>
        /// 商品名稱(框架)
        /// </summary>
        private LinkButton nameFrame = new LinkButton();
        /// <summary>
        /// 商品名稱
        /// </summary>
        private Label name = new Label();
        /// <summary>
        /// 價格(框架)
        /// </summary>
        private Panel priceContent = new Panel();
        /// <summary>
        /// 價格符號
        /// </summary>
        private Label priceSymbol = new Label();
        /// <summary>
        /// 價格
        /// </summary>
        private Label price = new Label();

        /// <summary>
        /// 購物車
        /// </summary>
        private LinkButton shoppingCart = new LinkButton();
        /// <summary>
        /// 購物車(圖示)
        /// </summary>
        private Panel shoppingCart_Icon = new Panel();

        /// <summary>
        /// 商品項目UI 建構子
        /// </summary>
        /// <param name="commodity">商品資料</param>
        /// <param name="page">該UI所在的頁面 (this)</param>
        public CommodityUI(Commodity commodity, Page page)
        {
            //UI Css掛載
            this.CssClass = "CommodityItem";
            //商品縮圖
            thumbnail.CssClass = "CommodityIcon";
            thumbnail.ImageUrl = commodity.thumbnail;
            thumbnailFrame.Controls.Add(thumbnail);
            //商品資訊
            contentFrame.CssClass = "CommodityContent";
            //商品名
            nameFrame.CssClass = "CommodityNameText";
            name.Text = commodity.name;
            nameFrame.Controls.Add(name);
            //商品價格
            priceContent.CssClass = "CommodityPriceBox";
            priceSymbol.CssClass = "CommodityPriceSymbol";
            priceSymbol.Text = "$";
            price.CssClass = "CommodityPriceText";
            price.Text = commodity.price.ToString("N0");
            priceContent.Controls.Add(priceSymbol);
            priceContent.Controls.Add(price);
            //購物車
            shoppingCart.CssClass = "ShoppingCart";
            shoppingCart_Icon.CssClass = "ShoppingCart_Icon";
            shoppingCart.Controls.Add(shoppingCart_Icon);
            shoppingCart.Click +=
                delegate (object sender1, EventArgs e1) {
                    VBT_ShoppingCart(
                        commodity.id,
                        page
                    );
                };
            //連接商品頁面
            string commodityLink = $"../commodity/Item.aspx?commodityId={commodity.id}";
            thumbnailFrame.Attributes.Add("href", commodityLink);
            nameFrame.Attributes.Add("href", commodityLink);
            //掛載至文字框架
            contentFrame.Controls.Add(nameFrame);
            contentFrame.Controls.Add(priceContent);
            //掛載至主UI
            this.Controls.Add(thumbnailFrame);
            this.Controls.Add(shoppingCart);
            this.Controls.Add(contentFrame);
        }

        /// <summary>
        /// 加入購物車 (點擊事件)
        /// </summary>
        /// <param name="commodityId">商品ID</param>
        /// <param name="page">該UI所在的頁面 (this)</param>
        protected void VBT_ShoppingCart(int commodityId, Page page)
        {
            //判斷是否登入
            if (page.Session["UserId"] == null)
            {
                page.Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='../buyer/Login.aspx';</script>");
                return;
            }

            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            DetailsView dv = new DetailsView();
            //調用DB
            DB.connectionReader(
                "joinShoppingCart.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.Int, page.Session["UserId"]),
                    new DB.Parameter("CommodityId", SqlDbType.Int, commodityId),
                    new DB.Parameter("CommodityNum", SqlDbType.Int, 1)
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
                    page.Response.Write("<script>alert('已加入購物車！')</script>");
                    break;
                case "-1":
                    page.Response.Write("<script>alert('已存在購物車中！')</script>");
                    break;
                default:
                    page.Response.Write("<script>alert('加入購物車失敗！')</script>");
                    break;
            }
        }
    }
}