using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Transaction;

namespace ShoppingSiteWeb.buyer
{

    /// <summary>
    /// 訂單紀錄頁面
    /// </summary>
    public partial class TransactionList : System.Web.UI.Page
    {
        /// <summary>
        /// 訂單紀錄資料
        /// </summary>
        ArrayList Transactions = new ArrayList();

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
                    ScriptManager.RegisterStartupScript(
                        this, GetType(),
                        "goBackJS",
                        "alert('尚未登入！進入登入頁面！');" +
                        "window.location.replace(window.location.href);",
                        true);
                }
                else if (ViewState["UserId"].ToString() != Session["UserId"].ToString())
                {
                    ScriptManager.RegisterStartupScript(
                        this, GetType(),
                        "goBackJS",
                        "alert('頁面內容與帳號不符，重新入頁面！');" +
                        "window.location.replace(window.location.href);",
                        true);
                }
                //驗證Token
                else if (Session["Token"] == null || TB_Token.Text != Session["Token"].ToString())
                {
                    ScriptManager.RegisterStartupScript(
                        this, GetType(),
                        "goBackJS",
                        "alert('頁面閒置過久，重新載入！');" +
                        "window.location.replace(window.location.href);",
                        true);
                }
            }
            else
            {   //first load
                //已登入
                if (Session["UserId"] != null)
                {
                    //創建Token許可證
                    String Token = Path.GetRandomFileName().Replace(".", "");
                    //用於判斷表單是否被認證(存儲用戶與server互動的數據) 
                    Session["Token"] = Token;
                    //在表單存入Token許可證
                    TB_Token.Text = Token;
                    //紀錄User (驗證身分使用)
                    ViewState["UserId"] = Session["UserId"].ToString();

                    LoadUserData();
                    selectTransactionListData();
                }
                else
                {
                    Response.Write("<script>alert('尚未登入！進入登入頁面！');window.location='Login.aspx';</script>");
                    return;
                }
            }
            if (Session["UserId"] != null)
            {
                if (Transactions.Count != 0)
                {   //訂單紀錄不為空
                    initTransactionList();
                }
                else
                {   //訂單紀錄為空
                    Label ShoppingCartIsEmpty = new Label();
                    ShoppingCartIsEmpty.CssClass = "SC_ShoppingCartIsEmpty";
                    ShoppingCartIsEmpty.Text = "暫無訂單紀錄！";
                    Panel_ShoppingCartBox.Controls.Add(ShoppingCartIsEmpty);
                }
            }
        }

        /// <summary>
        /// 載入用戶資料
        /// </summary>
        private void LoadUserData()
        {
            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            DetailsView userTable = new DetailsView();

            //調用DB 取得用戶名稱
            DB.connectionReader(
                "selectUserName.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.Int, Session["UserId"])
                },
                (SqlDataReader ts) => {
                    userTable.DataSource = ts;
                    userTable.DataBind();
                }
            );
            //輸出歡迎語的用戶名稱
            userRealName.Text = userTable.Rows[0].Cells[1].Text;
        }

        /// <summary>
        /// 取出訂單紀錄資料
        /// </summary>
        private void selectTransactionListData()
        {
            /// <summary>
            /// SQL Server 數據暫存
            /// </summary>
            GridView gv = new GridView();

            //調用DB 取得交易紀錄
            DB.connectionReader(
                "selectTransactionList.sql",
                new ArrayList {
                    new DB.Parameter("UserId", SqlDbType.Int, Session["UserId"])
                },
                (SqlDataReader ts) => {
                    gv.DataSource = ts;
                    gv.DataBind();
                }
            );

            //規一化訂單清單
            normalizationTransactionList(gv);
        }

        /// <summary>
        /// 規一化訂單清單資料
        /// </summary>
        /// <param name="gv">訂單清單資料</param>
        private void normalizationTransactionList(GridView gv)
        {
            for (int index = 0; index < gv.Rows.Count;)
            {
                ///單筆訂單
                var record = new TransactionRecord(
                    gv.Rows[index].Cells[0].Text,
                    gv.Rows[index].Cells[1].Text);
                while (
                    index < gv.Rows.Count && 
                    record.TT_Id == gv.Rows[index].Cells[0].Text
                ){
                    //單筆訂單 多筆商品
                    record.Commoditys.Add(new TransactionCommodity(
                        gv.Rows[index].Cells[2].Text,
                        gv.Rows[index].Cells[3].Text,
                        gv.Rows[index].Cells[4].Text,
                        gv.Rows[index].Cells[5].Text,
                        gv.Rows[index].Cells[6].Text
                    ));
                    ++index;
                }
                //規一完成後儲存
                Transactions.Add(record);
            }
        }

        /// <summary>
        /// 初始化交易清單UI
        /// </summary>
        private void initTransactionList()
        {
            foreach (TransactionRecord item in Transactions)
            {
                Panel_ShoppingCartBox.Controls.Add(
                    new TransactionRecordUI(item)
                );
            }
        }
    }
}