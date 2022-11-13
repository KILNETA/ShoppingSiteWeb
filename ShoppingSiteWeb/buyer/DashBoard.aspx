<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="ShoppingSiteWeb.buyer.DashBoard" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-個人儀錶板</title>

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

        div.DashBoardBox {
            margin: auto;
            padding-top: 30px;
            padding-bottom: 30px;
            background-position: center;
        }
        div.DashBoardBoxContent {
            height: 600px;
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
        .SignOutButton{
            font-family: 微軟正黑體;
            font-size: 16px;
            width: 100%;
            height: 40px;
            cursor: pointer;
            background: #004469;
        }
        .SignOutButton:hover {
            background: #0d5982;
        }
        .SignOutButton:active{
            background: #3d474d;
        }
        .UserDataTable{
            height:100%;
            width:100%;
        }

    </style>
</head>

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
            <div class="DashBoardBox">
                <div class="DashBoardBoxContent">
                    <div style="font-size: 24px; margin-bottom:30px">
                        您好! <asp:Label ID="userRealName" runat="server" Text=""></asp:Label>
                    </div>
                    <div style=" flex-grow: 1;">
                        <asp:GridView ID="GV_UserData" CssClass="UserDataTable" runat="server" Font-Size="16"></asp:GridView>
                    </div>

                    <div style="display: flex; align-items: flex-end; justify-content:center">
                        <asp:Button  CssClass="SignOutButton" ID="SignOutButton" runat="server" Width="160" Text="登出" BorderStyle="None" ForeColor="White" OnClick="SignOutButton_Click"/>
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
