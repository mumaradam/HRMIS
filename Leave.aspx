<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Leave.aspx.cs" MaintainScrollPositionOnPostBack="true"  Inherits="Leave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave List</title>
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
    <script src='https://kit.fontawesome.com/a076d05399.js'></script>
    <link rel="stylesheet" href="css/font-awesome/css/font-awesome.min.css"/>
    
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="https://netdna.bootstrapcdn.com/bootstrap/2.3.2/js/bootstrap.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.2.0/css/datepicker.min.css" rel="stylesheet"/>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.2.0/js/bootstrap-datepicker.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <nav>
        <div class="nav-bar">
            <i class='bx bx-menu sidebarOpen' ></i>
            <img src="images/Seiha-Eagle-Philippine-Colorways.png" style="height: 50px; width: 50px;" />
            <span class="logo navLogo">
                <asp:LinkButton ID="LinkButton2" runat="server" style="font-size: 25px; font-weight: 500; color: var(--text-color); text-decoration: none;" OnClick="lblDash_Click">HRMIS</asp:LinkButton>
            </span>

            <div class="menu">
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
                        <li><asp:LinkButton ID="LinkButton6" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblAccount_Click">Account</asp:LinkButton></li>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="well">
            <h1 style="color:#3075BA;">Leave</h1>
            <div aria-label="breadcrumb">
                <ol class="breadcrumb pmd-breadcrumb mb-0">
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="LinkButton1" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblDash_Click">Dashboard</asp:LinkButton>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Leave</li>
                </ol>
            </div>
        </div>
        <div class="well">
            <div class="row" style="margin-top: 20px;">
                <div class="col-12">
                <!-- Card Component -->
                    <h4 style="color: #3075BA;">Leave Filed</h4>
                    <div class="card pmd-card">
                        <!-- Card Body -->
                        <div class="card-body">
                            <div class="form-row" style="background-color:lightgray;">
                                <span style="margin-top:15px; margin-left:20px;">Search by Date</span>
                                <div class="col-md-2 mb-3" style="margin-left:20px; margin-top:5px;">
                                    <asp:TextBox type="text" ID="datepicker1" runat="server" class="form-control" name="datepicker" style="font-size:1.6rem; line-height: 1;"></asp:TextBox>
                                </div>
                                <asp:Button ID="btnSearchOwn" runat="server" Text="Search" class="btn btn-primary pmd-ripple-effect pmd-btn-raised" style="font-size: 1.5rem;" OnClick="btnSearchOwn_Click"/>
                                <asp:Button ID="btnResetOwn" runat="server" Text="Reset" class="btn btn-warning pmd-ripple-effect pmd-btn-raised" style="font-size: 1.5rem; margin-left:10px;" OnClick="btnResetOwn_Click"/>
                            </div>
                            <div class="body">
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Auto" >
                                                <asp:Literal ID="Literal1" runat="server">
                                                </asp:Literal>
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSearchOwn" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnResetOwn" EventName="Click" />      
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <!-- Card Body End -->
                    </div>
                    <!-- Card Component End -->
                </div>
                <div class="col-12"  id="AllLeave" runat="server">
                    <h4 style="color: #3075BA;">All Leave Filed</h4>
                <!-- Card Component -->
                    <div class="card pmd-card">
                        <!-- Card Body -->
                        <div class="card-body">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-row" style="background-color:lightgray;">
                                            <asp:DropDownList ID="searchBy" runat="server" class="col-md-2 mb-3" style="height:35px; margin-left:15px; margin-top:5px; width:100px;" AutoPostBack="true" OnSelectedIndexChanged="searchBy_SelectedIndexChanged">
                                                <asp:ListItem Text="Search...." Selected="True" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Search by Date" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Search by Name" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Search by Department" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                         
                                            <%--<span style="margin-top:12px; margin-left:20px;">Search by Date</span>--%>
                                            <div class="col-md-2 mb-3" style="margin-left:15px; margin-top:5px; width:200px;" runat="server" id="sDate" visible="false">
                                                <asp:TextBox type="text" ID="datepicker" runat="server" class="form-control" name="datepicker" style="font-size:1.6rem; line-height: 1;" placeholder="MM-YYYY"></asp:TextBox>
                                            </div>
                                            <%--<span style="margin-top:12px; margin-left:50px;">Search by Name</span>--%>
                                            <div class="col-md-2 mb-3" style="margin-left:15px; margin-top:5px;" runat="server" id="sName" visible="false">
                                                <asp:TextBox type="text" ID="searchAllName" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1; " placeholder="Name"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 mb-3" style="width:105px; margin-left:15px; margin-top:5px;" runat="server" id="sDept" visible="false">
                                                <asp:DropDownList ID="DropDownList1" runat="server" class="col-md-2 mb-3" style="margin-left:15px; margin-top:5px; width:100px;">
                                                    <asp:ListItem Text="Search...." Selected="True" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Search by Date" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Search by Name" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Search by Department" Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div style="margin-top:8px;" runat="server" id="sAllBtn" visible="false">
                                                <asp:Button ID="btnSearchAll" runat="server" Text="Search" class="btn btn-primary pmd-ripple-effect pmd-btn-raised" style="font-size: 1.5rem;" OnClick="btnSearchAll_Click"/>
                                                <asp:Button ID="btnResetAll" runat="server" Text="Reset" class="btn btn-warning pmd-ripple-effect pmd-btn-raised" style="font-size: 1.5rem; margin-left:10px;" OnClick="btnResetAll_Click"/>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>     
                                        <asp:AsyncPostBackTrigger ControlID="searchBy" EventName="SelectedIndexChanged" />  
                                    </Triggers>
                                </asp:UpdatePanel>
                            <div class="body">
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel4" runat="server" Height="350px" ScrollBars="Auto" >
                                                <asp:Literal ID="Literal4" runat="server">
                                                </asp:Literal>
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSearchAll" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="btnResetAll" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <!-- Card Body End -->
                    </div>
                    <!-- Card Component End -->
                </div>
                <div class="col-12"  id="Div1" runat="server">
                    <h4 style="color: #3075BA;">All Approved Leave Filed</h4>
                <!-- Card Component -->
                    <div class="card pmd-card">
                        <!-- Card Body -->
                        <div class="card-body">
                            <div class="form-row" style="background-color:lightgray;">
                                <span style="margin-top:15px; margin-left:20px;">Search by Date</span>
                                <div class="col-md-2 mb-3" style="margin-left:20px; margin-top:5px;">
                                    <asp:TextBox type="text" ID="TextBox1" runat="server" class="form-control" name="datepicker" style="font-size:1.6rem; line-height: 1;"></asp:TextBox>
                                </div>
                                <asp:Button ID="Button1" runat="server" Text="Search" class="btn btn-primary pmd-ripple-effect pmd-btn-raised" style="font-size: 1.5rem;" OnClick="btnSearchOwn_Click"/>
                                <asp:Button ID="Button2" runat="server" Text="Reset" class="btn btn-warning pmd-ripple-effect pmd-btn-raised" style="font-size: 1.5rem; margin-left:10px;" OnClick="btnResetOwn_Click"/>
                            </div>
                            <div class="body">
                                <div class="table-responsive">
                                    <asp:Panel ID="Panel2" runat="server" Height="350px" ScrollBars="Auto" >
                                        <asp:Literal ID="Literal2" runat="server">
                                        </asp:Literal>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <!-- Card Body End -->
                    </div>
                    <!-- Card Component End -->
                </div>
                <div class="col-12"  id="Div2" runat="server">
                    <h4 style="color: #3075BA;">All Denied Leave Filed</h4>
                <!-- Card Component -->
                    <div class="card pmd-card">
                        <!-- Card Body -->
                        <div class="card-body">
                            <div class="form-row" style="background-color:lightgray;">
                                <span style="margin-top:15px; margin-left:20px;">Search by Date</span>
                                <div class="col-md-2 mb-3" style="margin-left:20px; margin-top:5px;">
                                    <asp:TextBox type="text" ID="TextBox2" runat="server" class="form-control" name="datepicker" style="font-size:1.6rem; line-height: 1;"></asp:TextBox>
                                </div>
                                <asp:Button ID="Button3" runat="server" Text="Search" class="btn btn-primary pmd-ripple-effect pmd-btn-raised" style="font-size: 1.5rem;" OnClick="btnSearchOwn_Click"/>
                                <asp:Button ID="Button4" runat="server" Text="Reset" class="btn btn-warning pmd-ripple-effect pmd-btn-raised" style="font-size: 1.5rem; margin-left:10px;" OnClick="btnResetOwn_Click"/>
                            </div>
                            <div class="body">
                                <div class="table-responsive">
                                    <asp:Panel ID="Panel3" runat="server" Height="350px" ScrollBars="Auto" >
                                        <asp:Literal ID="Literal3" runat="server">
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
        $("#datepicker").datepicker({
        format: "MM-yyyy",
        startView: "months",
        minViewMode: "months",
        minViewMode: "months"
        });
        $("#datepicker1").datepicker({
            format: "MM-yyyy",
            startView: "months",
            minViewMode: "months",
            minViewMode: "months"
        });
        dp.on('changeMonth', function (e) {
            //do something here
            alert("Month changed");
        });
    </script>

</body>
</html>
