<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="ShoppingSiteWeb.buyer.DashBoard" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-會員儀錶板</title>

    <style type="text/css">
        html,body,form {
            margin: 0;
            display: flex;
            flex-direction: column;
        }
        div.Body{
            margin: auto;
            width: 1200px;
            height: 100%;
            background-position: center;
            display: flex;
        }
        p,div{
            font-family: 微軟正黑體;
        }

        div.contentBody{
            background-image: url("picture/login_background.jpg");
            min-width: 1200px;
            background-size: cover;
            background-position: center;
        }

        .HeaderBackground-Style {
            background-color: #004469;
            width: 100%;
            min-width: 1200px;
            height: 90px;
            float: left;
        }

        .FooterBackground-Style {
            background-color: #c3c8c9;
            width: 100%;
            min-width: 1200px;
            height: 320px;
            float: left;
        }

        .TitelBox-Style {
            display: flex;
            align-items: center;
            height: 80px;
            padding-left: 5%;
        }

        .Titel-Style {
            color: #FFF4E1;
            font-family: 微軟正黑體;
            font-size: 32px;
            font-weight: bolder;
            text-decoration:none;
        }

        div.DashBoardBox {
            margin: auto;
            padding-top: 30px;
            padding-bottom: 30px;
            background-position: center;
        }
        div.DashBoardBoxContent {
            height: 600px;
            background-position: center;
            background: #ffffff;
            padding: 30px;
            box-shadow: 0 0 5px 3px #003b3b3b;
	        display: flex;
	        flex-direction: column;
        }
        div.InputBox{
            margin-bottom: 10px;
            flex-shrink: 0;
        }
        div.Button{
            text-align: center;
            flex-shrink: 0;
        }
        .SignOutButton{
            font-family: 微軟正黑體;
            font-size: 16px;
            width: 100%;
            height: 40px;
            cursor: pointer;
            background: #004469;
        }
        .SignOutButton:hover {
            background: #0d5982;
        }
        .SignOutButton:active{
            background: #3d474d;
        }
        .UserDataTable{
            min-height:100px;
            height:100%;
            width:100%;
        }
        .CenterButton{
            font-family: 微軟正黑體;
            font-size: 16px;
            width: 100%;
            height: 40px;
            cursor: pointer;
            background: #004469;
            margin: 0px 10px;
        }
        .CenterButton:hover {
            background: #0d5982;
        }
        .CenterButton:active{
            background: #3d474d;
        }

    </style>
</head>

<body>
    <form runat="server">
    <header class="HeaderBackground-Style">
        <div class="Body">
            <div class="TitelBox-Style">
                <asp:LinkButton CssClass="Titel-Style" ID="LB_Titel" runat="server" PostBackUrl="~/Default.aspx">
                    Einkaufen 愛康福
                </asp:LinkButton>
            </div>
        </div>
    </header>


    <div class="contentBody">
        <div class="Body">
            <div class="DashBoardBox">
                <div class="DashBoardBoxContent">
                    <div style="font-size: 24px; margin-bottom:30px">
                        您好! <asp:Label ID="userRealName" runat="server" Text=""></asp:Label>
                    </div>
                    <div style=" flex-grow: 1;">
                        <asp:GridView ID="GV_UserData" CssClass="UserDataTable" runat="server" Font-Size="16"></asp:GridView>
                        <div style="display: flex; align-items: flex-end; justify-content: center; margin: 10px 0px">
                            <asp:Button  CssClass="CenterButton" ID="Button4" runat="server" Width="160" Text="購物車" BorderStyle="None" ForeColor="White"/>
                            <asp:Button  CssClass="CenterButton" ID="Button5" runat="server" Width="160" Text="交易明細" BorderStyle="None" ForeColor="White"/>
                        </div>
                    </div>
                    <div style=" flex-grow: 1;">
                        <asp:GridView ID="GV_ShopData" CssClass="UserDataTable" runat="server" Font-Size="16" EmptyDataText="尚未註冊商鋪"></asp:GridView>
                        <div style="display: flex; align-items: flex-end; justify-content: center; margin: 10px 0px">
                            <asp:Button  CssClass="CenterButton" ID="BT_ShopRegister" runat="server" Width="160" Text="註冊商鋪" BorderStyle="None" ForeColor="White" PostBackUrl="~/shop/Register.aspx"/>
                            <asp:Button  CssClass="CenterButton" ID="BT_OnShelves" runat="server" Width="160" Text="上架新商品" BorderStyle="None" ForeColor="White" PostBackUrl="~/shop/OnShelves.aspx"/>
                            <asp:Button  CssClass="CenterButton" ID="BT_ShopDashBoard" runat="server" Width="160" Text="商鋪儀表板" BorderStyle="None" ForeColor="White" PostBackUrl="~/shop/DashBoard.aspx"/>
                        </div>
                    </div>

                    <div style="display: flex; align-items: flex-end; justify-content:center">
                        <asp:Button  CssClass="SignOutButton" ID="SignOutButton" runat="server" Width="160" Text="登出" BorderStyle="None" ForeColor="White" OnClick="SignOutButton_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <footer class="FooterBackground-Style">
        <div class="Body">
            <p>&copy; <%: DateTime.Now.Year %> - Einkaufen 愛康福</p>
        </div>
    </footer>
</form>
</body>
</html>
