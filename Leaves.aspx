<%@ Page Title="Leaves" Language="C#" MasterPageFile="~/Site.master" MaintainScrollPositionOnPostBack="true" AutoEventWireup="true" CodeFile="Leaves.aspx.cs" Inherits="HRMIS.Leaves" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div id="loading-screen">
        <div class="loader"></div>
    </div>
    <!-- Page-header start -->
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-8">
                    <div class="page-header-title">
                        <h5 class="m-b-10">Leaves</h5>
                    </div>
                </div>
                <div class="col-md-4">
                    <ul class="breadcrumb-title">
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash" runat="server" OnClick="lblbtnDash_Click"><i class="fa fa-home"></i></asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash1" runat="server" OnClick="lblbtnDash1_Click">Leaves</asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="pcoded-inner-content">
        <div class="main-body">
            <div class="page-wrapper" style="padding: 1rem;">
                <div class="page-body">
                    <!-- Config. table start -->
                    <div class="card"><!--User Leaves Filed-->
                        <div class="card-header">
                            <h5>Leaves Filed</h5>
                        </div>
                        <div class="card-block"  >
                            <table id="new-cons" class="table table-striped table-bordered nowrap">
                                <thead>
                                    <tr>
                                        <th>Leave ID</th>
                                        <th>Name</th>
                                        <th>Type</th>
                                        <th>Date Filed</th>
                                        <th>Leave Date</th>
                                        <th>Total Days</th>
                                        <th>Status</th>
                                        <th>Reason</th>
                                        <th>Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Panel ID="Panel1" runat="server" ClientIDMode="Static">
                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    </asp:Panel>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div id="AllLeaves" runat="server" class="card">
                        <div class="card-header">
                            <h5>All Leaves Filed</h5>
                            <div class="row" style="float:right; margin-right:20px;">
                                <div class="col-sm-2">
                                    <label style="font-weight: 500; font-size: 14px; padding: 0.7rem 0.75rem; margin: 0px; text-align: right; color: #37474f;">View </label>
                                </div>
                                <div class="col-sm-5" style="width:260px;">
                                    <asp:DropDownList ID="ddlViewBy" runat="server" ClientIDMode="Static" AutoPostBack="True" AppendDataBoundItems="False" class="form-control input-sm" OnSelectedIndexChanged="ddlViewBy_SelectedIndexChanged" style="width:180px;">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Pending</asp:ListItem>
                                        <asp:ListItem>Approved</asp:ListItem>
                                        <asp:ListItem>Denied</asp:ListItem>
                                        <asp:ListItem>Month</asp:ListItem> 
                                        <asp:ListItem>Approved/Denied By</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div id="viewMonth" runat="server" class="row" visible="false">
                                    <asp:TextBox ID="dateFrom" runat="server" type="month" ClientIDMode="Static" class="form-control" style="width: 150px;"></asp:TextBox> 
                                    <asp:LinkButton ID="lnkbtnLookDate" runat="server" ClientIDMode="Static" class="btn waves-effect waves-light btn-inverse" style="margin-left: 10px;" OnClick="lnkbtnLookDate_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>
                                <asp:DropDownList name="managers" ID="managers" runat="server" ClientIDMode="Static" class="form-control" style="width:240px;" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="card-block" style="padding: 15px;">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" style="margin-top:10px;">
                                <ContentTemplate>
                                    <div class='dt-responsive table-responsive'>
                                        <asp:Panel ID="Panel5" runat="server" ClientIDMode="Static" Visible="true">
                                            <asp:Literal ID="Literal5" runat="server"></asp:Literal>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div> 
                </div>
            </div>
        </div>
    </div>
    <script>
        document.getElementById("myDropdown").addEventListener("change", function () {
            var selectedDivId = this.value;
            var selectedDiv = document.getElementById(selectedDivId);

            // Hide all divs
            var divs = document.querySelectorAll("div[id^='Div']");
            divs.forEach(function (div) {
                div.classList.add("hidden");
            });

            // Show the selected div
            selectedDiv.classList.remove("hidden");
        });
        window.addEventListener('load', function () {
            var loadingScreen = document.getElementById('loading-screen');
            var content = document.getElementById('content');

            loadingScreen.style.display = 'none';
            content.style.display = 'block';
        });
        function hideLoadingScreen() {
            var loadingScreen = document.getElementById('loading-screen');
            loadingScreen.style.display = 'none';
        }
        function openNewTab(param) {
            var url = '<%= ResolveUrl("~/LeaveDet") %>' + '?param=' + param;
            window.open(url, '_blank');
        }
    </script>
</asp:Content>
