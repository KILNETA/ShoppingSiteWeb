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
        p{
            font-family: 微軟正黑體;
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
            width: 350px;
            height: 450px;
            background-position: center;
            background: #ffffff;
            padding: 30px;
            padding-top: 10px;
            box-shadow: 0 0 5px 3px #003b3b3b;
        }
        div.InputBox{
            text-align: center;
            margin-top: 20px;
        }
        div.Button{
            margin-top: 20px;
            text-align: center;
        }
        .TextBox{
            border: 1px solid #AAAAAA;
            border-radius: 3px;
            padding: 15px;
            outline: none;
            width: 100%;
            box-sizing: border-box; 
            font-size: 12px;
        }
        .TextBox:hover,.TextBox:active{
            border: 1px solid #004469;
            border-radius: 3px;
            padding: 15px;
            outline: none;
            width: 100%;
            box-sizing: border-box;
        }
        .TextBox:focus{
            border: 1px solid #004469;
            border-radius: 3px;
            padding: 15px;
            outline: none;
            width: 100%;
            box-sizing: border-box;
            box-shadow: 0 0 3px 2px #003b3b3b;
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
        #LoginQRcode{
            width: 128px;
            background: #ffffff;
            box-shadow: 0 0 3px 2px #003b3b3b;
            border-radius: 5px;
            padding: 10px;
        }
    </style>
</head>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>
<script type="text/javascript" src='../referenceJS/jquery.qrcode.js'></script>
<script type="text/javascript" src='../referenceJS/qrcode.js'></script>
<script language="JavaScript" type="text/JavaScript">
    function UserBoxHasError(obj) {
        if (obj.value == "") {
            document.getElementById('ErrorLB_1').innerText = "請輸入此欄";
            obj.style.borderColor = "#FF0000";
        }
        else {
            document.getElementById('ErrorLB_1').innerText = "　";
            obj.style = "TextBox";
        }
    }
    function PasswordBoxHasError(obj) {
        if (obj.value == "") {
            document.getElementById('ErrorLB_2').innerText = "請輸入此欄";
            obj.style.borderColor = "#FF0000";
        }
        else {
            document.getElementById('ErrorLB_2').innerText = "　";
            obj.style = "TextBox";
        }
    }
    jQuery(function () {
        var time = new Date();
        var text = "Welcome Einkaufen. Current time:" + time.toUTCString();
        jQuery('#LoginQRcode').qrcode({
            width: 128, height: 128, text: text
        });
    })
</script>
<body>
    <form runat="server">
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
                    <p style="font-size: 24px; margin-bottom:30px">
                        登入
                    </p>
                    <div class="InputBox">
                        <asp:TextBox CssClass="TextBox" ID="TB_User" placeholder="使用者名稱/E-mail" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)"/>
                    </div>
                    <p style="font-size: 12px; margin-top:0px; color:red" >
                        <asp:Label ID="ErrorLB_1" runat="server" Text="　"/>
                    </p>
                    <div class="InputBox">
                        <asp:TextBox CssClass="TextBox" ID="TB_Password" placeholder="密碼" runat="server" BorderWidth="1px" TextMode="Password" onBlur="PasswordBoxHasError(this)"/>
                    </div>
                    <p style="font-size: 12px; margin-top:0px; color:red" >
                        <asp:Label ID="ErrorLB_2" runat="server" Text="　"/>
                    </p>
                    <div class="Button">
                        <asp:Button  CssClass="LoginButton" ID="Button1" runat="server" Text="登入" BorderStyle="None" ForeColor="White" />
                    </div>
                    <div id="LoginQRcode"></div>
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