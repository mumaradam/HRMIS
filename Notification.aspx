<%--<%@ Page Title="Notifications" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostBack="true" CodeFile="Notification.aspx.cs" Inherits="HRMIS.Notification" %>--%>
<%@ Page Language="C#" AutoEventWireup="true"  MaintainScrollPositionOnPostBack="true" CodeFile="Notification.aspx.cs" Inherits="Notification" %>
<%--<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="content" class="pmd-content content-area dashboard">
        <div class="container">
            <div class="well" style="height: 90px;">
                <h3 style="color: #3075BA;">Notifications</h3>
                <ol class="breadcrumb pmd-breadcrumb">
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="LinkButton1" runat="server" class="labelStyle" OnClick="lblDash_Click">Dashboard</asp:LinkButton>
                    </li>
                    <li class="breadcrumb-item">
                        <a>Notifications</a>
                    </li>
                </ol>
            </div>
            <div class ="well">
                <div class="section">
                    <ul class="list-group pmd-list">

                    </ul>
                </div>
            </div>
        </div>
    </div>
    
     
</asp:Content>--%>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notifications</title>
    <meta charset="UTF-8"/>
    <meta name="apple-mobile-web-app-capable" content="yes" /> 
    <meta name="mobile-web-app-capable" content="yes"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <link rel="icon" type="image/png" href="images/icons/Seiha-Eagle-Philippine-Colorways.ico"/>
    <link rel="shortcut icon" type="image/x-icon" href="images/icons/Seiha-Eagle-Philippine-Colorways.ico" />
    <link rel="icon" type="image/ico" href="images/icons/Seiha-Eagle-Philippine-Colorways.ico"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <!-- ===== CSS ===== -->
    <link rel="stylesheet" href="style.css"/>
    <link rel="stylesheet" type="text/css" href="css/util.css"/>
	<link rel="stylesheet" type="text/css" href="css/main.css"/>
    <!-- ===== Boxicons CSS ===== -->
    <link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap4-admin-compress.min.css"/>
    <link href='https://unpkg.com/boxicons@2.1.1/css/boxicons.min.css' rel='stylesheet'/>
    <script src='https://kit.fontawesome.com/a076d05399.js'></script>
    <link rel="stylesheet" href="css/font-awesome/css/font-awesome.min.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <nav>
        <div class="nav-bar">
            <i class='bx bx-menu sidebarOpen' ></i>
            <img src="images/Seiha-Eagle-Philippine-Colorways.png" style="height: 50px; width: 50px;" />
            <span class="logo navLogo">
                <asp:LinkButton ID="LinkButton2" runat="server" style="font-size: 25px; font-weight: 500; color: var(--text-color); text-decoration: none;" OnClick="lblDash_Click">HRMIS</asp:LinkButton>
            </span>

            <div class="menu">
                <div class="logo-toggle">
                    <span class="logo"><a href="#">HRMIS</a></span>
                    <i class='bx bx-x siderbarClose'></i>
                </div>

                <ul class="nav-links">
                    <li><asp:LinkButton ID="lblDash" runat="server" class="labelStyle" OnClick="lblDash_Click">Dashboard</asp:LinkButton></li>
                    <li class="btn-group">
                        <a class="dropdown-toggle" data-toggle="dropdown">
                        Admin
                        </a>
                        <ul class="dropdown-menu">
                        <li><asp:LinkButton ID="lblEmployee" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblEmployee_Click">Employee</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lblReg" runat="server" class="labelStyle" OnClick="lblReg_Click" style="color: #242526;">Register</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lblCalendar" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblCalendar_Click">Calendar</asp:LinkButton></li>
                        </ul>
                    </li>
                    <li class="btn-group">
                        <a class="labelStyle dropdown-toggle" data-toggle="dropdown">
                        Leave
                        </a>
                        <ul class="dropdown-menu">
                        <li><asp:LinkButton ID="lblLeave" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblLeave_Click">Leave</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lblLeaveA" runat="server" class="labelStyle" OnClick="lblLeaveA_Click" style="color: #242526;">Leave Application</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lblLeaveC" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblLeaveC_Click">Leave Credits</asp:LinkButton></li>
                        </ul>
                    </li>
                    <li class="btn-group">
                        <a class="labelStyle dropdown-toggle" data-toggle="dropdown">
                        Profile
                        </a>
                        <ul class="dropdown-menu">
                        <li><asp:LinkButton ID="lblAccount" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblAccount_Click">Account</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lblPassword" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblPassword_Click">Password</asp:LinkButton></li>
                        </ul>
                    </li>
                    <li>
                        <div class="align-items-center" style="width:120%">
                            <asp:LinkButton ID="lblNoti" runat="server" class="labelStyle" OnClick="lblNoti_Click">Notifications
                                <span class="badge badge-pill badge-primary ml-2" id="notDis" runat="server" visible="false"><asp:Label ID="notCount" runat="server" Text=""></asp:Label></span>
                            </asp:LinkButton>
                        </div>
                    </li>
                    <li><asp:LinkButton ID="lblLogout" runat="server" class="labelStyle" OnClick="lblLogout_Click">Logout</asp:LinkButton></li>
                </ul>
            </div>

            <div class="darkLight-searchBox">
                <div class="dark-light">
                    <i class='bx bx-moon moon'></i>
                    <i class='bx bx-sun sun'></i>
                </div>
            </div>
        </div>
    </nav>
        <br/><br/><br/><br/>
    <div class="container">
        <div class="well" style="height: 90px;">
            <h3 style="color: #3075BA;">Notifications</h3>
            <ol class="breadcrumb pmd-breadcrumb">
                <li class="breadcrumb-item">
                    <asp:LinkButton ID="LinkButton1" runat="server" class="labelStyle" OnClick="lblDash_Click">Dashboard</asp:LinkButton>
                </li>
                <li class="breadcrumb-item">
                    <a>Notifications</a>
                </li>
            </ol>
        </div>
        <div class ="well">
            <div class="section">
                <ul class="list-group pmd-list">

                </ul>
            </div>
        </div>
    </div>
    </form>
    <footer class="pmd-footer" style=" height: -5.5rem; position: inherit;">
	    <div class="container-fluid">
		    <div class="row">
			    <div class="col-12 align-self-center">
				    <div class="pmd-site-info" style="color: var(--text-color);">
					    © <span class="auto-update-year">2022</span>   <a href="https://uptalk.jp/" target="_blank"><strong style="color: var(--text-color);">SEIHA English Network Philippines Inc.</strong></a>
				    </div>
			    </div>
		    </div>
	    </div>
    </footer>
    <script src="js/script.js"></script>
</body>
</html>
