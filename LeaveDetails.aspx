<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostBack="true" CodeFile="LeaveDetails.aspx.cs" Inherits="LeaveDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Details</title>
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
                    <a class='pmd-avatar-list-img' style="border-radius: 50%; width: 40px; height: 40px; margin-right: 1rem; overflow: hidden; display: inline-block;"><img class='img-fluid' style='height: 40px; width: 40px;' runat="server" id="UserPic"/></a>
                    <div style="padding-top: 5px;">
                        <asp:LinkButton ID="lblUser" runat="server" style="font-size: 18px; font-weight: 500; color: var(--text-color); text-decoration: none;" class="labelStyle" OnClick="lblAccount_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </nav>
        <br/><br/><br/><br/>
        <section style="width: 1170px; padding-right: 15px; padding-left: 15px; margin-right: auto; margin-left: auto;" runat="server" id="adminSection" visible="false">
            <div class="col-lg-12 well">
                <asp:Button class="btn btn-success" ID="btnPrint" runat="server" Text="Print"  OnClientClick="JavaScript: printPartOfPage('toPrint');" />
                <asp:Button class="btn btn-danger" ID="btnDelete" runat="server" Text="Delete" Visible="false" OnClick="btnDelete_Click" OnClientClick="ConfirmDelete()"/>
            </div>
        </section>
    <div class="container" id="toPrint">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                      <asp:TextBox type="text" class="form-control" ID="DateFiled" runat="server" name="date" ReadOnly="true"></asp:TextBox>
                  </div>
                </div>
              </div>
              <div class="form-row">
                  <div class="col-md-3 mb-3" style="top: 10px;">
                    <label>Position</label>
                    <asp:TextBox type="text" class="form-control" ID="position" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
                 <div class="col-md-6 mb-3" style="top: 10px;">
                     <label>Address while on Leave</label>
                     <asp:TextBox type="address" class="form-control" ID="address" runat="server" placeholder="Address" name="adress" TextMode="MultiLine" height="106px" ReadOnly="true"></asp:TextBox>
                  </div>
              </div>
         </div>
         <div class="col-lg-12 well" style="margin-bottom: 10px;">
             <div class="form-row">
                 <div class="col-md-3">
                    <label>Type of Absence/Leave</label>
                     <br/>
                     <asp:Panel ID="Panel1" runat="server">
                         <asp:CheckBox ID="vacation" runat="server" name="vacation" Text="&nbsp;&nbsp;Vacation&nbsp;/&nbsp;Loyalty" Enabled ="false"/>
                         <br/>
                         <asp:CheckBox ID="sick" runat="server" name="sick" Text="&nbsp;&nbsp;Sick" Enabled ="false"/>
                         <br/>
                         <asp:CheckBox ID="maternity" runat="server" name="maternity" Text="&nbsp;&nbsp;Maternity" Enabled ="false"/>
                         <br/>
                         <asp:CheckBox ID="paternity" runat="server" name="paternity" Text="&nbsp;&nbsp;Paternity" Enabled ="false"/>
                         <br/>
                         <asp:CheckBox ID="emergency" runat="server" name="emergency" Text="&nbsp;&nbsp;Emergency" Enabled ="false"/>
                         <br/>
                         <asp:CheckBox ID="undertime" runat="server" name="undertime" Text="&nbsp;&nbsp;Undertime" Enabled ="false"/>
                         <br/>
                         <asp:CheckBox ID="changeOff" runat="server" name="changeoff" Text="&nbsp;&nbsp;Change Day Off/Shift" Enabled ="false"/>
                         <br/>
                     </asp:Panel>
                     <br/><br/>
                 </div>
                  <div class="col-md-5 mb-3">
                     <label>Reason</label>
                     <asp:TextBox type="text" class="form-control" ID="reason" runat="server" placeholder="Reason" name="reason" TextMode="MultiLine" height="185px" ReadOnly="true"></asp:TextBox>
                  </div>
                  <div>
                      <div class="col-md-2 mb-3">
                          <label>Date From</label>
                            <asp:TextBox type="text" class="form-control" ID="dateFrom" runat="server" name="date" ReadOnly="true" style="width:150px;"></asp:TextBox>
                            <br/>
                            <asp:TextBox type="time" ID="timeFrom" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                       </div>
                       <div class="col-md-2">
                           <label>Date To</label>
                               <asp:TextBox type="text" class="form-control" ID="dateTo" runat="server" name="date" ReadOnly="true" style="width:150px;"></asp:TextBox>
                               <br/>
                               <asp:TextBox type="time" ID="timeTo" runat="server" class="form-control" Enabled="false"></asp:TextBox>
                       </div>
                  </div>
                 <div class="col-md-3 mb-3" style="top: 20px;">
                    <label>Document Attached</label>
                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText = "No files uploaded">
                        <Columns>
                            <asp:BoundField DataField="Text" HeaderText="File Name" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                     </asp:GridView>
                    <%--<asp:Button class="btn btn-success" runat="server" id="UploadButton" text="Open" />--%>
                 </div>
              </div>
         </div>
        <div class="col-lg-12 well" style="margin-bottom: 10px;" id="forMana" runat="server">
            <div class="row">
                <div class="col-md-6 mb-3">
                    <div>
                        <asp:Panel ID="Panel3" runat="server">
                            <asp:CheckBox ID="sapproved" runat="server" name="approved" Text="&nbsp;&nbsp;Approved" Enabled="false" OnCheckedChanged="sapproved_Checked" AutoPostBack="true"/>
                            <asp:CheckBox ID="sdenied" runat="server" name="denied" Text="&nbsp;&nbsp;Denied" style="margin-left:20px;" Enabled="false" OnCheckedChanged="sdenied_Cheked" AutoPostBack="true"/>
                            <br/><label>By :</label>
                            <asp:TextBox type="text" class="form-control" ID="SupName" runat="server" placeholder="" name="lname" ReadOnly="true"></asp:TextBox>
                        </asp:Panel>
                        <br/>
                        <label>Remarks</label>
                        <asp:TextBox type="text" class="form-control" ID="txtSRemarks" runat="server" placeholder="Remarks" name="sreason" TextMode="MultiLine" height="100px" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-5 mb-3" style="width:50%">
                        <div class="col-md-5 mb-3" style="top: 10px; left:-20px;">
                            <asp:Button class="btn btn-success" ID="btnSaveSup" runat="server" Text="Sumbit" OnClick="btnSaveSup_Click" OnClientClick="Confirm()" Visible ="false"/>
                        </div>
                    </div>
                </div>
                
                <div class="col-md-6 mb-3">
                    <div>
                        <asp:Panel ID="Panel2" runat="server">
                            <asp:CheckBox ID="mapproved" runat="server" name="approved" Text="&nbsp;&nbsp;Approved" Enabled="false" OnCheckedChanged="mapproved_Checked" AutoPostBack="true"/>
                            <asp:CheckBox ID="mdenied" runat="server" name="denied" Text="&nbsp;&nbsp;Denied" style="margin-left:20px;" Enabled="false" OnCheckedChanged="mdenied_Checked" AutoPostBack="true"/>
                            <br/><label>By :</label>
                            <asp:TextBox type="text" class="form-control" ID="ManName" runat="server" placeholder="" name="lname" ReadOnly="true"></asp:TextBox>
                        </asp:Panel>
                        <br/>
                        <label>Remarks</label>
                        <asp:TextBox type="text" class="form-control" ID="txtMRemarks" runat="server" placeholder="Remarks" name="mreason" TextMode="MultiLine" height="100px" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="col-md-5 mb-3" style="width:250%">
                        <div class="col-md-5 mb-3" style="top: 10px; left:-20px;">
                            <asp:Button class="btn btn-success" ID="btnSaveMana" runat="server" Text="Submit" OnClick="btnSaveMana_Click" OnClientClick="Confirm()" Visible ="false"/>
                        </div>
                    </div>
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
                        <td style="text-align: center">
                            <asp:Label ID="lblVacL" runat="server" Text="0"></asp:Label></td>
                        <td style="text-align: center">
                            <asp:Label ID="lblSckL" runat="server" Text="0"></asp:Label></td>
                        <td style="text-align: center">
                            <asp:Label ID="lblLolL" runat="server" Text="0"></asp:Label></td>
                        <td style="text-align: center">
                            <asp:Label ID="lblSpcL" runat="server" Text="0"></asp:Label></td>
                    </tr>
                </tbody>
            </table>
            <div runat="server" id="adApproWho" visible="false">
                <asp:Label ID="Label2" runat="server" Text="This Leave is approved by the Admin: "></asp:Label>
                <asp:Label ID="Label3" runat="server" Text="and Deducted through your: "></asp:Label>
            </div>
            <div runat="server" id="adRemarks" visible="false">
                <asp:Label ID="Label4" runat="server" Text="Remarks: "></asp:Label>
            </div>

        </div>
        <%--For Admin/HR approval and Leave Credits Deduction--%>
        
    </div>
        <div class="container"></div>
        <section class="container" style="width: 1170px; padding-right: 15px; padding-left: 15px; margin-right: auto; margin-left: auto;" runat="server" id="forAdminSection" visible="false">
           
            <div class="col-lg-12 well">
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel4" runat="server">
                                        <div style="width:700px;">
                                            <asp:TextBox type="text" class="form-control" ID="txtStoreDays" runat="server" placeholder="" name="txtStoreDays" Visible="false"></asp:TextBox>
                                            <asp:Label ID="Label1" runat="server" Text="Apply number of applicable Leave: "></asp:Label>
                                            <div class="row" style="display:flex; margin-top:20px; margin-left:30px;">
                                                <div class="col-md-6" style="text-align: center">
                                                    <asp:CheckBox ID="CheckBox1" CssClass="styled-checkbox" runat="server" Text="Vacation Leave"/>
                                                    <asp:TextBox type="text" class="form-control" ID="TextBox1" runat="server" placeholder="" name="lname" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6" style="text-align: center">
                                                    <asp:CheckBox ID="CheckBox2" CssClass="styled-checkbox" runat="server" Text="Sick Leave"/>
                                                    <asp:TextBox type="text" class="form-control" ID="TextBox2" runat="server" placeholder="" name="lname" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6" style="text-align: center">
                                                    <asp:CheckBox ID="CheckBox3" CssClass="styled-checkbox" runat="server" Text="Loyalty Leave"/>
                                                    <asp:TextBox type="text" class="form-control" ID="TextBox3" runat="server" placeholder="" name="lname" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6" style="text-align: center">
                                                    <asp:CheckBox ID="CheckBox4" CssClass="styled-checkbox" runat="server" Text="Special Leave"/>
                                                    <asp:TextBox type="text" class="form-control" ID="TextBox4" runat="server" placeholder="" name="lname" ReadOnly="true"></asp:TextBox>

                                                </div>
                                            </div>
                                            <%--<asp:DropDownList ID="HRChoiceLeave" runat="server" AppendDataBoundItems="False" AutoPostBack="true" OnSelectedIndexChanged="HRChoiceLeave_SelectedIndexChanged" style="color: #3075BA; background-color: transparent;border: none;border-bottom: solid 1px rgba(23,31,35,.12);outline: 0;box-shadow: none;padding: 0;border-radius: 0;font-size: 1rem;padding: 0.25rem 0 0.5rem; font-size: 1.6rem;line-height: 1;height: calc(3rem + 13px);word-wrap: break-word; width: 180px; margin-left:30px; font-weight:900;">
                                                <asp:ListItem Enabled="true" Text= "-Select-" Value= ""></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text= "Vacation Leave" Value= "vct"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text= "Sick Leave" Value= "sck"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text= "Loyalty Leave" Value= "lyty"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text= "Special Leave" Value= "spcl"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text= "Leave without Pay" Value= "lopay"></asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <%--<asp:TextBox type="text" class="form-control" ID="noOfLeave" runat="server" placeholder="0" name="noLeave" readonly="true" onkeypress="allowOnlyNumbers(event)" style="font-weight: 600; text-align: center; width:100px; display:initial; margin-left:30px;"></asp:TextBox>--%>
                                        </div>
                                        <br/><label>By :</label>
                                        <asp:TextBox type="text" class="form-control" ID="hrName" runat="server" placeholder="" name="hrName" ReadOnly="true"></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:AsyncPostBackTrigger ControlID="HRChoiceLeave" EventName="SelectedIndexChanged"/>--%>
                                </Triggers>
                            </asp:UpdatePanel>
                            <br/>
                            <label>Remarks</label>
                            <asp:TextBox type="text" class="form-control" ID="hrRemarks" runat="server" placeholder="Remarks" name="sreason" TextMode="MultiLine" height="100px" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-5 mb-3" style="width:50%">
                            <div class="col-md-5 mb-3" style="top: 10px; left:-20px;">
                                <asp:Button class="btn btn-success" ID="btnAdminPass" runat="server" Text="Sumbit" OnClientClick="Confirm()" OnClick="btnAdminPass_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="myModalLabel">Modal Title</h4>
                    </div>
                    <div class="modal-body">
                        <p>Modal body text goes here.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary">Save changes</button>
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
