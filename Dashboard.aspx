<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
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
    <script src="https://code.jquery.com/jquery-1.11.1.min.js"></script>
    <!-- ===== CSS ===== -->
    <link rel="stylesheet" href="style.css"/>
    <link rel="stylesheet" type="text/css" href="css/util.css"/>
	<link rel="stylesheet" type="text/css" href="css/main.css"/>
    <!-- ===== Boxicons CSS ===== -->
    <link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap4-admin-compress.min.css"/>
    <link href='https://unpkg.com/boxicons@2.1.1/css/boxicons.min.css' rel='stylesheet'/>
    <link rel="stylesheet" href="css/font-awesome/css/font-awesome.min.css"/>
    <script src='https://kit.fontawesome.com/a076d05399.js'></script>
</head>
<body>
    <form id="form1" runat="server">
     <nav>
        <div class="nav-bar animate">
            <i class='bx bx-menu sidebarOpen' data-toggle="open" data-target="#menu"  aria-expanded="false" aria-label="Toggle navigation">  </i>
            
            <img src="images/Seiha-Eagle-Philippine-Colorways.png" style="height: 50px; width: 50px;" />
            <span class="logo navLogo">
                <asp:LinkButton ID="LinkButton1" runat="server" style="font-size: 25px; font-weight: 500; color: var(--text-color); text-decoration: none;" OnClick="lblDash_Click">HRMIS</asp:LinkButton>
            </span>
            <div class="menu animated">
                <div class="logo-toggle">
                    <span class="logo"><a href="#">SEIHA HRMIS</a></span>
                    <i class='bx bx-x siderbarClose'></i>
                </div>

                <ul class="nav-links">
                    <li><asp:LinkButton ID="lblDash" runat="server" class="labelStyle" OnClick="lblDash_Click">Dashboard</asp:LinkButton></li>
                    <li class="btn-group">
                      <a class="labelStyle dropdown-toggle" data-toggle="dropdown">
                        Admin
                      </a>
                      <ul class="dropdown-menu">
                        <li><asp:LinkButton ID="lblEmployee" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblEmployee_Click">Employee</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lblReg" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblReg_Click">Register</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lblCalendar" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblCalendar_Click">Calendar</asp:LinkButton></li>
                        <li><asp:LinkButton ID="lblBiometric" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblBiometric_Click">Biometric</asp:LinkButton></li>
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
            <div class="darkLight-searchBox menu">
                <div class="dark-light">
                    <i class='bx bx-moon moon'></i>
                    <i class='bx bx-sun sun'></i>
                </div>
                <div style="width: 200px; margin-right:-110px; padding-left:15px; position:relative; display:flex;">
                    <a class='pmd-avatar-list-img'><img class='img-fluid' style='height: 40px; width: 40px;' runat="server" id="UserPic"/></a>
                    <div style="padding-top: 5px;">
                        <asp:LinkButton ID="lblUser" runat="server" style="font-size: 18px; font-weight: 500; color: var(--text-color); text-decoration: none;" class="labelStyle" OnClick="lblAccount_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
         
    </nav>
        <br/><br/><br/><br/>
        <div class="container">
            <h1 class="well">Dashboard</h1>
            <div class="well">
                <div class="row" style="margin-top: 25px;">
                     <div class="col-sm-6 col-lg-6 col-xl-4">
                        <div class="card pmd-card statistic-col new-users-statistic">
                            <div class="card-body media">
                                <i class="pmd-icon-circle pmd-icon-xl md-light">
                                    <img src="images/id.gif" style="height: 62px; width: 62px; border-radius: 50%;" />
                                    <%--<i class='fa fa-users' style='font-size:46px'></i>--%>
                                </i>
                                <div class="media-body">
                                    <asp:Label ID="totEmp" runat="server" Text="00" class="card-title display-3 h2"></asp:Label>
                                    <p class="card-subtitle" style="font-size: 1.875rem;">Employees</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-6 col-xl-4">
                        <div class="card pmd-card statistic-col download-statistic">
                            <div class="card-body media">
                                <i class="pmd-icon-circle pmd-icon-xl md-light">
                                    <img src="images/folder.gif" style="height: 62px; width: 62px; border-radius: 50%;" />
                                    <%--<i class="fa fa-file-text-o" style='font-size:36px'></i>--%>
                                </i>
                                <div class="media-body">
                                    <asp:Label ID="totLeave" runat="server" Text="00" class="card-title display-3 h2"></asp:Label>
                                    <p class="card-subtitle" style="font-size: 1.875rem;">Leaves Filed</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-lg-6 col-xl-4" >
                        <div class="card pmd-card statistic-col visits-statistic">
                            <div class="card-body media">
                                <i class="material-icons pmd-icon-circle pmd-icon-xl md-light">
                                    <img src="images/file.gif" style="height: 62px; width: 62px; border-radius: 50%;" />
                                    <%--<i class="fa fa-clock-o" style='font-size:36px'></i>--%>
                                </i>
                                <div class="media-body">
                                    <asp:Label ID="totLeaveM" runat="server" Text="00" class="card-title display-3 h2"></asp:Label>
                                    <p class="card-subtitle" style="font-size: 1.875rem;">Leaves Filed Today</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="well">
                <div class="row" style="margin-top: 20px;">
                    <div class="col-lg-4 col-sm-6 col-12">
                        <div class="card pmd-card">
                            <!-- Card hedaer -->
                            <div class="card-header pmd-card-border d-flex">
                                <i class="pmd-icon-circle icon-circle-48 border border-primary mr-3 md-dark">
                                    <img src="images/balloons.gif" style="height: 50px; width: 50px;" />
                                    <%--<i class="fa fa-birthday-cake" style='font-size:26px; color:#3075BA;'></i>--%>
                                    
                                </i>
                                <div class="media-body">
                                    <h2 class="card-title h4">Upcoming Birthdays</h2>
                                    <h4 class="card-subtitle" style="font-size: 1.2rem;">List of employees whose birthdays are in the month of <asp:Label ID="lblTmonth" runat="server" Text="Label"></asp:Label></h4>
                                </div>
                            </div>
                            <!-- Card list -->
                            <ul class="list-group pmd-list">
                                <asp:Panel ID="Panel2" runat="server" Height="250px" Width="345px" ScrollBars="Auto">
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                </asp:Panel>
                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <div class="card pmd-card">
                            <!-- Card hedaer -->
                            <div class="card-header pmd-card-border d-flex">
                                <i class="pmd-icon-circle icon-circle-48 border border-primary mr-3 md-dark">
                                    <img src="images/calendar.gif" style="height: 50px; width: 50px;" />
                                    <%--<i class="fa fa-calendar-check-o" style='font-size:26px; color:#3075BA;'></i>--%>
                                </i>
                                <div class="media-body">
                                    <h2 class="card-title h4">This Month Work Anniversaries</h2>
                                    <p class="card-subtitle" style="font-size: 1.2rem;">List of employees with work anniversaries</p>
                                </div>
                            </div>
                            <!-- Card list -->
                            <ul class="list-group pmd-list">
                                <asp:Panel ID="Panel3" runat="server" Height="250px" Width="345px" ScrollBars="Auto">
                                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                </asp:Panel>
                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-6 col-12">
                        <div class="card pmd-card">
                            <!-- Card hedaer -->
                            <div class="card-header pmd-card-border d-flex">
                                <i class="pmd-icon-circle icon-circle-48 border border-primary mr-3 md-dark">
                                    <img src="images/cake.gif" style="height: 50px; width: 50px;" />
                                    <%--<i class="fa fa-birthday-cake" style='font-size:26px; color:#3075BA;'></i>--%>
                                </i>
                                <div class="media-body">
                                    <h2 class="card-title h4">This Month Birthdays</h2>
                                    <h4 class="card-subtitle" style="font-size: 1.2rem;">List of employees whose birthdays this Month</h4>
                                </div>
                            </div>
                            <!-- Card list -->
                            <ul class="list-group pmd-list">
                                <asp:Panel ID="Panel1" runat="server" Height="250px" Width="345px" ScrollBars="Auto">
                                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                </asp:Panel>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="well" runat="server" id="DashLeave">
                <div class="row" style="margin-top: 20px;">
                    <div class="col-12">
                    <!-- Card Component -->
                        <div class="card pmd-card">
                            <!-- Card Header -->
                            <div class="card-header pmd-card-border d-flex align-items-start">
                                <div class="media-body">
                                    <h2 class="card-title h4" style="line-height: 1.8; color: #3075BA; font-size: 2.5rem;">Leave Applications</h2>
                                    <p class="card-subtitle" style="font-size: 1.2rem;">Application of Leaves</p>
                                </div>
                                <asp:Button class="btn pmd-ripple-effect btn-outline-primary ml-auto btn-sm" ID="btnLeaveAll" runat="server" Text="View All" style="font-size: 1.35rem;" OnClick="btnLeaveAll_Click"/>
                            </div>
                            <!-- Card Header End -->
                            <!-- Card Body -->
                            <div class="card-body">
                                <div class="body">
                                    <div class="table-responsive">
                                        <asp:Panel ID="Panel4" runat="server" Height="250px" ScrollBars="Auto" >
                                            <asp:Literal ID="Literal4" runat="server">
                                            </asp:Literal>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <!-- Card Body End -->
                        </div>
                        <!-- Card Component End -->
                    </div>
                </div>
            </div>
<%--            <footer class="well pmd-footer" style=" height: -5.5rem; position: inherit;">
	            <div class="container-fluid">
		            <div class="row">
			            <div class="col-12 align-self-center">
				            <div class="pmd-site-info">
					            © <span class="auto-update-year">2022</span> A <a href="https://pro.propeller.in/" target="_blank"><strong>Propeller Pro </strong></a> Theme
				            </div>
			            </div>
		            </div>
	            </div>
            </footer>--%>
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
    <script>
        $(document).ready(function () {
            $('.sidebarOpen').on('click', function (e) {
                $('.menu').toggleClass("open");
                e.preventDefault();
            });
        });
    </script>
</body>
</html>
