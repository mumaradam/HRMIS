<%@ Page Title="Leave Application" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" MaintainScrollPositionOnPostBack="true" CodeFile="LeaveApp.aspx.cs" Inherits="HRMIS.LeaveApp" %>
<asp:Content ID="scriptHead" ContentPlaceHolderID="ChildScript" runat="server" >
    <script>
        window.addEventListener('load', function () {
            // Send a message to other tabs
            window.postMessage('Hello', '*');
        });

        window.addEventListener('message', function (event) {
            // Check if the message is from the same origin and contains the expected data
            if (event.origin === window.location.origin && event.data === 'Hello') {
                // Tabs are on the same page
                console.log('Tabs are on the same page');
            }
        });
    </script>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <!-- Page-header start -->
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-8">
                    <div class="page-header-title">
                        <h5 class="m-b-10">Leave Application</h5>
                    </div>
                </div>
                <div class="col-md-4">
                    <ul class="breadcrumb-title">
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash1" runat="server" OnClick="lblbtnDash_Click"><i class="fa fa-home"></i></asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash" runat="server" OnClick="lblbtnDash_Click">Leave Application</asp:LinkButton>
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
                                <h5>Leave Form</h5>
                                <p class="text-muted">
                                    Note: Operation officer will verify for change in day off request, Admin Officer will verify the absence or leave availment.<br/>
                                    The approver for Operations are the Operations Manager, for IT the IT Manager and for Admin the Operations Manager-Admin.
                                </p>
                            </div>
                            <div class="card-block">
                                <div class="form-control">
                                    <h4 class="sub-title">Employee Information</h4>
                                    <div class="row">
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
                                            <h6 class="form-label">Date</h6>
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
                                            <asp:TextBox type="address" class="form-control" ID="txtaddress" runat="server" placeholder="Address" name="adress" TextMode="MultiLine" height="125px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <h4 class="sub-title">Leave Information</h4>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="row">
                                        <ContentTemplate>
                                        <div class="col-md-3">
                                            <h6>Type of Absence/Leave</h6>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:CheckBox ID="vacation" runat="server" name="vacation" Text="&nbsp;&nbsp;Vacation&nbsp;/&nbsp;Loyalty" OnCheckedChanged="vacation_Checked" AutoPostBack="true"/>
                                                <br/>
                                                <asp:CheckBox ID="sick" runat="server" name="sick" Text="&nbsp;&nbsp;Sick" ClientIDMode="Static" OnCheckedChanged="sick_Checked" AutoPostBack="true"/>
                                                <br/>
                                                <asp:CheckBox ID="maternity" runat="server" name="maternity" Text="&nbsp;&nbsp;Maternity" ClientIDMode="Static" OnCheckedChanged="maternity_Checked" AutoPostBack="true"/>
                                                <br/>
                                                <asp:CheckBox ID="paternity" runat="server" name="paternity" Text="&nbsp;&nbsp;Paternity" ClientIDMode="Static" OnCheckedChanged="paternity_Checked" AutoPostBack="true"/>
                                                <br/>
                                                <asp:CheckBox ID="emergency" runat="server" name="emergency" Text="&nbsp;&nbsp;Emergency" ClientIDMode="Static" OnCheckedChanged="emergency_Checked" AutoPostBack="true"/>
                                                <br/>
                                                <asp:CheckBox ID="undertime" runat="server" name="undertime" Text="&nbsp;&nbsp;Undertime" ClientIDMode="Static" OnCheckedChanged="undertime_Checked" AutoPostBack="true"/>
                                                <br/>
                                                <asp:CheckBox ID="changeOff" runat="server" name="changeoff" Text="&nbsp;&nbsp;Change Day Off/Shift" ClientIDMode="Static" OnCheckedChanged="changeOff_Checked" AutoPostBack="true"/>
                                                <br/>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-md-5">
                                            <h6 class="form-label">Reason</h6>
                                            <asp:TextBox ID="txtReason" runat="server" class="form-control" name="reason" TextMode="MultiLine" height="125px"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2 mb-3">
                                            <label>Date From</label>
                                            <asp:TextBox type="date" ID="dateFrom" runat="server" ClientIDMode="Static" class="form-control" required></asp:TextBox>
                                            <br/>
                                            <asp:TextBox type="time" ID="timeFrom" runat="server" ClientIDMode="Static" class="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                          <label>Date To</label>
                                            <asp:TextBox type="date" ID="dateTo" runat="server" ClientIDMode="Static" class="form-control" required></asp:TextBox>
                                            <br/>
                                            <asp:TextBox type="time" ID="timeTo" runat="server" ClientIDMode="Static" class="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                     </ContentTemplate>
                                   </asp:UpdatePanel>
                                    <div class="form">
                                        <h4 class="sub-title" style="margin-top:20px;">Document Attached</h4>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="form-group row">
                                            <ContentTemplate>
                                                <div class="col-sm-8">
                                                    <label>maximum of 2 files per application</label>
                                                    <asp:FileUpload id="FileUploadControl" runat="server" ClientIDMode="Static" accept=".jpg, .jpeg, .png, .doc, .docx, .pdf" class="form-control" />
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Button class="btn btn-inverse btn-round waves-effect waves-light" runat="server" id="UploadButton" text="Upload" ClientIDMode="Static" OnClick="UploadButton_Click" style="margin:25px;"/>
                                                </div>
                                                <br />
                                                <div class="card-block table-border-style" style="margin-top:-40px;padding: 15px;">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="grdvFiles" CssClass="grdHeader table-hover" HeaderStyle-Height="50" style="font-size:12px; width:790px; padding: 0.75rem;" runat="server" AutoGenerateColumns="false" EmptyDataText = "No files uploaded">
                                                            <Columns>
                                                                <asp:BoundField DataField="Text" HeaderText="File Attached" HeaderStyle-VerticalAlign="Middle"/>
                                                                <asp:TemplateField HeaderText="Action" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="text-center" HeaderStyle-VerticalAlign="Middle" >
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDelete" class="btn btn-danger waves-effect waves-light" Text = "Delete" AppendDataBoundItems="False" AutoPostBack="true" CommandArgument = '<%# Eval("Value") %>' runat = "server" OnClick="DeleteFile" >
                                                                            <i class="ti-trash"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="UploadButton"/>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                   <button type="button" class="btn btn-primary waves-effect waves-light" OnClick="validateFormAndShowModal();">Save</button> 
                               </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="page-body">
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
                                                    <asp:label id="lblCountVL" runat="server" text="00"></asp:label>
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
                                                    <asp:label id="lblCountSL" runat="server" text="00"></asp:label>
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
                                                    <asp:label id="lblCountLL" runat="server" text="00"></asp:label>
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
                                                    <asp:label id="lblCountSPL" runat="server" text="00"></asp:label>
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
                                <h3 class="text-center">Leave Application</h3>
                            </div>
                        </div>                  
                        <div class="form-group form-primary">
                            <h4>Are you sure you want save this application?</h4>
                        </div>
                        <div class="row">
                            <div class="notifications" style="width:500px; display:flex;">
                                <div class="col-md-6">
                                    <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" UseSubmitBehavior="false" AutoPostBack="true" data-type="success" data-ptitle="Leave Application" data-msg="Successfully Save!" data-from="top" data-align="right" data-dismiss="modal" class="btn btn-success btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" OnClick="btnSave_Click"/>
                                    <%--<button type="button" class="btn btn-success btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" data-type="success" data-from="top" data-align="right" data-dismiss="modal" >Save</button>--%>
                                </div>
                                <div class="col-md-6">
                                     <asp:button ID="btnCancel" runat="server" ClientIDMode="Static" text="Cancel" data-type="danger" data-ptitle="Leave Application" data-msg="has been Cancelled!" data-from="top" data-align="right" data-dismiss="modal" class="btn btn-inverse btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" />
                                    <%--<button type="button" class="btn btn-inverse btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" data-dismiss="modal">Cancel</button>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="jsScript">
    <!-- jquery file upload js -->
    <script src="assets/pages/jquery.filer/js/jquery.filer.min.js"></script>
    <script src="assets/pages/filer/custom-filer.js" type="text/javascript"></script>
    <script src="assets/pages/filer/jquery.fileuploads.init.js" type="text/javascript"></script>
    <script type="text/javascript">
        <%--window.addEventListener('onbeforeunload', function (e) {
            var gridview = document.getElementById('<%= grdvFiles.ClientID %>');
            if (gridview && gridview.rows.length > 0) {
                deleteFolder();
                var confirmationMessage = 'Are you sure you want to leave this page? You have unsaved changes.';
                e.returnValue = confirmationMessage;  // Gecko, Trident, Chrome 34+
                return confirmationMessage;           // Gecko, WebKit, Chrome <34
            }
        });--%>
        window.onbeforeunload = function (e) {
            var gridview = document.getElementById('<%= grdvFiles.ClientID %>');
            if (!window.__doPostBack) {
                if (gridview && gridview.rows.length > 0) {
                    // Make an AJAX call to delete the folder
                    //deleteFolder();
                    return 'Are you sure you want to leave this page? You have unsaved changes.';
                }
            }
        };
        function deleteFolder() {
            // Make an AJAX call to the server-side code
            var xmlhttp = new XMLHttpRequest();
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    // Handle the response from the server, if needed
                    // ... (additional logic)
                }
            };
            xmlhttp.open("POST", "LeaveApp.aspx/DeleteFolder", true);
            xmlhttp.setRequestHeader("Content-Type", "application/json;charset=utf-8");
            xmlhttp.send();

        }
        function validateFormAndShowModal() {
            // Get references to the textboxes
            var checkboxes = document.querySelectorAll('input[type="checkbox"]');
            var txtDFrm = document.getElementById('<%= dateFrom.ClientID %>');
            var txtDTo = document.getElementById('<%= dateTo.ClientID %>');
            var txtTFrm = document.getElementById('<%= timeFrom.ClientID %>');
            var txtTTo = document.getElementById('<%= timeTo.ClientID %>');
            var isAnyChecked = false;

            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isAnyChecked = true;
                    break;
                }
            }
            if (!isAnyChecked || txtDFrm.value.trim() === '' || txtDTo.value.trim() === '') {
                alert('Please fill in all required fields.');

            } else {
                $('#saveApp').modal('show');
            }

        }
    </script>
</asp:Content>
