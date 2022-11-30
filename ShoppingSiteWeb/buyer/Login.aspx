<%@ Page Title="登入" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShoppingSiteWeb.buyer.Login" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-登入</title>

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
            height: 500px;
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
        .otherLoginBox{
            width: 144px;
            background: #ffffff;
            box-shadow: 0 0 3px 2px #003b3b3b;
            border-radius: 5px;
            padding: 10px;
        }
        .otherLoginButton{
            width: 144px;
            background: #ffffff;
            box-shadow: 0 0 3px 2px #003b3b3b;
            border-radius: 5px;
            padding: 10px;
            color:#000000;
            text-decoration:none;
        }
        .otherLoginButton:hover {
            background: #fdfdfd;
            box-shadow: 0 0 3px 2px #00004469;
        }
        .otherLoginButton:active{
            background: #f8f8f8;
            box-shadow: 0 0 3px 2px #00004469;
            color:#000000;
        }
        .otherLoginButtonContent {
            display: flex;
            align-items: center;
        }
        .generalLink{
            color:#1871a1;
            text-decoration:none;
            font-size:12px;
        }
        .generalLink:active{
            color:#004469;
            text-decoration:none;
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
            width: 144, height: 144, text: text
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
                    <div style="font-size: 24px; margin-bottom:30px">
                        登入
                    </div>
                    <div class="InputBox">
                        <asp:TextBox CssClass="TextBox" ID="TB_User" placeholder="使用者名稱/E-mail" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)"/>
                        <div>
                            <asp:Label CssClass="ErrorMessage" ID="ErrorLB_1" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <asp:TextBox CssClass="TextBox" ID="TB_Password" placeholder="密碼" runat="server" BorderWidth="1px" TextMode="Password" onBlur="PasswordBoxHasError(this)"/>
                        <div>
                            <asp:Label CssClass="ErrorMessage" ID="ErrorLB_2" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="Button">
                        <asp:Button  CssClass="LoginButton" ID="LoginButton" runat="server" Text="登入" BorderStyle="None" ForeColor="White" OnClick="LoginButton_Click" />
                    </div>

                    <div style="display:flex; margin-top: 5px;">
                        <div style="flex-grow: 1; font-size:12px">
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton5" runat="server">忘記密碼</asp:LinkButton>
                        </div>
                        <div style="flex-grow: 1; text-align: end; font-size:12px">
                            <div style="display: inline-block;">
                                新朋友？
                            </div>
                            <asp:LinkButton CssClass="generalLink" ID="RegisterButton" runat="server" OnClick="RegisterButton_Click">註冊</asp:LinkButton>
                        </div>
                    </div>

                    <div class="Button" style="width: 100%; display:flex; flex-direction: row; margin-top: 5px;">
                        <hr style="height: 0.1px; background:#000000; flex-grow: 1;"/>
                         <div style="margin: 0px 10px">或</div>
                        <hr style="height: 0.1px; background:#000000; flex-grow: 1;"/>
                    </div>

                    <div style="width: 100%; flex-grow: 1; margin-top: 10px; display:flex; flex-direction: row; font-size:0">
                        
                        <div style="flex-grow: 1;" >
                            <div class="otherLoginBox">
                                <div id="LoginQRcode"></div>
                                <div style="font-size: 16px; text-align: center">使用QRcode登入</div>
                            </div>
                        </div>

                        <div style="flex-grow: 1; display:flex; align-items: flex-end; flex-direction: column;">
                            <div style="margin-bottom: 10px">
                                 <asp:LinkButton CssClass="otherLoginButton" Height="20px" ID="LinkButton1" runat="server">
                                    <div class="otherLoginButtonContent">
                                        <img src="picture/facebook_logo.png" width="20" style="display: inline-block; margin: 0 10px;"/>
                                        <div style="display: inline-block; font-size:14px">Facebook</div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                            <div style="margin-bottom: 10px">
                                <asp:LinkButton CssClass="otherLoginButton" Height="20px" ID="LinkButton2" runat="server">
                                    <div class="otherLoginButtonContent">
                                        <img src="picture/google_logo.png" width="20" style="display: inline-block; margin: 0 10px;"/>
                                        <div style="display: inline-block; font-size:14px">Google</div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                            <div style="margin-bottom: 10px">
                                <asp:LinkButton CssClass="otherLoginButton" Height="20px" ID="LinkButton3" runat="server">
                                    <div class="otherLoginButtonContent">
                                        <img src="picture/line_logo.png" width="20" style="display: inline-block; margin: 0 10px;"/>
                                        <div style="display: inline-block; font-size:14px">LINE</div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                        </div>
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
    <asp:DetailsView ID="useCheckLoginTable" runat="server" Height="50px" Width="125px" Visible="False"></asp:DetailsView>
</form>
</body>
</html>