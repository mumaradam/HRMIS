<%@ Page Title="Biometric" Language="C#" MasterPageFile="~/Site.master" MaintainScrollPositionOnPostBack="true" AutoEventWireup="true" CodeFile="EmployeeBiomtrc.aspx.cs" Inherits="HRMIS.EmployeeBiomtrc" %>

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
                            <h5>Logins/Logouts in SENPI</h5>
                        </div>
                        <div class="card-block">
                            <div class="dt-responsive table-responsive">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>Time Range:</td>
                                        </tr>
                                        <tr>
                                            <td style="float:right; margin-top: 8px; margin-right: 10px;">From :</td>
                                            <td>
                                                <asp:TextBox ID="dateFrom" runat="server" type="date" ClientIDMode="Static" class="form-control" EnableViewState="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="float:right; margin-top: 8px; margin-right: 10px;">To :</td>
                                            <td>
                                                <asp:TextBox ID="dateTo" runat="server" type="date" ClientIDMode="Static" class="form-control" EnableViewState="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCalculate" runat="server" Text="Calculate" AutoPostBack="true" class="btn waves-effect waves-light hor-grd btn-grd-inverse" style="margin-left:10px;" OnClick="btnCalculate_Click"/>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Label ID="lblError" runat="server" ClientIDMode="Static" Text="Invalid date range!" ForeColor="Red" style="margin-left: 90px;" Visible="false"></asp:Label>
                            </div>
                             
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class='dt-responsive table-responsive'>
                                        <asp:Panel ID="Panel1" runat="server" ClientIDMode="Static">
                                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                        </asp:Panel>
                                     </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <h5>Logins/Logouts in SGA</h5>
                        </div>
                        <div class="card-block">
                            <div class="dt-responsive table-responsive">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>Time Range:</td>
                                        </tr>
                                        <tr>
                                            <td style="float:right; margin-top: 8px; margin-right: 10px;">From :</td>
                                            <td>
                                                <asp:TextBox ID="dateFromSGA" runat="server" type="date" ClientIDMode="Static" class="form-control" EnableViewState="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="float:right; margin-top: 8px; margin-right: 10px;">To :</td>
                                            <td>
                                                <asp:TextBox ID="dateToSGA" runat="server" type="date" ClientIDMode="Static" class="form-control" EnableViewState="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCalculateSGA" runat="server" Text="Calculate" AutoPostBack="true" class="btn waves-effect waves-light hor-grd btn-grd-inverse" style="margin-left:10px;" OnClick="btnCalculateSGA_Click"/>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <asp:Label ID="lblErrorSGA" runat="server" ClientIDMode="Static" Text="Invalid date range!" ForeColor="Red" style="margin-left: 90px;" Visible="false"></asp:Label>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="dt-responsive table-responsive">
                                        <asp:Panel ID="Panel2" runat="server" ClientIDMode="Static">
                                            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
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
</asp:Content>
