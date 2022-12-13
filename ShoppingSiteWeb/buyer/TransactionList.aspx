<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionList.aspx.cs" Inherits="ShoppingSiteWeb.buyer.TransactionList" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-會員儀錶板</title>

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

        div.DashBoardBox {
            width: 1200px;
            margin: auto;
            padding-top: 30px;
            padding-bottom: 30px;
            background-position: center;
	        display: flex;
        }
        div.DashBoardBoxContent {
            flex: 1;
            min-height: 600px;
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
            height: 40px;
            width:100%;
            cursor: pointer;
            background: #004469;
            color:#FFFFFF;
        }
        .SignOutButton:hover {
            background: #0d5982;
        }
        .SignOutButton:active{
            background: #3d474d;
        }
        .SignOutButton_Lack{
            font-family: 微軟正黑體;
            font-size: 16px;
            height: 40px;
            width:100%;
            cursor: pointer;
            background: #777777;
            color:#FFFFFF;
        }
        .UserDataTable{
            min-height:100px;
            height:100%;
            width:100%;
        }

        .Panel_ShoppingCartBox{
            display: flex;
            flex-direction: column;
            flex:1;
        }

        .TT_Item{
            border: 1px solid #00446940;
            padding: 5px 10px;
            margin-bottom : 2px;
            display: flex;
	        flex-direction: row;
        }
        .TT_TextId_Box{
            width: 80px;
            margin-right: 5px;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .TT_TextId{
            margin:auto;
            font-size: 16px;
            color: #777777;
            text-align: center;
        }
        .TT_TextDate{
            width:100px;
            font-size: 14px;
            color: #777777;
            text-align: center;
            margin: auto 0px;
        }
        .TT_hr{
            border:0; 
            height: 100%;
            background: #00446920; 
            width:1px; 
        }
        .TT_SubTotal_Box{
            width: 110px;
            display: flex;
            align-items: center;
            justify-content: center;
            padding-left:10px;
        }
        .TT_SubTotal{
            font-size: 18px;
            font-family: 微軟正黑體;
            font-weight: bolder;
            color: #ff5d0e;
        }

        .CT_ListBox{
            flex:1;
            display: flex;
            flex-direction: column;
        }
        .CT_hr{
            border:0; 
            height: 1px;
            background: #00446920; 
            width: 100%; 
        }
        .CT_Item{
            flex: 1;
            height: 50px;
            display: flex;
            flex-direction: row;
            padding:0px 5px;
        }
        .CT_LinkBox{
            height:100%;
            flex:1;
            display: flex;
            flex-direction: row;
            text-decoration: none;
            align-items: center;
        }
        .CT_Thumbnail{
            width: 48px;
            height: 48px;
            object-fit: scale-down;
        }
        .CT_Name{
            font-size: 16px;
            color: #004469;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 1;
            overflow: hidden;
        }
        .CT_Id{
            padding:0px 5px;
            font-size: 14px;
            color: #777777;
        }
        .CT_Price_Box{
            width: 100px;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
        }
        .CT_PriceNum{
            font-size: 16px;
            color: #777777;
        }
        .CT_Price{
            font-size: 16px;
            padding-top: 1px;
            font-family: 微軟正黑體;
            font-weight: bolder;
            color: #ff5d0e;
        }

        .SC_ShoppingCartIsEmpty{
            font-size: 28px;
            font-family: 微軟正黑體;
            color: #444444;
            margin: auto;
        }
        .Table_Title{
            margin: auto;
            font-size: 16px;
            color: #777777;
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
            <div class="DashBoardBox">
                <div class="DashBoardBoxContent">
                    <div style="font-size: 24px; margin-bottom:30px">
                        您好! <asp:Label ID="userRealName" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="font-size: 24px; margin-bottom:10px">
                        訂單清單：
                    </div>
                    <div class="TT_Item">
                        <div class="TT_TextId_Box">
                            <span class="Table_Title">訂單編號</span>
                        </div>
                        <div class="TT_hr"></div>
                        <div style="width:100px; display: flex;">
                            <span class="Table_Title">下訂時間</span>
                        </div>
                        <div class="TT_hr"></div>
                        <div style="flex:1; display: flex; flex-direction: row; padding:0px 5px;">
                            <div style="flex:1; display: flex; align-items: center;">
                                <span class="Table_Title">單品項目(商品編號)</span>
                            </div>
                            <div class="TT_hr"></div>
                            <div class="CT_Price_Box" style ="align-items: center;">
                                <span class="Table_Title">數量/單價</span>
                            </div>
                        </div>
                        <div class="TT_hr"></div>
                        <div class="TT_SubTotal_Box">
                            <span class="Table_Title">訂單小計</span>
                        </div>
                    </div>
                    <asp:Panel ID="Panel_ShoppingCartBox" CssClass="Panel_ShoppingCartBox" runat="server"></asp:Panel>
                </div>
            </div>
        </div>
    </div>
        
    <footer class="FooterBackground-Style">
        <div class="Body">
            <p>&copy; <%: DateTime.Now.Year %> - Einkaufen 愛康福</p>
        </div>
    </footer>
    <asp:TextBox ID="TB_CheckUser" runat="server" Enabled="False" Visible="False"></asp:TextBox>
    <asp:TextBox ID="TB_Token" runat="server" Enabled="False" Visible="False"></asp:TextBox>
</form>
</body>
</html>

