﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnShelves.aspx.cs" Inherits="ShoppingSiteWeb.shop.OnShelves" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-商品上架</title>

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

        div.RegisterBox {
            margin: auto;
            padding: 40px 0px;
            background-position: center;
        }
        div.RegisterBoxContent {
            width: 460px;
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
        div.InputField{
            display: flex;
        }
        div.FieldText{
            margin: auto 0px;
            font-size: 16px;
            margin-right: 10px;
            height: 100%;
        }
        .TextBox{
            border: 1px solid #AAAAAA;
            border-radius: 3px;
            padding: 13px;
            outline: none;
            box-sizing: border-box; 
            font-size: 12px;
            flex-grow: 1;
            resize: none;
        }
        .TextBox:hover,.TextBox:active{
            border: 1px solid #004469;
        }
        .TextBox:focus{
            border: 1px solid #004469;
            box-shadow: 0 0 3px 2px #003b3b3b;
        }
        .ErrorMessage{
            font-size: 12px; 
            margin-top:0px; 
            color:red
        }
        .NoticeMessage{
            font-size: 12px; 
            margin-top:0px; 
            color:green
        }
        div.Button{
            text-align: center;
            flex-shrink: 0;
            flex-direction: row;
        }
        .RegisterButton{
            font-family: 微軟正黑體;
            font-size: 16px;
            width: 100%;
            height: 40px;
            cursor: pointer;
            background: #004469;
        }
        .RegisterButton:hover {
            background: #0d5982;
        }
        .RegisterButton:active{
            background: #3d474d;
        }
        .DropDownList{
            text-align:center;
            width: 82px;
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
            <div class="RegisterBox">
                <div class="RegisterBoxContent">
                    <div style="font-size: 24px; margin-bottom:30px">
                        商品上架
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">商品名稱</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_ShopName" placeholder="最短4個，最長50個字元" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="24" AutoPostBack="True"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_ShopName" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">商品價格</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_ShopEMail" placeholder="最多$9,999" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" AutoPostBack="True" MaxLength="4"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px; display: flex;">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_EMail" runat="server" Text="　" style="flex-grow: 1"/>
                            <asp:Label CssClass="NoticeMessage" ID="LB_SendEMailMessage" runat="server" Text="　" style="margin-right: 30px"/>
                        </div>
                    </div>
                     <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">商品數量</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_ShopPhoneNum" placeholder="最多99個" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="2"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_ShopPhoneNum" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div  class="FieldText">商品簡述</div>
                        <div class="InputField">
                            <asp:TextBox CssClass="TextBox" ID="TB_ShopAddress" placeholder="最長200個字" Height="200px" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="200" TextMode="MultiLine" />
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div>
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_Address" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div  class="FieldText">商品圖片</div>
                        <div class="InputField">
                            <asp:TextBox CssClass="TextBox" ID="TextBox1" placeholder="請填寫有效圖片連結" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" TextMode="Url" />
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div>
                            <asp:Label CssClass="ErrorMessage" ID="Label1" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="Button" style="margin: 0px 60px">
                        <asp:Button  CssClass="RegisterButton" ID="ShopRegisterButton" runat="server" Text="上架商品" BorderStyle="None" ForeColor="White"/>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <footer class="FooterBackground-Style">
        <div class="Body">
            <p>&copy; <%: DateTime.Now.Year %>- Einkaufen 愛康福</p>
        </div>
    </footer>
    <asp:TextBox ID="TB_Token" runat="server" Enabled="False" Visible="False"></asp:TextBox>
</form>
</body>
</html>

