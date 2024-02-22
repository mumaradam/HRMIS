<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostBack="true" CodeFile="LeaveCredits.aspx.cs" Inherits="LeaveCredits" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Credits</title>
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
    <link rel="stylesheet" href="css/font-awesome/css/font-awesome.min.css"/>
    <link href='https://unpkg.com/boxicons@2.1.1/css/boxicons.min.css' rel='stylesheet'/>
    <script src='https://kit.fontawesome.com/a076d05399.js'></script>
    
   <script type="text/javascript">
       function displayCredit(id) {
           ///document.getElementById('empNum').value = id;
          var table = document.getElementById('table'), rIndex;
          table.rows[id].onclick = function () {
               rIndex = this.rowIndex;
               console.log(rIndex);
               document.getElementById('empNum').value = this.cells[0].innerHTML;
               document.getElementById('fullName').value = this.cells[1].innerHTML;
               document.getElementById('emPos').value = this.cells[4].innerHTML;
               document.getElementById('empDOHired').value = this.cells[2].innerHTML;
               document.getElementById('empDept').value = this.cells[3].innerHTML;
               document.getElementById('empPos').value = this.cells[5].innerHTML;
               document.getElementById('empRol').value = this.cells[6].innerHTML;
               document.getElementById('empRepH').value = this.cells[7].innerHTML;
               document.getElementById('empStat').value = this.cells[8].innerHTML;
               document.getElementById('vleav').value = this.cells[9].innerHTML;
               document.getElementById('uvleav').value = this.cells[9].innerHTML;
               document.getElementById('scleav').value = this.cells[10].innerHTML;
               document.getElementById('uscleav').value = this.cells[10].innerHTML;
               document.getElementById('lleav').value = this.cells[11].innerHTML;
               document.getElementById('ulleav').value = this.cells[11].innerHTML;
               document.getElementById('spleav').value = this.cells[12].innerHTML;
               document.getElementById('uspleav').value = this.cells[12].innerHTML;
               event.stopPropagation();
               document.getElementById("Panel1").style.display = "none";
           };
        }
   </script>
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript">
                function openEmployee(id) {
                    PageMethods.fillInfo(id);
                }
        </script>
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
            <h1>Leave Credits</h1>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="well" id="listEmp" runat="server">
                    <section style="width: 1170px; padding-right: 15px; padding-left: 15px; margin-right: auto; margin-left: auto;" runat="server" id="adminSection">
                        <asp:Button class="btn btn-warning" ID="btnReset" runat="server" OnClick="btnReset_Click" OnClientClick="ResetAllCredit()" Text="Reset All Leave" style="border: 1px solid transparent; padding: 6px 12px; font-size: 14px; line-height: 1.42857143; border-radius: 4px; touch-action: manipulation; color: #fff;"/>
                    </section>
                    <div class="card-body">
                        <div class="body">
                            <div class="table-responsive">
                                <asp:Panel ID="Panel4" runat="server" Height="600px" ScrollBars="Auto" >
                                    <asp:Literal ID="Literal4" runat="server">
                                    </asp:Literal>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
             </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnuvleav" EventName="Click" /> 
                <asp:AsyncPostBackTrigger ControlID="btnuscleav" EventName="Click" /> 
                <asp:AsyncPostBackTrigger ControlID="btnulleav" EventName="Click" /> 
                <asp:AsyncPostBackTrigger ControlID="btnuspleav" EventName="Click" />   
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />    
            </Triggers>
         </asp:UpdatePanel>
                <div class="well" id="forDisplayInfo" runat="server">
                    <div class="page-content profile-view">
                        <div class="card pmd-card">
                            <div class="card-body">
                                <div class="details-tab">
                                    <div class="row view-basic-card">
                                        <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Employee No</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;">
                                                <strong>
                                                <input type="text" name="empNum" id="empNum" readonly="true" runat="server"/>
                                                </strong>
								            </p>
							            </div>
                                        <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Employee Name</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><input type="text" name="fullName" id="fullName" readonly="true"/></strong></p>
							            </div>
                                        <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Position</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><input type="text" name="emPos" id="emPos" readonly="true"/></strong></p>
							            </div>
                                        <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Date of Joining</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><input type="text" name="empDOHired" id="empDOHired" readonly="true"/></strong></p>
							            </div>
                                    </div>
                                </div>
                                <div class="details-tab">
                                    <div class="row view-employee-card">
                                        <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Department</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><input type="text" name="empDept" id="empDept" readonly="true"/></strong></p>
							            </div>
                                        <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Position</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><input type="text" name="empPos" id="empPos" readonly="true"/></strong></p>
							            </div>
							            <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Role</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><input type="text" name="empRol" id="empRol" readonly="true"/></strong></p>
							            </div>
                                        <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Reporting Head</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><input type="text" name="empRepH" id="empRepH" readonly="true"/></strong></p>
							            </div>
							            <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Type of Employee</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><input type="text" name="empStat" id="empStat" readonly="true"/></strong></p>
							            </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="details-tab">
                                            <h4 style="color: #3075BA;">Leave Credits</h4>
                                            <table class="table table-bordered" style="margin-top: 10px;">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center">Vacation Leave</th>
                                                        <th style="text-align: center">Sick Leave</th>
                                                        <th style="text-align: center">Loyalty Leave</th>
                                                        <th style="text-align: center">Special Leave</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr style="font-weight:600;">
                                                        <td style="text-align: center;">
                                                            <input type="text" name="vleav" id="vleav" readonly="true" value="0" style="text-align: center;" runat="server"/></td>
                                                        <td style="text-align: center;">
                                                            <input type="text" name="scleav" id="scleav" readonly="true" value="0" style="text-align: center;" runat="server"/></td>
                                                        <td style="text-align: center;">
                                                            <input type="text" name="lleav" id="lleav" readonly="true" value="0" style="text-align: center;" runat="server"/></td>
                                                        <td style="text-align: center;">
                                                            <input type="text" name="spleav" id="spleav" readonly="true" value="0" style="text-align: center;" runat="server"/></td>
                                                    </tr>
                                                </tbody>
                                                <tbody>
                                                    <tr style="font-weight:600;">
                                                    <td style="text-align: center; background-color:azure;">
                                                        <asp:TextBox ID="uvleav" runat="server" text="0" style="text-align: center; background-color:azure;" onkeypress="allowOnlyNumbers(event)" ></asp:TextBox>
                                                        <asp:Button class="btn btn-success" ID="btnuvleav" runat="server" Text="Update" OnClick="btnuvleav_Click" style="margin-top: 15px; border: 1px solid transparent; font-size: 14px; border-radius: 4px; touch-action: manipulation; color: #fff;"/>
                                                    </td>
                                                    <td style="text-align: center; background-color:azure;">
                                                        <asp:TextBox ID="uscleav" runat="server" text="0" style="text-align: center; background-color:azure;" onkeypress="allowOnlyNumbers(event)"></asp:TextBox>
                                                        <asp:Button class="btn btn-success" ID="btnuscleav" runat="server" Text="Update" OnClick="btnuscleav_Click" style="margin-top: 15px; border: 1px solid transparent; font-size: 14px; border-radius: 4px; touch-action: manipulation; color: #fff;"/>
                                                    </td>
                                                    <td style="text-align: center; background-color:azure;">
                                                        <asp:TextBox ID="ulleav" runat="server" text="0" style="text-align: center; background-color:azure;" onkeypress="allowOnlyNumbers(event)"></asp:TextBox>
                                                        <asp:Button class="btn btn-success" ID="btnulleav" runat="server" Text="Update" OnClick="btnulleav_Click" style="margin-top: 15px; border: 1px solid transparent; font-size: 14px; border-radius: 4px; touch-action: manipulation; color: #fff;"/>
                                                    </td>
                                                    <td style="text-align: center; background-color:azure;">
                                                        <asp:TextBox ID="uspleav" runat="server" text="0" style="text-align: center; background-color:azure;" onkeypress="allowOnlyNumbers(event)"></asp:TextBox>
                                                        <asp:Button class="btn btn-success" ID="btnuspleav" runat="server" Text="Update" OnClick="btnuspleav_Click" style="margin-top: 15px; border: 1px solid transparent; font-size: 14px; border-radius: 4px; touch-action: manipulation; color: #fff;"/>
                                                    </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                                <asp:Button class="btn btn-warning" ID="btnLeaveHis" runat="server" Text="Leave History" Onclick="getLeaves_Click" style="margin-top: 15px; border: 1px solid transparent; font-size: 14px; border-radius: 4px; touch-action: manipulation; color: #fff;"/>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="well" id="allLeavesFiled" runat="server">
                                        <div class="card-body">
                                            <div class="body">
                                                <div class="table-responsive">
                                                    <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Auto" Visible ="false" >
                                                        <asp:Literal ID="Literal1" runat="server">
                                                        </asp:Literal>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnLeaveHis" EventName="Click" />    
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="well" id="credEmp" runat="server">
                    <div class="page-content profile-view">
                        <div class="card pmd-card">
                            <div class="card-body">
                                <div class="details-tab">
						            <div class="row view-basic-card">
							            <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">First Name</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblFname" runat="server"></asp:Label></strong></p>
							            </div>
							            <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Last Name</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblLname" runat="server"></asp:Label></strong></p>
							            </div>
							            <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Middle Initial</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblMI" runat="server"></asp:Label></strong></p>
							            </div>
							            <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Phone</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblConNo" runat="server"></asp:Label></strong></p>
							            </div>
							            <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Personal Email</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><a id="lblemail" runat="server"></a></strong></p>
							            </div>
							            <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Date of Birth</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblDOB" runat="server"></asp:Label></strong></p>
							            </div>
							            <div class="col-12 col-md-6 col-lg-3">
								            <label class="pmd-list-subtitle" style="font-size:1.5rem;">Gender</label>
								            <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblgen" runat="server"></asp:Label></strong></p>
							            </div>
						            </div>
					            </div><!---end basic information---->
                                <div class="details-tab">
						        <!-- View Card -->
					                <div class="row view-employee-card">
							                <div class="col-12 col-md-6 col-lg-3">
								                <label class="pmd-list-subtitle" style="font-size:1.5rem;">Employee ID</label>
								                <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblempno" runat="server"></asp:Label></strong></p>
							                </div>
							                <div class="col-12 col-md-6 col-lg-3">
								                <label class="pmd-list-subtitle" style="font-size:1.5rem;">Date of Joining</label>
								                <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblDOH" runat="server"></asp:Label></strong></p>
							                </div>
							                <div class="col-12 col-md-6 col-lg-3">
								                <label class="pmd-list-subtitle" style="font-size:1.5rem;">Company Email</label>
								                <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><a id="lblComEmail" runat="server"></a></strong></p>
							                </div>
							                <div class="col-12 col-md-6 col-lg-3">
								                <label class="pmd-list-subtitle" style="font-size:1.5rem;">Department</label>
								                <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblDept" runat="server"></asp:Label></strong></p>
							                </div>
                                            <div class="col-12 col-md-6 col-lg-3">
								                <label class="pmd-list-subtitle" style="font-size:1.5rem;">Position</label>
								                <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblPos" runat="server"></asp:Label></strong></p>
							                </div>
							                <div class="col-12 col-md-6 col-lg-3">
								                <label class="pmd-list-subtitle" style="font-size:1.5rem;">Role</label>
								                <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblrole" runat="server"></asp:Label></strong></p>
							                </div>
							                <div class="col-12 col-md-6 col-lg-3">
								                <label class="pmd-list-subtitle" style="font-size:1.5rem;">Reporting Head</label>
								                <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblRepHead" runat="server"></asp:Label></strong></p>
							                </div>
							                <div class="col-12 col-md-6 col-lg-3">
								                <label class="pmd-list-subtitle" style="font-size:1.5rem;">Type of Employee</label>
								                <p class="pmd-list-title" style="font-size:1.6rem; line-height: 1;"><strong><asp:Label ID="lblstatus" runat="server"></asp:Label></strong></p>
							                </div>
					                </div>
					            </div><!----end of Employee Information------>
                                <div class="details-tab">
                                    <h4 style="color: #3075BA;">Leave Credits</h4>
                                    <table class="table table-bordered" style="margin-top: 10px;">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center">Vacation Leave</th>
                                                <th style="text-align: center">Sick Leave</th>
                                                <th style="text-align: center">Loyalty Leave</th>
                                                <th style="text-align: center">Special Leave</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr style="font-weight:600;">
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblVacL" runat="server" Text="0"></asp:Label></td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblSckL" runat="server" Text="0"></asp:Label></td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblLolL" runat="server" Text="0"></asp:Label></td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblSpcL" runat="server" Text="0"></asp:Label></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
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
    <script src="js/subscript.js"></script>
</body>
</html>
