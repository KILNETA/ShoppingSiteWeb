<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item.aspx.cs" Inherits="ShoppingSiteWeb.commodity.Item" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-購好物</title>

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
            background-image: url("picture/login_background.jpg");
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

        div.DashBoardBox {
            width:100%;
            margin: auto;
            padding-top: 30px;
            padding-bottom: 30px;
            background-position: center;
        }
        div.DashBoardBoxContent {
            width:100%;
            height: 600px;
            background-position: center;
            background: #ffffff;
            padding: 30px;
            box-shadow: 0 0 5px 3px #003b3b3b;
	        display: flex;
	        flex-direction: row;
            font-size: 12px;
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

        .commodityContent_Box{
            display: flex;
	        flex-direction: column;
        }
        .commodityThumbnail{
            width: 400px;
            height: 400px;
            object-fit: scale-down;
            border: 1px solid #00446920;
            margin-bottom: 10px;
        }
        .commodityId{
            font-size: 16px;
            color: #444444;
        }
        .commodityName{
            font-size: 24px;
            line-height: 28px;
            font-weight: bolder;
            margin-bottom: 10px;
            color: #444444;
            letter-spacing: .7px;

        }
        .shopBuy_Box{
            background: #00446910;
            padding: 20px 10px;
            border-radius: 5px;
            
            display: flex;
            flex-direction: column;
        }
        .shopBuyName{
            font-size: 18px;
            color: #004469;
            font-weight: bolder;
            text-decoration:none;
            margin-bottom: 10px;
        }
        .shopBuyText{
            font-size: 16px;
            color: #444444;
        }
        .commodityBuy_Box{
            background: #00446910;
            padding: 20px 10px;
            border-radius: 5px;
        }
        .commodityPrice_Box{
            display: flex;
	        flex-direction: row;
            align-items: center;
            margin-bottom: 10px;
        }
        .commodityPriceText{
            font-size: 16px;
            color: #444444;
            margin-right: 10px;
        }
        .commodityPrice{
            font-size: 28px;
            font-family: 微軟正黑體;
            font-weight: bolder;
            color: #ff5d0e;
        }
        .commodityNum{
            margin-left:10px;
            font-size: 16px;
            font-family: 微軟正黑體;
            color: #444444;
        }
        .commodityNum_Lack{
            margin-left:10px;
            font-size: 16px;
            font-family: 微軟正黑體;
            color: #d9333f;
            font-weight: bolder;
        }
        .commodityInput{
            height: 28px;
            width: 70px;
            outline: none;
            box-sizing: border-box; 
            font-size: 14px;
            text-align:center;
        }
        .commodityIntroductionTitle{
            margin: 10px 0px;
            font-size: 24px;
            color: #444444;
            font-weight: bolder;
        }
        .commodityIntroduction{
            font-size: 20px;
        }

        .commodityBuyButton_Box{
            padding:10px 0px;
            margin-left: 62px;
            display: flex;
            flex-direction: row;
            align-items: center;
        }
        .commodityBuyButton{
            height: 30px;
            font-family: 微軟正黑體;
            font-size: 16px;
            padding: 5px 10px;
            cursor: pointer;
            background: #004469;

            border-radius: 5px;
            color:#FFFFFF;
            text-decoration:none;
            display: flex;
            flex-direction: row;
            align-items: center;
            justify-content: center;
        }
        .commodityBuyButton_Cant{
            height: 30px;
            font-family: 微軟正黑體;
            font-size: 16px;
            padding: 5px 10px;
            cursor: pointer;
            background: #9ba0a3;

            border-radius: 5px;
            color:#FFFFFF;
            text-decoration:none;
            display: flex;
            flex-direction: row;
            align-items: center;
            justify-content: center;
        }
        .commodityBuyButtonContext {
            display: flex;
            flex-direction: row;
            align-items: center;
        }
        .commodityBuyButtonIcon {
            width:30px;
            height:30px;
            background: #FFFFFF;
            -webkit-mask: url('picture/shoppingCart.svg') no-repeat center;
            mask: url('picture/shoppingCart.svg') no-repeat center;
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
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton3" runat="server">客服中心</asp:LinkButton>
                            <div class="TitelMenuseparate">|</div>
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton4" runat="server">賣家中心</asp:LinkButton>
                            <div class="TitelMenuseparate">|</div>
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton10" runat="server">幫助中心</asp:LinkButton>
                        </div>
                        <div style="flex-grow: 1;"></div>
                        <asp:Panel ID="Panel_TitelMenuLogin" runat="server" CssClass="TitelMenu-Style"></asp:Panel>
                    </div>
                    <div class="Titel-Style" style="flex-grow: 1">
                        <asp:LinkButton CssClass="Titel" ID="LinkButton11" runat="server" PostBackUrl="~/Default.aspx">
                            Einkaufen 愛康福
                        </asp:LinkButton>
                        <div class="Titel-Style" style="width: 640px">
                            <div class="InputBox">
                                <asp:TextBox CssClass="TextBox" ID="TB_Search" placeholder="來愛康福購好物～" runat="server" BorderWidth="0px"/>
                                <asp:DropDownList ID="DDL_SearchMode" CssClass="SearchDropDownList" runat="server" BorderWidth="0px">
                                    <asp:ListItem>查商品</asp:ListItem>
                                    <asp:ListItem>查商店</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinkButton CssClass="otherSearchButton" ID="LB_runSearch" runat="server" OnClick="LB_runSearch_Click">
                                    <img src="picture/Inquire.png" width="20" style="display: inline-block; margin: 0 10px;"/>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="Titel-Style" style="flex: 1; height:100%; align-items: center; justify-content: left;">
                            <asp:LinkButton CssClass="otherLoginButton" Width="60" ID="LinkButton13" runat="server" PostBackUrl="~/shop/OnShelves.aspx">
                                <div class="otherLoginButtonContent">
                                    <img src="picture/shoppingBag.png" width="30" style="display: inline-block; margin: 0 10px;"/>
                                    <div style="display: inline-block; font-size:14px">賣東西</div>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="otherLoginButton" Width="60" ID="LinkButton14" runat="server" PostBackUrl="~/shop/DashBoard.aspx">
                                <div class="otherLoginButtonContent">
                                    <img src="picture/store.png" width="30" style="display: inline-block; margin: 0 10px;"/>
                                    <div style="display: inline-block; font-size:14px">我的商店</div>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="otherLoginButton" Width="60" ID="LinkButton15" runat="server" PostBackUrl="~/buyer/ShoppingCart.aspx">
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

    <div class="contentBody">
        <div class="Body">
            <div class="DashBoardBox">
                <div class="DashBoardBoxContent">
                    <div class="commodityContent_Box" >
                        <asp:Image ID="commodityThumbnail" CssClass="commodityThumbnail" runat="server" />
                        <hr style="height: 1px; border:0; background:#BBBBBB; width:100%"/>
                        <div class="commodityId">
                            <span>商品編號：</span>
                            <asp:Label ID="commodityId" runat="server" Text=""></asp:Label>
                        </div>
                        <hr style="height: 1px; border:0; background:#BBBBBB; width:100%"/>
                        <div class="shopBuy_Box">
                            <asp:LinkButton ID="LB_shopName" CssClass="shopBuyName" runat="server"></asp:LinkButton>
                            <asp:Label ID="LB_shopEMail" CssClass="shopBuyText" runat="server" Text="商家信箱："></asp:Label>
                            <asp:Label ID="LB_shopPhone" CssClass="shopBuyText" runat="server" Text="商家電話："></asp:Label>
                        </div>
                    </div>
                    <div class="commodityContent_Box" style="flex: 1; padding: 0px 20px" >
                        <asp:Label ID="commodityName" CssClass="commodityName" runat="server" Text=""></asp:Label>
                        <div class="commodityBuy_Box">
                            <div class="commodityPrice_Box">
                                <span class="commodityPriceText">直購價：</span>
                                <asp:Label ID="commodityPrice" CssClass="commodityPrice" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="commodityPrice_Box">
                                <span class="commodityPriceText">數量：</span>
                                <asp:TextBox CssClass="commodityInput" ID="TB_commodityNum" runat="server" AutoPostBack="True" OnTextChanged="TB_commodityNum_TextChanged">1</asp:TextBox>
                                <asp:Label ID="commodityNum" CssClass="commodityNum" runat="server" Text=""></asp:Label>
                                <asp:Label ID="commodityNumLack" CssClass="commodityNum_Lack" runat="server" Text=""></asp:Label>
                            </div>
                            <div class ="commodityBuyButton_Box">
                                <asp:LinkButton CssClass="commodityBuyButton" ID="LB_JoinShoppingCart" runat="server" OnClick="LB_JoinShoppingCart_Click">
                                    <div class="commodityBuyButtonContext">
                                        <div class="commodityBuyButtonIcon" width="30" style="margin-right: 5px;"></div>
                                        <span style="font-size:16px">加入購物車</span>
                                    </div>
                                </asp:LinkButton>
                                <div style="width:10px;"></div>
                                <asp:LinkButton CssClass="commodityBuyButton" ID="LB_ToShopping" runat="server" OnClick="LB_ToShopping_Click">
                                    <span style= "font-size:16px">直接購買</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="commodityIntroductionTitle">
                                商品說明：
                        </div>
                        <asp:Label ID="commodityIntroduction" CssClass="commodityIntroduction" runat="server" Text=""></asp:Label>
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



