<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShoppingSiteWeb.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-登入</title>

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
            height: 700px;
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
        div.TitelMenuseparate{
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
            padding 0px 5px;
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
        #flash {
            width: 1000px;
            height: 400px;
            position: relative;
        }
        #flash #play li {
   
            /*position: absolute;
            top: 0px;
            left: 0px;*/
        }
        #play li:not(:first-child) {
            display: none;
        }
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
            background: #DDDDDD;
            border-radius: 6px;
            cursor: pointer;
        }
        #CarouselPrev {
            width: 40px;
            height: 63px;
            position: absolute;
            top: 205px;
            left: 10px;
        }
        #CarouselNext {
            width: 40px;
            height: 63px;
            position: absolute;
            top: 205px;
            right: 10px;
        }
        #CarouselPrev:hover {

        }
        #CarouselNext:hover {

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
                aDiv[i].style.background = "#DDDDDD";
            }
            aDiv[now].style.background = '#004469';

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
                        <div class="TitelMenu-Style">
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton3" runat="server">會員註冊</asp:LinkButton>
                            <div class="TitelMenuseparate">|</div>
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton4" runat="server">會員登入</asp:LinkButton>
                        </div>
                    </div>
                    <div class="Titel-Style" style="flex-grow: 1">
                        <div class="Titel" style="flex: 1;">
                            Einkaufen 愛康福
                        </div>
                        <div class="Titel-Style" style="width: 640px">
                            <div class="InputBox">
                                <asp:TextBox CssClass="TextBox" ID="TB_Password" placeholder="來愛康福購好物～" runat="server" BorderWidth="0px"/>
                                <asp:DropDownList ID="DDL_BirthdayDay" CssClass="SearchDropDownList" runat="server" BorderWidth="0px">
                                    <asp:ListItem>查商品</asp:ListItem>
                                    <asp:ListItem>查商店</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinkButton CssClass="otherSearchButton" ID="LinkButton7" runat="server">
                                    <img src="Default_Picture/Inquire.png" width="20" style="display: inline-block; margin: 0 10px;"/>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="Titel-Style" style="flex: 1; height:100%; align-items: center; justify-content: left;">
                            <asp:LinkButton CssClass="otherLoginButton" Width="50" ID="LinkButton9" runat="server">
                                <div class="otherLoginButtonContent">
                                    <img src="Default_Picture/shoppingBag.png" width="30" style="display: inline-block; margin: 0 10px;"/>
                                    <div style="display: inline-block; font-size:14px">賣東西</div>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="otherLoginButton" Width="50" ID="LinkButton6" runat="server">
                                <div class="otherLoginButtonContent">
                                    <img src="Default_Picture/store.png" width="30" style="display: inline-block; margin: 0 10px;"/>
                                    <div style="display: inline-block; font-size:14px">賣東西</div>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="otherLoginButton" Width="50" ID="LinkButton8" runat="server">
                                <div class="otherLoginButtonContent">
                                    <img src="Default_Picture/shopping.png" width="30" style="display: inline-block; margin: 0 10px;"/>
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
            <div style="height:400px; width:1000px; margin:0px auto; display:flex; justify-content: center; position: relative; overflow: hidden;">
                <div id="CarouselPrev"></div>
	            <div id="CarouselNext"></div>
                <ul id="CarouselView" style="display: flex; position: absolute; width:100%; height:100%; padding:0px; transition: transform 0.5s;">
                    <li><img src="Default_Picture/DefaultCarousel/DefaultCarousel_1.png" alt="img1"/></li>
                    <li><img src="Default_Picture/DefaultCarousel/DefaultCarousel_2.png" alt="img2"/></li>
                    <li><img src="Default_Picture/DefaultCarousel/DefaultCarousel_3.png" alt="img3"/></li>
                    <li><img src="Default_Picture/DefaultCarousel/DefaultCarousel_4.png" alt="img4"/></li>
                    <li><img src="Default_Picture/DefaultCarousel/DefaultCarousel_5.png" alt="img5"/></li>
                    <li><img src="Default_Picture/DefaultCarousel/DefaultCarousel_6.png" alt="img6"/></li>
	            </ul>
                <ul id="button">
			        <li><div style="background: #004469;"></div></li>
			        <li><div></div></li>
			        <li><div></div></li>
			        <li><div></div></li>
			        <li><div></div></li>
			        <li><div></div></li>
	            </ul>
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