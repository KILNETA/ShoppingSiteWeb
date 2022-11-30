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
    </form>
</body>
</html>