<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminControl.aspx.cs" Inherits="AdminControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Control</title>
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
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
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
                            <li><asp:LinkButton ID="lblLeave" runat="server" class="labelStyle" style="color: #242526;">Leave</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lblLeaveA" runat="server" class="labelStyle" OnClick="lblLeaveA_Click" style="color: #242526;">Leave Application</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lblLeaveC" runat="server" class="labelStyle" style="color: #242526;">Leave Credits</asp:LinkButton></li>
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
                            <asp:LinkButton ID="LinkButton4" runat="server" class="labelStyle">Notifications
                            <span class="badge badge-pill badge-primary ml-2">5</span>
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
            <div class="container"> <%--Department--%>
                <div class="edit-employee-card">
                    <div class="row">
                        <div class="column" style="margin-left:10px; width:50%;">
                            <h3 class="card-title media-body" style="color: #3075BA;">Department</h3>
				            <div class="row">
					            <div class="col-12 col-md-6 col-lg-3" style="width:auto;">
						            <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
							            <label for="id" class="col-form-label control-label" style="font-size:1.5rem;">Name</label>
							            <asp:TextBox type="text" ID="txtDeptName" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
						            </div>
					            </div>
                                <div class="col-12 col-md-6 col-lg-3" style="width:auto;">
						            <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
							            <label for="id" class="col-form-label control-label" style="font-size:1.5rem;">Value</label>
							            <asp:TextBox type="text" ID="txtDeptValue" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
						            </div>
					            </div>
				            </div>
				            <div class="row">
					            <div class="col-12 mt-3 col-lg-3">
						            <asp:Button class="btn btn-primary pmd-ripple-effect pmd-btn-raised" type="submit" ID="btnSubDept" runat="server" Text="Submit" style="font-size:1.5rem;" OnClick="btnSubDept_Click"/>
					            </div>
				            </div>
                        </div>
                        <div class="column" style="margin-left:10px; width:40%;">
                            <asp:GridView ID="dgvDepartment" runat="server"  style="width:460px;" class="table table-striped table-bordered" AllowPaging="True" PageSize="3" ShowFooter="false" EnableViewState="False" OnPageIndexChanging="dgvDepartment_PageIndexChanging">
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                    </div>
			    </div>
            </div><%-- END of Department--%>
        </div>
        <div class="well">
            <div class="container"> <%--Position--%>
                <div class="edit-employee-card">
                    <div class="row">
                        <div class="column" style="margin-left:10px; width:50%;">
                            <h3 class="card-title media-body" style="color: #3075BA;">Position</h3>
				            <div class="row">
                                <div class="col-12 col-md-6 col-lg-4" style="margin-top:10px;">
                                    <label>Department</label>
                                    <asp:DropDownList name="department" ID="department" runat="server" class="form-control" OnSelectedIndexChanged="department_SelectedIndexChanged" AutoPostBack="True" ViewStateMode="Enabled" EnableViewState="true" >
                                    </asp:DropDownList>
                                </div>
					            <div class="col-12 col-md-6 col-lg-3" style="width:auto; margin-top:8px;">
						            <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
							            <label for="id" class="col-form-label control-label" style="font-size:1.5rem;">Name</label>
							            <asp:TextBox type="text" ID="txtPosName" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
						            </div>
					            </div>
                                <div class="col-12 col-md-6 col-lg-3" style="width:auto;">
						            <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
							            <label for="id" class="col-form-label control-label" style="font-size:1.5rem;">Value</label>
							            <asp:TextBox type="text" ID="txtPosValue" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
						            </div>
					            </div>
				            </div>
				            <div class="row">
					            <div class="col-12 mt-3 col-lg-3">
						            <asp:Button class="btn btn-primary pmd-ripple-effect pmd-btn-raised" type="submit" ID="btnSubPos" runat="server" Text="Submit" style="font-size:1.5rem;" OnClick="btnSubPos_Click"/>
					            </div>
				            </div>
                        </div>
                        <div class="column" style="margin-left:10px; width:40%;">
                            <asp:GridView ID="dgvPosition" runat="server"  style="width:460px;" class="table table-striped table-bordered" AllowPaging="True" PageSize="3" ShowFooter="false" EnableViewState="False" OnPageIndexChanging="dgvPosition_PageIndexChanging">
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                    </div>
			    </div>
            </div><%-- END of Position--%>
        </div>
        <div class="well">
            <div class="container">
                <div class="edit-employee-card">
				    <div class="row">
                        <div class="column" style="margin-left:10px; width:50%;">
                             <h3 class="card-title media-body" style="color: #3075BA;">Role</h3>
                             <div class="row">
                                 <div class="col-12 col-md-6 col-lg-3" style="width:auto;">
						            <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
							            <label for="id" class="col-form-label control-label" style="font-size:1.5rem;">Name</label>
							            <asp:TextBox type="text" ID="txtRoleName" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
						            </div>
					            </div>
                                <div class="col-12 col-md-6 col-lg-3" style="width:auto;">
						            <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
							            <label for="id" class="col-form-label control-label" style="font-size:1.5rem;">Value</label>
							            <asp:TextBox type="text" ID="txtRoleValue" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
						            </div>
					            </div>
                             </div>
                            <div class="row">
                                <div class="col-12 mt-3 col-lg-3" style="width:auto;">
						            <asp:Button class="btn btn-primary pmd-ripple-effect pmd-btn-raised" type="submit" ID="btnSubRole" runat="server" Text="Submit" style="font-size:1.5rem;" OnClick="btnSubRole_Click"/>
                                    <button class="btn btn-outline-secondary pmd-ripple-effect" type="submit" id="reset-employee-info"style="font-size:1.5rem;">Cancel</button> 
					            </div>
                             </div>
                        </div>
                        <div class="column" style="margin-left:10px; width:40%;">
                            <asp:GridView ID="dgvRole" runat="server"  style="width:460px;" class="table table-striped table-bordered" AllowPaging="True" PageSize="3" ShowFooter="false" EnableViewState="False">
                                <PagerStyle CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
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
<script src="js/script.js"></script>
</body>
</html>
