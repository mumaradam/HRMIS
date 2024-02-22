<%@ Page Title="Employee Registration" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EmpReg.aspx.cs" Inherits="HRMIS.EmpReg" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <!-- Page-header start -->
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-8">
                    <div class="page-header-title">
                        <h5 class="m-b-10">Employee Registration</h5>
                    </div>
                </div>
                <div class="col-md-4">
                    <ul class="breadcrumb-title">
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash1" runat="server" OnClick="lblbtnDash_Click"><i class="fa fa-home"></i></asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash" runat="server" OnClick="lblbtnDash1_Click">Employee Registration</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="pcoded-inner-content">
        <div class="main-body">
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="card">
                                <div class="card-header">
                                    <h5>Register</h5>
                                </div>
                                <div class="card-block">
                                    <div class="row" style="margin-top: 10px;">
                                        <div class="col-sm-4 form-group">
                                            <label>Employee No.</label>
                                            <asp:TextBox type="empno" class="form-control" ID="empno" runat="server" ClientIDMode="Static" placeholder="Enter ID" name="empno" required></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6 form-group">
                                            <label>Firstname</label>
                                            <asp:TextBox type="firstname" class="form-control" ID="firstname" runat="server" ClientIDMode="Static" placeholder="First Name" name="firstname" required></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 form-group">
                                            <label>Middle Initial</label>
                                            <asp:TextBox type="middle" class="form-control" ID="middle" runat="server" ClientIDMode="Static" placeholder="Middle Initial" name="middle"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-8 form-group">
                                            <label>Last Name</label>
                                            <asp:TextBox type="lastname" class="form-control" ID="lastname" runat="server" ClientIDMode="Static" placeholder="Last Name" name="lastname" required></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                       <%-- <div class="col-sm-12 form-group">
                                            <label>Address</label>
                                            <asp:TextBox type="address" class="form-control" ID="address" runat="server" placeholder="Address" name="adress" TextMode="MultiLine" height="156px"></asp:TextBox>
                                        </div>--%>
                                        <%--<div class="col-sm-6 form-group">
                                            <label>Email</label>
                                            <asp:TextBox type="email" class="form-control" ID="email" runat="server" placeholder="Email" name="email"></asp:TextBox>
                                        </div>--%>
                                        <div class="col-sm-6 form-group">
                                            <label>Date of Birth</label>
                                            <asp:TextBox type="date" ID="birthDate" runat="server" ClientIDMode="Static" class="form-control" required></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6 form-group">
                                            <label>Contact No.</label>
                                            <asp:TextBox type="tel" ID="contactNo" runat="server" ClientIDMode="Static" class="form-control" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-5 form-group" >
                                                <label>Gender</label>
                                                <asp:DropDownList name="gender" ID="gender" runat="server" ClientIDMode="Static" class="form-control" style="height: calc(2.25rem + 2px);">
                                                    <asp:ListItem Enabled="true" Text= "Male" Value= "0"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text= "Female" Value= "1"></asp:ListItem>
                                                </asp:DropDownList>
                                        </div>
                                    </div> <%--Address--%>
                                </div>
                                <div class="card-header">
                                    <h5>Employee Information</h5>
                                </div>
                                <div class="card-block">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-6 form-group">
                                                <label>Company Email</label>
                                                <asp:TextBox type="email" class="form-control" ID="comEmail" ClientIDMode="Static" runat="server" placeholder="Email" name="email"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6 form-group">
                                                <label>Department</label>
                                                <asp:DropDownList name="department" ID="department" runat="server" ClientIDMode="Static" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="departmentSelect_SelectedIndexChanged" style="height: calc(2.25rem + 2px);">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6 form-group">
                                                <label>Position</label>
                                                <asp:DropDownList name="position" ID="position" runat="server" ClientIDMode="Static" class="form-control" AppendDataBoundItems="False" AutoPostBack="true" OnSelectedIndexChanged="position_SelectedIndexChanged" style="height: calc(2.25rem + 2px);">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6 form-group">
                                                <label>Role</label>
                                                <asp:DropDownList name="role" ID="role" runat="server" ClientIDMode="Static" class="form-control" AppendDataBoundItems="False"  AutoPostBack="true" OnSelectedIndexChanged="role_SelectedIndexChanged" style="height: calc(2.25rem + 2px);">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6 form-group">
                                                <label>Status</label>
                                                <asp:DropDownList name="status" ID="empstat" runat="server" ClientIDMode="Static" class="form-control" style="height: calc(2.25rem + 2px);" required>
                                                    <asp:ListItem Enabled="true" Text= "Type of Employment" Value= "" Selected="True" hidden></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text= "Probationary" Value= "0"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text= "Contractual" Value= "1"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text= "Part-Time" Value= "2"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text= "Regular" Value= "3"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6 form-group">
                                                <label>Date Hired</label>
                                                <asp:TextBox type="date" ID="DOH" runat="server" ClientIDMode="Static" class="form-control" required></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 10px;">
                                            <div class="col-sm-6 form-group">
                                                <label>Reporting Head</label>
                                                <asp:DropDownList name="role" ID="reportHead" runat="server" ClientIDMode="Static" class="form-control" AppendDataBoundItems="False" style="height: calc(2.25rem + 2px);">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6 form-group">
                                                <asp:Button ID="Button1" runat="server" Text="Save" class="btn btn-primary waves-effect waves-light" OnClientClick="validateFormAndShowModal();" style="float:right; margin-top:20px;"/>
                                            </div>
                                        </div> <%--DropDownList--%>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="department" EventName="SelectedIndexChanged"/>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="card">
                                <div class="card-header">
                                    <h5>Employee List</h5>
                                </div>
                                <div class="card-block">
                                    <asp:Panel ID="Panel1" runat="server" ClientIDMode="Static">
                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    </asp:Panel>
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
                                <h3 class="text-center">Employee Registration</h3>
                            </div>
                        </div>                  
                        <div class="form-group form-primary">
                            <h4>Are you sure you want save this employee?</h4>
                        </div>
                        <div class="row">
                            <div class="notifications" style="width:500px; display:flex;">
                                <div class="col-md-6">
                                    <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" UseSubmitBehavior="false" AutoPostBack="true" data-type="success" data-ptitle="Employee Registration" data-msg="Successfully Save!" data-from="top" data-align="right" data-dismiss="modal" class="btn btn-success btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" OnClick="btnSave_Click"/>
                                    <%--<button type="button" class="btn btn-success btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" data-type="success" data-from="top" data-align="right" data-dismiss="modal" >Save</button>--%>
                                </div>
                                <div class="col-md-6">
                                     <asp:button ID="btnCancel" runat="server" ClientIDMode="Static" text="Cancel" data-type="danger" data-ptitle="Employee Registration" data-msg="has been Cancelled!" data-from="top" data-align="right" data-dismiss="modal" class="btn btn-inverse btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" />
                                    <%--<button type="button" class="btn btn-inverse btn-round btn-md btn-block waves-effect waves-light text-center m-b-5" data-dismiss="modal">Cancel</button>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function validateFormAndShowModal() {
            // Get references to the textboxes
            var textBox1 = document.getElementById('<%= empno.ClientID %>');
            var textBox2 = document.getElementById('<%= firstname.ClientID %>');
            var textBox3 = document.getElementById('<%= lastname.ClientID %>');
            var textBox4 = document.getElementById('<%= birthDate.ClientID %>');
            var textBox5 = document.getElementById('<%= DOH.ClientID %>');
            var textBox6 = document.getElementById('empstat');
            var selectedIndx = textBox6.value;
            // Check if the required textboxes are empty
            if (selectedIndx === '' || textBox1.value.trim() === '' || textBox2.value.trim() === '' || textBox3.value.trim() === '' || textBox4.value.trim() === '' || textBox5.value.trim() === '') {
                alert('Please fill in all required fields.');
            } else {
                // Code to show the modal here
                $('#saveApp').modal('show');
            }
        }
    </script>
</asp:Content>
