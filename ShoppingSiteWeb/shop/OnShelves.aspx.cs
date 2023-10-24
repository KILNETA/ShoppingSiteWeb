using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.shop
{
    /// <summary>
    /// 商品上架頁面
    /// </summary>
    public partial class OnShelves : System.Web.UI.Page
    {

        /// <summary>
        /// 頁面加載
        /// </summary>
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
            }
        }

        /// <summary>
        /// 確認是否已存在商店
        /// </summary>
        /// <returns>是否已存在商店</returns>
        private bool CheakHasShop()
        {
            /// <summary>
            /// 暫存資料表
            /// </summary>
            DetailsView dv = new DetailsView();

            //調用DB 確認是否已存在商店
            DB.connectionReader(
                "chackHasShop.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.NVarChar,  Session["UserId"].ToString())
                },
                (SqlDataReader ts) => {
                    dv.DataSource = ts;
                    dv.DataBind();
                }
            );

            //是否已存在商店
            if (dv.Rows[0].Cells[1].Text == "1")
                return true;
            else
                return false;
        }


        /// <summary>
        /// 按鈕 商品縮圖預覽 按下
        /// </summary>
        protected void CommodityThumbnailViewButton_Click(object sender, EventArgs e)
        {
            //圖片連結欄位是否為空
            if (TB_CommodityThumbnail.Text == String.Empty) 
            {
                LB_ErrorMessage_CommodityThumbnail.Text = "此欄不可為空";
            }
            else
            {
                //顯示商品縮圖於圖片控件
                CommodityThumbnailView.ImageUrl = TB_CommodityThumbnail.Text;
                LB_ErrorMessage_CommodityThumbnail.Text = "　";
            }
        }

        /// <summary>
        /// 輸入框 商品名稱 更改
        /// </summary>
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

        /// <summary>
        /// 輸入框 商品價格 更改
        /// </summary>
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
            //金額不可高於 > 99999
            else if ( Int32.Parse(TB_CommodityPrice.Text) > 99999)
            {
                LB_ErrorMessage_CommodityPrice.Text = "金額不可高於 99999";
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

        /// <summary>
        /// 輸入框 商品數量 更改
        /// </summary>
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

        /// <summary>
        /// 檢測未填寫或有誤的警告訊息
        /// </summary>
        /// <returns>填寫狀態</returns>
        private bool RegisterWarningMessageCheck()
        {
            // 填寫狀態
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
            //金額不可高於 > 99999
            else if (Int32.Parse(TB_CommodityPrice.Text) > 99999)
            {
                complete = false;
                LB_ErrorMessage_CommodityPrice.Text = "金額不可高於 99999";
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

        /// <summary>
        /// 按鈕 商品上架 按下
        /// </summary>
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

            /// <summary>
            /// 暫存資料表
            /// </summary>
            DetailsView dv = new DetailsView();

            //調用DB 註冊商店
            DB.connectionReader(
                "onShelvesCommodity.sql",
                new ArrayList {
                    new DB.Parameter("UserID",                  SqlDbType.NVarChar,  Session["UserId"].ToString()   ),
                    new DB.Parameter("TB_CommodityName",        SqlDbType.NVarChar,  TB_CommodityName.Text          ),
                    new DB.Parameter("TB_CommodityPrice",       SqlDbType.NVarChar,  TB_CommodityPrice.Text         ),
                    new DB.Parameter("TB_CommodityNum",         SqlDbType.NVarChar,  TB_CommodityNum.Text           ),
                    new DB.Parameter("TB_CommodityIntroduction",SqlDbType.NVarChar,  TB_CommodityIntroduction.Text  ),
                    new DB.Parameter("TB_CommodityThumbnail",   SqlDbType.NVarChar,  TB_CommodityThumbnail.Text     )
                },
                (SqlDataReader ts) => {
                    dv.DataSource = ts;
                    dv.DataBind();
                }
            );

            //清除此頁面所有暫存資料
            ViewState.Clear();
            //清除Token
            Session.Remove("Token");

            //判斷用戶帳號註冊成功與否
            if ( dv.Rows[0].Cells[1].Text == "1")
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "goBackJS",
                    "alert('商品上架成功！');" +
                    "window.location='DashBoard.aspx';",
                    true);
            }
            //會員註冊失敗，用戶名或信箱已被使用
            else if (dv.Rows[0].Cells[1].Text == "0")
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "goBackJS",
                    "alert('商品上架失敗！');" +
                    "window.location.replace(window.location.href);",
                    true);
            }
            //表單已失效，創建新註冊表單
            else
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "goBackJS",
                    "alert('表單已失效，創建新商品上架表單！');" +
                    "window.location.replace(window.location.href);",
                    true);
            }
        }
    }
}