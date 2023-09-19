<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="ShoppingSiteWeb.buyer.DashBoard" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-會員儀錶板</title>
    <link rel="stylesheet" type="text/css" href="../Style/DashBoard.css">
</head>

<body>
    <form runat="server">
    <header class="HeaderBackground-Style">
        <div class="Body">
            <div class="TitelBox-Style">
                <asp:LinkButton CssClass="Titel-Style" ID="LB_Titel" runat="server" href="../Default.aspx">
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
                    <div style=" flex-grow: 1;">
                        <asp:GridView ID="GV_UserData" CssClass="UserDataTable" runat="server" Font-Size="16"></asp:GridView>
                        <div style="display: flex; align-items: flex-end; justify-content: center; margin: 10px 0px">
                            <asp:LinkButton  CssClass="CenterButton" runat="server" href="ShoppingCart.aspx">
                                <span>購物車</span>
                            </asp:LinkButton>
                            <asp:LinkButton  CssClass="CenterButton" runat="server" href="TransactionList.aspx">
                                <span>訂單清單</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div style=" flex-grow: 1;">
                        <asp:GridView ID="GV_ShopData" CssClass="UserDataTable" runat="server" Font-Size="16" EmptyDataText="尚未註冊商鋪"></asp:GridView>
                        <div style="display: flex; align-items: flex-end; justify-content: center; margin: 10px 0px">
                            <asp:LinkButton  CssClass="CenterButton" ID="BT_ShopRegister" runat="server" href="../shop/Register.aspx">
                                <span>註冊商鋪</span>
                            </asp:LinkButton>
                            <asp:LinkButton  CssClass="CenterButton" ID="BT_OnShelves" runat="server" href="../shop/OnShelves.aspx">
                                <span>上架新商品</span>
                            </asp:LinkButton>
                            <asp:LinkButton  CssClass="CenterButton" ID="BT_ShopDashBoard" runat="server" href="../shop/DashBoard.aspx">
                                <span>商鋪儀表板</span>
                            </asp:LinkButton>
                        </div>
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
