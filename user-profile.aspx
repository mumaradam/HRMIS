 <%@ Page Title="User Profile" Language="C#" MasterPageFile="~/Site.master" MaintainScrollPositionOnPostBack="true" AutoEventWireup="true" CodeFile="user-profile.aspx.cs" Inherits="HRMIS.user_profile" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<!-- Page-header start -->
<div class="page-header">
    <div class="page-block">
        <div class="row align-items-center">
            <div class="col-md-8">
                <div class="page-header-title">
                    <h5 class="m-b-10">User Profile</h5>
                </div>
            </div>
            <div class="col-md-4">
            <ul class="breadcrumb-title">
                <li class="breadcrumb-item">
                    <asp:LinkButton ID="lblbtnDash1" runat="server" OnClick="lblbtnDash1_Click"><i class="fa fa-home"></i></asp:LinkButton>
                </li>
                <li id="breadEmp" runat="server" ClientIDMode="Static" class="breadcrumb-item">
                    <asp:LinkButton ID="lblbtnDash2" runat="server" OnClick="lblbtnDash2_Click">Employee</asp:LinkButton>
                </li>
                <li class="breadcrumb-item">
                    <asp:LinkButton ID="lblbtnDash" runat="server" OnClick="lblbtnDash_Click">User Profile</asp:LinkButton>
                </li>
            </ul>
        </div>
        </div>
    </div>
</div>
<!-- Page-header end -->
<div class="pcoded-inner-content">
    <div class="main-body">
        <div class="page-wrapper">
            <!-- Page-body start -->
            <div class="page-body">
                <!--profile cover start-->
                <div class="row">
                    <div class="col-lg-12">
                        <div class="cover-profile">
                            <div class="profile-bg-img">
                                <img class="profile-bg-img img-fluid" src="../images/bg-profile.jpg" alt="bg-img">
                                <div class="card-block user-info">
                                    <div class="col-md-12">
                                        <div class="media-left">
                                            <asp:FileUpload ID="FileUploadControl" runat="server" ClientIDMode="Static" accept=".jpg, .jpeg, .png" onchange="handleFileSelection(this);" style="display:none;"/>
                                            <asp:LinkButton  ID="uploadButton" runat="server" ClientIDMode="Static" CssClass="profile-image img-100 img-radius" OnClientClick="uploadFile(event); return false;" Text="">
                                                <asp:Image ID="userpicSide2" runat="server" ClientIDMode="Static" class="profile-image img-100 img-radius" alt="User-Profile-Image" onclick="uploadFile()"></asp:Image> 
                                            </asp:LinkButton>
                                            <asp:HiddenField ID="ImageDataHiddenField" runat="server" />
                                            <asp:LinkButton ID="savePic" runat="server" ClientIDMode="Static" class="btn btn-primary btn-mini waves-effect waves-light" style="display:none" OnClick="savePic_Click"><i class="icofont icofont-info-square"></i>Save</asp:LinkButton>
                                        </div>
                                        <div class="media-body row">
                                            <div class="col-lg-12">
                                                <div class="user-title" style="bottom:auto;">
                                                    <h2><asp:Label ID="lblFullName" runat="server" text="Label"></asp:Label></h2>
                                                    <asp:Label ID="lblPosition" runat="server" text="Label" class="text-white"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--profile cover end-->
                <div class="row">
                    <div class="col-lg-12">
                        <!-- tab header start -->
                        <div class="tab-header card">
                            <ul class="nav nav-tabs md-tabs tab-timeline" role="tablist" id="mytab">
                                <li class="nav-item">
                                    <a class="nav-link active show" data-toggle="tab" href="#personal" role="tab" aria-selected="false">Personal Info</a>
                                    <div class="slide"></div>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#eduinfo" role="tab" aria-selected="false">Educational Info</a>
                                    <div class="slide"></div>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#empinfo" role="tab" aria-selected="true">Employement Info</a>
                                    <div class="slide"></div>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" data-toggle="tab" href="#leavelog" role="tab" aria-selected="false">Logs</a>
                                    <div class="slide"></div>
                                </li>
                            </ul>
                        </div>
                        <!-- tab header end -->
                        <!-- tab content start -->
                        <div class="tab-content">
                            <!-- tab panel Personal Info start -->
                            <div class="tab-pane active show" id="personal" role="tabpanel">
                                <!-- personal card start -->
                                <div class="card">
                                    <div class="card-header">
                                        <h5 class="card-header-text">About Me</h5>
                                        <button id="edit_btn" runat="server" ClientIDMode="Static" type="button" class="btn btn-sm btn-primary waves-effect waves-light f-right">
                                            <i class="icofont icofont-edit"></i>
                                        </button>
                                    </div>
                                    <div class="card-block">
                                        <div class="view-info">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="general-info">
                                                        <div class="row">
                                                            <div class="col-lg-12 col-xl-6">
                                                                <div class="table-responsive">
                                                                    <table class="table m-0">
                                                                        <tbody>
                                                                            <asp:Panel ID="AboutMePanel" runat="server" ClientIDMode="Static">
                                                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                                            </asp:Panel>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <!-- end of table col-lg-6 -->
                                                            <div class="col-lg-12 col-xl-6">
                                                                <div class="table-responsive">
                                                                    <table class="table">
                                                                        <tbody>
                                                                            <asp:Panel ID="AboutMePanel2" runat="server" ClientIDMode="Static">
                                                                                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                                                            </asp:Panel>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <!-- end of table col-lg-6 -->
                                                        </div>
                                                        <!-- end of row -->
                                                    </div>
                                                    <!-- end of general info -->
                                                </div>
                                                <!-- end of col-lg-12 -->
                                            </div>
                                            <!-- end of row -->
                                        </div>
                                        <!-- end of view-info -->
                                        <div class="edit-info" style="display: none;">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="general-info form-material">
                                                        <div class="row">
                                                            <div class="col-lg-6 ">
                                                                <div class="material-group" style="display:block;"><!----NAME--->
                                                                    <div class="material-group">
                                                                        <div class="material-addone">
                                                                            <i class="icofont icofont-user"></i>
                                                                        </div>
                                                                        <div class="form-group form-primary">
                                                                            <asp:TextBox ID="txtFname" runat="server" class="form-control" required=""></asp:TextBox>
                                                                            <span class="form-bar"></span>
                                                                            <label class="float-label">First Name</label>
                                                                        </div>
                                                                    </div> 
                                                                    <div class="material-group">
                                                                        <div class="form-group form-primary" style="margin-left: 40px;">
                                                                            <asp:TextBox ID="txtLname" runat="server" class="form-control" required=""></asp:TextBox>
                                                                            <span class="form-bar"></span>
                                                                            <label class="float-label">Last Name</label>
                                                                        </div>
                                                                    </div> 
                                                                    <div class="material-group">
                                                                        <div class="form-group form-primary" style="margin-left: 40px;">
                                                                            <asp:TextBox ID="txtMIname" runat="server" class="form-control"></asp:TextBox>
                                                                            <span class="form-bar"></span>
                                                                            <label class="float-label">Middle Name</label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="material-group"> <!----GENDER--->
                                                                    <div class="material-addone">
                                                                        <i class="fa fa-mars-double"></i>
                                                                    </div>
                                                                    <div class="form-group form-primary">
                                                                        <div class="form-radio">
                                                                            <div class="group-add-on">
                                                                                <div class="radio radiofill radio-inline">
                                                                                    <label>
                                                                                        <input type="radio" name="radio" checked=""><i class="helper"></i> Male
                                                                                    </label>
                                                                                </div>
                                                                                <div class="radio radiofill radio-inline">
                                                                                    <label>
                                                                                        <input type="radio" name="radio"><i class="helper"></i> Female
                                                                                    </label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>    
                                                                <div class="material-group"> <!----BIRTHDATE--->
                                                                    <div class="material-addone">
                                                                        <i class="fa fa-birthday-cake"></i>
                                                                    </div>
                                                                    <div class="form-group form-primary">
                                                                        <asp:TextBox ID="txtDOB" runat="server" type="date" class="form-control" required="" onfocus="clearPlaceholderValue()"></asp:TextBox>
                                                                        <span class="form-bar"></span>
                                                                        <label class="float-label"></label>
                                                                    </div>
                                                                </div>
                                                                <div class="material-group"> <!----Marital Status--->
                                                                    <div class="material-addone">
                                                                        <i class="fa fa-heart-o"></i>
                                                                    </div>
                                                                    <div class="form-group form-primary">
                                                                        <select id="hello-single" class="form-control">
                                                                        <option value="">---- Marital Status ----</option>
                                                                        <option value="single">Single</option>
                                                                        <option value="married">Married</option>
                                                                        <option value="devorced">Devorced</option>
                                                                    </select>
                                                                    <span class="form-bar"></span>
                                                                    </div>
                                                                </div>
                                                                <div class="material-group"><!----Mobile Number--->
                                                                    <div class="material-addone">
                                                                        <i class="icofont icofont-ui-touch-phone"></i>
                                                                    </div>
                                                                    <div class="form-group form-primary">
                                                                        <input type="tel" name="footer-email" class="form-control" required="" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);">
                                                                        <span class="form-bar"></span>
                                                                        <label class="float-label">Mobile Number</label>
                                                                    </div>
                                                                </div>
                                                                <div class="material-group"><!----Email Number--->
                                                                    <div class="material-addone">
                                                                        <i class="icofont icofont-email"></i>
                                                                    </div>
                                                                    <div class="form-group form-primary">
                                                                        <input type="email" name="footer-email" class="form-control" required="">
                                                                        <span class="form-bar"></span>
                                                                        <label class="float-label">Email</label>
                                                                    </div>
                                                                </div>
                                                            
                                                        </div>
                                                        <!-- end of table col-lg-6 -->
                                                        <div class="col-lg-6">
                                                            <div class="material-group" style="display:block;">
                                                                <div class="material-group">
                                                                    <div class="material-addone">
                                                                        <i class="fa fa-home"></i>
                                                                    </div>
                                                                    <div class="form-group form-primary">
                                                                        <input type="text" name="footer-email" class="form-control" required="">
                                                                        <span class="form-bar"></span>
                                                                        <label class="float-label">House/Blk. No</label>
                                                                    </div>
                                                                </div>
                                                                <div class="material-group">
                                                                    <div class="form-group form-primary" style="margin-left: 40px;">
                                                                        <input type="text" name="footer-email" class="form-control" required="">
                                                                        <span class="form-bar"></span>
                                                                        <label class="float-label">Street/Subdivision</label>
                                                                    </div>
                                                                </div>
                                                                <div class="material-group" style="margin-left: 40px;">
                                                                    <div class="form-group form-primary">
                                                                        <input type="text" name="footer-email" class="form-control" required="">
                                                                        <span class="form-bar"></span>
                                                                        <label class="float-label">Barangay</label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="material-group" style="display:block;">
                                                                <div class="material-group">
                                                                    <div class="material-addone">
                                                                        <i class="fa fa-street-view"></i>
                                                                    </div>
                                                                    <div class="form-group form-primary">
                                                                        <input type="text" name="footer-email" class="form-control" required="">
                                                                        <span class="form-bar"></span>
                                                                        <label class="float-label">City/Municipality</label>
                                                                    </div>
                                                                </div>
                                                                <div class="material-group">
                                                                    <div class="form-group form-primary" style="margin-left: 40px;">
                                                                        <input type="text" name="footer-email" class="form-control" required="">
                                                                        <span class="form-bar"></span>
                                                                        <label class="float-label">Country</label>
                                                                    </div>
                                                                </div>
                                                                <div class="material-group">
                                                                    <div class="form-group form-primary" style="margin-left: 40px;">
                                                                        <input type="text" name="footer-email" class="form-control" required="">
                                                                        <span class="form-bar"></span>
                                                                        <label class="float-label">Zip Code</label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="material-group">
                                                                <div class="material-addone">
                                                                    <i class="icofont icofont-location-pin"></i>
                                                                </div>
                                                                <div class="form-group form-primary">
                                                                    <input type="text" id="address" class="form-control" name="address" autocomplete="address" required enterkeyhint="next"></input>
                                                                    <span class="form-bar"></span>
                                                                    <label class="float-label">Provincial Address</label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <!-- end of table col-lg-6 -->
                                                    </div>
                                                    <!-- end of row -->
                                                    <div class="text-center">
                                                        <a href="#!" class="btn btn-primary waves-effect waves-light m-r-20">Save</a>
                                                        <a href="#!" id="edit-cancel" class="btn btn-default waves-effect">Cancel</a>
                                                    </div>
                                                </div>
                                                <!-- end of edit info -->
                                            </div>
                                            <!-- end of col-lg-12 -->
                                        </div>
                                        <!-- end of row -->
                                    </div>
                                    <!-- end of edit-info -->
                                </div>
                                <!-- end of card-block -->
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header">
                                            <h5 class="card-header-text">Employee Information</h5>
                                            <button id="edit-info-emp-info" type="button" class="btn btn-sm btn-primary waves-effect waves-light f-right">
                                                <i class="icofont icofont-edit"></i>
                                            </button>
                                        </div>
                                        <!-- DISPLAY -->
                                        <div class="card-block user-desc">
                                            <div class="view-emp-info">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="general-info">
                                                            <div class="row">
                                                                <div class="col-lg-12 col-xl-6">
                                                                    <div class="table-responsive">
                                                                        <table class="table m-0">
                                                                            <tbody>

                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                                <!-- end of table col-lg-6 -->
                                                                <div class="col-lg-12 col-xl-6">
                                                                    <div class="table-responsive">
                                                                        <table class="table">
                                                                            <tbody>

                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                                <!-- end of table col-lg-6 -->
                                                            </div>
                                                            <!-- end of row -->
                                                        </div>
                                                        <!-- end of general info -->
                                                    </div>
                                                    <!-- end of col-lg-12 -->
                                                </div>
                                                <!-- end of row -->
                                            </div>
                                            <!-- EDIT -->
                                            <div class="edit-emp-info" style="display: none;">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="general-info form-material">
                                                            <div class="row">
                                                                <div class="col-lg-6 ">
                                                                    <div class="material-group" style="display:block;">
                                                                        <div class="material-group">
                                                                            <div class="material-addone">
                                                                                <i class="icofont icofont-user"></i>
                                                                            </div>
                                                                            <div class="form-group form-primary">
                                                                                <asp:TextBox ID="txtEmpNo" runat="server" class="form-control" required=""></asp:TextBox>
                                                                                <span class="form-bar"></span>
                                                                                <label class="float-label">Employee No</label>
                                                                            </div>
                                                                        </div> 
                                                                        <div class="material-group">
                                                                            <div class="form-group form-primary" style="margin-left: 40px;">
                                                                                <asp:TextBox ID="TextBox2" runat="server" class="form-control" required=""></asp:TextBox>
                                                                                <span class="form-bar"></span>
                                                                                <label class="float-label">Last Name</label>
                                                                            </div>
                                                                        </div> 
                                                                        <div class="material-group">
                                                                            <div class="form-group form-primary" style="margin-left: 40px;">
                                                                                <asp:TextBox ID="TextBox3" runat="server" class="form-control"></asp:TextBox>
                                                                                <span class="form-bar"></span>
                                                                                <label class="float-label">Middle Name</label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="material-group"> <!----GENDER--->
                                                                        <div class="material-addone">
                                                                            <i class="fa fa-mars-double"></i>
                                                                        </div>
                                                                        <div class="form-group form-primary">
                                                                            <div class="form-radio">
                                                                                <div class="group-add-on">
                                                                                    <div class="radio radiofill radio-inline">
                                                                                        <label>
                                                                                            <input type="radio" name="radio" checked=""><i class="helper"></i> Male
                                                                                        </label>
                                                                                    </div>
                                                                                    <div class="radio radiofill radio-inline">
                                                                                        <label>
                                                                                            <input type="radio" name="radio"><i class="helper"></i> Female
                                                                                        </label>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>    
                                                                    <div class="material-group"> <!----BIRTHDATE--->
                                                                        <div class="material-addone">
                                                                            <i class="fa fa-birthday-cake"></i>
                                                                        </div>
                                                                        <div class="form-group form-primary">
                                                                            <asp:TextBox ID="TextBox4" runat="server" type="date" class="form-control" required="" onfocus="clearPlaceholderValue()"></asp:TextBox>
                                                                            <span class="form-bar"></span>
                                                                            <label class="float-label"></label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="material-group"> <!----Marital Status--->
                                                                        <div class="material-addone">
                                                                            <i class="fa fa-heart-o"></i>
                                                                        </div>
                                                                        <div class="form-group form-primary">
                                                                            <select id="hello-single" class="form-control">
                                                                            <option value="">---- Marital Status ----</option>
                                                                            <option value="single">Single</option>
                                                                            <option value="married">Married</option>
                                                                            <option value="devorced">Devorced</option>
                                                                        </select>
                                                                        <span class="form-bar"></span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="material-group"><!----Mobile Number--->
                                                                        <div class="material-addone">
                                                                            <i class="icofont icofont-ui-touch-phone"></i>
                                                                        </div>
                                                                        <div class="form-group form-primary">
                                                                            <input type="tel" name="footer-email" class="form-control" required="" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);">
                                                                            <span class="form-bar"></span>
                                                                            <label class="float-label">Mobile Number</label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="material-group"><!----Email Number--->
                                                                        <div class="material-addone">
                                                                            <i class="icofont icofont-email"></i>
                                                                        </div>
                                                                        <div class="form-group form-primary">
                                                                            <input type="email" name="footer-email" class="form-control" required="">
                                                                            <span class="form-bar"></span>
                                                                            <label class="float-label">Email</label>
                                                                        </div>
                                                                    </div>
                                                            
                                                            </div>
                                                            <!-- end of table col-lg-6 -->
                                                            <div class="col-lg-6">
                                                                <div class="material-group" style="display:block;">
                                                                    <div class="material-group">
                                                                        <div class="material-addone">
                                                                            <i class="fa fa-home"></i>
                                                                        </div>
                                                                        <div class="form-group form-primary">
                                                                            <input type="text" name="footer-email" class="form-control" required="">
                                                                            <span class="form-bar"></span>
                                                                            <label class="float-label">House/Blk. No</label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="material-group">
                                                                        <div class="form-group form-primary" style="margin-left: 40px;">
                                                                            <input type="text" name="footer-email" class="form-control" required="">
                                                                            <span class="form-bar"></span>
                                                                            <label class="float-label">Street/Subdivision</label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="material-group" style="margin-left: 40px;">
                                                                        <div class="form-group form-primary">
                                                                            <input type="text" name="footer-email" class="form-control" required="">
                                                                            <span class="form-bar"></span>
                                                                            <label class="float-label">Barangay</label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="material-group" style="display:block;">
                                                                    <div class="material-group">
                                                                        <div class="material-addone">
                                                                            <i class="fa fa-street-view"></i>
                                                                        </div>
                                                                        <div class="form-group form-primary">
                                                                            <input type="text" name="footer-email" class="form-control" required="">
                                                                            <span class="form-bar"></span>
                                                                            <label class="float-label">City/Municipality</label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="material-group">
                                                                        <div class="form-group form-primary" style="margin-left: 40px;">
                                                                            <input type="text" name="footer-email" class="form-control" required="">
                                                                            <span class="form-bar"></span>
                                                                            <label class="float-label">Country</label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="material-group">
                                                                        <div class="form-group form-primary" style="margin-left: 40px;">
                                                                            <input type="text" name="footer-email" class="form-control" required="">
                                                                            <span class="form-bar"></span>
                                                                            <label class="float-label">Zip Code</label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="material-group">
                                                                    <div class="material-addone">
                                                                        <i class="icofont icofont-location-pin"></i>
                                                                    </div>
                                                                    <div class="form-group form-primary">
                                                                        <input type="text" id="address" class="form-control" name="address" autocomplete="address" required enterkeyhint="next"></input>
                                                                        <span class="form-bar"></span>
                                                                        <label class="float-label">Provincial Address</label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- end of table col-lg-6 -->
                                                        </div>
                                                        <!-- end of row -->
                                                            <div class="text-center">
                                                                <a href="#!" class="btn btn-primary waves-effect waves-light m-r-20">Save</a>
                                                                <a href="#!" id="edit-cancel-emp-info" class="btn btn-default waves-effect">Cancel</a>
                                                            </div>
                                                        </div>
                                                    <!-- end of edit info -->
                                                    </div>
                                                    <!-- end of col-lg-12 -->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                                <!-- personal card end-->
                           </div>
                            <!-- tab pane Personal Info end -->
                            <!-- tab pane Educational Info start -->
                            <div class="tab-pane" id="eduinfo" role="tabpanel">
                                <!-- info card start -->
                                <div class="card">
                                    <div class="card-header">
                                        <h5 class="card-header-text">Educational Background</h5>
                                    </div>
                                    <div class="card-block">
                                        <div class="row">

                                        </div>
                                    </div>
                                </div>
                                <!-- info card end -->
                            </div>
                            <!-- tab Educational Info end -->
                            <!-- tab pane Employement Info start -->
                            <div class="tab-pane" id="empinfo" role="tabpanel">
                                <!-- info card start -->
                                <div class="card">
                                    <div class="card-header">
                                        <h5 class="card-header-text">Employement Data</h5>
                                    </div>
                                    <div class="card-block">
                                        <div class="row">

                                        </div>
                                    </div>
                                </div>
                                <div class="card">
                                    <div class="card-header">
                                        <h5 class="card-header-text">Employement History</h5>
                                    </div>
                                    <div class="card-block">
                                        <div class="row">

                                        </div>
                                    </div>
                                </div>
                                <!-- info card end -->
                            </div>
                            <!-- tab pane employement info end -->
                            <div class="tab-pane" id="leavelog" role="tabpanel">
                                <!-- info card start -->
                                <div class="card">
                                    <div class="card-header">
                                        <h5 class="card-header-text">Leaves Filed</h5>
                                    </div>
                                    <div class="card-block">
                                        <table id="new-cons" class="table table-striped table-bordered break">
                                            <thead>
                                                <tr>
                                                    <th>Date Filed</th>
                                                    <th>Type</th>
                                                    <th>Leave Date</th>
                                                    <th>Total Days</th>
                                                    <th>Status</th>
                                                    <th>Reason</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Panel ID="Panel1" runat="server" ClientIDMode="Static">
                                                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                                </asp:Panel>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <!-- info card end -->
                            </div><!--tab Leave Logs end-->
                        </div>
                        <!-- tab content end -->
                    </div>
                </div>
            </div>
            <!-- Page-body end -->
        </div>
    </div>
</div>
    
<script>
    function clearPlaceholderValue() {
        var dateInput = document.getElementById("empdt");
        dateInput.placeholder = "";
    }
    function uploadFile(e) {
        e.preventDefault();
        document.getElementById("FileUploadControl").click();
    }
    function handleFileSelection(fileInput) {
        var selectedFile = fileInput.files[0];
        // Perform actions with the selected file
        if (selectedFile) {
            // Read the file and convert it to base64
            var reader = new FileReader();
            reader.onload = function (e) {
                var profileImage2 = document.getElementById("userpicSide2");
                var savePic = document.getElementById("savePic");
                profileImage2.src = e.target.result;
                document.getElementById('<%= ImageDataHiddenField.ClientID %>').value = e.target.result; // Set the value of the hidden field
                savePic.style.display = "";
                //saveImageToDatabase();
                
            };
            reader.readAsDataURL(selectedFile);
        }
    }
</script>
</asp:Content>
