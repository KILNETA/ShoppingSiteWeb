<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ShoppingSiteWeb.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福</title>
    <link rel="stylesheet" type="text/css" href="Style/Default.css">
    <script type="text/javascript" src="Scripts/WebFuns/Default.js"></script>
</head>

<body>
    <form runat="server">
    <header class="HeaderBackground-Style">
        <div class="Body">
            <div class="TitelBox-Style">
                <div class="TitelBox">
                    <div class="TitelMenu-Style">
                        <div class="TitelMenu-Style">
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton5" runat="server">客服中心</asp:LinkButton>
                            <div class="TitelMenuseparate">|</div>
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton1" runat="server">賣家中心</asp:LinkButton>
                            <div class="TitelMenuseparate">|</div>
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton2" runat="server">幫助中心</asp:LinkButton>
                        </div>
                        <div style="flex-grow: 1;"></div>
                        <asp:Panel ID="Panel_TitelMenuLogin" runat="server" CssClass="TitelMenu-Style"></asp:Panel>
                    </div>
                    <div class="Titel-Style" style="flex-grow: 1">
                        <asp:LinkButton CssClass="Titel" ID="LB_Titel" runat="server" PostBackUrl="~/Default.aspx">
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
                                    <img src="Default_Picture/Inquire.png" width="20" style="display: inline-block; margin: 0 10px;"/>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="Titel-Style" style="flex: 1; height:100%; align-items: center; justify-content: left;">
                            <asp:LinkButton CssClass="otherLoginButton" Width="60" ID="LinkButton9" runat="server" PostBackUrl="~/shop/OnShelves.aspx">
                                <div class="otherLoginButtonContent">
                                    <img src="Default_Picture/shoppingBag.png" width="30" style="display: inline-block; margin: 0 10px;"/>
                                    <div style="display: inline-block; font-size:14px">賣東西</div>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="otherLoginButton" Width="60" ID="LinkButton6" runat="server" PostBackUrl="~/shop/DashBoard.aspx">
                                <div class="otherLoginButtonContent">
                                    <img src="Default_Picture/store.png" width="30" style="display: inline-block; margin: 0 10px;"/>
                                    <div style="display: inline-block; font-size:14px">我的商店</div>
                                </div>
                            </asp:LinkButton>
                            <asp:LinkButton CssClass="otherLoginButton" Width="60" ID="LinkButton8" runat="server" PostBackUrl="~/buyer/ShoppingCart.aspx">
                                <div class="otherLoginButtonContent">
                                    <img src="Default_Picture/shopping.png" width="30" style="display: inline-block; margin: 0 10px;"/>
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
            <div style="height:250px; width:1000px; margin:25px auto; display:flex; justify-content: center; position: relative; overflow: hidden;">
                <ul id="CarouselView" style="display: flex; position: absolute; width:100%; height:100%; padding:0px; transition: transform 0.5s;">
                    <li><img src="Default_Picture/DefaultCarousel/DefaultCarousel_1.png" alt="img1"/></li>
                    <li><img src="Default_Picture/DefaultCarousel/DefaultCarousel_2.png" alt="img2"/></li>
                    <li><img src="Default_Picture/DefaultCarousel/DefaultCarousel_3.png" alt="img3"/></li>
                    <li><img src="Default_Picture/DefaultCarousel/DefaultCarousel_4.png" alt="img4"/></li>
                    <li><img src="Default_Picture/DefaultCarousel/DefaultCarousel_5.png" alt="img5"/></li>
                    <li><img src="Default_Picture/DefaultCarousel/DefaultCarousel_6.png" alt="img6"/></li>
	            </ul>
                <div id="CarouselNext"></div>
                <div id="CarouselPrev"></div>
                <ul id="button">
			        <li><div style="background: #004469C0;"></div></li>
			        <li><div></div></li>
			        <li><div></div></li>
			        <li><div></div></li>
			        <li><div></div></li>
			        <li><div></div></li>
	            </ul>
            </div>
        </div>
    </div>
    <div style="background:#E7E7E7; height:1px;"></div>
    <div class="contentBody" style="background:#F0F0F0; padding: 20px 0px">
        <div class="Body" style="display: flex; flex-direction: column;">
            <div class="commodityAllTitle">
                向您推薦
            </div>
            <asp:Panel ID="Panel_CommodityPage" runat="server" CssClass="CommodityPage"></asp:Panel>
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