<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ShoppingSiteWeb.shop.Register" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-商店註冊</title>

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
                        註冊
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">用戶名稱</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_UserName" placeholder="最短4個，最長24個字元" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="24" AutoPostBack="True" />
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_UserName" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">電子信箱</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_EMail" placeholder="sample@example.com" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" AutoPostBack="True" MaxLength="320"/>
                            <div class="Button" style="margin: 0px 10px">
                                <asp:Button  CssClass="RegisterButton" ID="BT_SendCheckCodeEMail" runat="server" Text="驗證" BorderStyle="None" ForeColor="White"/>
                            </div>
                            <asp:TextBox CssClass="TextBox" ID="TB_EMailCheckCode" placeholder="輸入驗證碼" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" AutoPostBack="True" Width="60" style="text-align: center;"/>
                            <asp:Image ID="IMG_EMailCheck" runat="server" ImageUrl="picture/verify_fail.png" Height="20" Width="20" style="margin: auto auto auto 10px"/>
                        </div>
                        <div style="margin-left:74px; display: flex;">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_EMail" runat="server" Text="　" style="flex-grow: 1"/>
                            <asp:Label CssClass="NoticeMessage" ID="LB_SendEMailMessage" runat="server" Text="　" style="margin-right: 30px"/>
                        </div>
                    </div>
                    <div class="Button" style="margin: 0px 60px">
                        <asp:Button  CssClass="RegisterButton" ID="RegisterButton" runat="server" Text="註冊" BorderStyle="None" ForeColor="White"/>
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

