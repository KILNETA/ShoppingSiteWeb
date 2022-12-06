using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
            }

        }

        protected void TB_CommodityName_TextChanged(object sender, EventArgs e)
        {
            //用戶名過長 >50
            if (TB_CommodityName.Text.Length > 50)
            {
                LB_ErrorMessage_CommodityName.Text = "商品名過長";
                return;
            }
            //用戶名過短 <4
            else if (TB_CommodityName.Text.Length < 4)
            {
                LB_ErrorMessage_CommodityName.Text = "商品名過短";
                return;
            }
        }

        protected void TB_CommodityPrice_TextChanged(object sender, EventArgs e)
        {
            //金額不可高於 > 9999
            if ( Int32.Parse(TB_CommodityPrice.Text) > 9999)
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
        }

        protected void TB_CommodityNum_TextChanged(object sender, EventArgs e)
        {
            //金額不可高於 > 99
            if (Int32.Parse(TB_CommodityNum.Text) > 99)
            {
                LB_ErrorMessage_CommodityNum.Text = "數量不可高於 99";
                return;
            }
            //金額不可低於 < 1
            else if (Int32.Parse(TB_CommodityNum.Text) < 1)
            {
                LB_ErrorMessage_CommodityNum.Text = "數量不可低於 1";
                return;
            }
        }

        /**
         * 檢測未填寫或有誤的警告訊息
         */
        private bool RegisterWarningMessageCheck()
        {

            bool complete = true;

            

            return complete;
        }

        protected void OnShelvesButton_Click(object sender, EventArgs e)
        {

        }
    }
}