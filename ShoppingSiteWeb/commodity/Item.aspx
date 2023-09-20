<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item.aspx.cs" Inherits="ShoppingSiteWeb.commodity.Item" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-購好物</title>
    <link rel="stylesheet" type="text/css" href="../Style/Item.css">
</head>

<body>
    <form runat="server">
    <header class="HeaderBackground-Style">
        <div class="Body">
            <div class="TitelBox-Style">
                <div class="TitelBox">
                    <div class="TitelMenu-Style">
                        <div class="TitelMenu-Style">
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton3" runat="server">客服中心</asp:LinkButton>
                            <div class="TitelMenuseparate">|</div>
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton4" runat="server">賣家中心</asp:LinkButton>
                            <div class="TitelMenuseparate">|</div>
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton10" runat="server">幫助中心</asp:LinkButton>
                        </div>
                        <div style="flex-grow: 1;"></div>
                        <asp:Panel ID="Panel_TitelMenuLogin" runat="server" CssClass="TitelMenu-Style"></asp:Panel>
                    </div>
                    <div class="Titel-Style" style="flex-grow: 1">
                        <asp:LinkButton CssClass="Titel" ID="LinkButton11" runat="server" href="../Default.aspx">
                            Einkaufen 愛康福
                        </asp:LinkButton>
                        <div class="Titel-Style" style="width: 640px">
                            <div class="InputBox">
                                <asp:TextBox CssClass="TextBox" ID="TB_Search" placeholder="來愛康福購好物～" runat="server" BorderWidth="0px"/>
                                <asp:DropDownList ID="DDL_SearchMode" CssClass="SearchDropDownList" runat="server" BorderWidth="0px">
                                    <asp:ListItem>查商品</asp:ListItem>
                                    <asp:ListItem>查商店</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinkButton CssClass="otherSearchButton" ID="LB_runSearch" runat="server" OnClick="LB_runSearch_Click">
                                    <img src="picture/Inquire.png" width="20" style="display: inline-block; margin: 0 10px;"/>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="Titel-Style" style="flex: 1; height:100%; align-items: center; justify-content: left;">
                            <asp:LinkButton CssClass="otherLoginButton" Width="60" ID="LinkButton13" runat="server" href="../shop/OnShelves.aspx">
                                <div class="otherLoginButtonContent">
                                    <img src="picture/shoppingBag.png" width="30" style="display: inline-block; margin: 0 10px;"/>
                                    <div style="display: inline-block; font-size:14px">賣東西</div>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="otherLoginButton" Width="60" ID="LinkButton14" runat="server" href="../shop/DashBoard.aspx">
                                <div class="otherLoginButtonContent">
                                    <img src="picture/store.png" width="30" style="display: inline-block; margin: 0 10px;"/>
                                    <div style="display: inline-block; font-size:14px">我的商店</div>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="otherLoginButton" Width="60" ID="LinkButton15" runat="server" href="../buyer/ShoppingCart.aspx">
                                <div class="otherLoginButtonContent">
                                    <img src="picture/shopping.png" width="30" style="display: inline-block; margin: 0 10px;"/>
                                    <div style="display: inline-block; font-size:14px">購物車</div>
                                </div>
                            </asp:LinkButton>
                       </div>
                    </div>
                </div>
            </div>
        </div>
    </header>

    <div class="contentBody">
        <div class="Body">
            <div class="DashBoardBox">
                <div class="DashBoardBoxContent">
                    <div class="commodityContent_Box" >
                        <asp:Image ID="commodityThumbnail" CssClass="commodityThumbnail" runat="server" />
                        <hr style="height: 1px; border:0; background:#BBBBBB; width:100%"/>
                        <div class="commodityId">
                            <span>商品編號：</span>
                            <asp:Label ID="commodityId" runat="server" Text=""></asp:Label>
                        </div>
                        <hr style="height: 1px; border:0; background:#BBBBBB; width:100%"/>
                        <div class="shopBuy_Box">
                            <asp:LinkButton ID="LB_shopName" CssClass="shopBuyName" runat="server"></asp:LinkButton>
                            <asp:Label ID="LB_shopEMail" CssClass="shopBuyText" runat="server" Text="商家信箱："></asp:Label>
                            <asp:Label ID="LB_shopPhone" CssClass="shopBuyText" runat="server" Text="商家電話："></asp:Label>
                        </div>
                    </div>
                    <div class="commodityContent_Box" style="flex: 1; padding: 0px 20px" >
                        <asp:Label ID="commodityName" CssClass="commodityName" runat="server" Text=""></asp:Label>
                        <div class="commodityBuy_Box">
                            <div class="commodityPrice_Box">
                                <span class="commodityPriceText">直購價：</span>
                                <asp:Label ID="commodityPrice" CssClass="commodityPrice" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="commodityPrice_Box">
                                <span class="commodityPriceText">數量：</span>
                                <asp:TextBox CssClass="commodityInput" ID="TB_commodityNum" runat="server" AutoPostBack="True" OnTextChanged="TB_commodityNum_TextChanged">1</asp:TextBox>
                                <asp:Label ID="commodityNum" CssClass="commodityNum" runat="server" Text=""></asp:Label>
                                <asp:Label ID="commodityNumLack" CssClass="commodityNum_Lack" runat="server" Text=""></asp:Label>
                            </div>
                            <div class ="commodityBuyButton_Box">
                                <asp:LinkButton CssClass="commodityBuyButton" ID="LB_JoinShoppingCart" runat="server" OnClick="LB_JoinShoppingCart_Click">
                                    <div class="commodityBuyButtonContext">
                                        <div class="commodityBuyButtonIcon" width="30" style="margin-right: 5px;"></div>
                                        <span style="font-size:16px">加入購物車</span>
                                    </div>
                                </asp:LinkButton>
                                <div style="width:10px;"></div>
                                <asp:LinkButton CssClass="commodityBuyButton" ID="LB_ToShopping" runat="server" OnClick="LB_ToShopping_Click">
                                    <span style= "font-size:16px">直接購買</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="commodityIntroductionTitle">
                                商品說明：
                        </div>
                        <asp:Label ID="commodityIntroduction" CssClass="commodityIntroduction" runat="server" Text=""></asp:Label>
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



