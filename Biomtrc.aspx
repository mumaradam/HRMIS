<%@ Page Title="Biometric" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"  CodeFile="Biomtrc.aspx.cs" Inherits="HRMIS.Biomtrc" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <!-- Page-header start -->
    <div class="page-header">
        <div class="page-block">
            <div class="row align-items-center">
                <div class="col-md-8">
                    <div class="page-header-title">
                        <h5 class="m-b-10">Biometics</h5>
                    </div>
                </div>
                <div class="col-md-4">
                    <ul class="breadcrumb-title">
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash1" runat="server" OnClick="lblbtnDash_Click"><i class="fa fa-home"></i></asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item">
                            <asp:LinkButton ID="lblbtnDash" runat="server" OnClick="lblbtnDash1_Click">Biometric</asp:LinkButton>
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
                    <div class="card">
                        <div class="card-header">
                            <h5>Your Logins/Logouts in SENPI</h5>
                        </div>
                        <div class="card-block">
                            <asp:Panel ID="Panel1" runat="server" ClientIDMode="Static">
                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h5>Your Logins/Logouts in SGA</h5>
                        </div>
                        <div class="card-block">
                            <asp:Panel ID="Panel2" runat="server" ClientIDMode="Static">
                                <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
