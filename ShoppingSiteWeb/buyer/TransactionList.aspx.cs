using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace ShoppingSiteWeb.buyer
{
    public partial class TransactionList : System.Web.UI.Page
    {
        /* C->commodity S->shop */
        private static readonly String[] dataNames =  {
            "TTtransactionId",
            "TTtransactionDate"
        };

        // TRTcommodityTotal

        /* C->commodity */
        private static readonly String[] dataCommoditys =  {
            "CTcommodityName",
            "CTcommodityId",
            "CTcommodityPrice",
            "CTcommodityThumbnail",
            "TRTcommodityNum"
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Write("<script>alert('已登出！返回登入頁面！');window.location='Login.aspx';</script>");
                    return;
                }
                //驗證Token
                else if (Session["Token"] == null || TB_Token.Text != Session["Token"].ToString())
                {
                    Response.Write("<script>alert('頁面閒置過久，重新載入！');window.location='ShoppingCart.aspx';</script>");
                    return;
                }
            }
            else
            {
                if (Session["UserId"] != null)
                {
                    //創建Token許可證
                    String Token = Path.GetRandomFileName().Replace(".", "");
                    //用於判斷表單是否被認證(存儲用戶與server互動的數據) 
                    Session["Token"] = Token;
                    //在表單存入Token許可證
                    TB_Token.Text = Token;

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
                if (Int32.Parse(ViewState["transactionNum"].ToString()) != 0)
                {
                    showShoppingCartList();
                }
                else
                {
                    Label ShoppingCartIsEmpty = new Label();
                    ShoppingCartIsEmpty.CssClass = "SC_ShoppingCartIsEmpty";
                    ShoppingCartIsEmpty.Text = "暫無訂單紀錄！";
                    Panel_ShoppingCartBox.Controls.Add(ShoppingCartIsEmpty);
                }
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
                $"Select TT.transactionId,TT.transactionDate " +
                $"FROM transactionTable TT " +
                $"Where TT.userId = @userId " +
                $"ORDER BY TT.transactionDate desc, TT.transactionId desc";

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

            ViewState["transactionNum"] = gv.Rows.Count;

            saveRecommendCommoditys(gv);
        }

        private void saveRecommendCommoditys(GridView gv)
        {
            for (int cell = 0; cell < dataNames.Length; cell++)
            {
                for (int row = 0; row < Int32.Parse(ViewState["transactionNum"].ToString()); row++)
                {
                    ViewState[$"{dataNames[cell]}_{row}"] = gv.Rows[row].Cells[cell].Text;
                }
            }

            for (int row = 0; row < Int32.Parse(ViewState["transactionNum"].ToString()); row++)
            {
                selectCommoditysData(row);
            }
        }

        private void selectCommoditysData(int row_index)
        {

            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_RegisterUser.SelectParameters.Add("transactionId", ViewState[$"TTtransactionId_{row_index}"].ToString());

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"Select CT.commodityName,CT.commodityId,CT.commodityPrice,CT.commodityThumbnail,TRT.commodityNum " +
                $"FROM transaction_recordsTable TRT " +
                $"INNER JOIN commodityTable CT " +
                $"ON TRT.commodityId = CT.commodityId " +
                $"Where TRT.transactionId = @transactionId ";

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

            ViewState[$"TRTcommodityTotal_{row_index}"] = gv.Rows.Count;

            saveCommoditys(gv,row_index);
        }

        private void saveCommoditys(GridView gv,int row_index)
        {
            for (int cell = 0; cell < dataCommoditys.Length; cell++)
            {
                for (int row = 0; row < Int32.Parse(ViewState[$"TRTcommodityTotal_{row_index}"].ToString()); row++)
                {
                    ViewState[$"{dataCommoditys[cell]}_{row_index}_{row}"] = gv.Rows[row].Cells[cell].Text;
                }
            }
        }

        private void showShoppingCartList()
        {
            for (int i = 0; i < Int32.Parse(ViewState["transactionNum"].ToString()); i++)
            {
                Panel_ShoppingCartBox.Controls.Add(showShoppingCartItem(i));
            }
        }
        private Panel showShoppingCartItem(int index)
        {
            Panel transactionItem = new Panel();
            Panel transactionIdd_Box = new Panel();
            Label transactionId = new Label();
            Label transactionDate = new Label();
            Panel transactionCommodityList = new Panel();
            Panel transactionSubTotal_Box = new Panel();
            Label transactionSubTotal = new Label();

            transactionItem.CssClass = "TT_Item";

            Panel hr_1 = new Panel();
            Panel hr_2 = new Panel();
            Panel hr_3 = new Panel();

            String transactionDateText = ViewState[$"TTtransactionDate_{index}"].ToString().Remove(10, 1);
            transactionDateText = transactionDateText.Insert(10, "<br>");

            transactionIdd_Box.CssClass = "TT_TextId_Box";
            transactionId.CssClass = "TT_TextId";
            transactionId.Text = ViewState[$"TTtransactionId_{index}"].ToString();
            transactionIdd_Box.Controls.Add(transactionId);
            transactionDate.CssClass = "TT_TextDate";
            transactionDate.Text = transactionDateText;

            hr_1.CssClass = "TT_hr";
            hr_2.CssClass = "TT_hr";
            hr_3.CssClass = "TT_hr";

            long subTotal = 0;

            transactionCommodityList.CssClass = "CT_ListBox";
            for (int row = 0; row < Int32.Parse(ViewState[$"TRTcommodityTotal_{index}"].ToString()); row++)
            {
                transactionCommodityList.Controls.Add(showCommodityItem(index,row));
                if(row+1 != Int32.Parse(ViewState[$"TRTcommodityTotal_{index}"].ToString()))
                {
                    Panel hr = new Panel();
                    hr.CssClass = "CT_hr";
                    transactionCommodityList.Controls.Add(hr);
                }
                subTotal += Int32.Parse(ViewState[$"CTcommodityPrice_{index}_{row}"].ToString()) * Int32.Parse(ViewState[$"TRTcommodityNum_{index}_{row}"].ToString());
            }

            transactionSubTotal_Box.CssClass = "TT_SubTotal_Box";
            transactionSubTotal.CssClass = "TT_SubTotal";
            transactionSubTotal.Text = $"${subTotal.ToString("N0")}";

            transactionSubTotal_Box.Controls.Add(transactionSubTotal);


            transactionItem.Controls.Add(transactionIdd_Box);
            transactionItem.Controls.Add(hr_1);
            transactionItem.Controls.Add(transactionDate);
            transactionItem.Controls.Add(hr_2);
            transactionItem.Controls.Add(transactionCommodityList);
            transactionItem.Controls.Add(hr_3);
            transactionItem.Controls.Add(transactionSubTotal_Box);

            return transactionItem;
        }

        private Panel showCommodityItem(int index, int row)
        {
            Panel commodityItem = new Panel();
            LinkButton commodityLink = new LinkButton();
            Image commodityThumbnail = new Image();
            Label commodityName = new Label();
            Label commodityId = new Label();
            Panel hr = new Panel();
            Panel commodityPrice_Box = new Panel();
            Label commodityPrice = new Label();
            Label commodityNum = new Label();

            commodityItem.CssClass = "CT_Item";


            commodityLink.CssClass = "CT_LinkBox";
            commodityLink.PostBackUrl = $"~/commodity/Item.aspx?commodityId={ViewState[$"CTcommodityId_{index}_{row}"]}";
            commodityThumbnail.CssClass = "CT_Thumbnail";
            commodityThumbnail.ImageUrl = ViewState[$"CTcommodityThumbnail_{index}_{row}"].ToString();
            commodityName.CssClass = "CT_Name";
            commodityName.Text = ViewState[$"CTcommodityName_{index}_{row}"].ToString();
            commodityId.CssClass = "CT_Id";
            commodityId.Text = $"({ViewState[$"CTcommodityId_{index}_{row}"]})";

            commodityLink.Controls.Add(commodityThumbnail);
            commodityLink.Controls.Add(commodityName);
            commodityLink.Controls.Add(commodityId);

            hr.CssClass = "TT_hr";

            commodityPrice_Box.CssClass = "CT_Price_Box";
            commodityNum.CssClass = "CT_PriceNum";
            commodityNum.Text = ViewState[$"TRTcommodityNum_{index}_{row}"].ToString();
            commodityPrice.CssClass = "CT_Price";
            commodityPrice.Text = $"${Int32.Parse(ViewState[$"CTcommodityPrice_{index}_{row}"].ToString()).ToString("N0")}";

            commodityPrice_Box.Controls.Add(commodityNum);
            commodityPrice_Box.Controls.Add(commodityPrice);

            commodityItem.Controls.Add(commodityLink);
            commodityItem.Controls.Add(hr);
            commodityItem.Controls.Add(commodityPrice_Box);

            return commodityItem;
        }
    }
}