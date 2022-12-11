<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="ShoppingSiteWeb.buyer.ShoppingCart" %>

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
            width: 900px;
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
            min-height:100px;
            height:100%;
            width:100%;
        }

        .SC_commodityItem{
            border: 1px solid #00446920;
            padding: 5px 10px;
            margin-bottom : 2px;
            display: flex;
	        flex-direction: row;
            position: relative;
        }
        .SC_Thumbnail_Box{
            padding: 1px;
            display: flex;
	        flex-direction: column;
            margin-right: 10px;
            text-decoration: none;
        }
        .SC_Thumbnail{
            width: 98px;
            height: 98px;
            margin-bottom: 2px;
            object-fit: scale-down;
        }
        .SC_Id{
            margin-left:10px;
            font-size: 12px;
            font-family: 微軟正黑體;
            color: #444444;
        }
        .SC_hr{
            border:0; 
            background: #00446920; 
            width:1px; 
        }
        .SC_Context_Box{
            display: flex;
            flex-direction: column;
            padding: 0px 10px;
            width: 540px;
        }
        .SC_Name{
            font-size: 20px;
            line-height: 28px;
            font-weight: bolder;
            color: #004469;
            letter-spacing: .7px;
            text-decoration: none;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 1;
            overflow: hidden;
        }
        .SC_Price{
            font-size: 20px;
            margin-bottom: 4px;
            font-family: 微軟正黑體;
            font-weight: bolder;
            color: #ff5d0e;
        }
        .SC_Introduction{
            font-size: 16px;
            color: #777777;
            letter-spacing: .7px;
            display: -webkit-box;
            -webkit-box-orient: vertical;
            -webkit-line-clamp: 2;
            overflow: hidden;
        }
        .SC_NumSubPrice_Box{
            display: flex;
            flex-direction: column;
            flex:1;
            padding-left: 10px;
        }
        .SC_SubPrice_Box{
            display: flex;
            flex-direction: column;
            flex:1;
        }
        .SC_SubPrice_Context{
            display: flex;
            flex-direction: column;
            flex:1;
            padding: 4px;
        }
        .SC_SubPriceTitle{
            font-size: 12px;
            color: #444444;
        }
        .SC_SubPrice{
            margin: auto;
            font-size: 24px;
            color: #ff5d0e;
            font-weight: bolder;
            text-align: center;
            vertical-align: middle;
        }
        .SC_SubPrice_Lack{
            margin: auto;
            font-size: 24px;
            color: #999999;
            font-weight: bolder;
            text-align: center;
            vertical-align: middle;
        }
        .SC_Num_Box{
            display: flex;
            flex-direction: column;
            justify-content: right;
            margin: 4px auto 4px 4px ;
        }
        .SC_Num_Text{
            display: flex;
            flex-direction: row;
            justify-content: right;
        }
        .SC_HasNum{
            margin-left:10px;
            font-size: 12px;
            font-family: 微軟正黑體;
            color: #444444;
        }
        .SC_HasNum_Lack{
            margin-left:10px;
            font-size: 12px;
            font-family: 微軟正黑體;
            color: #d9333f;
            font-weight: bolder;
        }
        .SC_Input{
            height: 28px;
            width: 70px;
            outline: none;
            box-sizing: border-box; 
            font-size: 14px;
            text-align:center;
        }
        
        .SC_Remove{
            width: 40px;
            height: 40px;
            background: #00000000;
            cursor: pointer;
            position: absolute;
            bottom: 0px;
            right: 0px;
            cursor: pointer;
        }
        .SC_RemoveIcon{
            width: 100%;
            height: 100%;
            background: #777777;
            -webkit-mask: url('picture/garbage.svg') no-repeat center;
            mask: url('picture/garbage.svg') no-repeat center;
        }
        .SC_RemoveIcon:hover{
            background: #ff8282;
        }
        .SC_RemoveIcon:active{
            background: #ff2b2b;
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
                        購物車清單：
                    </div>
                    <asp:Panel ID="Panel_ShoppingCartBox" runat="server"></asp:Panel>
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
