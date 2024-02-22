<%@ Page Title="Seiha Management Info System" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dash.aspx.cs" Inherits="HRMIS.Dash" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <!-- Page-header start -->
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-8">
                    <div class="page-header-title">
                        <h5 class="m-b-10">Dashboard</h5>
                        <p class="m-b-0">Welcome to SEIHA HR Management Information System</p>
                    </div>
                </div>
                <div class="col-md-4">
                    <ul class="breadcrumb-title">
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash1" runat="server" OnClick="lblbtnDash_Click"><i class="fa fa-home"></i></asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash" runat="server" OnClick="lblbtnDash_Click">Dashboard</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
     <!-- Page-header end -->
    <div class="pcoded-inner-content">
        <!-- Main-body start -->
        <div class="main-body">
            <div class="page-wrapper">
                <!-- Page-body start -->
                <div class="page-body">
                    <div class="row">
                        <!----Counter---->
                        <div id="empCount" runat="server" class="col-xl-3 col-md-6">
                            <div class="card">
                                <div class="card-block">
                                    <div class="row align-items-center">
                                        <div class="col-8">
                                            <h4 class="text-c-purple"><asp:Label ID="totEmp" runat="server" Text="00"></asp:Label></h4>
                                            <h6 class="text-muted m-b-0">Employees</h6>
                                        </div>
                                        <div class="col-4 text-right">
                                            <i class="fa fa-users f-28"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer bg-c-purple">
                                    <div class="row align-items-center">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-3 col-md-6">
                            <div class="card">
                                <div class="card-block">
                                    <div class="row align-items-center">
                                        <div class="col-8">
                                            <h4 class="text-c-green"><asp:Label ID="totLeave" runat="server" Text="00"></asp:Label></h4>
                                            <h6 class="text-muted m-b-0">Leaves Filed</h6>
                                            <h6 id="leavfiled" runat="server" class="text-muted m-b-0">this Month</h6>
                                        </div>
                                        <div class="col-4 text-right">
                                            <i class="fa fa-file-text-o f-28"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer bg-c-green">
                                    <div class="row align-items-center">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-3 col-md-6">
                            <div class="card">
                                <div class="card-block">
                                    <div class="row align-items-center">
                                        <div class="col-8">
                                            <h4 class="text-c-red"><asp:Label ID="totLeaveM" runat="server" Text="00"></asp:Label></h4>
                                            <h6 class="text-muted m-b-0">Leaves Filed</h6>
                                            <h6 class="text-muted m-b-0">Today</h6>
                                        </div>
                                        <div class="col-4 text-right">
                                            <i class="fa fa-calendar-check-o f-28"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer bg-c-red" style="padding: 5px 20px;">
                                    <div class="row align-items-center">
                                        <div class="col-3"></div><div class="col-9 text-right"><asp:LinkButton ID="lnkbtnViewLeave" runat="server" class="text-right" OnClick="lnkbtnViewLeave_Click">View Leave</asp:LinkButton></div>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-3 col-md-6">
                            <div class="card">
                                <div class="card-block">
                                    <div class="row align-items-center">
                                        <div class="col-8">
                                            <h4 class="text-c-blue"><asp:Label ID="totLeaveApp" runat="server" Text="00"></asp:Label></h4>
                                            <h6 class="text-muted m-b-0">Approved Leaves</h6>
                                            <h6 id="apprLeave" runat="server" class="text-muted m-b-0">this Month</h6>
                                        </div>
                                        <div class="col-4 text-right">
                                            <i class="fa fa-file-text f-28"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer bg-c-blue">
                                    <div class="row align-items-center">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="stayedComp" runat="server" class="col-xl-3 col-md-6" visible="false">
                            <div class="card">
                                <div class="card-block">
                                    <div class="row align-items-center">
                                        <div class="col-8">
                                            <h4 class="text-c-purple"><asp:Label ID="dayCalc" runat="server" Text="00"></asp:Label></h4>
                                            <h6 class="text-muted m-b-0">Years Employeed</h6>
                                        </div>
                                        <div class="col-4 text-right">
                                            <i class="fa fa-calendar f-28"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer bg-c-purple">
                                    <div class="row align-items-center">
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!------Birthday & Anniversaries------>
                        <div class="col-xl-4 col-md-12">
                            <div class="card ">
                                <div class="card-header">
                                    <h5 runat="server" id="lblTmonth"><img src="images/balloons.gif" style="height: 25px; width: 25px;"/> Upcoming Birthdays</h5>
                                </div>
                                <div class="card-block">
                                    <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Auto">
                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4 col-md-12">
                            <div class="card ">
                                <div class="card-header">
                                    <h5><img src="images/calendar.gif" style="height: 25px; width: 25px;"/>Work Anniversary this Month</h5>
                                </div>
                                <div class="card-block">
                                    <asp:Panel ID="Panel2" runat="server" Height="250px" ScrollBars="Auto">
                                        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="col-xl-4 col-md-12">
                            <div class="card ">
                                <div class="card-header">
                                    <h5><img src="images/cake.gif" style="height: 25px; width: 25px;"/>Birthdays This Month</h5>
                                </div>
                                <div class="card-block">
                                    <asp:Panel ID="Panel3" runat="server" Height="250px" ScrollBars="Auto">
                                        <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div id="todayBirth" runat="server" class="col-xl-4 col-md-12" visible="false">
                            <div class="card ">
                                <div class="card-header">
                                    <h5><img src="images/confetti.gif" style="height: 25px; width: 25px;"/>Today's Birthday</h5>
                                </div>
                                <div class="card-block" style="background-color:#F7C1BB;">
                                    <asp:Panel ID="Panel5" runat="server" Height="327px" ScrollBars="Auto">
                                        <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <!--  Leavs and team member start -->
                        <div id="leaveApplied" class="col-xl-8 col-md-12" visible="false" style="display:none;">
                            <div class="card table-card">
                                <div class="card-header">
                                    <h5>Leaves Applied</h5>
                                    <div class="card-header-right">
                                        <ul class="list-unstyled card-option">
                                            <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                            <li><i class="fa fa-window-maximize full-card"></i></li>
                                            <li><i class="fa fa-minus minimize-card"></i></li>
                                            <li><i class="fa fa-refresh reload-card"></i></li>
                                            <li><i class="fa fa-trash close-card"></i></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="card-block">
                                    <asp:Panel ID="Panel4" runat="server" Height="350px" ScrollBars="Auto">
                                        <asp:Literal ID="Literal4" runat="server"></asp:Literal>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <!--  project and team member end -->
                    </div>
                    <div id="teamMem" runat="server" class="col-xl-4 col-md-12" visible="false">
                        <div class="card ">
                            <div class="card-header">
                                <h5>Team Members</h5>
                                <div class="card-header-right">
                                    <ul class="list-unstyled card-option">
                                        <li><i class="fa fa fa-wrench open-card-option"></i></li>
                                        <li><i class="fa fa-window-maximize full-card"></i></li>
                                        <li><i class="fa fa-minus minimize-card"></i></li>
                                        <li><i class="fa fa-refresh reload-card"></i></li>
                                        <li><i class="fa fa-trash close-card"></i></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="card-block">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
