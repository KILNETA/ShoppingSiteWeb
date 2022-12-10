<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ShoppingSiteWeb.search.Search" %>

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
            min-height: 500px;
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

        /*轮播*/
        #button {
            position: absolute;
            bottom: 20px;
            list-style: none;
        }
        #button li {
            margin-left: 10px;
            float: left;
        }

        #button li div {
            width: 12px;
            height: 12px;
            background: #DDDDDDC0;
            border-radius: 6px;
            cursor: pointer;
        }

        #CarouselPrev {
            width: 48px;
            height: 100%;
            position: absolute;
            left: 10px;
            cursor: pointer;
            background-color: #FFFFFFC0;
            -webkit-mask: url('Default_Picture/previous.svg') no-repeat center;
            mask: url('Default_Picture/previous.svg') no-repeat center;
        }
        #CarouselNext {
            width: 48px;
            height: 100%;
            position: absolute;
            right: 10px;
            cursor: pointer;
            background-color: #FFFFFFC0;
            -webkit-mask: url('Default_Picture/next.svg') no-repeat center;
            mask: url('Default_Picture/next.svg') no-repeat center;
        }
        #CarouselPrev:hover ,#CarouselNext:hover {
            background-color: #FFFFFF;
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

        .commodityAllTitle{
            margin: 10px 0px;
            font-size: 24px;
            color: #444444;
            font-weight: bolder;
        }
    </style>
</head>

<script language="JavaScript" type="text/JavaScript">
    window.onload = function () {
        var aDiv = document.getElementById('button').getElementsByTagName('div');

        var CarouselPrev = document.getElementById('CarouselPrev');
        var CarouselNext = document.getElementById('CarouselNext');

        var CarouselImg = document.getElementById('CarouselView')

        var now = 0;
        var timer2 = null;

        for (var i = 0; i < aDiv.length; i++) {
            aDiv[i].index = i;
            aDiv[i].onclick = function () {
                if (now == this.index) return;
                now = this.index;
                tab();
            }
            aDiv[i].addEventListener('mouseover', function () {
                if (this.index == now)
                    this.style.background = '#004469'
                else
                    this.style.background = '#FFFFFFC0'
            })
            aDiv[i].addEventListener('mouseout', function () {
                if (this.index == now)
                    this.style.background = '#004469C0'
                else
                    this.style.background = '#DDDDDDC0'
            })
        }

        CarouselPrev.onclick = function () {
            now--;
            if (now == -1) {
                now = aDiv.length - 1;
            }
            tab();
        }
        CarouselNext.onclick = function () {
            now++;
            if (now == aDiv.length) {
                now = 0;
            }
            tab();
        }
        CarouselImg.onmouseover = function () {
            clearInterval(timer2);
        }
        CarouselImg.onmouseout = function () {
            timer2 = setInterval(CarouselNext.onclick, 6000);
        }
        timer2 = setInterval(CarouselNext.onclick, 6000);

        function tab() {
            clearInterval(timer2);
            for (var i = 0; i < aDiv.length; i++) {
                    aDiv[i].style.background = '#DDDDDDC0';
            }
            aDiv[now].style.background = '#004469C0';

            moveElement(CarouselImg, -1000 * now);
            timer2 = setInterval(CarouselNext.onclick, 6000);
        }

        function moveElement(ele, x_final) {//ele為元素物件
            var x_pos = ele.offsetLeft;

            if (ele.movement) {//防止懸停
                clearTimeout(ele.movement);
            }

            var dist = Math.ceil(Math.abs(x_final - x_pos));
            x_pos = x_pos < x_final ? x_pos + dist : x_pos - dist;

            CarouselImg.style.transform = `translateX(${x_pos}px)`;
        }
    }
</script>

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

    <div style="background:#E7E7E7; height:1px;"></div>
    <div class="contentBody" style="background:#F0F0F0; padding: 20px 0px">
        <div class="Body" style="display: flex; flex-direction: column;">
            <asp:Label CssClass="commodityAllTitle" ID="searchNum" runat="server" Text=""></asp:Label>
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