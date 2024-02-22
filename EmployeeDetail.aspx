<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostBack="true" CodeFile="EmployeeDetail.aspx.cs" Inherits="EmployeeDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Detail</title>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
        <div class="well">
            <h1>Employee Detail</h1>
            <div aria-label="breadcrumb">
                <ol class="breadcrumb pmd-breadcrumb mb-0">
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="LinkButton1" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblEmployee_Click">Employee</asp:LinkButton>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Employee Detail</li>
                </ol>
            </div>
        </div>
        <div class="well">
             <div class="cover-content">
                 <div class="pag-content">
                     <div class="d-flex d-flex-row align-items-center mobile-view">
					    <asp:Image ID="whosImage" runat="server" alt="Profile-Pic" class="profile-pic rounded-circle pmd-z-depth-light-2-1 mr-md-4 mr-sm-0" height="200" width="200"/>
					    <div class="media-body">
                            <asp:Label ID="lblEmpName" runat="server" Text="Label" class="h1"></asp:Label>
						    <p class="pmd-list-subtitle" style="font-size:1.6rem;"><asp:Label ID="lblPosition" runat="server" Text="Label"></asp:Label></p>
						    <div class="pmd-social-icon">
							    <a class="btn btn-sm pmd-btn-fab pmd-ripple-effect btn-dark ml-0" href="javascript:void(0);" title="Facebook">
								    <svg version="1.1" x="0px" y="0px" viewBox="0 0 310 310" style="enable-background:new 0 0 310 310;" xml:space="preserve" class="svg replaced-svg replaced-svg">
								    <g id="XMLID_834_">
									    <path id="XMLID_835_" d="M81.703,165.106h33.981V305c0,2.762,2.238,5,5,5h57.616c2.762,0,5-2.238,5-5V165.765h39.064   c2.54,0,4.677-1.906,4.967-4.429l5.933-51.502c0.163-1.417-0.286-2.836-1.234-3.899c-0.949-1.064-2.307-1.673-3.732-1.673h-44.996   V71.978c0-9.732,5.24-14.667,15.576-14.667c1.473,0,29.42,0,29.42,0c2.762,0,5-2.239,5-5V5.037c0-2.762-2.238-5-5-5h-40.545   C187.467,0.023,186.832,0,185.896,0c-7.035,0-31.488,1.381-50.804,19.151c-21.402,19.692-18.427,43.27-17.716,47.358v37.752H81.703   c-2.762,0-5,2.238-5,5v50.844C76.703,162.867,78.941,165.106,81.703,165.106z"></path>
								    </g> 
							    </svg>
							    </a>
							    <a class="btn btn-sm pmd-btn-fab pmd-ripple-effect btn-dark" href="javascript:void(0);" title="Twitter">
								    <svg version="1.1" x="0px" y="0px" viewBox="0 0 310 310" style="enable-background:new 0 0 310 310;" xml:space="preserve" class="svg replaced-svg replaced-svg">
									    <g id="XMLID_826_">
										    <path id="XMLID_827_" d="M302.973,57.388c-4.87,2.16-9.877,3.983-14.993,5.463c6.057-6.85,10.675-14.91,13.494-23.73   c0.632-1.977-0.023-4.141-1.648-5.434c-1.623-1.294-3.878-1.449-5.665-0.39c-10.865,6.444-22.587,11.075-34.878,13.783   c-12.381-12.098-29.197-18.983-46.581-18.983c-36.695,0-66.549,29.853-66.549,66.547c0,2.89,0.183,5.764,0.545,8.598   C101.163,99.244,58.83,76.863,29.76,41.204c-1.036-1.271-2.632-1.956-4.266-1.825c-1.635,0.128-3.104,1.05-3.93,2.467   c-5.896,10.117-9.013,21.688-9.013,33.461c0,16.035,5.725,31.249,15.838,43.137c-3.075-1.065-6.059-2.396-8.907-3.977   c-1.529-0.851-3.395-0.838-4.914,0.033c-1.52,0.871-2.473,2.473-2.513,4.224c-0.007,0.295-0.007,0.59-0.007,0.889   c0,23.935,12.882,45.484,32.577,57.229c-1.692-0.169-3.383-0.414-5.063-0.735c-1.732-0.331-3.513,0.276-4.681,1.597   c-1.17,1.32-1.557,3.16-1.018,4.84c7.29,22.76,26.059,39.501,48.749,44.605c-18.819,11.787-40.34,17.961-62.932,17.961   c-4.714,0-9.455-0.277-14.095-0.826c-2.305-0.274-4.509,1.087-5.294,3.279c-0.785,2.193,0.047,4.638,2.008,5.895   c29.023,18.609,62.582,28.445,97.047,28.445c67.754,0,110.139-31.95,133.764-58.753c29.46-33.421,46.356-77.658,46.356-121.367   c0-1.826-0.028-3.67-0.084-5.508c11.623-8.757,21.63-19.355,29.773-31.536c1.237-1.85,1.103-4.295-0.33-5.998   C307.394,57.037,305.009,56.486,302.973,57.388z"></path>
									    </g> 
								    </svg>
							    </a>
							    <a class="btn btn-sm pmd-btn-fab pmd-ripple-effect btn-dark" href="javascript:void(0);" title="LinkedIn">
								    <svg version="1.1" x="0px" y="0px" viewBox="0 0 310 310" style="enable-background:new 0 0 310 310;" xml:space="preserve" class="svg replaced-svg replaced-svg">
									    <g id="XMLID_801_">
										    <path id="XMLID_802_" d="M72.16,99.73H9.927c-2.762,0-5,2.239-5,5v199.928c0,2.762,2.238,5,5,5H72.16c2.762,0,5-2.238,5-5V104.73   C77.16,101.969,74.922,99.73,72.16,99.73z"></path>
										    <path id="XMLID_803_" d="M41.066,0.341C18.422,0.341,0,18.743,0,41.362C0,63.991,18.422,82.4,41.066,82.4   c22.626,0,41.033-18.41,41.033-41.038C82.1,18.743,63.692,0.341,41.066,0.341z"></path>
										    <path id="XMLID_804_" d="M230.454,94.761c-24.995,0-43.472,10.745-54.679,22.954V104.73c0-2.761-2.238-5-5-5h-59.599   c-2.762,0-5,2.239-5,5v199.928c0,2.762,2.238,5,5,5h62.097c2.762,0,5-2.238,5-5v-98.918c0-33.333,9.054-46.319,32.29-46.319   c25.306,0,27.317,20.818,27.317,48.034v97.204c0,2.762,2.238,5,5,5H305c2.762,0,5-2.238,5-5V194.995   C310,145.43,300.549,94.761,230.454,94.761z"></path>
									    </g> 
								    </svg>
							    </a>
						    </div>
					    </div>
				    </div>
                 </div>
                 <div class="page-content profile-view">
                     <div class="row">
                         <div class="col-12" style="position: relative;width: 100%;min-height: 1px;padding-right: 15px;padding-left: 15px;flex: 0 0 100%;max-width: 100%; top: 20px;">
                             <div class="pmd-tabs">
                                <div class="pmd-tab-active-bar" style="width: 93.125px; left: 0px;"></div>
                                 <ul class="nav nav-tabs" role="tablist" style="width: 325.578px;">
                                    <li class="nav-item"><a class="nav-link active" href="#about" aria-controls="about" role="tab" data-toggle="tab" style="font-size: 1.6rem;">About</a></li>
                                    <li class="nav-item"><a class="nav-link" href="#documents" aria-controls="documents" role="tab" data-toggle="tab" style="font-size: 1.6rem;">Documents</a></li>
                                </ul>
                            </div>
                             <div class="tab-content" id="tablist">
                                 <!--- About Tab-------->
                                 <div role="tabpanel" class="tab-pane active" id="about">
                                     <div class="card pmd-card">
                                         <div class="card-body">
                                             <div class="details-tab">
											    <div class="d-flex flex-row align-items-center mb-2">
												    <h3 class="card-title media-body" style="color: #3075BA;">Basic Information</h3>
												    <div class="dropdown pmd-dropdown pmd-user-info ml-auto">
													    <a href="#" class="pmd-btn-fab btn-outline-dark pmd-btn-flat btn btn-sm" data-toggle="dropdown" aria-expanded="true"><i class="material-icons pmd-icon-sm">more_vert</i></a>
													    <div class="dropdown-menu dropdown-menu-right" style="clip: rect(0px, 93.8594px, 0px, 93.8594px); transform: translate3d(-53.8594px, 0px, 0px);">
														    <a class="dropdown-item d-flex flex-row" id="edit-basic-info" href="javascript:void(0);"><i class="material-icons md-dark pmd-icon-xs mr-3">edit</i><span class="media-body" style="width: 150px;">Edit</span></a>
													    </div>
												    </div>
											    </div>
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
                                                    <div class="edit-basic-card" style="display:none;">
                                                        <div class="row">
													        <div class="col-12 col-md-6 col-lg-3">
														        <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
															        <label for="fn" class="col-form-label control-label" style="font-size:1.5rem;">First Name</label>
                                                                    <asp:TextBox type="text" ID="txtFName" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
														        </div>
													        </div>
													        <div class="col-12 col-md-6 col-lg-3">
														        <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
															        <label for="ln" class="col-form-label control-label" style="font-size:1.5rem;">Last Name</label>
															        <asp:TextBox type="text" ID="txtLName" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
														        </div>
													        </div>
													        <div class="col-12 col-md-6 col-lg-3">
														        <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
															        <label for="mn" class="col-form-label control-label" style="font-size:1.5rem;">Middle Initial</label>
															        <asp:TextBox type="text" ID="txtMI" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
														        </div>
													        </div>
													        <div class="col-12 col-md-6 col-lg-3">
														        <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
															        <label for="phone" class="col-form-label control-label" style="font-size:1.5rem;">Phone</label>
															        <asp:TextBox type="tel" ID="txtConNo" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox><span class="pmd-textfield-focused"></span>
														        </div>
													        </div>
													        <div class="col-12 col-md-6 col-lg-3">
														        <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
															        <label for="personal-email" class="col-form-label control-label" style="font-size:1.5rem;">Personal Email</label>
															        <asp:TextBox type="email" ID="txtEmail" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
														        </div>
													        </div>
													        <div class="col-12 col-md-6 col-lg-3">
														        <div class="form-group pmd-textfield pmd-textfield-floating-label">
															        <label for="dob" class="col-form-label control-label" style="font-size:1.5rem;">Date of Birth</label>
															        <asp:TextBox type="date" ID="txtDOB" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;" TextMode="Date" ReadOnly ="false"></asp:TextBox><span class="pmd-textfield-focused"></span>
														        </div>
													        </div>
													        <div class="col-12 col-md-6 col-lg-3">
                                                                 <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
                                                                    <label for="email" class="title-label d-block" style="font-size:1.5rem;">Gender</label>
                                                                    <asp:DropDownList name="gender" ID="gender" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1; height: calc(3rem + 13px);">
                                                                        <asp:ListItem Enabled="true" Text= "Male" Value= "0"></asp:ListItem>
                                                                        <asp:ListItem Enabled="true" Text= "Female" Value= "1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                 </div>
													        </div>
												        </div>
												        <div class="row">
													        <div class="col-12 mt-3">
                                                                <asp:Button class="btn btn-primary pmd-ripple-effect pmd-btn-raised" type="submit" ID="btnBISubmit" runat="server" Text="Submit" style="font-size:1.5rem;" OnClick="btnBISubmit_Click"/>
                                                                <button class="btn btn-outline-secondary pmd-ripple-effect" type="submit" id="reset-basic-info" style="font-size:1.5rem;">Cancel</button> 
													        </div>
												        </div>
                                                    </div>
										    </div><!---end basic information---->
                                             <div class="details-tab">
											    <div class="d-flex flex-row align-items-center mb-2">
												    <h3 class="card-title media-body" style="color: #3075BA;">Employee Information</h3>
												    <div class="dropdown pmd-dropdown pmd-user-info ml-auto">
													    <a href="#" class="pmd-btn-fab btn-outline-dark pmd-btn-flat btn btn-sm" data-toggle="dropdown" aria-expanded="true"><i class="material-icons pmd-icon-sm">more_vert</i></a>
													    <div class="dropdown-menu dropdown-menu-right" style="clip: rect(0px, 93.8594px, 0px, 93.8594px); transform: translate3d(-53.8594px, 0px, 0px);">
														    <a class="dropdown-item d-flex flex-row" id="edit-employee-info" href="javascript:void(0);"><i class="material-icons md-dark pmd-icon-xs mr-3">edit</i><span class="media-body" style="width: 150px;">Edit</span></a>
													    </div>
												    </div>
											    </div>
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
											    <div class="edit-employee-card" style="display: none;">
												    <div class="row">
													    <div class="col-12 col-md-6 col-lg-3">
														    <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
															    <label for="id" class="col-form-label control-label" style="font-size:1.5rem;">Employee ID</label>
															    <asp:TextBox type="text" ID="txtEmpno" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
														    </div>
													    </div>
													    <div class="col-12 col-md-6 col-lg-3">
														    <div class="form-group pmd-textfield pmd-textfield-floating-label">
															    <label for="doj" class="col-form-label control-label" style="font-size:1.5rem;">Date of Joining</label>
															    <asp:TextBox type="date" ID="txtDOH" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
														    </div>
													    </div>
													    <div class="col-12 col-md-6 col-lg-3">
														    <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
															    <label for="email" class="col-form-label control-label" style="font-size:1.5rem;">Company Email</label>
															    <asp:TextBox type="email" ID="txtComEmail" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1;"></asp:TextBox><span class="pmd-textfield-focused"></span>
														    </div>
													    </div>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-12 col-md-6 col-lg-3">
														            <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
															            <label for="advance_department" style="font-size:1.5rem;">Department</label>
															            <asp:DropDownList name="department" ID="department" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1; height: calc(3rem + 13px);" AutoPostBack="true" OnSelectedIndexChanged="dept_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <span class="pmd-textfield-focused"></span>
														            </div>
													            </div>
													            <div class="col-12 col-md-6 col-lg-3">
														            <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
															            <label for="advance_department" style="font-size:1.5rem;">Position</label>
															            <asp:DropDownList name="position" ID="position" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1; height: calc(3rem + 13px);" AppendDataBoundItems="false" AutoPostBack="true" OnSelectedIndexChanged="position_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <span class="pmd-textfield-focused"></span>
														            </div>
													            </div>
                                                                <div class="col-12 col-md-6 col-lg-3">
														            <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
															            <label for="advance_designation" style="font-size:1.5rem;">Role</label>
															            <asp:DropDownList name="role" ID="role" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1; height: calc(3rem + 13px);" AutoPostBack="true" OnSelectedIndexChanged="role_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <span class="pmd-textfield-focused"></span>
														            </div>
													            </div>
													            <div class="col-12 col-md-6 col-lg-3">
														            <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
															            <label for="reporting" style="font-size:1.5rem;">Reporting Head</label>
															            <asp:DropDownList name="reportHead" ID="reportHead" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1; height: calc(3rem + 13px);" >
                                                                        </asp:DropDownList>
														            </div>
													            </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="department" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
													    <div class="col-12 col-md-6 col-lg-3">
                                                            <div class="form-group pmd-textfield pmd-textfield-floating-label pmd-textfield-floating-label-completed">
                                                                <label for="type" style="font-size:1.5rem;">Status</label>
														        <asp:DropDownList name="status" ID="empstat" runat="server" class="form-control" style="font-size:1.6rem; line-height: 1; height: calc(3rem + 13px);">
                                                                    <asp:ListItem Enabled="true" Text= "Probationary" Value= "0"></asp:ListItem>
                                                                    <asp:ListItem Enabled="true" Text= "Contractual" Value= "1"></asp:ListItem>
                                                                    <asp:ListItem Enabled="true" Text= "Part-Time" Value= "2"></asp:ListItem>
                                                                    <asp:ListItem Enabled="true" Text= "Regular" Value= "3"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span class="pmd-textfield-focused"></span>
                                                            </div>
													    </div>
												    </div>
												    <div class="row">
													    <div class="col-12 mt-3">
														    <asp:Button class="btn btn-primary pmd-ripple-effect pmd-btn-raised" type="submit" ID="btnEISubmit" runat="server" Text="Submit" style="font-size:1.5rem;" OnClick="btnEISubmit_Click"/>
                                                            <asp:Button class="btn btn-outline-secondary pmd-ripple-effect" type="submit" ID="btnEICancel" runat="server" Text="Cancel" style="font-size:1.5rem;" OnClick="btnEICancel_Click"/>
													    </div>
												    </div>
											    </div>
										    </div><!----end of Employee Information------>
                                             <div class="details-tab">
                                                <div class="d-flex flex-row align-items-center mb-2">
												    <h3 class="card-title media-body" style="color: #3075BA;">Educational Background</h3>
												    <div class="dropdown pmd-dropdown pmd-user-info ml-auto" id="Div1" runat ="server">
													    <a href="#" class="pmd-btn-fab btn-outline-dark pmd-btn-flat btn btn-sm" data-toggle="dropdown" aria-expanded="true"><i class="material-icons pmd-icon-sm">more_vert</i></a>
													    <div class="dropdown-menu dropdown-menu-right" style="clip: rect(0px, 93.8594px, 0px, 93.8594px); transform: translate3d(-53.8594px, 0px, 0px);">
														    <a class="dropdown-item d-flex flex-row" id="edit-educational-info" href="javascript:void(0);"><i class="material-icons md-dark pmd-icon-xs mr-3">edit</i><span class="media-body" style="width: 150px;">Edit</span></a>
													    </div>
												    </div>
											    </div>
                                            </div>
                                                <div class="details-tab">
                                                <div class="d-flex flex-row align-items-center mb-2">
												    <h3 class="card-title media-body" style="color: #3075BA;">Experience</h3>
												    <div class="dropdown pmd-dropdown pmd-user-info ml-auto" id="Div2" runat ="server">
													    <a href="#" class="pmd-btn-fab btn-outline-dark pmd-btn-flat btn btn-sm" data-toggle="dropdown" aria-expanded="true"><i class="material-icons pmd-icon-sm">more_vert</i></a>
													    <div class="dropdown-menu dropdown-menu-right" style="clip: rect(0px, 93.8594px, 0px, 93.8594px); transform: translate3d(-53.8594px, 0px, 0px);">
														    <a class="dropdown-item d-flex flex-row" id="edit-experience-info" href="javascript:void(0);"><i class="material-icons md-dark pmd-icon-xs mr-3">edit</i><span class="media-body" style="width: 150px;">Edit</span></a>
													    </div>
												    </div>
											    </div>
                                            </div>
                                                <div class="details-tab">
                                                <div class="d-flex flex-row align-items-center mb-2">
												    <h3 class="card-title media-body" style="color: #3075BA;">Licenses & Certifications</h3>
												    <div class="dropdown pmd-dropdown pmd-user-info ml-auto" id="Div3" runat ="server">
													    <a href="#" class="pmd-btn-fab btn-outline-dark pmd-btn-flat btn btn-sm" data-toggle="dropdown" aria-expanded="true"><i class="material-icons pmd-icon-sm">more_vert</i></a>
													    <div class="dropdown-menu dropdown-menu-right" style="clip: rect(0px, 93.8594px, 0px, 93.8594px); transform: translate3d(-53.8594px, 0px, 0px);">
														    <a class="dropdown-item d-flex flex-row" id="edit-Licenses-info" href="javascript:void(0);"><i class="material-icons md-dark pmd-icon-xs mr-3">edit</i><span class="media-body" style="width: 150px;">Edit</span></a>
													    </div>
												    </div>
											    </div>
                                            </div>
                                         </div>
                                     </div>
                                 </div>
                                 <!--- Documents Tab-------->
                                
                                 <div role="tabpanel" class="tab-pane fade" id="documents">
                                     <div class="card pmd-card">
                                         <div class="card-body">
                                             <div class="details-tab">
                                                 <div class="d-flex flex-row align-items-center mb-2">
												    <h3 class="card-title media-body" style="color: #3075BA;">Documents</h3>
											    </div>
                                                <div class="row view-basic-card">
                                                    <div class="card pmd-card">
                                                    <!-- Card Body -->
                                                    <div class="card-body">
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
                                                </div>
                                             </div>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                         </div>
                     </div>
                 </div>
            </div>
        </div>
    </div>
        
    <script type="text/javascript" src="vendor/bootstrap/js/bootstrap4-admin-compress.min.js"></script>
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
            var sPath = window.location.pathname;
            var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
            $(".pmd-sidebar-nav").each(function () {
                $(this).find("a[href='" + sPage + "']").parents(".collapse").addClass("show");
                $(this).find("a[href='" + sPage + "']").parents(".collapse").prev('a.nav-link').addClass("active");
                $(this).find("a[href='" + sPage + "']").addClass("active");
            });
            $(".auto-update-year").html(new Date().getFullYear());
        });

        // Show Hide Edit Basic Info Card
        $(".edit-basic-card").hide();
        $("#edit-basic-info").click(function () {
            $(".edit-basic-card").show();
            $(".view-basic-card").hide();
        });
        $("#view-basic-info").click(function () {
            $(".view-basic-card").show();
            $(".edit-basic-card").hide();
        });
        $("#reset-basic-info").click(function () {
            $(".view-basic-card").show();
            $(".edit-basic-card").hide();
        });

        // Show Hide Edit Employee Info Card
        $(".edit-employee-card").hide();
        $("#edit-employee-info").click(function () {
            $(".edit-employee-card").show();
            $(".view-employee-card").hide();
        });
        $("#view-employee-info").click(function () {
            $(".view-employee-card").show();
            $(".edit-employee-card").hide();
        });
        $("#reset-employee-info").click(function () {
            $(".view-employee-card").show();
            $(".edit-employee-card").hide();
        });

        // Show Hide Edit Contact Info Card
        $(".edit-contact-card").hide();
        $("#edit-contact-info").click(function () {
            $(".edit-contact-card").show();
            $(".view-contact-card").hide();
        });
        $("#view-contact-info").click(function () {
            $(".view-contact-card").show();
            $(".edit-contact-card").hide();
        });
        $("#reset-contact-info").click(function () {
            $(".view-contact-card").show();
            $(".edit-contact-card").hide();
        });

        // Show Hide Edit Salary Info Card
        $(".edit-salary-card").hide();
        $("#edit-salary-info").click(function () {
            $(".edit-salary-card").show();
            $(".view-salary-card").hide();
        });
        $("#view-salary-info").click(function () {
            $(".view-salary-card").show();
            $(".edit-salary-card").hide();
        });
        $("#reset-salary-info").click(function () {
            $(".view-salary-card").show();
            $(".edit-salary-card").hide();
        });

        //Birthday DatePicker
        $('#birthdate-picker').datetimepicker({
            format: 'DD-MM-YYYY'
        });

        //Joining DatePicker
        $('#joindate-picker').datetimepicker({
            format: 'DD-MM-YYYY'
        });

        // Passing Year
        $('#passing-year').datetimepicker({
            viewMode: 'years',
            format: 'YYYY'
        });

        // Passing Year
        $('#edit-passing-year').datetimepicker({
            viewMode: 'years',
            format: 'YYYY'
        });

        // Add Education start date date and time picker 
        $('#datepicker-start').datetimepicker({
            format: 'DD-MM-YYYY'
        });

        // Add Education End date date and time picker 
        $('#datepicker-end').datetimepicker({
            useCurrent: false,
            format: 'DD-MM-YYYY'
        });

        // start date picker for Add Education
        $("#datepicker-start").on("dp.change", function (e) {
            $('#datepicker-end').data("DateTimePicker").minDate(e.date);
        });
        // end date picker for Add Education 
        $("#datepicker-end").on("dp.change", function (e) {
            $('#datepicker-start').data("DateTimePicker").maxDate(e.date);
        });

        // edit Education start date date and time picker 
        $('#edit-datepicker-start').datetimepicker({
            format: 'DD-MM-YYYY'
        });

        // edit Education End date date and time picker 
        $('#edit-datepicker-end').datetimepicker({
            useCurrent: false,
            format: 'DD-MM-YYYY'
        });

        // start date picker for edit Education
        $("#edit-datepicker-start").on("dp.change", function (e) {
            $('#edit-datepicker-end').data("DateTimePicker").minDate(e.date);
        });
        // end date picker for edit Education
        $("#edit-datepicker-end").on("dp.change", function (e) {
            $('#edit-datepicker-start').data("DateTimePicker").maxDate(e.date);
        });

        // edit experience  start date date and time picker 
        $('#editexp-datepicker-start').datetimepicker({
            format: 'DD-MM-YYYY'
        });

        // edit experience End date date and time picker 
        $('#editexp-datepicker-end').datetimepicker({
            useCurrent: false,
            format: 'DD-MM-YYYY'
        });

        // edit experience start date picker
        $("#editexp-datepicker-start").on("dp.change", function (e) {
            $('#edit-datepicker-end').data("DateTimePicker").minDate(e.date);
        });
        // edit experience end date picker
        $("#editexp-datepicker-end").on("dp.change", function (e) {
            $('#edit-datepicker-start').data("DateTimePicker").maxDate(e.date);
        });

        // add experience  start date date and time picker 
        $('#exp-datepicker-start').datetimepicker({
            format: 'DD-MM-YYYY'
        });

        // add experience End date date and time picker 
        $('#exp-datepicker-end').datetimepicker({
            useCurrent: false,
            format: 'DD-MM-YYYY'
        });

        // add experience start date picker
        $("#exp-datepicker-start").on("dp.change", function (e) {
            $('#edit-datepicker-end').data("DateTimePicker").minDate(e.date);
        });
        // add experience end date picker
        $("#exp-datepicker-end").on("dp.change", function (e) {
            $('#edit-datepicker-start').data("DateTimePicker").maxDate(e.date);
        });

        // single range slider with default tooltip open
        var pmdSliderTooltip = document.getElementById('pmd-slider-tooltip');
        noUiSlider.create(pmdSliderTooltip, {
            start: [7],
            connect: 'lower',
            tooltips: [wNumb({ decimals: 0 })],
            range: {
                'min': [0],
                'max': [10]
            }
        });
    </script>
</body>
</html>
