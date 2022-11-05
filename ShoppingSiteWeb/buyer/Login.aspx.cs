using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingSiteWeb.buyer
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(TB_User.Text) || String.IsNullOrEmpty(TB_Password.Text))
            {
                if (String.IsNullOrEmpty(TB_User.Text))
                    ErrorLB_1.Text = "請輸入此欄";
                else
                    ErrorLB_1.Text = "　";

                if (String.IsNullOrEmpty(TB_Password.Text))
                    ErrorLB_2.Text = "請輸入此欄";
                else
                    ErrorLB_2.Text = "　";
            }
            else
            {

            }
        }
    }
}