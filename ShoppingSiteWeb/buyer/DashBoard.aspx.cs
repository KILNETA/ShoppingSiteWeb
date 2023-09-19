using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace ShoppingSiteWeb.buyer
{

    /// <summary>
    /// 個人儀錶板頁面
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
                    Response.Write("<script>alert('已登出！返回登入頁面！');window.location='Login.aspx';</script>");
                }
            }
            else
            {   //first load
                //已登入
                if (Session["UserId"] != null)
                {
                    LoadUserData();
                    LoadShopData();
                }
                else
                {
                    Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='Login.aspx';</script>");
                }
            }
        }

        /// <summary>
        /// 登出按鈕 事件
        /// </summary>
        protected void SignOutButton_Click(object sender, EventArgs e)
        {
            Session["UserId"] = null;
            Response.Write("<script>alert('成功登出！返回登入頁面！');window.location='Login.aspx';</script>");
        }

        /// <summary>
        /// 加載用戶資料
        /// </summary>
        private void LoadUserData()
        {
            //調用DB 取得用戶資料
            DB.connectionReader(
                "selectUserData.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.Int, Session["UserId"])
                },
                (SqlDataReader ts) => {
                    GV_UserData.DataSource = ts;
                    GV_UserData.DataBind();
                }
            );
            //輸出歡迎語的用戶名稱
            userRealName.Text = GV_UserData.Rows[0].Cells[1].Text;
        }

        /// <summary>
        /// 取得用戶商店資料
        /// </summary>
        private void LoadShopData()
        {
            //調用DB 取得用戶商店資料
            DB.connectionReader(
                "selectShopData.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.Int, Session["UserId"])
                },
                (SqlDataReader ts) => {
                    GV_ShopData.DataSource = ts;
                    GV_ShopData.DataBind();
                }
            );

            //確認用戶存在商店
            if (GV_ShopData.Rows.Count != 0)
            {
                //隱藏註冊商店按鈕
                BT_ShopRegister.Visible = false;
            }
            else
            {
                //隱藏商店功能按鈕
                BT_OnShelves.Visible = false;
                BT_ShopDashBoard.Visible = false;
            }
        }
    }
}