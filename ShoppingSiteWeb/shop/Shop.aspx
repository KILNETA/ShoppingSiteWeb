<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="ShoppingSiteWeb.shop.Shop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福</title>

    <style type="text/css">
        html,body,form {
            margin: 0;
            display: flex;
            flex-direction: column;
            font-size:0px;
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
            min-width: 1200px;
            background-size: cover;
            background-position: center;
        }
        .HeaderBackground-Style {
            background-color: #004469;
            width: 100%;
            min-width: 1200px;
            height: 120px;
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
            padding: 5px 0px;
            display: flex;
            flex-grow: 1;
        }
        .TitelBox{
            display: inline-flex;
	        flex-direction: column;
            width: 100%;
        }
        .TitelMenu-Style{
            display: flex;
	        flex-direction: row;
        }
        .Titel-Style {
            display: flex;
            flex-direction: row;
            align-items: center;
            justify-content: center;
        }
        .Titel{
            color: #FFF4E1;
            font-family: 微軟正黑體;
            font-size: 32px;
            font-weight: bolder;
            margin: auto 0px;
            flex:1;
            text-decoration:none;
        }
        .menuWelcome{
            color:#D0D0D0;
            font-size:14px;
        }
        .generalLink{
            color:#FFFFFF;
            text-decoration:none;
            font-size:14px;
        }
        .generalLink:active{
            color:#E0E0E0;
            text-decoration:none;
        }
        .TitelMenuseparate{
            font-size: 13px; 
            padding:0px 5px; 
            color:#D0D0D0;
        }
        div.InputBox{
            background-color:#FFFFFF;
            border: 1px solid #FFFFFF;
            border-radius: 3px;
            width: 100%;
            display: flex;
            flex-direction: row;
        }
        .TextBox{
            padding: 15px 20px;
            outline: none;
            flex-grow: 1;
            box-sizing: border-box; 
            font-size: 14px;
        }
        .LoginButton{
            font-family: 微軟正黑體;
            font-size: 16px;
            width: 100%;
            height: 40px;
            cursor: pointer;
            background: #004469;
        }
        .LoginButton:hover {
            background: #0d5982;
        }
        .LoginButton:active{
            background: #3d474d;
        }
        .otherLoginButtonContent {
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .otherSearchButton{
            width: 10%;
            background: #004469;
            border-radius: 5px;
            margin: 2px;
            color:#004469;
            text-decoration:none;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .otherSearchButton:hover {
            background: #004469;
        }
        .otherSearchButton:active{
            background: #f8f8f8;
            color:#004469;
        }
        .SearchDropDownList{
            outline: none;
            margin : 10px 0px;
            padding: 0px 5px;
        }

        .otherLoginButton{
            width: 10%;
            background: #FFFFFF;
            border-radius: 5px;
            margin: 2px 0px 2px 25px;
            color:#004469;
            text-decoration:none;
        }
        .otherLoginButton:hover {
            background: #FFFFFF;
        }
        .otherLoginButton:active{
            background: #f8f8f8;
            color:#004469;
        }

        .CommodityPage{
            width: 100%;
            display: flex;
            flex-direction: column;
        }
        .CommodityList{
            width: 100%;
            display: flex;
            flex-direction: row;
            justify-content: space-between;
        }
        .CommodityItem{
            width: 193px;
            height: 320px;
            margin: 10px 0px;
            display: flex;
            flex-direction: column;
            overflow: hidden;

            background: #ffffff;
            border-radius: 5px;
            box-shadow: 0 0 0 1px #00000010;
            border: 1px solid #E0E0E0;
            position: relative;
        }
        .CommodityItem_none{
            width: 193px;
            height: 320px;
            margin: 10px 0px;
            background: #FFFFFF00;
        }
        .CommodityIcon{
            width: 193px;
            height: 193px;
            object-fit: scale-down;
        }
        .CommodityContent{
            flex: 1;
            padding: 12px 12px 8px 12px;
            display: flex;
            flex-direction: column;
        }
        .CommodityNameText{
            font-size: 16px;
            letter-spacing: .6px;
            font-family: 微軟正黑體;
            color: #444444;
            text-decoration:none;
            
            height: 44px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 2;
            overflow: hidden;
        }
        .CommodityPriceBox{
            margin-top: 5px;
        }
        .CommodityPriceSymbol{
            font-size: 14px;
            font-family: 微軟正黑體;
            color: #ff5d0e;
            font-weight: bolder;
            margin-right: 4px;
        }
        .CommodityPriceText{
            font-size: 18px;
            font-family: 微軟正黑體;
            color: #ff5d0e;
            font-weight: bolder;
        }
        .ShoppingCart{
            width: 40px;
            height: 40px;
            background: #F0F0F0;
            border-radius: 100px;
            box-shadow: 0 0 2px 1px #00000020;
            position: absolute;
            bottom: 10px;
            right: 10px;
            cursor: pointer;
        }
        .ShoppingCart_Icon{
            width: 100%;
            height: 100%;
            background: #495e69;
            -webkit-mask: url('picture/shoppingCart.svg') no-repeat center;
            mask: url('picture/shoppingCart.svg') no-repeat center;
        }

        .shopDataContext{
            padding: 20px 20px;
            height:250px; 
            width:1000px; 
            margin:25px auto; 
            display:flex; 
            flex-direction:column; 
            position: relative; 
            overflow: hidden;
            background: #FFFFFFE0;
            border-radius: 6px;
            box-shadow: 0 0 5px 3px #00000030;
        }
        .shopBuyText{
            font-size: 22px;
            color: #444444;
            letter-spacing: .7px;
            padding-bottom: 10px;
        }
        .shopBuyName{
            font-size: 28px;
            color: #004469;
            font-weight: bolder;
            letter-spacing: .7px;
        }
        .WelcomeText{
            font-size: 22px;
            color: #444444;
        }

        .shopContentBody {
            background-image: url("picture/login_background.jpg");
            background-size: cover;
            background-position: center;
        }

        .commodityAllTitle{
            margin: 10px 0px;
            font-size: 24px;
            color: #444444;
            font-weight: bolder;
        }
    </style>
</head>

<body>
    <form runat="server">
    <header class="HeaderBackground-Style">
        <div class="Body">
            <div class="TitelBox-Style">
                <div class="TitelBox">
                    <div class="TitelMenu-Style">
                        <div class="TitelMenu-Style">
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton5" runat="server">客服中心</asp:LinkButton>
                            <div class="TitelMenuseparate">|</div>
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton1" runat="server">賣家中心</asp:LinkButton>
                            <div class="TitelMenuseparate">|</div>
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton2" runat="server">幫助中心</asp:LinkButton>
                        </div>
                        <div style="flex-grow: 1;"></div>
                        <asp:Panel ID="Panel_TitelMenuLogin" runat="server" CssClass="TitelMenu-Style"></asp:Panel>
                    </div>
                    <div class="Titel-Style" style="flex-grow: 1">
                        <asp:LinkButton CssClass="Titel" ID="LB_Titel" runat="server" PostBackUrl="~/Default.aspx">
                            Einkaufen 愛康福
                        </asp:LinkButton>
                        <div class="Titel-Style" style="width: 640px">
                            <div class="InputBox">
                                <asp:TextBox CssClass="TextBox" ID="TB_Password" placeholder="來愛康福購好物～" runat="server" BorderWidth="0px"/>
                                <asp:DropDownList ID="DDL_BirthdayDay" CssClass="SearchDropDownList" runat="server" BorderWidth="0px">
                                    <asp:ListItem>查商品</asp:ListItem>
                                    <asp:ListItem>查商店</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinkButton CssClass="otherSearchButton" ID="LinkButton7" runat="server">
                                    <img src="picture/Inquire.png" width="20" style="display: inline-block; margin: 0 10px;"/>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="Titel-Style" style="flex: 1; height:100%; align-items: center; justify-content: left;">
                            <asp:LinkButton CssClass="otherLoginButton" Width="60" ID="LinkButton9" runat="server" PostBackUrl="~/shop/OnShelves.aspx">
                                <div class="otherLoginButtonContent">
                                    <img src="picture/shoppingBag.png" width="30" style="display: inline-block; margin: 0 10px;"/>
                                    <div style="display: inline-block; font-size:14px">賣東西</div>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="otherLoginButton" Width="60" ID="LinkButton6" runat="server" PostBackUrl="~/shop/DashBoard.aspx">
                                <div class="otherLoginButtonContent">
                                    <img src="picture/store.png" width="30" style="display: inline-block; margin: 0 10px;"/>
                                    <div style="display: inline-block; font-size:14px">我的商店</div>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="otherLoginButton" Width="60" ID="LinkButton8" runat="server">
                                <div class="otherLoginButtonContent">
                                    <img src="picture/shopping.png" width="30" style="display: inline-block; margin: 0 10px;"/>
                                    <div style="display: inline-block; font-size:14px">購物車</div>
                                </div>
                           </asp:LinkButton>
                       </div>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <div class="shopContentBody">
        <div class="Body">
            <div class="shopDataContext">
                <div style="padding-bottom: 20px;">
                    <span class="WelcomeText" >歡迎來到 </span>
                    <asp:Label ID="LB_shopName" CssClass="shopBuyName" runat="server" Text=""></asp:Label>
                </div>
                <asp:Label ID="LB_shopEMail" CssClass="shopBuyText" runat="server" Text="聯絡信箱："></asp:Label>
                <asp:Label ID="LB_shopPhone" CssClass="shopBuyText" runat="server" Text="聯絡電話："></asp:Label>
                <asp:Label ID="LB_shopAddress" CssClass="shopBuyText" runat="server" Text="商家地址："></asp:Label>
            </div>
        </div>
    </div>
    <div style="background:#E7E7E7; height:1px;"></div>
    <div class="contentBody" style="background:#F0F0F0; padding: 20px 0px">
        <div class="Body" style="display: flex; flex-direction: column;">
            <div class="commodityAllTitle">
                所有商品
            </div>
            <asp:Panel ID="Panel_CommodityPage" runat="server" CssClass="CommodityPage"></asp:Panel>
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
