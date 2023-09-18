<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionList.aspx.cs" Inherits="ShoppingSiteWeb.buyer.TransactionList" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-會員儀錶板</title>
    <link rel="stylesheet" type="text/css" href="../Style/TransactionList.css">
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
    <asp:TextBox ID="TB_Token" runat="server" Enabled="False" Visible="False"></asp:TextBox>
</form>
</body>
</html>

