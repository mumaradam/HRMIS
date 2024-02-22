<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostBack="true" CodeFile="Employee.aspx.cs" Inherits="Employee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee</title>
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
        <nav>
            <div class="nav-bar">
                <i class='bx bx-menu sidebarOpen' ></i>
                <img src="images/Seiha-Eagle-Philippine-Colorways.png" style="height: 50px; width: 50px;" />
                <span class="logo navLogo">
                    <asp:LinkButton ID="LinkButton1" runat="server" style="font-size: 25px; font-weight: 500; color: var(--text-color); text-decoration: none;" OnClick="lblDash_Click">HRMIS</asp:LinkButton>
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
        <div class="well">
            <div>
                <h1>Employee</h1>
            </div>
        </div>
        <div class="well">
            <section class="component-section">
                <div class="card pmd-card">
                    <div class="search-bar card-body">
                        <div class="row d-flex align-items-center">
                            <div class="row" style="flex-basis:0; flex-grow: 1; max-width:100%; position: relative; width: 100%; min-height: 1px; padding-right: 15px; padding-left: 15px;">
                                <div class="d-md-block">
                                    <div class="search-form form-row">
                                        <div class="col-md-4 col-lg-4">
                                            <div class="input-group pmd-input-group pmd-input-group-icon mb-0">
                                                <div class="input-group-prepend" style="bottom:50px;">
                                                    <div class="input-group-text">
                                                        <i class="material-icons md-dark pmd-sm">search</i>
                                                    </div>
                                                </div>
                                                <div class="pmd-textfield pmd-textfield-floating-label" style="padding-top: 2rem;">
                                                    <asp:TextBox ID="txtEmpSearch" runat="server" class="form-control" placeholder="Search" style="font-size:1.8rem; margin-bottom:1px;"></asp:TextBox><span class="pmd-textfield-focused"></span>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="col-md-6 mb-0">
                                            <ContentTemplate>
                                                <div class="form-group pmd-textfield pmd-textfield-floating-label col-md-6 mb-0" style="font-size:2.2rem; padding-top: 2rem;">
                                                    <asp:DropDownList name="department" ID="department" runat="server" class="form-control" style="font-size:1.8rem; height: calc(3.25rem + 1px);" AutoPostBack="true" OnSelectedIndexChanged="dpt_SelectedIndexChanged">
                                                        
                                                        <%--<asp:ListItem Enabled="true" Text= "Teacher" Value= "0"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text= "Admin" Value= "1"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text= "IT" Value= "2"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text= "Manager" Value= "3"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text= "Operation Manager" Value= "4"></asp:ListItem>--%>
                                                    </asp:DropDownList>    
                                                    <span class="pmd-textfield-focused"></span>
                                                </div>
                                                <div class="form-group pmd-textfield pmd-textfield-floating-label col-md-6 mb-0" style="font-size:2.2rem; padding-top: 2rem;">
                                                    <asp:DropDownList name="position" ID="position" runat="server" class="form-control" style="font-size:1.8rem; height: calc(3.25rem + 1px);" placeholder="Search" >
                                                        <%--<asp:ListItem Enabled="true" Text= "None" Value= "0"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text= "Leader" Value= "1"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text= "Manager" Value= "2"></asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <span class="pmd-textfield-focused"></span>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="department" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <div class="col-md-1 col-lg-1 mt-3" style="padding-top: 1rem;">
                                            <asp:Button ID="btnSearch" runat="server" Text="Go" class="btn btn-primary pmd-ripple-effect pmd-btn-raised" style="font-size: 1.5rem;" onClick="btnSearch_Click"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                
            </section>
            <div class="tab-content">
                    <div class="tab-pane active">
                        <div class="row">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" style="width: 100%;" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel3" runat="server" Height="850px" ScrollBars="Auto" >
                                        <asp:Literal ID="Literal3" runat="server">
                                        </asp:Literal>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />    
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
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
<script type="text/javascript">

    // Advanced Search Toggle
    $("#advanced-search-toggler").click(function () {
        $(".search-form").toggle();
        $(".advanced-search-title").toggle();
    });

    // Filter Tags Toggle
    $("#apply-filter").click(function () {
        $(".filter-tags").show();
    });

    // Date of Joining Datepicker
    $('#datepicker').datetimepicker({
        format: 'DD-MM-YYYY'
    });
    //window.onload = function () { check_for_hover() };
    //function check_for_hover() {
    //    var hover_element = document.getElementById('hover_el');
    //    var hover_status = (getStyle(hover_element, 'border-style') === 'dashed') ? true : false;
    //    document.getElementById('display').innerHTML = 'you are' + (hover_status ? '' : ' not') + ' hovering';
    //    setTimeout(check_for_hover, 1000);
    //};
</script>
<script src="js/script.js"></script>
<script>
    $(document).ready(function () {
        var sPath = window.location.pathname;
        var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
        $(".pmd-sidebar-nav").each(function () {
            $(this).find("a[href='" + sPage + "']").parents(".collapse").addClass("show");
            $(this).find("a[href='" + sPage + "']").parents(".collapse").prev('a.nav-link').addClass("active");
            $(this).find("a[href='" + sPage + "']").addClass("active");
        });

        $(".auto-update-year").html(new Date().getFullYear());
    });
</script>
</body>
</html>
