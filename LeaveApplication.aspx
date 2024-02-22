<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostBack="true" CodeFile="LeaveApplication.aspx.cs" Inherits="LeaveApplication" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Application</title>
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
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.5.js" type="text/javascript"></script>
     
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
                    <a class='pmd-avatar-list-img' style="border-radius: 50%; width: 40px; height: 40px; margin-right: 1rem; overflow: hidden; display: inline-block;"><img class='img-fluid' style='height: 40px; width: 40px;' runat="server" id="UserPic"/></a>
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
        <h1 class="well">Absent Request Form</h1>
        <div class="col-lg-12 well" style="margin-bottom: 10px;">
             <div class="form-row">
                <div class="col-md-2">
                    <label for="validationServer013">Employee No</label>
                    <asp:TextBox type="empno" class="form-control" ID="empno" runat="server" placeholder="Enter ID" name="empno" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-4 mb-3">
                  <label for="validationServer013">First name</label>
                    <asp:TextBox type="fname" class="form-control" ID="fname" runat="server" placeholder="First Name" name="fname" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-4 mb-3">
                  <label for="validationServer023">Last name</label>
                   <asp:TextBox type="lname" class="form-control" ID="lname" runat="server" placeholder="Last Name" name="lname" ReadOnly="true"></asp:TextBox>
                </div>
                <div class="col-md-2 mb-3">
                  <label>Date</label>
                  <div class="input-group">
                      <asp:TextBox type="date" ID="DateFiled" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                  </div>
                </div>
                 <asp:TextBox type="text" class="form-control" ID="RHNo" runat="server" placeholder="Reporting Head" name="RHNo" ReadOnly="true" visible="false"></asp:TextBox>
              </div>
              <div class="form-row">
                  <div class="col-md-3 mb-3" style="top: 10px;">
                    <label>Position</label>
                    <asp:TextBox type="text" class="form-control" ID="position" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
                 <div class="col-md-6 mb-3" style="top: 10px;">
                     <label>Address while on Leave</label>
                     <asp:TextBox type="address" class="form-control" ID="address" runat="server" placeholder="Address" name="adress" TextMode="MultiLine" height="106px"></asp:TextBox>
                  </div>
              </div>
         </div>
         <div class="col-lg-12 well" style="margin-bottom: 10px;">
             <div class="form-row">
                 <div class="col-md-3">
                    <label>Type of Absence/Leave</label>
                     <br/>
                     <asp:Panel ID="Panel1" runat="server">
                         <asp:CheckBox ID="vacation" runat="server" name="vacation" Text="&nbsp;&nbsp;Vacation&nbsp;/&nbsp;Loyalty" OnCheckedChanged="vacation_Checked" AutoPostBack="true"/>
                         <br/>
                         <asp:CheckBox ID="sick" runat="server" name="sick" Text="&nbsp;&nbsp;Sick" OnCheckedChanged="sick_Checked" AutoPostBack="true"/>
                         <br/>
                         <asp:CheckBox ID="maternity" runat="server" name="maternity" Text="&nbsp;&nbsp;Maternity" OnCheckedChanged="maternity_Checked" AutoPostBack="true"/>
                         <br/>
                         <asp:CheckBox ID="paternity" runat="server" name="paternity" Text="&nbsp;&nbsp;Paternity" OnCheckedChanged="paternity_Checked" AutoPostBack="true"/>
                         <br/>
                         <asp:CheckBox ID="emergency" runat="server" name="emergency" Text="&nbsp;&nbsp;Emergency" OnCheckedChanged="emergency_Checked" AutoPostBack="true"/>
                         <br/>
                         <asp:CheckBox ID="undertime" runat="server" name="undertime" Text="&nbsp;&nbsp;Undertime" OnCheckedChanged="undertime_Checked" AutoPostBack="true"/>
                         <br/>
                         <asp:CheckBox ID="changeOff" runat="server" name="changeoff" Text="&nbsp;&nbsp;Change Day Off/Shift" OnCheckedChanged="changeOff_Checked" AutoPostBack="true"/>
                         <br/>
                     </asp:Panel>
                     <br/><br/>
                     <div class="col-md-6" style="width:250%">
                         <div class="custom-control custom-checkbox">
                            <%--<input type="checkbox" class="custom-control-input is-invalid" id="invalidCheck33" required="">
                            <label class="custom-control-label" for="invalidCheck33">Agree to terms and conditions</label>
                            <div class="invalid-feedback">
                            You must agree before submitting.
                            </div>--%>
                             <div class="invalid-feedback" style="font-size:10px;">Note: Operation officer will verify for change in day off request, Admin Officer will verify the absence or leave availment.</div>
                             <div class="invalid-feedback" style="font-size:10px;">The approver for Operations are the Operations Manager, for IT the IT Manager and for Admin the Operations Manager-Admin.</div>
                        </div>
                        <div class="col-md-5 mb-3" style="top: 10px; left:-20px;">
                            <asp:Button class="btn btn-success" ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" OnClientClick="Confirm()"/>
                        </div>
                     </div>
                     
                 </div>
                  <div class="col-md-5 mb-3">
                     <label>Reason</label>
                     <asp:TextBox type="text" class="form-control" ID="reason" runat="server" placeholder="Reason" name="reason" TextMode="MultiLine" height="185px" ></asp:TextBox>
                  </div>
                  <div>
                      <div class="col-md-2 mb-3">
                          <label>Date From</label>
                            <asp:TextBox type="date" ID="dateFrom" runat="server" class="form-control"></asp:TextBox>
                            <br/>
                            <asp:TextBox type="time" ID="timeFrom" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                       </div>
                       <div class="col-md-2">
                           <label>Date To</label>
                               <asp:TextBox type="date" ID="dateTo" runat="server" class="form-control"></asp:TextBox>
                               <br/>
                               <asp:TextBox type="time" ID="timeTo" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                       </div>
                  </div>
                 <div class="col-md-3 mb-3" style="top: 20px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <label>Document Attached</label>
                            <asp:FileUpload id="FileUploadControl" runat="server" accept=".jpg, .jpeg, .png, .doc, .docx, .pdf"/>
                            <br />
                            <asp:Button class="btn btn-success" runat="server" id="UploadButton" text="Upload" OnClick="UploadButton_Click" />
                            <br /><br />
                            <asp:GridView ID="grdvFiles" runat="server" AutoGenerateColumns="false" EmptyDataText = "No files uploaded">
                                <Columns>
                                    <asp:BoundField DataField="Text" HeaderText="File Name" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" Text = "Delete" AppendDataBoundItems="False" AutoPostBack="true" CommandArgument = '<%# Eval("Value") %>' runat = "server" OnClick="DeleteFile" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="UploadButton"/>
                        </Triggers>
                    </asp:UpdatePanel>
                 </div>
              </div>
         </div>
        <div class="col-lg-12 well" style="margin-bottom: 10px;">
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
