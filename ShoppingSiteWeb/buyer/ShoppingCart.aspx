<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="ShoppingSiteWeb.buyer.ShoppingCart" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-會員儀錶板</title>
    <link rel="stylesheet" type="text/css" href="../Style/ShoppingCart.css">
    <script type="text/javascript" src="../Scripts/WebFuns/ShoppingCart.js"></script>
</head>

<body>
    <form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                    <div style="font-size: 24px; margin-bottom:10px">
                        購物車清單：
                    </div>
                    <asp:UpdatePanel ID="Panel_ShoppingCartBox" class="Panel_ShoppingCartBox" runat="server"></asp:UpdatePanel>
                    <hr class="SC_hr" style=" width:100%; height:1px; ">
                    <div style="display:flex; flex-direction: column; width:400px; margin:auto;">
                        <asp:UpdatePanel ID="sutTotal" style="width: 100%; display:flex; flex-direction: row; padding:10px 0px" runat="server">
                            <ContentTemplate>
                                <span class="sutTotalTitle" style="margin-right:auto">商品小計：</span>
                                <asp:Label ID="subTotal" CssClass="sutTotal" runat="server" Text="$0"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button ID="LB_ToShopping" CssClass="SignOutButton" runat="server" BorderStyle="None" ForeColor="White" Text="購買" OnClick="LB_ToShopping_Click" />
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
    <asp:TextBox ID="TB_CheckUser" runat="server" Enabled="False" Visible="False"></asp:TextBox>
    <asp:TextBox ID="TB_Token" runat="server" Enabled="False" Visible="False"></asp:TextBox>
</form>
</body>
</html>
