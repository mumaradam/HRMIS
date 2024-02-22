<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostBack="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
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
    <link href='https://unpkg.com/boxicons@2.1.1/css/boxicons.min.css' rel='stylesheet'/>
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
            </div>
        </div>
    </nav>
        <br/><br/><br/><br/>
        <div class="container">
            <div class="column" style="float: left; width: 70%; padding: 3px;">
                <h1 class="well" style="width: 164.6%;">Registration Form</h1>
                        
	        <div class="col-lg-12 well" style="margin-bottom: 10px;">
                    <h3 class="card-title media-body" style="color: #3075BA;">Basic Information</h3>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-sm-2 form-group">
                            <label>Employee No.</label>
                            <asp:TextBox type="empno" class="form-control" ID="empno" runat="server" placeholder="Enter ID" name="empno"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6 form-group">
                            <label>Firstname</label>
                            <asp:TextBox type="firstname" class="form-control" ID="firstname" runat="server" placeholder="First Name" name="firstname"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 form-group">
                            <label>Middle Initial</label>
                            <asp:TextBox type="middle" class="form-control" ID="middle" runat="server" placeholder="Middle Initial" name="middle"></asp:TextBox>
                        </div>
                        <div class="col-sm-6 form-group">
                            <label>Last Name</label>
                            <asp:TextBox type="lastname" class="form-control" ID="lastname" runat="server" placeholder="Last Name" name="lastname"></asp:TextBox>
                        </div>
                    </div>   <%--Employee Name--%>     
                    <div class="row">
                        <div class="col-sm-12 form-group">
                            <label>Address</label>
                            <asp:TextBox type="address" class="form-control" ID="address" runat="server" placeholder="Address" name="adress" TextMode="MultiLine" height="156px"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 form-group">
                            <label>Email</label>
                            <asp:TextBox type="email" class="form-control" ID="email" runat="server" placeholder="Email" name="email"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 form-group">
                            <label>Date of Birth</label>
                            <asp:TextBox type="date" ID="birthDate" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 form-group">
                            <label>Contact No.</label>
                            <asp:TextBox type="tel" ID="contactNo" runat="server" class="form-control" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                        </div>
                        <div class="col-sm-3" style="left:12px;">
                            <div class="row">
                                <div "col-sm-12">
                                    <label>Gender</label>
                                    <asp:DropDownList name="gender" ID="gender" runat="server" class="form-control">
                                        <asp:ListItem Enabled="true" Text= "Male" Value= "0"></asp:ListItem>
                                        <asp:ListItem Enabled="true" Text= "Female" Value= "1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div> <%--Address--%>
                    
                </div>
                <div class="col-lg-12 well" style="margin-bottom: 10px;">
                    <h3 class="card-title media-body" style="color: #3075BA;">Employee Information</h3>
                    <div class="row" style="margin-top: 10px;">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-md-12 col-sm-12 form-group">
                                    <div class="row">
                                        <div class="col-sm-4 form-group">
                                            <label>Company Email</label>
                                            <asp:TextBox type="email" class="form-control" ID="comEmail" runat="server" placeholder="Email" name="email"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <label>Department</label>
                                                    <asp:DropDownList name="department" ID="department" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="departmentSelect_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <label>Position</label>
                                                    <asp:DropDownList name="position" ID="position" runat="server" class="form-control" AppendDataBoundItems="False" AutoPostBack="true" OnSelectedIndexChanged="position_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <label>Role</label>
                                                    <asp:DropDownList name="role" ID="role" runat="server" class="form-control" AppendDataBoundItems="False"  AutoPostBack="true" OnSelectedIndexChanged="role_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                    
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <label>Status</label>
                                                    <asp:DropDownList name="status" ID="empstat" runat="server" class="form-control">
                                                        <asp:ListItem Enabled="true" Text= "Type of Employment" Value= "" Selected="True" hidden></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text= "Probationary" Value= "0"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text= "Contractual" Value= "1"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text= "Part-Time" Value= "2"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text= "Regular" Value= "3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                    
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <label>Date Hired</label>
                                                    <asp:TextBox type="date" ID="DOH" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-md-4">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <label>Reporting Head</label>
                                                    <asp:DropDownList name="role" ID="reportHead" runat="server" class="form-control" AppendDataBoundItems="False">
                                                    </asp:DropDownList>
                                                </div>
                                    
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div> <%--DropDownList--%>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="department" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-12 col-sm-12 form-group">
                            <div class="col-sm-5">
                                <div class="row">
                                    <br/>
                                    <asp:Button class="btn btn-success" ID="btnSave" runat="server" Text="Submit" Onclick="btnSave_Click" OnClientClick="Confirm()"/>
                                </div>  
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="column" style="float: left; width: 30%; padding: 3px; top:50px; margin-top: 8.8%;">
                <div class="col-lg-12 well" style="width: 150%;">
                    <div class="row">    
                        <div class="col-xs-9 col-xs-offset-2" style="margin-left:21%;">
		                    <div class="input-group">
                                <div class="input-group-btn search-panel">
                                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                    	                <span id="search_concept">Filter by</span> <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu" role="menu">
                                      <li><a href="#Emp_no">Employee No</a></li>
                                      <li><a href="#Name">Name</a></li>
                                    </ul>
                                </div>
                                <input type="hidden" name="search_param" value="all" id="search_param"/>         
                                <input type="text" class="form-control" name="x" placeholder="Search term..."/>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button"><span class="glyphicon glyphicon-search"></span></button>
                                </span>
                            </div>
                        </div>
	                </div>
                    <div class="table-responsive" style="margin-top:27px;">
                        <asp:GridView ID="dgvEmp" runat="server"  style="width:460px;" class="table table-striped table-bordered" AllowPaging="True" PageSize="19" ShowFooter="false" EnableViewState="False" OnPageIndexChanging="dgvEmp_PageIndexChanging">
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
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
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script src="js/script.js"></script>
</body>
</html>
