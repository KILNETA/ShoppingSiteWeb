<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ShoppingSiteWeb.buyer.Register" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-註冊</title>

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
            background-color: #c3c8c9;
            width: 100%;
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

        div.RegisterBox {
            margin: auto;
            padding-top: 30px;
            padding-bottom: 30px;
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
            margin: auto;
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
            <div class="RegisterBox">
                <div class="RegisterBoxContent">
                    <div style="font-size: 24px; margin-bottom:30px">
                        註冊
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">用戶名稱</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_User" placeholder="最短4個，最長24個字元" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="24" />
                            <div class="Button" style="margin-left:10px">
                                <asp:Button  CssClass="RegisterButton" ID="Button1" runat="server" Text="驗證可用性" BorderStyle="None" ForeColor="White"/>
                            </div>
                            <img src="picture/verify_fail.png" width="20" height="20" style="margin: auto auto auto 10px"/>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="ErrorLB_1" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">電子信箱</div>
                            <asp:TextBox CssClass="TextBox" ID="TextBox1" placeholder="sample@example.com" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)"/>
                            <img src="picture/verify_fail.png" width="20" height="20" style="margin: auto auto auto 10px"/>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="Label1" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">密　　碼</div>
                            <asp:TextBox CssClass="TextBox" ID="TextBox2" placeholder="" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)"/>
                            <img src="picture/verify_fail.png" width="20" height="20" style="margin: auto auto auto 10px"/>
                        </div>
                        <asp:Label CssClass="ErrorMessage" ID="Label2" runat="server" Text="　"/>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">確認密碼</div>
                            <asp:TextBox CssClass="TextBox" ID="TextBox3" placeholder="" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)"/>
                            <img src="picture/verify_fail.png" width="20" height="20" style="margin: auto auto auto 10px"/>
                        </div>
                        <asp:Label CssClass="ErrorMessage" ID="Label3" runat="server" Text="　"/>
                    </div>
                    <hr style="width:100%; height: 0.1px; background:#000000; flex-grow: 1;"/>
                    <div style="font-size: 20px; margin:10px 0px">
                        基本資料
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">姓　　名</div>
                            <asp:TextBox CssClass="TextBox" ID="TextBox7" placeholder="" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)"/>
                            <img src="picture/verify_fail.png" width="20" height="20" style="margin: auto auto auto 10px"/>
                        </div>
                        <asp:Label CssClass="ErrorMessage" ID="Label7" runat="server" Text="　"/>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">行動電話</div>
                            <asp:TextBox CssClass="TextBox" ID="TextBox4" placeholder="" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)"/>
                            <img src="picture/verify_fail.png" width="20" height="20" style="margin: auto auto auto 10px"/>
                        </div>
                        <asp:Label CssClass="ErrorMessage" ID="Label4" runat="server" Text="　"/>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">地　　址</div>
                            <asp:TextBox CssClass="TextBox" ID="TextBox6" placeholder="" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)"/>
                            <img src="picture/verify_fail.png" width="20" height="20" style="margin: auto auto auto 10px"/>
                        </div>
                        <asp:Label CssClass="ErrorMessage" ID="Label6" runat="server" Text="　"/>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">生　　日</div>
                            <asp:TextBox CssClass="TextBox" ID="TextBox5" placeholder="" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)"/>
                            <img src="picture/verify_fail.png" width="20" height="20" style="margin: auto auto auto 10px"/>
                        </div>
                        <asp:Label CssClass="ErrorMessage" ID="Label5" runat="server" Text="　"/>
                    </div>
                    <div class="Button">
                        <asp:Button  CssClass="RegisterButton" ID="RegisterButton" runat="server" Text="註冊" BorderStyle="None" ForeColor="White"/>
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
