<%@ Page Title="Employee" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Emp.aspx.cs" Inherits="HRMIS.Emp" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <!-- Page-header start -->
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-8">
                    <div class="page-header-title">
                        <h5 class="m-b-10">Employee</h5>
                    </div>
                </div>
                <div class="col-md-4">
                    <ul class="breadcrumb-title">
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash1" runat="server" OnClick="lblbtnDash_Click"><i class="fa fa-home"></i></asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash" runat="server" OnClick="lblbtnDash1_Click">Employee</asp:LinkButton>
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
                    <!-- Config. table start -->
                    <div class="card">
                        <div class="card-header">
                            <h5>Employee List</h5>
                        </div>
                        <div class="card-block"  >
                            <div class="dt-responsive table-responsive">
                                <table id="res-config" class="table table-striped table-bordered break">
                                    <thead>
                                        <tr>
                                            <th>Employee ID</th>
                                            <th>Name</th>
                                            <th>Position</th>
                                            <th>Department</th>
                                            <th>Gender</th>
                                            <th>Date Hired</th>
                                            <th>E-Mail</th>
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
                    </div>
                    <!-- Config. table end -->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
