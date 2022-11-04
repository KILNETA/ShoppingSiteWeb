<%@ Page Title="登入" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShoppingSiteWeb.buyer.Login" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-登入</title>

    <style type="text/css">
        html,body {
            margin: 0;
            height: 100%;
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

        div.contentBody{
            flex-grow: 1;
            background-image: url("picture/login_background.jpg");
            background-size: cover;
            background-position: center;
        }

        .HeaderBackground-Style {
            background-color: #004469;
            width: 100%;
            height: 90px;
            float: left;
        }

        .FooterBackground-Style {
            background-color: #CEDDE0;
            width: 100%;
            height: 240px;
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
        }

        div.LoginBox {
            margin: auto;
            margin-left: 0;
            padding-top: 30px;
            padding-bottom: 30px;
            background-position: center;
        }
        div.LoginBoxContent {
            width: 380px;
            height: 500px;
            background-position: center;
            background: #ffffff;
        }
        .auto-style1 {
            text-align: center;
        }
        .TextBox{
            outline: none;
        }
    </style>
</head>


<body>
    <form id="form1" runat="server">
    <header class="HeaderBackground-Style">
        <div class="Body">
            <div class="TitelBox-Style">
                <p class="Titel-Style">Einkaufen 愛康福</p>
            </div>
        </div>
    </header>


    <div class="contentBody">
        <div class="Body">
            <img src="picture/MainActivity.png" width="500" height="500" alt="一張圖片" style="margin: auto; margin-right: 50px;"/>
            <div class="LoginBox">
                <div class="LoginBoxContent">
                    <p class="auto-style1">
                        登入
                    </p>
                    <p class="auto-style1">
                        <asp:TextBox CssClass="TextBox" ID="TextBox1" runat="server" BorderStyle="None" BorderWidth="0" />
                    </p>
                    <p class="auto-style1">
                        <asp:TextBox CssClass="TextBox" ID="TextBox2" runat="server" BorderStyle="None" BorderWidth="0px"/>
                    </p>
                    <p class="auto-style1">
                        <asp:Label ID="Label1" runat="server" Text="登入"/>
                    </p>
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