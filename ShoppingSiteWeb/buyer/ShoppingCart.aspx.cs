using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.buyer
{
    public partial class ShoppingCart : System.Web.UI.Page
    {

        /* C->commodity S->shop */
        private static readonly String[] dataNames =  {
            "ACTcommodityId",
            "ACTcommodityNum",
            "CTcommodityName",
            "CTcommodityPrice",
            "CTcommodityIntroduction",
            "CTcommodityThumbnail",
            "CTcommodityNum"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Write("<script>alert('已登出！返回登入頁面！');window.location='Login.aspx';</script>");
                }
            }
            else
            {
                if (Session["UserId"] != null)
                {
                    ViewState["UserId"] = Session["UserId"];
                    LoadUserData();
                }
                else
                {
                    Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='Login.aspx';</script>");
                }
            }
            if (Session["UserId"] != null)
            {
                selectShoppingCartData();
                showShoppingCartList();
            }
        }

        private void LoadUserData()
        {
            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_RegisterUser.SelectParameters.Add("UserId", Session["UserId"].ToString());

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"SELECT " +
                    $"userRealName " +
                $"FROM userTable " +
                $"WHERE userId = @UserId";

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

            userRealName.Text = gv.Rows[0].Cells[1].Text;
        }

        private void selectShoppingCartData()
        {
            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_RegisterUser.SelectParameters.Add("UserId", Session["UserId"].ToString());

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"SELECT DISTINCT " +
                    $"ACT.commodityId, " +
                    $"ACT.commodityNum, " +
                    $"CT.commodityName, " +
                    $"CT.commodityPrice, " +
                    $"CT.commodityIntroduction, " +
                    $"CT.commodityThumbnail, " +
                    $"CT.commodityNum "+
                $"FROM shoppingCartTable ACT " +
                $"INNER JOIN commodityTable CT " +
                    $"ON ACT.commodityId = CT.commodityId " +
                $"WHERE ACT.userId = @UserId ";

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
                for (int row = 0; row < Int32.Parse(ViewState["commodityNum"].ToString()) ; row++)
                {
                    ViewState[$"{dataNames[cell]}_{row}"] = gv.Rows[row].Cells[cell].Text;
                }
            }
        }

        private void showShoppingCartList()
        {
            for (int i = 0; i < Int32.Parse(ViewState["commodityNum"].ToString()); i++)
            {
                Panel_ShoppingCartBox.Controls.Add(showShoppingCartItem(i));
            }
        }
        private Panel showShoppingCartItem(int index)
        {
            Panel commodityItem = new Panel();

            LinkButton commodityThumbnail_Box = new LinkButton();
            Image commodityThumbnail = new Image();
            Label commodityId = new Label();
            Panel hr_1 = new Panel();
            Panel hr_2 = new Panel();
            Panel commodityContext_Box = new Panel();
            LinkButton commodityName = new LinkButton();
            Label commodityPrice = new Label();
            Label commodityIntroduction = new Label();
            Panel commodityNumSubPrice_Box = new Panel();
            Panel commodityNum_Box = new Panel();
            TextBox commodityNum = new TextBox();
            Panel commoditySubPrice_Box = new Panel();
            Panel commoditySubPrice_Context = new Panel();
            Label commoditySubPriceTitle = new Label();
            Label commoditySubPrice = new Label();
            Panel commodityNum_Text = new Panel();
            Label commodityHasNum = new Label();
            Label commodityHasNumLack = new Label();
            LinkButton ShoppingCart_remove = new LinkButton();
            Panel ShoppingCart_remove_Icon = new Panel();

            String PriceNum = ViewState[$"CTcommodityPrice_{index}"].ToString();
            if (PriceNum.Length > 3)
                PriceNum = PriceNum.Insert(PriceNum.Length - 3, ",");

            String SubPriceNum = (Int32.Parse(ViewState[$"CTcommodityPrice_{index}"].ToString())* Int32.Parse(ViewState[$"ACTcommodityNum_{index}"].ToString())).ToString();
            if (SubPriceNum.Length > 6)
                SubPriceNum = SubPriceNum.Insert(SubPriceNum.Length - 6, ",");
            if (SubPriceNum.Length > 3)
                SubPriceNum = SubPriceNum.Insert(SubPriceNum.Length - 3, ",");

            commodityItem.CssClass = "SC_commodityItem";
            commodityThumbnail_Box.CssClass = "SC_Thumbnail_Box";
            commodityThumbnail.CssClass = "SC_Thumbnail";
            commodityThumbnail.ImageUrl = ViewState[$"CTcommodityThumbnail_{index}"].ToString();
            commodityId.CssClass = "SC_Id";
            commodityId.Text = $"商品編號：{ViewState[$"ACTcommodityId_{index}"]}";

            commodityThumbnail_Box.Controls.Add(commodityThumbnail);
            commodityThumbnail_Box.Controls.Add(commodityId);

            hr_1.CssClass = "SC_hr";
            hr_2.CssClass = "SC_hr";

            commodityContext_Box.CssClass = "SC_Context_Box";
            commodityName.CssClass = "SC_Name";
            commodityName.Text = ViewState[$"CTcommodityName_{index}"].ToString();
            commodityPrice.CssClass = "SC_Price";
            commodityPrice.Text = $"${PriceNum}";
            commodityIntroduction.CssClass = "SC_Introduction";
            commodityIntroduction.Text = ViewState[$"CTcommodityIntroduction_{index}"].ToString();


            commodityContext_Box.Controls.Add(commodityName);
            commodityContext_Box.Controls.Add(commodityPrice);
            commodityContext_Box.Controls.Add(commodityIntroduction);

            commodityNumSubPrice_Box.CssClass = "SC_NumSubPrice_Box";
            commoditySubPrice_Box.CssClass = "SC_SubPrice_Box";
            commoditySubPrice_Context.CssClass = "SC_SubPrice_Context";
            commoditySubPriceTitle.CssClass = "SC_SubPriceTitle";
            commoditySubPriceTitle.Text = "小計：";
            commoditySubPrice.CssClass = "SC_SubPrice";
            commoditySubPrice.Text = $"${SubPriceNum}";

            commoditySubPrice_Context.Controls.Add(commoditySubPriceTitle);
            commoditySubPrice_Context.Controls.Add(commoditySubPrice);
            commoditySubPrice_Box.Controls.Add(commoditySubPrice_Context);

            commodityNum_Box.CssClass = "SC_Num_Box";
            commodityNum.CssClass = "SC_Input";
            commodityNum.Text = ViewState[$"ACTcommodityNum_{index}"].ToString();
            commodityNum_Text.CssClass = "SC_Num_Text";
            commodityHasNum.CssClass = "SC_HasNum";
            commodityHasNumLack.CssClass = "SC_HasNum_Lack";
            commodityNum_Text.Controls.Add(commodityHasNum);
            commodityNum_Text.Controls.Add(commodityHasNumLack);
            commodityNum.TextChanged +=
                delegate (object sender1, EventArgs e1) {
                    VBT_ShoppingCartCommodityNumChanged(
                        new object(),
                        new EventArgs(),
                        ViewState[$"ACTcommodityId_{index}"].ToString(),
                        commodityNum
                    );
                };

            //缺貨
            if (Int32.Parse(ViewState[$"CTcommodityNum_{index}"].ToString()) < 1)
            {
                commodityHasNum.Text = "";
                commodityHasNumLack.Text = "缺貨中";
                commodityNum.Enabled = false;
                commoditySubPrice.CssClass = "SC_SubPrice_Lack";
            }
            else
            {
                commodityHasNum.Text = $"庫存 {ViewState[$"CTcommodityNum_{index}"]} 件";
                commodityHasNumLack.Text = "";
                commodityNum.Enabled = true;
                commoditySubPrice.CssClass = "SC_SubPrice";
            }
            commodityNum_Box.Controls.Add(commodityNum);
            commodityNum_Box.Controls.Add(commodityNum_Text);

            commodityNumSubPrice_Box.Controls.Add(commoditySubPrice_Box);
            commodityNumSubPrice_Box.Controls.Add(commodityNum_Box);

            ShoppingCart_remove.CssClass = "SC_Remove";
            ShoppingCart_remove_Icon.CssClass = "SC_RemoveIcon";
            ShoppingCart_remove.Controls.Add(ShoppingCart_remove_Icon);
            ShoppingCart_remove.Click +=
                delegate (object sender1, EventArgs e1) {
                    VBT_ShoppingCartRemove(
                        new object(),
                        new EventArgs(),
                        ViewState[$"ACTcommodityId_{index}"].ToString()
                    );
                };

            commodityThumbnail_Box.PostBackUrl = $"~/commodity/Item.aspx?commodityId={ViewState[$"ACTcommodityId_{index}"]}";
            commodityName.PostBackUrl = $"~/commodity/Item.aspx?commodityId={ViewState[$"ACTcommodityId_{index}"]}";

            commodityItem.Controls.Add(commodityThumbnail_Box);
            commodityItem.Controls.Add(hr_1);
            commodityItem.Controls.Add(commodityContext_Box);
            commodityItem.Controls.Add(hr_2);
            commodityItem.Controls.Add(commodityNumSubPrice_Box);
            commodityItem.Controls.Add(ShoppingCart_remove);

            return commodityItem;
        }

        protected void VBT_ShoppingCartCommodityNumChanged(object sender, EventArgs e, String commodityId, TextBox commodityNum)
        {

        }

        protected void VBT_ShoppingCartRemove(object sender, EventArgs e, String commodityId)
        {

            if (Session["UserId"] == null )
            {
                Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='../buyer/Login.aspx';</script>");
                return;
            }
            else if( ViewState["UserId"] != Session["UserId"] )
            {
                Response.Write("<script>alert('頁面內容與帳號不符，重新入頁面！');window.location='ShoppingCart.aspx';</script>\"");
                return;
            }

            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_RegisterUser.SelectParameters.Add("UserId", Session["UserId"].ToString());
            SqlDataSource_RegisterUser.SelectParameters.Add("CommodityId", commodityId);

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"DELETE FROM shoppingCartTable " +
                $"WHERE userId = @UserId " +
                    $"AND commodityId = @CommodityId " +

                $"SELECT @@ROWCOUNT ";

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

            if(gv.Rows[0].Cells[1].Text != "0")
                Response.Write("<script>alert('已從購物車移除！');window.location='ShoppingCart.aspx';</script>\"");
            else
                Response.Write("<script>alert('從購物車移除，失敗！');window.location='ShoppingCart.aspx';</script>\"");
        }

    }
}