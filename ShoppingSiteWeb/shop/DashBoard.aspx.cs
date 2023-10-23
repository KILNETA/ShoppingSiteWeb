using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingSiteWeb.shop
{

    /// <summary>
    /// 商店儀錶板頁面
    /// </summary>
    public partial class DashBoard : System.Web.UI.Page
    {
        /// <summary>
        /// 頁面加載
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {   //next load
                //檢驗用戶狀態
                if (Session["UserId"] == null)
                {
                    Response.Write("<script>alert('已登出！返回登入頁面！');window.location='~/buyer/Login.aspx';</script>");
                }
            }
            else
            {   //first load
                //已登入
                if (Session["UserId"] != null)
                {
                    // 加載商店資料
                    LoadShopData();
                }
                else
                {
                    Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='../buyer/Login.aspx';</script>");
                }
            }
        }

        /// <summary>
        /// 加載商店資料
        /// </summary>
        private void LoadShopData()
        {
            //調用DB 取得商店資料
            DB.connectionReader(
                "selectShopDataUseUserId.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.Int, Session["UserId"])
                },
                (SqlDataReader ts) => {
                    GV_ShopData.DataSource = ts;
                    GV_ShopData.DataBind();
                }
            );

            if (GV_ShopData.Rows.Count == 0)
                // 店鋪不存在
                Response.Write("<script>alert('尚未註冊商店！進入商店註冊頁面！');window.location='Register.aspx';</script>");
            else
                // 為按鈕添加前往店鋪的連結
                BT_GoShop.Attributes.Add("href", $"../shop/Shop.aspx?shopId={GV_ShopData.Rows[0].Cells[0].Text}");
        }

        /// <summary>
        /// 登出按鈕 事件
        /// </summary>
        protected void SignOutButton_Click(object sender, EventArgs e)
        {
            Session["UserId"] = null;
            Response.Write("<script>alert('成功登出！返回登入頁面！');window.location='../buyer/Login.aspx';</script>");
        }
    }
}