<%@ Page Title="登入" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShoppingSiteWeb.buyer.Login" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>Einkaufen 愛康福-登入</title>

    <style type="text/css">
        html,body {
            margin: 0;
            height: 100%;
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

        div.contentBody{
            flex-grow: 1;
            background-image: url("picture/login_background.jpg");
            background-size: cover;
            background-position: center;
        }

        .HeaderBackground-Style {
            background-color: #004469;
            width: 100%;
            height: 90px;
            float: left;
        }

        .FooterBackground-Style {
            background-color: #CEDDE0;
            width: 100%;
            height: 240px;
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
        }
    </style>
</head>


<body>
    <header class="HeaderBackground-Style">
        <div class="Body">
            <div class="TitelBox-Style">
                <p class="Titel-Style">Einkaufen 愛康福</p>
            </div>
        </div>
    </header>


    <div class="contentBody">
        <div class="Body">
            hello world!!
        </div>
    </div>

    <footer class="FooterBackground-Style">
        <div class="Body">
            <p>&copy; <%: DateTime.Now.Year %> - Einkaufen 愛康福</p>
        </div>
    </footer>
</body>
</html>