<%@ Page Title="Leave Detail" Language="C#" MasterPageFile="~/Site.master"  MaintainScrollPositionOnPostBack="true" AutoEventWireup="true" CodeFile="LeaveDet.aspx.cs" Inherits="HRMIS.LeaveDet" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <!-- Page-header start -->
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-8">
                    <div class="page-header-title">
                        <h5 class="m-b-10">Leave Detail</h5>
                    </div>
                </div>
                <div class="col-md-4">
                    <ul class="breadcrumb-title">
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash" runat="server" OnClick="lblbtnDash_Click"><i class="fa fa-home"></i></asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash1" runat="server" OnClick="lblbtnDash1_Click">Leave</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash2" runat="server" OnClick="lblbtnDash2_Click">Leave Detail</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="main-body">
        <div class="page-wrapper">
            <div class="page-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header">
                                <div>
                                    <h5>Leave Form</h5>
                                    <p class="text-muted">
                                        Note: Operation officer will verify for change in day off request, Admin Officer will verify the absence or leave availment.<br/>
                                        The approver for Operations are the Operations Manager, for IT the IT Manager and for Admin the Operations Manager-Admin.
                                    </p>
                                    <asp:Image ID="userpicSide2" runat="server" ClientIDMode="Static" class="profile-image img-100 img-radius" alt="User-Profile-Image" style="float:right; height:100px;"></asp:Image> 
                                </div>
                                <div>
                                    <asp:Button class="btn btn-success btn-out-dashed" ID="btnPrint" runat="server" Text="Print" Visible="false" OnClientClick="JavaScript: printPartOfPage('toPrint');" />
                                    <asp:Button class="btn btn-danger btn-out-dashed" ID="btnDelete" runat="server" Text="Delete" Visible="false" OnClick="btnDelete_Click"/>
                                    <asp:Label ID="lblStat" runat="server" Text="" class=""></asp:Label>
                                </div>
                            </div>
                            <div class="card-block" id="toPrint">
                                <div class="form-control">
                                    <h4 class="sub-title">Employee Information</h4>
                                    <div class="row">
                                        <asp:TextBox type="text" class="form-control" ID="txtStoreDays" runat="server" placeholder="" name="txtStoreDays" Visible="false"></asp:TextBox>
                                        <div class="col-md-2">
                                            <h6 class="form-label">Employee No</h6>
                                            <asp:TextBox ID="txtEmpNo" class="form-control" runat="server" ReadOnly></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <h6 class="form-label">First Name</h6>
                                            <asp:TextBox ID="txtFname" class="form-control" runat="server" ReadOnly></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <h6 class="form-label">Last Name</h6>
                                            <asp:TextBox ID="txtLname" class="form-control" runat="server" ReadOnly></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <h6 class="form-label">Date/Time</h6>
                                            <asp:TextBox ID="txtDateNow" class="form-control" runat="server" ReadOnly></asp:TextBox>
                                        </div>
                                        <asp:TextBox type="text" class="form-control" ID="RHNo" runat="server" placeholder="Reporting Head" name="RHNo" ReadOnly="true" visible="false"></asp:TextBox>
                                    </div>
                                    <div class="row" style="margin-top:10px;">
                                        <div class="col-md-4 mb-3">
                                            <h6 class="form-label">Position</h6>
                                            <asp:TextBox ID="txtEmpPos" class="form-control" runat="server" ReadOnly></asp:TextBox>
                                        </div>
                                        <div class="col-md-8">
                                            <h6 class="form-label">Address while on Leave</h6>
                                            <asp:TextBox type="address" class="form-control" ID="txtaddress" runat="server" placeholder="Address" name="adress" TextMode="MultiLine" height="125px" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <h4 class="sub-title">Leave Information</h4>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <h6>Type of Absence/Leave</h6>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:CheckBox ID="vacation" runat="server" name="vacation" Text="&nbsp;&nbsp;Vacation&nbsp;/&nbsp;Loyalty" Enabled ="false"/>
                                                <br/>
                                                <asp:CheckBox ID="sick" runat="server" name="sick" Text="&nbsp;&nbsp;Sick" ClientIDMode="Static" Enabled ="false"/>
                                                <br/>
                                                <asp:CheckBox ID="maternity" runat="server" name="maternity" Text="&nbsp;&nbsp;Maternity" ClientIDMode="Static" Enabled ="false"/>
                                                <br/>
                                                <asp:CheckBox ID="paternity" runat="server" name="paternity" Text="&nbsp;&nbsp;Paternity" ClientIDMode="Static" Enabled ="false"/>
                                                <br/>
                                                <asp:CheckBox ID="emergency" runat="server" name="emergency" Text="&nbsp;&nbsp;Emergency" ClientIDMode="Static" Enabled ="false"/>
                                                <br/>
                                                <asp:CheckBox ID="undertime" runat="server" name="undertime" Text="&nbsp;&nbsp;Undertime" ClientIDMode="Static" Enabled ="false"/>
                                                <br/>
                                                <asp:CheckBox ID="changeOff" runat="server" name="changeoff" Text="&nbsp;&nbsp;Change Day Off/Shift" ClientIDMode="Static" Enabled ="false"/>
                                                <br/>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-md-5">
                                            <h6 class="form-label">Reason</h6>
                                            <asp:TextBox ID="txtReason" runat="server" class="form-control" name="reason" TextMode="MultiLine" height="125px" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 mb-3">
                                            <label>Date From</label>
                                            <asp:TextBox ID="dateFrom" runat="server" name="date" ClientIDMode="Static" class="form-control" ReadOnly="true"></asp:TextBox>
                                            <br/>
                                            <asp:TextBox type="time" ID="timeFrom" runat="server" ClientIDMode="Static" class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <label>Date To</label>
                                            <asp:TextBox ID="dateTo" runat="server" name="date" ClientIDMode="Static" class="form-control" ReadOnly="true"></asp:TextBox>
                                            <br/>
                                            <asp:TextBox type="time" ID="timeTo" runat="server" ClientIDMode="Static" class="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    <div class="form">
                                        <h4 class="sub-title" style="margin-top:20px;">Document Attached</h4>
                                        <asp:GridView ID="GridView1" class="table table-striped table-bordered nowrap" runat="server" AutoGenerateColumns="false" EmptyDataText = "No files uploaded">
                                            <Columns>
                                                <asp:BoundField DataField="Text" HeaderText="File Name" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" ClientIDMode="Static" class="btn btn-out waves-effect waves-light btn-primary btn-square" OnClick = "DownloadFile"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnPreview" Text = "Preview" CommandArgument = '<%# Eval("Value") %>' runat="server" ClientIDMode="Static" class="btn btn-out waves-effect waves-light btn-info btn-square" OnClick = "PreviewFile"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                               </div>
                                <div class="card" style="margin-top: 20px;"> <!-- Admin/Mangers Approval Area -->
                                    <div class="card-header">
                                    </div>
                                    <div class="card-block box-list">
                                        <div class="row">
                                            <div id="LeadArea" runat="server" class="col-xl-6"><!-- Leader Approval Area -->
                                                <div class="card">
                                                    <div class="card-header" style="margin-bottom: 0;">
                                                        <h5>Leader</h5>
                                                        <button id="btnsupSub" runat="server" type="button" class="btn btn-primary waves-effect waves-light" data-toggle="modal" data-target="#saveApp" style="float:right;">Submit</button>
                                                    </div>
                                                    <div class="card-block">
                                                        <asp:Label ID="msgLead" runat="server" Text="Label" Visible="false">Leader has not yet approved nor denied this leave!</asp:Label>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="leadPanel" runat="server" Visible="false">
                                                                    <asp:CheckBox ID="sapproved" runat="server" name="approved" Text="&nbsp;&nbsp;Approved" Enabled="false" OnCheckedChanged="sapproved_Checked" AutoPostBack="true"/>
                                                                    <asp:CheckBox ID="sdenied" runat="server" name="denied" Text="&nbsp;&nbsp;Denied" style="margin-left:20px;" Enabled="false" OnCheckedChanged="sdenied_Cheked" AutoPostBack="true"/>
                                                                    <br/><label>By :</label>
                                                                    <asp:TextBox type="text" class="form-control" ID="txtLeadName" runat="server" ClientIDMode="Static" placeholder="" ReadOnly="true"></asp:TextBox>
                                                                    <label>Remarks</label>
                                                                    <asp:TextBox type="text" class="form-control" ID="txtSRemarks" runat="server" ClientIDMode="Static" placeholder="Remarks" name="sreason" TextMode="MultiLine" height="70px" ReadOnly="true"></asp:TextBox>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="ManaArea" runat="server" class="col-xl-6"><!-- Manager Approval Area -->
                                                <div class="card">
                                                    <div class="card-header" style="margin-bottom: 0;">
                                                        <h5>Manager</h5>
                                                        <button id="btnmanSub" runat="server" type="button" class="btn btn-primary waves-effect waves-light" data-toggle="modal" data-target="#saveApp" style="float:right;">Submit</button>
                                                    </div>
                                                    <div class="card-block">
                                                        <asp:Label ID="msgMana" runat="server" Text="Label" Visible ="false">Manager has not yet approved nor denied this leave!</asp:Label>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="manaPanel" runat="server">
                                                                    <asp:CheckBox ID="mapproved" runat="server" name="approved" Text="&nbsp;&nbsp;Approved" Enabled="false" OnCheckedChanged="mapproved_Checked" AutoPostBack="true"/>
                                                                    <asp:CheckBox ID="mdenied" runat="server" name="denied" Text="&nbsp;&nbsp;Denied" style="margin-left:20px;" Enabled="false" OnCheckedChanged="mdenied_Checked" AutoPostBack="true"/>
                                                                    <br/><label>By :</label>
                                                                    <asp:TextBox type="text" class="form-control" ID="txtManaName" runat="server" ClientIDMode="Static" placeholder="" ReadOnly="true"></asp:TextBox>
                                                                    <label>Remarks</label>
                                                                    <asp:TextBox type="text" class="form-control" ID="txtMRemarks" runat="server" ClientIDMode="Static" placeholder="Remarks" name="sreason" TextMode="MultiLine" height="70px" ReadOnly="true"></asp:TextBox>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="AdmArea" runat="server" class="col-xl-12" visible="true"><!-- Admin Approval Area -->
                                                <div class="card">
                                                    <div class="card-header" style="margin-bottom: 0px;padding-bottom: 0px;">
                                                        <h5>Admin</h5>
                                                        <div id="buttonsAdmin" style="float: right; margin-bottom: 5px;">
                                                            <button id="btnadmSub" runat="server" type="button" ClientIDMode="Static" class="btn btn-success waves-effect waves-light" data-toggle="modal" data-target="#saveApp">Approve</button>
                                                            <button id="btnadmCan" runat="server" type="button" ClientIDMode="Static" class="btn btn-danger waves-effect waves-light" data-toggle="modal" data-target="#saveApp" style="margin-left:5px;">Cancel</button>
                                                        </div>
                                                        <asp:HiddenField ID="hiddenCommandArgument" runat="server" />
                                                    </div>
                                                    <div class="card">
                                                        <asp:Label ID="msgAdmin" runat="server" Text="Label" Visible ="false" style="text-align: center;margin-top: 10px;color: red;font-weight: 500;">This Leave File is Canceled!</asp:Label>
                                                        <div class="card-block" style="display:flex;">
                                                            <asp:Panel ID="adminPanel" runat="server" class="col-md-6">
                                                                <br/><label>By :</label>
                                                                <asp:TextBox type="text" class="form-control" ID="txtAdminName" runat="server" placeholder="" name="lname" ReadOnly="true" style="width: 90%;"></asp:TextBox>
                                                                <label>Remarks</label>
                                                                <asp:TextBox type="text" class="form-control" ID="txtAdminRemarks" runat="server" placeholder="Remarks" name="sreason" TextMode="MultiLine" Width="90%" height="100px" ReadOnly="true"></asp:TextBox>
                                                            </asp:Panel>
                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="col-md-6">
                                                                <ContentTemplate>
                                                                    <asp:Panel ID="adminPanel2" runat="server" >
                                                                        <br/><label>Apply this leave to:</label>
                                                                        <div class="row" style="display:flex;">
                                                                            <div class="col-md-12" style="text-align: center; border: 1px solid red;">
                                                                                <asp:CheckBox ID="chkbxWOPay" CssClass="styled-checkbox" runat="server" Text="Without Pay" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkReadOnly" style="margin-top: 20px;"/>
                                                                            </div>
                                                                            <div class="col-md-4" style="text-align: center;">
                                                                                <asp:CheckBox ID="chkbxAdmVL" CssClass="styled-checkbox" runat="server" Text="Vacation Leave" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkReadOnly"/>
                                                                                <asp:TextBox type="text" class="form-control" ID="txtAdmVL" runat="server" ClientIDMode="Static" onkeypress="allowOnlyNumbers(event)" onkeyup="validateInputCreditVL(this)" name="txtAdmVL" ReadOnly="true" style="text-align:center;" Text="0"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-4" style="text-align: center">
                                                                                <asp:CheckBox ID="chkbxAdmSL" CssClass="styled-checkbox" runat="server" Text="Sick Leave" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkReadOnly"/>
                                                                                <asp:TextBox type="text" class="form-control" ID="txtAdmSL" runat="server" ClientIDMode="Static" onkeypress="allowOnlyNumbers(event)" onkeyup="validateInputCreditSL(this)" name="txtAdmSL" ReadOnly="true" style="text-align:center;" Text="0"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-4" style="text-align: center">
                                                                                <asp:CheckBox ID="chkbxAdmLL" CssClass="styled-checkbox" runat="server" Text="Loyalty Leave" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkReadOnly"/>
                                                                                <asp:TextBox type="text" class="form-control" ID="txtAdmLL" runat="server" ClientIDMode="Static" onkeypress="allowOnlyNumbers(event)" onkeyup="validateInputCreditLL(this,)" name="txtAdmLL" ReadOnly="true" style="text-align:center;" Text="0"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-4" style="text-align: center">
                                                                                <asp:CheckBox ID="chkbxAdmSPL" CssClass="styled-checkbox" runat="server" Text="Special Leave" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkReadOnly"/>
                                                                                <asp:TextBox type="text" class="form-control" ID="txtAdmSPL" runat="server" ClientIDMode="Static" onkeypress="allowOnlyNumbers(event)" onkeyup="validateInputCreditSPL(this)" name="txtAdmSPL" ReadOnly="true" style="text-align:center;" Text="0"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
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
            <div class="page-body"> <!-- Leave Credits -->
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header">
                                <h5>Leave Credits</h5>
                                <p class="text-muted">
                                    Remaining Leaves
                                </p>
                            </div>
                            <div class="card-block box-list">
                                <div class="row">
                                    <div class="col-xl-3">
                                        <div class="card o-visible" data-toggle="tooltip" data-placement="top" title data-original-title="Vacation Leave">
                                            <div class="card-header">
                                                <h5>Vacation Leave</h5>
                                            </div> 
                                            <div class="card-block">
                                                <p>
                                                    You have &nbsp  
                                                    <asp:label id="lblCountVL" ClientIDMode="Static" runat="server" text="00"></asp:label>
                                                    &nbsp remaining.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-3">
                                        <div class="card o-visible" data-toggle="tooltip" data-placement="top" title data-original-title="Sick Leave">
                                            <div class="card-header">
                                                <h5>Sick Leave</h5>
                                            </div> 
                                            <div class="card-block">
                                                <p>
                                                    You have &nbsp  
                                                    <asp:label id="lblCountSL" ClientIDMode="Static" runat="server" text="00"></asp:label>
                                                    &nbsp remaining.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-3">
                                        <div class="card o-visible" data-toggle="tooltip" data-placement="top" title data-original-title="Loyalty Leave">
                                            <div class="card-header">
                                                <h5>Loyalty Leave</h5>
                                            </div> 
                                            <div class="card-block">
                                                <p>
                                                    You have &nbsp  
                                                    <asp:label id="lblCountLL" ClientIDMode="Static" runat="server" text="00"></asp:label>
                                                    &nbsp remaining.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-3">
                                        <div class="card o-visible" data-toggle="tooltip" data-placement="top" title data-original-title="Special Leave">
                                            <div class="card-header">
                                                <h5>Special Leave</h5>
                                            </div> 
                                            <div class="card-block">
                                                <p>
                                                    You have &nbsp
                                                    <asp:label id="lblCountSPL" ClientIDMode="Static" runat="server" text="00"></asp:label>
                                                    &nbsp remaining.
                                                </p>
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
    <!-----Modal----->
    <div id="saveApp" class="modal" role="dialog" style="padding-right: 17px;">
        <div class="modal-dialog">
            <div class="card">
                <div class="card-block">
                    <div class="md-float-material form-material">
                        <div class="row m-b-20">
                            <div class="col-md-12">
                                <h3 class="text-center">Leave Detail</h3>
                            </div>
                        </div>                  
                        <div class="form-group form-primary" style="text-align: -webkit-center;">
                            <h4><label id="lblmdlMsg">Are you sure you want save this?</label></h4>
                        </div>
                        <div class="row">
                            <div class="notifications" style="width:500px; display:flex;">
                                <div class="col-md-6">
                                    <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" UseSubmitBehavior="false" data-ptitle="Leave Detail" AutoPostBack="true" OnClick="btnSave_Click" data-type="success" data-msg="Successfully Save!" data-from="top" data-align="right" data-dismiss="modal" class="btn btn-success btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" CommandArgument=""/>
                                    <%--<button type="button" class="btn btn-success btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" data-type="success" data-from="top" data-align="right" data-dismiss="modal" >Save</button>--%>
                                </div>
                                <div class="col-md-6">
                                     <asp:button ID="btnCancel" runat="server" ClientIDMode="Static" text="Cancel" data-type="danger" data-ptitle="Leave Detail" data-msg="has been Cancelled!" data-from="top" data-align="right" data-dismiss="modal" class="btn btn-inverse btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" />
                                    <%--<button type="button" class="btn btn-inverse btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" data-dismiss="modal">Cancel</button>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function printPartOfPage(elementId) {
            var printContent = document.getElementById(elementId);
            var windowUrl = 'Leave Application';
            var uniqueName = new Date();
            var windowName = 'Print' + uniqueName.getTime();
            var printWindow = window.open(windowUrl, windowName, 'left=50000,top=50000,width=0,height=0');
            var scriptV = "/";
            printWindow.document.write('<html><head><title>Leave Application</title><link href="//maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css"><style>@media print { #buttonsAdmin{display:none;} #lnkDownload {display: none;} #lnkbtnPreview {display: none;} #btnsupSub{display:none;} #btnmanSub{display:none;} #btnadmSub{display:none;} #btnadmSub(display:none;) #btnadmCan{display:none;}}</style><script type = "text/javascript"> @media print {#btnSaveMana {.setAttribute("Visible","false");}}<' + scriptV + 'script></head><body onload="window.print()"><img src="images/Seiha-Eagle-Philippine-Colorways.png" style="height: 50px; width: 50px;" /><span style="font-size: 8px;">' + uniqueName + '</span></br></br><h2>Leave Application Form</h2>' + printContent.innerHTML + '</body></html>');
            printWindow.document.close();
        }
        var modalButton = document.getElementById('<%= btnSave.ClientID %>');
        function setModalApproveCancel(ID) {
            
            modalButton.id = ID.id;
            document.getElementById('<%= hiddenCommandArgument.ClientID %>').value = ID.id;
        }

        function handleButton1Click() {
            console.log('btnadmSub clicked!');
            var triggerButton = document.getElementById('<%= btnadmSub.ClientID %>');
            document.getElementById("lblmdlMsg").innerHTML = "Are you sure you want to Save this File?";
          setModalApproveCancel(triggerButton);
        }

        function handleButton2Click() {
            console.log('btnadmCan clicked!');
            var triggerButton = document.getElementById('<%= btnadmCan.ClientID %>');
            document.getElementById("lblmdlMsg").innerHTML = "Are you sure you want to Cancel this File?";
          setModalApproveCancel(triggerButton);
        }

        document.getElementById('btnadmSub').addEventListener('click', handleButton1Click);
        document.getElementById('btnadmCan').addEventListener('click', handleButton2Click);
        
    </script>

</asp:Content>