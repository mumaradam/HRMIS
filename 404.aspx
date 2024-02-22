<%@ Page Language="C#" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="_404" %>

<!DOCTYPE html>

<html lang="en-us" class="no-js"><head>
    <meta charset="utf-8">
    <title>404 Mega Able bootstrap admin template by Phoenixcoded</title>
    <meta name="description" content="Mega Able 404 Error page design">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="author" content="Phoenixcoded">
    <!-- ================= Favicons ================== -->
    <link rel="icon" type="image/png" href="images/icons/Seiha-Eagle-Philippine-Colorways.ico"/>
    <link rel="shortcut icon" type="image/x-icon" href="images/icons/Seiha-Eagle-Philippine-Colorways.ico" />
    <link rel="icon" type="image/ico" href="images/icons/Seiha-Eagle-Philippine-Colorways.ico"/>
    <!-- ============== Resources style ============== -->
    <link rel="stylesheet" type="text/css" href="assets/pages/extra-pages/404/1/css/style.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" type="text/css" href="assets/icon/font-awesome/css/font-awesome.min.css"/>
</head>

<body class="bubble">
    <form id="form1" runat="server">
        <canvas id="canvasbg" height="490" width="1583"></canvas>
        <canvas id="canvas" height="490" width="1583"></canvas>
        <!-- Your logo on the top left -->
        <asp:LinkButton ID="LinkButton1" runat="server" class="logo-link" title="back home" OnClick="lblbtnDashSide_Click">
            <img class="logo" src="images/Theme_logo.png" alt="Company's logo"/>
        </asp:LinkButton>
        <div class="content">
            <div class="content-box">
                <div class="big-content">
                    <!-- Main squares for the content logo in the background -->
                    <div class="list-square">
                        <span class="square"></span>
                        <span class="square"></span>
                        <span class="square"></span>
                    </div>
                    <!-- Main lines for the content logo in the background -->
                    <div class="list-line">
                        <span class="line"></span>
                        <span class="line"></span>
                        <span class="line"></span>
                        <span class="line"></span>
                        <span class="line"></span>
                        <span class="line"></span>
                    </div>
                    <!-- The animated searching tool -->
                    <i class="fa fa-search color" aria-hidden="true"></i>
                    <!-- div clearing the float -->
                    <div class="clear"></div>
                </div>
                <!-- Your text -->
                <h1>Oops! Error 404 not found.</h1>
                <p>The page you were looking for doesn't exist.
                    <br> We think the page may have moved.</p>
            </div>
        </div>
        <footer class="light">
        </footer>
    </form>
    <script src="assets/pages/extra-pages/404/1/js/jquery.min.js"></script>
    <script src="assets/pages/extra-pages/404/1/js/bootstrap.min.js"></script>
    <!-- Bubble plugin -->
    <script src="assets/pages/extra-pages/404/1/js/bubble.js"></script>



</body></html>
