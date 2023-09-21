<%@ Page Title="登入" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShoppingSiteWeb.buyer.Login" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-登入</title>
    <link rel="stylesheet" type="text/css" href="../Style/Login.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>
    <script type="text/javascript" src='../Scripts/WebFuns/jquery.qrcode.js'></script>
    <script type="text/javascript" src='../Scripts/WebFuns/qrcode.js'></script>
    <script type="text/javascript" src='../Scripts/WebFuns/Login.js'></script>
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
            <img src="picture/MainActivity.png" width="500" height="500" alt="一張圖片" style="margin: auto; margin-right: 50px;"/>
            <div class="LoginBox">
                <div class="LoginBoxContent">
                    <div style="font-size: 24px; margin-bottom:30px">
                        登入
                    </div>
                    <div class="InputBox">
                        <asp:TextBox CssClass="TextBox" ID="TB_User" placeholder="使用者名稱/E-mail" runat="server" BorderWidth="1px" onBlur="inputBoxIsNullOrEmpty(this, ErrorLB_1)"/>
                        <div>
                            <asp:Label CssClass="ErrorMessage" ID="ErrorLB_1" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="InputBox">
                        <asp:TextBox CssClass="TextBox" ID="TB_Password" placeholder="密碼" runat="server" BorderWidth="1px" TextMode="Password" onBlur="inputBoxIsNullOrEmpty(this, ErrorLB_2)"/>
                        <div>
                            <asp:Label CssClass="ErrorMessage" ID="ErrorLB_2" runat="server" Text="　"/>
                        </div>
                    </div>
                    <div class="Button">
                        <asp:Button  CssClass="LoginButton" ID="LoginButton" runat="server" Text="登入" BorderStyle="None" ForeColor="White" OnClick="LoginButton_Click" />
                    </div>

                    <div style="display:flex; margin-top: 5px;">
                        <div style="flex-grow: 1; font-size:12px">
                            <asp:LinkButton CssClass="generalLink" ID="LinkButton5" runat="server">忘記密碼</asp:LinkButton>
                        </div>
                        <div style="flex-grow: 1; text-align: end; font-size:12px">
                            <div style="display: inline-block;">
                                新朋友？
                            </div>
                            <asp:LinkButton CssClass="generalLink" ID="RegisterButton" runat="server" href="../buyer/Register.aspx">註冊</asp:LinkButton>
                        </div>
                    </div>

                    <div class="Button" style="width: 100%; display:flex; flex-direction: row; margin-top: 5px;">
                        <hr style="height: 1px; border:0; background:#9f9f9f; flex-grow: 1;"/>
                         <div style="margin: 0px 10px">或</div>
                        <hr style="height: 1px; border:0; background:#9f9f9f; flex-grow: 1;"/>
                    </div>

                    <div style="width: 100%; flex-grow: 1; margin-top: 10px; display:flex; flex-direction: row; font-size:0">
                        
                        <div style="flex-grow: 1;" >
                            <div class="otherLoginBox">
                                <div id="LoginQRcode"></div>
                                <div style="font-size: 16px; text-align: center">使用QRcode登入</div>
                            </div>
                        </div>

                        <div style="flex-grow: 1; display:flex; align-items: flex-end; flex-direction: column;">
                            <div style="margin-bottom: 10px">
                                 <asp:LinkButton CssClass="otherLoginButton" Height="20px" ID="LinkButton1" runat="server">
                                    <div class="otherLoginButtonContent">
                                        <img src="picture/facebook_logo.png" width="20" style="display: inline-block; margin: 0 10px;"/>
                                        <div style="display: inline-block; font-size:14px">Facebook</div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                            <div style="margin-bottom: 10px">
                                <asp:LinkButton CssClass="otherLoginButton" Height="20px" ID="LinkButton2" runat="server">
                                    <div class="otherLoginButtonContent">
                                        <img src="picture/google_logo.png" width="20" style="display: inline-block; margin: 0 10px;"/>
                                        <div style="display: inline-block; font-size:14px">Google</div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                            <div style="margin-bottom: 10px">
                                <asp:LinkButton CssClass="otherLoginButton" Height="20px" ID="LinkButton3" runat="server">
                                    <div class="otherLoginButtonContent">
                                        <img src="picture/line_logo.png" width="20" style="display: inline-block; margin: 0 10px;"/>
                                        <div style="display: inline-block; font-size:14px">LINE</div>
                                    </div>
                                </asp:LinkButton>
                            </div>
                        </div>
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