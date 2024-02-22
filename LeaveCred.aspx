<%@ Page Title="Leave Credits" Language="C#" EnableViewState="true"  MaintainScrollPositionOnPostBack="true" MasterPageFile="~/Site.master" AutoEventWireup="true"  CodeFile="LeaveCred.aspx.cs" Inherits="HRMIS.LeaveCred" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<!-- Page-header start -->
<div class="page-header">
    <div class="page-block">
        <div class="row align-items-center">
            <div class="col-md-8">
                <div class="page-header-title">
                    <h5 class="m-b-10">Leave Credits</h5>
                </div>
            </div>
            <div class="col-md-4">
                <ul class="breadcrumb-title">
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="lblbtnDash1" runat="server" OnClick="lblbtnDash_Click"><i class="fa fa-home"></i></asp:LinkButton>
                    </li>
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="lblbtnDash" runat="server">Leave Credits</asp:LinkButton>
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
                            <h5>Leave Credits</h5>
                            <p class="text-muted">
                                This is your remaining credits
                            </p>
                        </div>
                        <div class="card-block">
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
                    <div class="row">
                        <div id="emplist" runat="server" class="col-lg-6">
                             <div class="card">
                                <div class="card-header">
                                    <h5>Employee List</h5>
                                    <div id="buttonsAdmin" style="float: right;">
                                        <asp:LinkButton ID="btnReset" runat="server" ClientIDMode="Static" class="btn btn-inverse waves-effect waves-light"  data-toggle="modal" data-target="#credReset">Reset</asp:LinkButton>
                                    </div>
                                </div>
                                 <div class="card-block">
                                     <asp:UpdatePanel ID="updateAllEmpPanel" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel1" runat="server" ClientIDMode="Static" class="table-responsive">
                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                            </asp:Panel>
                                        </ContentTemplate>
                                     </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div id='emplogs' runat="server" class="col-lg-6">
                            <asp:UpdatePanel ID="updateLogsPanel" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
                                 <ContentTemplate>
                                     <div class="card">
                                        <div class="card-header">
                                            <h5>Leave Logs of <asp:Label ID="lbleName" runat="server" ClientIDMode="Static" Text="" style="font-size:16px; display:inherit; color: darkcyan;"></asp:Label></h5>
                                        </div>
                                        <div class="card-block">
                                            <asp:Panel ID="Panel2" runat="server" ClientIDMode="Static">
                                                <asp:Literal ID="Literal2" runat="server" ClientIDMode="Static"></asp:Literal>
                                            </asp:Panel>
                                        </div>
                                     </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnVLPlus" EventName="Click"/>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div id='empCred' runat="server" class="col-sm-12">
                            <asp:UpdatePanel ID="updateCredPanel" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
                                 <ContentTemplate>
                                     <div class="card">
                                        <div class="card-header">
                                            <h5>Leave Credits of <asp:Label ID="lblEmpCred" runat="server" ClientIDMode="Static" Text="" style="font-size:16px; display:inherit; color: darkcyan;"></asp:Label></h5>
                                            <asp:HiddenField ID="hidEmpNo" runat="server" />
                                        </div>
                                         <div class="card-block">
                                             <div class="row">
                                                <div class="col-xl-3">
                                                    <div class="card o-visible" data-toggle="tooltip" data-placement="top" title data-original-title="Vacation Leave">
                                                        <div class="card-header">
                                                            <h5>Vacation Leave</h5>
                                                        </div> 
                                                        <div class="card-block">
                                                            <p>have &nbsp  
                                                                <asp:label id="lblEVL" runat="server" text="00"></asp:label>
                                                                &nbsp remaining.
                                                            </p>
                                                            <asp:LinkButton ID="btnVLMinus" runat="server" class="btn waves-effect waves-dark btn-primary btn-outline-primary btn-icon" style="padding: 5px 5px 3px 10px;" OnClick="MinusCred"><i class="ti-minus"></i></asp:LinkButton>
                                                            <asp:TextBox ID="txtEVL" runat="server" ClientIDMode="Static" style="text-align: center; width: 110px;" disabled></asp:TextBox>
                                                            <asp:LinkButton ID="btnVLPlus" runat="server" class="btn waves-effect waves-dark btn-primary btn-outline-primary btn-icon" style="padding: 5px 5px 3px 10px;" OnClick="PlusCred"><i class="ti-plus"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xl-3">
                                                    <div class="card o-visible" data-toggle="tooltip" data-placement="top" title data-original-title="Sick Leave">
                                                        <div class="card-header">
                                                            <h5>Sick Leave</h5>
                                                        </div> 
                                                        <div class="card-block">
                                                            <p>have &nbsp  
                                                                <asp:label id="lblESL" runat="server" text="00"></asp:label>
                                                                &nbsp remaining.
                                                            </p>
                                                            <asp:LinkButton ID="btnSLMinus" runat="server" class="btn waves-effect waves-dark btn-primary btn-outline-primary btn-icon" style="padding: 5px 5px 3px 10px;" OnClick="MinusCred"><i class="ti-minus"></i></asp:LinkButton>
                                                            <asp:TextBox ID="txtESL" runat="server" ClientIDMode="Static" style="text-align: center; width: 110px;" disabled></asp:TextBox>
                                                            <asp:LinkButton ID="btnSLPlus" runat="server" class="btn waves-effect waves-dark btn-primary btn-outline-primary btn-icon" style="padding: 5px 5px 3px 10px;" OnClick="MinusCred"><i class="ti-plus"></i></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xl-3">
                                                    <div class="card o-visible" data-toggle="tooltip" data-placement="top" title data-original-title="Loyalty Leave">
                                                        <div class="card-header">
                                                            <h5>Loyalty Leave</h5>
                                                        </div> 
                                                        <div class="card-block">
                                                            <p>have &nbsp  
                                                                <asp:label id="lblELL" runat="server" text="00"></asp:label>
                                                                &nbsp remaining.
                                                            </p>
                                                            <button class="btn waves-effect waves-dark btn-primary btn-outline-primary btn-icon" style="padding: 5px 5px 3px 10px;" onclick="minusCred('txtELL')"><i class="ti-minus"></i></button>
                                                            <asp:TextBox ID="txtELL" runat="server" ClientIDMode="Static" style="text-align: center; width: 110px;" disabled></asp:TextBox>
                                                            <button class="btn waves-effect waves-dark btn-primary btn-outline-primary btn-icon" style="padding: 5px 5px 3px 10px;" onclick="plusCred('txtELL')"><i class="ti-plus"></i></button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xl-3">
                                                    <div class="card o-visible" data-toggle="tooltip" data-placement="top" title data-original-title="Special Leave">
                                                        <div class="card-header">
                                                            <h5>Special Leave</h5>
                                                        </div> 
                                                        <div class="card-block">
                                                            <p>have &nbsp
                                                                <asp:label id="lblESPL" runat="server" text="00"></asp:label>
                                                                &nbsp remaining.
                                                            </p>
                                                            <button class="btn waves-effect waves-dark btn-primary btn-outline-primary btn-icon" style="padding: 5px 5px 3px 10px;" onclick="minusCred('txtESPL')"><i class="ti-minus"></i></button>
                                                            <asp:TextBox ID="txtESPL" runat="server" ClientIDMode="Static" style="text-align: center; width: 110px;" disabled></asp:TextBox>
                                                            <button class="btn waves-effect waves-dark btn-primary btn-outline-primary btn-icon" style="padding: 5px 5px 3px 10px;" onclick="plusCred('txtESPL')"><i class="ti-plus"></i></button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                         </div>
                                     </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<!-----Modal----->
<div id="credReset" class="modal" role="dialog" style="padding-right: 17px;">
    <div class="modal-dialog">
        <div class="card">
            <div class="card-block">
                <div class="md-float-material form-material">
                    <div class="row m-b-20">
                        <div class="col-md-12">
                            <h3 class="text-center">Leave Credits</h3>
                        </div>
                    </div>                  
                    <div class="form-group form-primary">
                        <h4>Are you sure you want to Reset Leave Credits?</h4>
                    </div>
                    <div class="row">
                        <div class="notifications" style="width:500px; display:flex;">
                            <div class="col-md-6">
                                <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" UseSubmitBehavior="false" AutoPostBack="true" data-type="success" data-ptitle="Leave Credit" data-msg="Successfully Save!" data-from="top" data-align="right" data-dismiss="modal" class="btn btn-success btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" OnClick="btnReset_Click"/>
                                <%--<button type="button" class="btn btn-success btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" data-type="success" data-from="top" data-align="right" data-dismiss="modal" >Save</button>--%>
                            </div>
                            <div class="col-md-6">
                                    <asp:button ID="btnCancel" runat="server" ClientIDMode="Static" text="Cancel" data-type="danger" data-ptitle="Leave Credit" data-msg="has been Cancelled!" data-from="top" data-align="right" data-dismiss="modal" class="btn btn-inverse btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" />
                                <%--<button type="button" class="btn btn-inverse btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" data-dismiss="modal">Cancel</button>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<%--<script type="text/javascript">
    function handleButtonClick(empNo) {
        // Make an AJAX request
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                // Handle the response from the server if needed
                // You can access the response using xmlhttp.responseText
                // Get the ID of the target UpdatePanel
                var updatePanelID = document.getElementById("V" + empNo).getAttribute("data-updatepanel");
                // Trigger the update of the corresponding UpdatePanel
                __doPostBack(updatePanelID, "");
            }
        };

        xmlhttp.open("GET", "LeaveCred.aspx?empNo=" + empNo, true); // Replace "HandleButtonClick.aspx" with the actual URL of your server-side code
        xmlhttp.send();
    }
    function plusCred(txtbx) {
        var credVL = parseInt(document.getElementById(txtbx).value);
        var result = credVL + 1;
        document.getElementById(txtbx).value = result;
    }
    function minusCred(txtbx) {
        var credVL = parseInt(document.getElementById(txtbx).value);
        var result = credVL - 1;
        document.getElementById(txtbx).value = result;
    }
</script>--%>

</asp:Content>
<asp:Content runat="server" ID="jsScript" ContentPlaceHolderID="jsScript">

</asp:Content>