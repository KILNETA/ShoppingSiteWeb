using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace ShoppingSiteWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

            }
            else {
                showCommodityPage();
            }
        }

        private void showCommodityPage()
        {
            List<Panel> commodityPage = new List<Panel>();

            for (int i = 0 ; i < 10 ; i++)
            {
                commodityPage.Add(showCommodityList());
                commodityPage[i].CssClass = "CommodityList"; 
                Panel_CommodityPage.Controls.Add(commodityPage[i]);
            }
        }

        private Panel showCommodityList()
        {
            Panel commodityList = new Panel();

            for (int i = 0; i < 6; i++)
            {
                commodityList.Controls.Add(showCommodityItem());
            }

            return commodityList;
        }

        private Panel showCommodityItem()
        {
            Panel commodityItem = new Panel();
            commodityItem.CssClass = "CommodityItem";

            LinkButton commodityThumbnail_Box = new LinkButton();
            Image commodityThumbnail = new Image();
            Label commodityName = new Label();
            Label commodityPrice = new Label();
            LinkButton commodityShoppingCart = new LinkButton();
            Panel commodityShoppingCart_Icon = new Panel();

            commodityThumbnail.CssClass = "CommodityIcon";
            commodityThumbnail.ImageUrl = "https://memeprod.ap-south-1.linodeobjects.com/user-template/3e82d6cf0ed82a242e887d73455921d6.png";
            commodityThumbnail_Box.Controls.Add(commodityThumbnail);

            commodityName.Text = "好康的";
            commodityName.Font.Size = 12;
            commodityPrice.Text = "1,200";
            commodityPrice.Font.Size = 12;

            commodityShoppingCart.CssClass = "ShoppingCart";
            commodityShoppingCart_Icon.CssClass = "ShoppingCart_Icon";
            commodityShoppingCart.Controls.Add(commodityShoppingCart_Icon);

            commodityItem.Controls.Add(commodityThumbnail_Box);
            commodityItem.Controls.Add(commodityName);
            commodityItem.Controls.Add(commodityPrice);
            commodityItem.Controls.Add(commodityShoppingCart);

            return commodityItem;
        }
    }
}