using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.commodity
{
    public partial class Item : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

            }
            else
            {
                LoadCommodityData();
            }
        }

        private void LoadCommodityData()
        {
            //新建SqlDataSource元件
            SqlDataSource SqlDataSource_RegisterUser = new SqlDataSource();

            //連結資料庫的連接字串 ConnectionString
            SqlDataSource_RegisterUser.ConnectionString =
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database_Main.mdf;Integrated Security=True";

            SqlDataSource_RegisterUser.SelectParameters.Add("CommodityId", Context.Request.QueryString["commodityId"].ToString() );

            //SQL指令
            SqlDataSource_RegisterUser.SelectCommand =
                $"SELECT " +
                    $"commodityId, " +
                    $"commodityName, " +
                    $"commodityPrice, " +
                    $"commodityNum, " +
                    $"commodityThumbnail, " +
                    $"commodityIntroduction " +
                $"FROM commodityTable " +
                $"WHERE commodityId = @CommodityId ";

            //執行SQL指令 .select() ==
            SqlDataSource_RegisterUser.DataSourceMode = SqlDataSourceMode.DataSet;
            //取得查找資料
            DataView dv = (DataView)SqlDataSource_RegisterUser.Select(new DataSourceSelectArguments());
            GridView gv = new GridView();
            //資料匯入表格
            GV_CommodityData.DataSource = dv;
            //更新表格
            GV_CommodityData.DataBind();
            //SqlDataSource元件釋放資源
            SqlDataSource_RegisterUser.Dispose();
        }
    }
}