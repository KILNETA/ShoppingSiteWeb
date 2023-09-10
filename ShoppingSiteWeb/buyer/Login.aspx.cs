using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.buyer
{
    /// <summary>
    /// 登入頁面
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {

        /// <summary>
        /// 頁面加載
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    Response.Write("<script>alert('已登入！進入個人儀錶板！');window.location='DashBoard.aspx';</script>");
                }
            }
            else
            {
                if (Session["UserId"] != null)
                {
                    Response.Redirect("DashBoard.aspx");
                }
            }
        }

        /// <summary>
        /// 登入按鈕 事件
        /// </summary>
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            //如果帳號、密碼欄位不為空
            if(String.IsNullOrEmpty(TB_User.Text) || String.IsNullOrEmpty(TB_Password.Text))
            {
                //啟用js判斷式 (未填入資料的欄位 亮起提醒)
                //帳號、用戶名欄位
                ScriptManager.RegisterStartupScript(
                    Page, GetType(),
                    "inputIsEmpty_1", 
                    "inputBoxIsNullOrEmpty(TB_User, ErrorLB_1);", 
                    true);
                //密碼欄位
                ScriptManager.RegisterStartupScript(
                    Page, GetType(),
                    "inputIsEmpty_2",
                    "inputBoxIsNullOrEmpty(TB_Password, ErrorLB_2);", 
                    true);
            }
            //已填入帳密 ()
            else
            {
                /// <summary>
                /// SQL Server 數據暫存
                /// </summary>
                DetailsView dv = new DetailsView();

                //調用DB 隨機取出商品 作為推薦
                DB.connectionReader(
                    "login.sql",
                    new ArrayList {
                    new DB.Parameter("SwitchField", SqlDbType.NVarChar,
                        (TB_User.Text.ToLower().Contains("@".ToLower())) ?
                        "userEMail" :
                        "userName"
                    ),
                    new DB.Parameter("TB_User", SqlDbType.NVarChar, TB_User.Text),
                    new DB.Parameter("TB_Password", SqlDbType.NVarChar, TB_Password.Text)
                    },
                    (SqlDataReader ts) => {
                        dv.DataSource = ts;
                        dv.DataBind();
                    }
                );
                
                //如果回傳數為1 (為登入成功)
                if(1 == dv.DataItemCount)
                {
                    ErrorLB_1.Text = "　";
                    Session["UserId"] = dv.Rows[0].Cells[1].Text;
                    Response.Redirect("DashBoard.aspx");
                }
                //否則為登入失敗
                else
                {
                    ErrorLB_1.Text = "登入失敗，帳號或密碼有誤！！";
                }
            }
        }
    }
}