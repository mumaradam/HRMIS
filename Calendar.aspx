<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="Calendar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calendar Schedule</title>
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
    <link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap4-admin-compress.min.css"/>

</head>
<body>
    <form id="form1" runat="server">
        <nav>
        <div class="nav-bar">
            <i class='bx bx-menu sidebarOpen' ></i>
            <img src="images/Seiha-Eagle-Philippine-Colorways.png" style="height: 50px; width: 50px;" />
            <span class="logo navLogo"><a href="#">HRMIS</a></span>

            <div class="menu">
                <div class="logo-toggle">
                    <span class="logo">
                        <asp:LinkButton ID="LinkButton1" runat="server" style="font-size: 25px; font-weight: 500; color: var(--text-color); text-decoration: none;" OnClick="lblDash_Click">HRMIS</asp:LinkButton>
                    </span>
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
                        <li><asp:LinkButton ID="lblLeaveC" runat="server" class="labelStyle" style="color: #242526;">Leave Credits</asp:LinkButton></li>
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
                        <asp:LinkButton ID="LinkButton4" runat="server" class="labelStyle">Notifications
                        <span class="badge badge-pill badge-primary ml-2">5</span>
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
                <div style="margin-right:-110px; padding-left:15px; position:relative; display:flex;">
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
            <h1 style="color:#3075BA;">Event Calendar</h1>
            <div aria-label="breadcrumb">
                <ol class="breadcrumb pmd-breadcrumb mb-0">
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="LinkButton2" runat="server" class="labelStyle" style="color: #242526;" OnClick="lblDash_Click">Dashboard</asp:LinkButton>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Calendar</li>
                </ol>
            </div>
        </div>
        <div class ="well">
            <div class="fc-toolbar fc-header-toolbar">
                <div class="fc-left">
                    <button type="button" class="fc-today-button btn btn-primary disabled" disabled="">today</button>
                </div>
                <div class="fc-right">
                    <div class="btn-group">
                        <button type="button" class="fc-month-button btn btn-primary active">month</button>
                        <button type="button" class="fc-agendaWeek-button btn btn-primary ">week</button>
                        <button type="button" class="fc-agendaDay-button btn btn-primary ">day</button>
                        <button type="button" class="fc-listMonth-button btn btn-primary ">list</button>
                    </div>
                </div>
                <div class="fc-center">
                    <div>
                        <%--<button id="PrevMonth" type="button" OnClick="PrevMonth_Click" class="fc-prev-button btn btn-primary" aria-label="prev" runat="server"><span class="fa fa-chevron-left"></span></button>--%>
                        <asp:Button ID="PrevMonth" class="fc-prev-button btn btn-primary" runat="server" Text="&lt;" aria-label="prev" Onclick="PrevMonth_Click"/>
                        <asp:Label ID="MonthYear" runat="server" Text=""></asp:Label>
                        <%--<h2>December 2022</h2>--%>
                        <asp:Button ID="NextMonth" class="fc-next-button btn btn-primary" runat="server" Text="&gt;" aria-label="next" Onclick="NextMonth_Click"/>
                        <%--<button id="NextMonth" type="button" class="fc-next-button btn btn-primary" aria-label="next" runat="server"><span class="fa fa-chevron-right"></span></button>--%>
                    </div>
                </div>
                <div class="fc-clear">
                </div>
            </div>
        </div>
        <div class="well">
                <div id="calendar" class="pmd-calendar fc fc-bootstrap4 fc-ltr" style="margin-top: 45px;">
                    <div class="fc-view-container" style="">
                        <div class="fc-view fc-month-view fc-basic-view" style="">
                            <asp:Table ID="CalendarTable" runat="server" class="table-bordered">
                                
                            </asp:Table>
                            <table class="table-bordered" runat="server" visible="false">
                                <thead class="fc-head">
                                <tr>
                                    <td class="fc-head-container ">
                                        <div class="fc-row table-bordered">
                                            <table class="table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th class="fc-day-header  fc-sun" style="padding: 0.75rem; font-size:1.5rem;"><span>Sun</span></th>
                                                        <th class="fc-day-header  fc-mon" style="padding: 0.75rem; font-size:1.5rem;"><span>Mon</span></th>
                                                        <th class="fc-day-header  fc-tue" style="padding: 0.75rem; font-size:1.5rem;"><span>Tue</span></th>
                                                        <th class="fc-day-header  fc-wed" style="padding: 0.75rem; font-size:1.5rem;"><span>Wed</span></th>
                                                        <th class="fc-day-header  fc-thu" style="padding: 0.75rem; font-size:1.5rem;"><span>Thu</span></th>
                                                        <th class="fc-day-header  fc-fri" style="padding: 0.75rem; font-size:1.5rem;"><span>Fri</span></th>
                                                        <th class="fc-day-header  fc-sat" style="padding: 0.75rem; font-size:1.5rem;"><span>Sat</span></th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                </thead>
                                <tbody class="fc-body">
                                    <tr>
                                        <td class="">
                                            <div class="fc-scroller fc-day-grid-container" style="overflow: hidden; height: 742px;">
                                                <div class="fc-day-grid fc-unselectable">
                                                    <div class="fc-row fc-week table-bordered fc-rigid" style="height: 123px;">
                                                        <div class="fc-bg">
                                                            <table class="table-bordered">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="fc-day  fc-sun fc-other-month fc-past" data-date="2022-11-27"></td>
                                                                    <td class="fc-day  fc-mon fc-other-month fc-past" data-date="2022-11-28"></td>
                                                                    <td class="fc-day  fc-tue fc-other-month fc-past" data-date="2022-11-29"></td>
                                                                    <td class="fc-day  fc-wed fc-other-month fc-past" data-date="2022-11-30"></td>
                                                                    <td class="fc-day  fc-thu fc-past" data-date="2022-12-01"></td>
                                                                    <td class="fc-day  fc-fri fc-past" data-date="2022-12-02"></td>
                                                                    <td class="fc-day  fc-sat fc-past" data-date="2022-12-03"></td>
                                                                </tr>
                                                            </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="fc-content-skeleton">
                                                            <table>
                                                                <thead>
                                                                    <tr>
                                                                        <td class="fc-day-top fc-sun fc-other-month fc-past" data-date="2022-11-27"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-11-27&quot;,&quot;type&quot;:&quot;day&quot;}" >27</a></td>
                                                                        <td class="fc-day-top fc-mon fc-other-month fc-past" data-date="2022-11-28"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-11-28&quot;,&quot;type&quot;:&quot;day&quot;}">28</a></td>
                                                                        <td class="fc-day-top fc-tue fc-other-month fc-past" data-date="2022-11-29"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-11-29&quot;,&quot;type&quot;:&quot;day&quot;}">29</a></td>
                                                                        <td class="fc-day-top fc-wed fc-other-month fc-past" data-date="2022-11-30"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-11-30&quot;,&quot;type&quot;:&quot;day&quot;}">30</a></td>
                                                                        <td class="fc-day-top fc-thu fc-past" data-date="2022-12-01"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-01&quot;,&quot;type&quot;:&quot;day&quot;}">1</a></td>
                                                                        <td class="fc-day-top fc-fri fc-past" data-date="2022-12-02"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-02&quot;,&quot;type&quot;:&quot;day&quot;}">2</a></td>
                                                                        <td class="fc-day-top fc-sat fc-past" data-date="2022-12-03"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-03&quot;,&quot;type&quot;:&quot;day&quot;}">3</a></td>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td><a style="align-content:center;">Testing sa Leave</a></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div class="fc-row fc-week table-bordered fc-rigid" style="height: 123px;">
                                                        <div class="fc-bg">
                                                            <table class="table-bordered">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="fc-day  fc-sun fc-past" data-date="2022-12-04"></td><td class="fc-day  fc-mon fc-past" data-date="2022-12-05"></td>
                                                                    <td class="fc-day  fc-tue fc-past" data-date="2022-12-06"></td><td class="fc-day  fc-wed fc-past" data-date="2022-12-07"></td>
                                                                    <td class="fc-day  fc-thu fc-past" data-date="2022-12-08"></td><td class="fc-day  fc-fri fc-past" data-date="2022-12-09"></td>
                                                                    <td class="fc-day  fc-sat fc-past" data-date="2022-12-10"></td>
                                                                </tr>
                                                            </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="fc-content-skeleton">
                                                            <table>
                                                                <thead>
                                                                    <tr>
                                                                        <td class="fc-day-top fc-sun fc-past" data-date="2022-12-04"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-04&quot;,&quot;type&quot;:&quot;day&quot;}">4</a></td>
                                                                        <td class="fc-day-top fc-mon fc-past" data-date="2022-12-05"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-05&quot;,&quot;type&quot;:&quot;day&quot;}">5</a></td>
                                                                        <td class="fc-day-top fc-tue fc-past" data-date="2022-12-06"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-06&quot;,&quot;type&quot;:&quot;day&quot;}">6</a></td>
                                                                        <td class="fc-day-top fc-wed fc-past" data-date="2022-12-07"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-07&quot;,&quot;type&quot;:&quot;day&quot;}">7</a></td>
                                                                        <td class="fc-day-top fc-thu fc-past" data-date="2022-12-08"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-08&quot;,&quot;type&quot;:&quot;day&quot;}">8</a></td>
                                                                        <td class="fc-day-top fc-fri fc-past" data-date="2022-12-09"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-09&quot;,&quot;type&quot;:&quot;day&quot;}">9</a></td>
                                                                        <td class="fc-day-top fc-sat fc-past" data-date="2022-12-10"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-10&quot;,&quot;type&quot;:&quot;day&quot;}">10</a></td>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td><td></td><td></td><td></td></tr></tbody></table></div></div><div class="fc-row fc-week table-bordered fc-rigid" style="height: 123px;"><div class="fc-bg"><table class="table-bordered"><tbody><tr><td class="fc-day  fc-sun fc-past" data-date="2022-12-11"></td><td class="fc-day  fc-mon fc-today alert alert-info" data-date="2022-12-12"></td><td class="fc-day  fc-tue fc-future" data-date="2022-12-13"></td><td class="fc-day  fc-wed fc-future" data-date="2022-12-14"></td><td class="fc-day  fc-thu fc-future" data-date="2022-12-15"></td><td class="fc-day  fc-fri fc-future" data-date="2022-12-16"></td><td class="fc-day  fc-sat fc-future" data-date="2022-12-17"></td></tr></tbody></table></div><div class="fc-content-skeleton"><table><thead><tr><td class="fc-day-top fc-sun fc-past" data-date="2022-12-11"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-11&quot;,&quot;type&quot;:&quot;day&quot;}">11</a></td><td class="fc-day-top fc-mon fc-today alert alert-info" data-date="2022-12-12"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-12&quot;,&quot;type&quot;:&quot;day&quot;}">12</a></td><td class="fc-day-top fc-tue fc-future" data-date="2022-12-13"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-13&quot;,&quot;type&quot;:&quot;day&quot;}">13</a></td><td class="fc-day-top fc-wed fc-future" data-date="2022-12-14"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-14&quot;,&quot;type&quot;:&quot;day&quot;}">14</a></td><td class="fc-day-top fc-thu fc-future" data-date="2022-12-15"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-15&quot;,&quot;type&quot;:&quot;day&quot;}">15</a></td><td class="fc-day-top fc-fri fc-future" data-date="2022-12-16"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-16&quot;,&quot;type&quot;:&quot;day&quot;}">16</a></td><td class="fc-day-top fc-sat fc-future" data-date="2022-12-17"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-17&quot;,&quot;type&quot;:&quot;day&quot;}">17</a></td></tr></thead><tbody><tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr></tbody></table></div></div><div class="fc-row fc-week table-bordered fc-rigid" style="height: 123px;"><div class="fc-bg"><table class="table-bordered"><tbody><tr><td class="fc-day  fc-sun fc-future" data-date="2022-12-18"></td><td class="fc-day  fc-mon fc-future" data-date="2022-12-19"></td><td class="fc-day  fc-tue fc-future" data-date="2022-12-20"></td><td class="fc-day  fc-wed fc-future" data-date="2022-12-21"></td><td class="fc-day  fc-thu fc-future" data-date="2022-12-22"></td><td class="fc-day  fc-fri fc-future" data-date="2022-12-23"></td><td class="fc-day  fc-sat fc-future" data-date="2022-12-24"></td></tr></tbody></table></div><div class="fc-content-skeleton"><table><thead><tr><td class="fc-day-top fc-sun fc-future" data-date="2022-12-18"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-18&quot;,&quot;type&quot;:&quot;day&quot;}">18</a></td><td class="fc-day-top fc-mon fc-future" data-date="2022-12-19"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-19&quot;,&quot;type&quot;:&quot;day&quot;}">19</a></td><td class="fc-day-top fc-tue fc-future" data-date="2022-12-20"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-20&quot;,&quot;type&quot;:&quot;day&quot;}">20</a></td><td class="fc-day-top fc-wed fc-future" data-date="2022-12-21"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-21&quot;,&quot;type&quot;:&quot;day&quot;}">21</a></td><td class="fc-day-top fc-thu fc-future" data-date="2022-12-22"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-22&quot;,&quot;type&quot;:&quot;day&quot;}">22</a></td><td class="fc-day-top fc-fri fc-future" data-date="2022-12-23"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-23&quot;,&quot;type&quot;:&quot;day&quot;}">23</a></td><td class="fc-day-top fc-sat fc-future" data-date="2022-12-24"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-24&quot;,&quot;type&quot;:&quot;day&quot;}">24</a></td></tr></thead><tbody><tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr></tbody></table></div></div><div class="fc-row fc-week table-bordered fc-rigid" style="height: 123px;"><div class="fc-bg"><table class="table-bordered"><tbody><tr><td class="fc-day  fc-sun fc-future" data-date="2022-12-25"></td><td class="fc-day  fc-mon fc-future" data-date="2022-12-26"></td><td class="fc-day  fc-tue fc-future" data-date="2022-12-27"></td><td class="fc-day  fc-wed fc-future" data-date="2022-12-28"></td><td class="fc-day  fc-thu fc-future" data-date="2022-12-29"></td><td class="fc-day  fc-fri fc-future" data-date="2022-12-30"></td><td class="fc-day  fc-sat fc-future" data-date="2022-12-31"></td></tr></tbody></table></div><div class="fc-content-skeleton"><table><thead><tr><td class="fc-day-top fc-sun fc-future" data-date="2022-12-25"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-25&quot;,&quot;type&quot;:&quot;day&quot;}">25</a></td><td class="fc-day-top fc-mon fc-future" data-date="2022-12-26"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-26&quot;,&quot;type&quot;:&quot;day&quot;}">26</a></td><td class="fc-day-top fc-tue fc-future" data-date="2022-12-27"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-27&quot;,&quot;type&quot;:&quot;day&quot;}">27</a></td><td class="fc-day-top fc-wed fc-future" data-date="2022-12-28"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-28&quot;,&quot;type&quot;:&quot;day&quot;}">28</a></td><td class="fc-day-top fc-thu fc-future" data-date="2022-12-29"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-29&quot;,&quot;type&quot;:&quot;day&quot;}">29</a></td><td class="fc-day-top fc-fri fc-future" data-date="2022-12-30"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-30&quot;,&quot;type&quot;:&quot;day&quot;}">30</a></td><td class="fc-day-top fc-sat fc-future" data-date="2022-12-31"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2022-12-31&quot;,&quot;type&quot;:&quot;day&quot;}">31</a></td></tr></thead><tbody><tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr></tbody></table></div></div><div class="fc-row fc-week table-bordered fc-rigid" style="height: 127px;"><div class="fc-bg"><table class="table-bordered"><tbody><tr><td class="fc-day  fc-sun fc-other-month fc-future" data-date="2023-01-01"></td><td class="fc-day  fc-mon fc-other-month fc-future" data-date="2023-01-02"></td><td class="fc-day  fc-tue fc-other-month fc-future" data-date="2023-01-03"></td><td class="fc-day  fc-wed fc-other-month fc-future" data-date="2023-01-04"></td><td class="fc-day  fc-thu fc-other-month fc-future" data-date="2023-01-05"></td><td class="fc-day  fc-fri fc-other-month fc-future" data-date="2023-01-06"></td><td class="fc-day  fc-sat fc-other-month fc-future" data-date="2023-01-07"></td></tr></tbody></table></div><div class="fc-content-skeleton"><table><thead><tr><td class="fc-day-top fc-sun fc-other-month fc-future" data-date="2023-01-01"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2023-01-01&quot;,&quot;type&quot;:&quot;day&quot;}">1</a></td><td class="fc-day-top fc-mon fc-other-month fc-future" data-date="2023-01-02"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2023-01-02&quot;,&quot;type&quot;:&quot;day&quot;}">2</a></td><td class="fc-day-top fc-tue fc-other-month fc-future" data-date="2023-01-03"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2023-01-03&quot;,&quot;type&quot;:&quot;day&quot;}">3</a></td><td class="fc-day-top fc-wed fc-other-month fc-future" data-date="2023-01-04"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2023-01-04&quot;,&quot;type&quot;:&quot;day&quot;}">4</a></td><td class="fc-day-top fc-thu fc-other-month fc-future" data-date="2023-01-05"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2023-01-05&quot;,&quot;type&quot;:&quot;day&quot;}">5</a></td><td class="fc-day-top fc-fri fc-other-month fc-future" data-date="2023-01-06"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2023-01-06&quot;,&quot;type&quot;:&quot;day&quot;}">6</a></td><td class="fc-day-top fc-sat fc-other-month fc-future" data-date="2023-01-07"><a class="fc-day-number" data-goto="{&quot;date&quot;:&quot;2023-01-07&quot;,&quot;type&quot;:&quot;day&quot;}">7</a></td></tr></thead><tbody><tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr></tbody></table></div></div></div></div></td></tr></tbody></table></div></div></div>
	        
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
