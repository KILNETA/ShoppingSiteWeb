<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ShoppingSiteWeb.shop.Register" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-商店註冊</title>
    <link rel="stylesheet" type="text/css" href="../Style/shopRegister.css" />
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
                        商店註冊
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">商店名稱</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_ShopName" placeholder="最短4個，最長24個字元" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="24" AutoPostBack="True" OnTextChanged="TB_ShopName_TextChanged" />
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_ShopName" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">電子信箱</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_ShopEMail" placeholder="sample@example.com" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" AutoPostBack="True" MaxLength="320" OnTextChanged="TB_ShopEMail_TextChanged"/>
                            <div class="Button" style="margin: 0px 10px">
                                <asp:Button  CssClass="RegisterButton" ID="BT_SendCheckCodeEMail" runat="server" Text="驗證" BorderStyle="None" ForeColor="White" OnClick="BT_SendCheckCodeEMail_Click"/>
                            </div>
                            <asp:TextBox CssClass="TextBox" ID="TB_EMailCheckCode" placeholder="輸入驗證碼" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" AutoPostBack="True" Width="60" style="text-align: center;" OnTextChanged="TB_EMailCheckCode_TextChanged"/>
                            <asp:Image ID="IMG_EMailCheck" runat="server" ImageUrl="picture/verify_fail.png" Height="20" Width="20" style="margin: auto auto auto 10px"/>
                        </div>
                        <div style="margin-left:74px; display: flex;">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_EMail" runat="server" Text="　" style="flex-grow: 1"/>
                            <asp:Label CssClass="NoticeMessage" ID="LB_SendEMailMessage" runat="server" Text="　" style="margin-right: 30px"/>
                        </div>
                    </div>
                     <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">商家電話</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_ShopPhoneNum" placeholder="09XXXXXXXX" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="10"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_ShopPhoneNum" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">出貨地址</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_ShopAddress" placeholder="XX市XX路XX巷XX號XX樓，最長50個字" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="50"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_Address" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="Button" style="margin: 0px 60px">
                        <asp:Button  CssClass="RegisterButton" ID="ShopRegisterButton" runat="server" Text="註冊" BorderStyle="None" ForeColor="White" OnClick="ShopRegisterButton_Click"/>
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
