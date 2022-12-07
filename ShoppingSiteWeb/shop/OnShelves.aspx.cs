using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.shop
{
    public partial class OnShelves : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //使網頁不被塊取記憶體存取 (但重整頁面仍能讀取 仍需搭配Token判斷表單是否被認證)
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();

            //網頁PostBack
            if (IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='../buyer/Login.aspx';</script>");
                }
                //判斷是否已有商店
                else if (!CheakHasShop())
                {
                    Response.Write("<script>alert('尚未註冊商店！進入商店註冊頁面！');window.location='Register.aspx';</script>");
                }

                //判斷Token是否過期 
                if (Session["Token"] == null || TB_Token.Text != Session["Token"].ToString())
                    Response.Write("<script>alert('表單已失效，創建新商品上架表單！');window.location='OnShelves.aspx';</script>");
            }
            //首次加載
            else
            {
                if (Session["UserId"] == null)
                {
                    Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='../buyer/Login.aspx';</script>");
                }
                //判斷是否已有商店
                else if (!CheakHasShop())
                {
                    Response.Write("<script>alert('尚未註冊商店！進入商店註冊頁面！');window.location='Register.aspx';</script>");
                }

                //創建Token許可證
                String Token = Path.GetRandomFileName().Replace(".", "");
                //用於判斷表單是否被認證(存儲用戶與server互動的數據) 
                Session["Token"] = Token;

                //在表單存入Token許可證
                TB_Token.Text = Token;

                //初始化各項 (只存儲於該頁面的數據) 用於判斷表單是否填寫正確
            }
        }

        private bool CheakHasShop()
        {
            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_CheckUserName = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_CheckUserName.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            //新增SQL參數
            SqlDataSource_CheckUserName.SelectParameters.Add("UserId", Session["UserId"].ToString());

            //SQL指令 ==
            SqlDataSource_CheckUserName.SelectCommand =
                $"Select shopId " +
                $"FROM user_shopTable " +
                $"WHERE ( userId = @UserId )";

            //執行SQL指令 .select() ==
            SqlDataSource_CheckUserName.DataSourceMode = SqlDataSourceMode.DataSet;
            //取得查找資料
            DataView dv = (DataView)SqlDataSource_CheckUserName.Select(new DataSourceSelectArguments());
            DetailsView dvTable = new DetailsView();

            dvTable.DataSource = dv;
            dvTable.DataBind();
            //SqlDataSource元件釋放資源
            SqlDataSource_CheckUserName.Dispose();

            if (1 == dvTable.DataItemCount)
                return true;
            else
                return false;
        }

        protected void CommodityThumbnailViewButton_Click(object sender, EventArgs e)
        {
            if (TB_CommodityThumbnail.Text == String.Empty) 
            {
                LB_ErrorMessage_CommodityThumbnail.Text = "此欄不可為空";
            }
            else
            {
                CommodityThumbnailView.ImageUrl = TB_CommodityThumbnail.Text;
                LB_ErrorMessage_CommodityThumbnail.Text = "　";
            }

        }

        protected void TB_CommodityName_TextChanged(object sender, EventArgs e)
        {
            if (TB_CommodityName.Text == String.Empty)
            {
                LB_ErrorMessage_CommodityName.Text = "此欄不可為空";
                return;
            }
            //商品名過長 >50
            else if (TB_CommodityName.Text.Length > 50)
            {
                LB_ErrorMessage_CommodityName.Text = "商品名過長";
                return;
            }
            //商品名過短 <4
            else if (TB_CommodityName.Text.Length < 4)
            {
                LB_ErrorMessage_CommodityName.Text = "商品名過短";
                return;
            }
            else
            {
                LB_ErrorMessage_CommodityName.Text = "　";
            }
        }

        protected void TB_CommodityPrice_TextChanged(object sender, EventArgs e)
        {
            if (TB_CommodityPrice.Text == String.Empty)
            {
                LB_ErrorMessage_CommodityPrice.Text = "此欄不可為空";
                return;
            }
            else if (!new Regex("^[0-9]*$").IsMatch(TB_CommodityPrice.Text))
            {
                LB_ErrorMessage_CommodityPrice.Text = "請輸入數字";
                return;
            }
            //金額不可高於 > 9999
            else if ( Int32.Parse(TB_CommodityPrice.Text) > 9999)
            {
                LB_ErrorMessage_CommodityPrice.Text = "金額不可高於 9999";
                return;
            }
            //金額不可低於 < 1
            else if (Int32.Parse(TB_CommodityPrice.Text) < 1)
            {
                LB_ErrorMessage_CommodityPrice.Text = "金額不可低於 1";
                return;
            }
            else
            {
                LB_ErrorMessage_CommodityPrice.Text = "　";
            }
        }

        protected void TB_CommodityNum_TextChanged(object sender, EventArgs e)
        {
            if (TB_CommodityNum.Text == String.Empty)
            {
                LB_ErrorMessage_CommodityNum.Text = "此欄不可為空";
                return;
            }
            else if (!new Regex("^[0-9]*$").IsMatch(TB_CommodityNum.Text))
            {
                LB_ErrorMessage_CommodityNum.Text = "請輸入數字";
                return;
            }
            //數量不可高於 > 99
            else if (Int32.Parse(TB_CommodityNum.Text) > 99)
            {
                LB_ErrorMessage_CommodityNum.Text = "數量不可高於 99";
                return;
            }
            //數量不可低於 < 1
            else if (Int32.Parse(TB_CommodityNum.Text) < 1)
            {
                LB_ErrorMessage_CommodityNum.Text = "數量不可低於 1";
                return;
            }
            else
            {
                LB_ErrorMessage_CommodityNum.Text = "　";
            }
        }

        /**
         * 檢測未填寫或有誤的警告訊息
         */
        private bool RegisterWarningMessageCheck()
        {
            bool complete = true;

            if (TB_CommodityName.Text == String.Empty)
            {
                complete = false;
                LB_ErrorMessage_CommodityName.Text = "此欄不可為空";
            }
            //商品名過長 >50
            else if (TB_CommodityName.Text.Length > 50)
            {
                complete = false;
                LB_ErrorMessage_CommodityName.Text = "商品名過長";
            }
            //商品名過短 <4
            else if (TB_CommodityName.Text.Length < 4)
            {
                complete = false;
                LB_ErrorMessage_CommodityName.Text = "商品名過短";
            }
            else
            {
                LB_ErrorMessage_CommodityName.Text = "　";
            }

            if (TB_CommodityPrice.Text == String.Empty)
            {
                complete = false;
                LB_ErrorMessage_CommodityPrice.Text = "此欄不可為空";
            }
            else if (!new Regex("^[0-9]*$").IsMatch(TB_CommodityPrice.Text))
            {
                complete = false;
                LB_ErrorMessage_CommodityPrice.Text = "請輸入數字";
            }
            //金額不可高於 > 9999
            else if (Int32.Parse(TB_CommodityPrice.Text) > 9999)
            {
                complete = false;
                LB_ErrorMessage_CommodityPrice.Text = "金額不可高於 9999";
            }
            //金額不可低於 < 1
            else if (Int32.Parse(TB_CommodityPrice.Text) < 1)
            {
                complete = false;
                LB_ErrorMessage_CommodityPrice.Text = "金額不可低於 1";
            }
            else
            {
                LB_ErrorMessage_CommodityPrice.Text = "　";
            }

            if (TB_CommodityNum.Text == String.Empty)
            {
                complete = false;
                LB_ErrorMessage_CommodityNum.Text = "此欄不可為空";
            }
            else if (!new Regex("^[0-9]*$").IsMatch(TB_CommodityNum.Text))
            {
                complete = false;
                LB_ErrorMessage_CommodityNum.Text = "請輸入數字";
            }
            //數量不可高於 > 99
            else if (Int32.Parse(TB_CommodityNum.Text) > 99)
            {
                complete = false;
                LB_ErrorMessage_CommodityNum.Text = "數量不可高於 99";
            }
            //數量不可低於 < 1
            else if (Int32.Parse(TB_CommodityNum.Text) < 1)
            {
                complete = false;
                LB_ErrorMessage_CommodityNum.Text = "數量不可低於 1";
            }
            else
            {
                LB_ErrorMessage_CommodityNum.Text = "　";
            }

            if (TB_CommodityIntroduction.Text == String.Empty)
            {
                complete = false;
                LB_ErrorMessage_CommodityIntroduction.Text = "此欄不可為空";
            }
            else
            {
                LB_ErrorMessage_CommodityIntroduction.Text = "　";
            }

            if (TB_CommodityThumbnail.Text == String.Empty)
            {
                complete = false;
                LB_ErrorMessage_CommodityThumbnail.Text = "此欄不可為空";
            }
            else
            {
                LB_ErrorMessage_CommodityThumbnail.Text = "　";
            }

            return complete;
        }

        protected void OnShelvesButton_Click(object sender, EventArgs e)
        {
            //驗證Token
            if (Session["Token"] == null || TB_Token.Text != Session["Token"].ToString())
            {
                Response.Write("<script>alert('表單已失效，創建新註冊表單！');window.location='Register.aspx';</script>");
                return;
            }

            //確認表單是否填寫正確
            if (!RegisterWarningMessageCheck())
                return;

            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_OnShelves = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_OnShelves.ConnectionString =
                        "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_OnShelves.SelectParameters.Add("UserID", Session["UserId"].ToString());

            SqlDataSource_OnShelves.SelectParameters.Add("TB_CommodityName", TB_CommodityName.Text);
            SqlDataSource_OnShelves.SelectParameters.Add("TB_CommodityPrice", TB_CommodityPrice.Text);
            SqlDataSource_OnShelves.SelectParameters.Add("TB_CommodityNum", TB_CommodityNum.Text);
            SqlDataSource_OnShelves.SelectParameters.Add("TB_CommodityIntroduction", TB_CommodityIntroduction.Text);
            SqlDataSource_OnShelves.SelectParameters.Add("TB_CommodityThumbnail", TB_CommodityThumbnail.Text);

            // SQL指令 ==
            SqlDataSource_OnShelves.SelectCommand =
                $"DECLARE @shopId INT " +
                $"SELECT @shopId = user_shopTable.shopId " +
                $"FROM shopTable " +
                $"INNER JOIN user_shopTable " +
                $"ON shopTable.shopId = user_shopTable.shopId " +
                $"WHERE user_shopTable.userId = @UserID " +

                $"INSERT INTO commodityTable([commodityName],[commodityPrice],[commodityNum],[commodityIntroduction],[commodityThumbnail]) " +
                $"VALUES( " +
                    $"@TB_CommodityName, " +
                    $"@TB_CommodityPrice, " +
                    $"@TB_CommodityNum, " +
                    $"@TB_CommodityIntroduction, " +
                    $"@TB_CommodityThumbnail " +
                $") " +
                $"DECLARE @commodityId INT " +
                $"SELECT @commodityId = ISNULL(successful.commodityId, 0) from(SELECT SCOPE_IDENTITY() AS commodityId) successful " +

                $"IF (@commodityId != 0) " +
                    $"BEGIN " +
                        $"INSERT INTO shop_commodityTable([commodityId], [shopId]) " +
                        $"SELECT @commodityId, @shopId " +
                        $"Where Not Exists( " +
                            $"Select shop_commodityTable.commodityId " +
                            $"From shop_commodityTable " +
                            $"Where commodityId = @commodityId " +
                        $") " +
                    $"END " +
                $"SELECT @@ROWCOUNT ";

            // 執行SQL指令 .select() ==
            SqlDataSource_OnShelves.DataSourceMode = SqlDataSourceMode.DataSet;
            //取得查找資料
            DataView dv = (DataView)SqlDataSource_OnShelves.Select(new DataSourceSelectArguments());
            DetailsView dvTable = new DetailsView();
            //資料匯入表格
            dvTable.DataSource = dv;
            //更新表格
            dvTable.DataBind();

            SqlDataSource_OnShelves.Dispose();

            //清除此頁面所有暫存資料
            ViewState.Clear();
            //清除Token
            Session.Remove("Token");

            //判斷用戶帳號註冊成功與否
            if (1 == dvTable.DataItemCount
                && dvTable.Rows[0].Cells[1].Text != "0")
            {
                Response.Write("<script>alert('商品上架成功！');window.location='DashBoard.aspx';</script>");
            }
            //會員註冊失敗，用戶名或信箱已被使用
            else if (dvTable.Rows[0].Cells[1].Text == "0")
            {
                Response.Write("<script>alert('商品上架失敗！');window.location='OnShelves.aspx';</script>");
            }
            //表單已失效，創建新註冊表單
            else
            {
                Session["UserId"] = null;
                Response.Write("<script>alert('表單已失效，創建新商品上架表單！');window.location='OnShelves.aspx';</script>");
            }
        }
    }
}