<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ShoppingSiteWeb.buyer.Register" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-會員註冊</title>
    <link rel="stylesheet" type="text/css" href="../Style/Register.css">
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
                        註冊
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">用戶名稱</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_UserName" placeholder="最短4個，最長24個字元" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="24" OnTextChanged="TB_UserName_TextChanged" AutoPostBack="True" />
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_UserName" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">電子信箱</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_EMail" placeholder="sample@example.com" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" AutoPostBack="True" OnTextChanged="TB_EMail_TextChanged" MaxLength="320"/>
                            <div class="Button" style="margin: 0px 10px">
                                <asp:Button  CssClass="RegisterButton" ID="BT_SendCheckCodeEMail" runat="server" Text="驗證" BorderStyle="None" ForeColor="White" OnClick="BT_SendCheckCodeEMail_Click"/>
                            </div>
                            <asp:TextBox CssClass="TextBox" ID="TB_EMailCheckCode" placeholder="輸入驗證碼" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" AutoPostBack="True" Width="60" style="text-align: center;" OnTextChanged="TB_EMailCheckCode_TextChanged" />
                            <asp:Image ID="IMG_EMailCheck" runat="server" ImageUrl="picture/verify_fail.png" Height="20" Width="20" style="margin: auto auto auto 10px"/>
                        </div>
                        <div style="margin-left:74px; display: flex;">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_EMail" runat="server" Text="　" style="flex-grow: 1"/>
                            <asp:Label CssClass="NoticeMessage" ID="LB_SendEMailMessage" runat="server" Text="　" style="margin-right: 30px"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">密　　碼</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_Password" placeholder="6 ~ 18 字元，至少搭配 1 個英文字母。" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" AutoPostBack="True" OnTextChanged="TB_Password_TextChanged" TextMode="Password"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_Password" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">確認密碼</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_PasswordCheck" placeholder="" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" AutoPostBack="True" OnTextChanged="TB_PasswordCheck_TextChanged" TextMode="Password"/>
                            <asp:Image ID="IMG_PasswordCheck" runat="server" ImageUrl="picture/verify_fail.png" Height="20" Width="20" style="margin: auto auto auto 10px"/>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_PasswordCheck" runat="server" Text="　"/>
                        </div>
                    </div>
                    <hr style="width:100%; height: 0.1px; background:#000000; flex-grow: 1;"/>
                    <div style="font-size: 20px; margin:10px 0px 20px 0px">
                        基本資料
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">姓　　名</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_RealName" placeholder="真實姓名，最長20個字" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="20"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_RealName" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">行動電話</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_PhoneNum" placeholder="09XXXXXXXX" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="10"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_PhoneNum" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField">
                            <div  class="FieldText">地　　址</div>
                            <asp:TextBox CssClass="TextBox" ID="TB_Address" placeholder="XX市XX路XX巷XX號XX樓，最長50個字" runat="server" BorderWidth="1px" onBlur="UserBoxHasError(this)" MaxLength="50"/>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_Address" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <div class="InputField" style="align-items:flex-start">
                            <div class="FieldText">生　　日</div>
                            <asp:DropDownList ID="DDL_BirthdayYear" CssClass="DropDownList" runat="server" style="font-size: large" AutoPostBack="True" OnSelectedIndexChanged="DDL_BirthdayYear_SelectedIndexChanged">
                                <asp:ListItem>－</asp:ListItem>
                            </asp:DropDownList>
                            <div style="font-size: 16px; margin:auto 10px">年</div>
                            <asp:DropDownList ID="DDL_BirthdayMonth" CssClass="DropDownList" runat="server" style="font-size: large" AutoPostBack="True" OnSelectedIndexChanged="DDL_BirthdayMonth_SelectedIndexChanged">
                                <asp:ListItem>－</asp:ListItem>
                            </asp:DropDownList>
                            <div style="font-size: 16px; margin:auto 10px">月</div>
                            <asp:DropDownList ID="DDL_BirthdayDay" CssClass="DropDownList" runat="server" style="font-size: large" AutoPostBack="True">
                                <asp:ListItem>－</asp:ListItem>
                            </asp:DropDownList>
                            <div style="font-size: 16px; margin:auto 10px">日</div>
                            <div style="margin-right: 30px"></div>
                        </div>
                        <div style="margin-left:74px">
                            <asp:Label CssClass="ErrorMessage" ID="LB_ErrorMessage_BirthdayDate" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="Button" style="margin: 0px 60px">
                        <asp:Button  CssClass="RegisterButton" ID="RegisterButton" runat="server" Text="註冊" BorderStyle="None" ForeColor="White" OnClick="RegisterButton_Click"/>
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
    <asp:DetailsView ID="useCheckUserNameTable" runat="server" Height="50px" Width="125px" Visible="False"></asp:DetailsView>
    <asp:DetailsView ID="useCheckEmailTable" runat="server" Height="50px" Width="125px" Visible="False"></asp:DetailsView>
    <asp:DetailsView ID="useCheckRegisterTable" runat="server" Height="50px" Width="125px" Visible="False"></asp:DetailsView>
</form>
</body>
</html>
