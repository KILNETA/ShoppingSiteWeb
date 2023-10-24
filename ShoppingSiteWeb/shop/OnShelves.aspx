<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnShelves.aspx.cs" Inherits="ShoppingSiteWeb.shop.OnShelves" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-商品上架</title>
    <link rel="stylesheet" type="text/css" href="../Style/OnShelves.css" />
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
            <div class="RegisterBox">
                <asp:UpdatePanel ID="RegisterBoxContent" class="RegisterBoxContent" runat="server">
                <ContentTemplate>
                    <div style="font-size: 24px; margin-bottom:30px">
                        商品上架
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">商品名稱</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_CommodityName" placeholder="最短4個，最長50個字元" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="50" AutoPostBack="True" OnTextChanged="TB_CommodityName_TextChanged"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_CommodityName" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">商品價格</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_CommodityPrice" placeholder="最多$99,999" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" AutoPostBack="True" MaxLength="5" OnTextChanged="TB_CommodityPrice_TextChanged"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px; display: flex;">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_CommodityPrice" runat="server" Text="　" style="flex-grow: 1"/>
                        </div>
                    </div>
                     <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">商品數量</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_CommodityNum" placeholder="最多99個" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" AutoPostBack="True"  MaxLength="2" OnTextChanged="TB_CommodityNum_TextChanged"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_CommodityNum" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div  class="FieldText">商品簡述</div>
                        <div class="InputField">
                            <asp:TextBox CssClass="TextBox" ID="TB_CommodityIntroduction" placeholder="最長200個字" Height="200px" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="200" TextMode="MultiLine"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div>
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_CommodityIntroduction" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div  class="FieldText">商品圖片</div>
                        <div class="InputField">
                            <asp:TextBox CssClass="TextBox" ID="TB_CommodityThumbnail" placeholder="請填寫有效圖片連結，建議大小 800x800px" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" TextMode="Url" />
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div>
                            <asp:Label CssClass="ErrorMessage" ID="LB_CommodityThumbnail" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">商品圖片預覽</div>
                            <div class="Button" style="margin: 0px 10px">
                                <asp:Button  CssClass="RegisterButton" ID="CommodityThumbnailViewButton" runat="server" Text="預覽圖示" BorderStyle="None" ForeColor="White" OnClick="CommodityThumbnailViewButton_Click"/>
                            </div>
                        </div>
                        <div class="InputField" style="justify-content: center;">
                            <asp:Image CssClass="CommodityThumbnailView" ImageUrl="~/shop/picture/no_image.png" ID="CommodityThumbnailView" runat="server" />
                        </div>
                        <div>
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_CommodityThumbnail" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="Button" style="margin: 0px 60px">
                        <asp:Button  CssClass="RegisterButton" ID="OnShelvesButton" runat="server" Text="上架商品" BorderStyle="None" ForeColor="White" OnClick="OnShelvesButton_Click"/>
                    </div>
                </ContentTemplate>
                </asp:UpdatePanel>
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

