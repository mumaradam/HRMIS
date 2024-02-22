using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Web.UI.HtmlControls;
namespace HRMIS
{
    public partial class Leaves : System.Web.UI.Page
    {
        private static DataTable dtQuery;
        private static string getEmpUser = "";
        private static string getUserAdmin = "";
        private static string getUserDept = "";
        private static string getUserPos = "";
        private static string getUserRole = "";
        private static bool fselect = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            getEmpUser = Session["Uname"] as string;
            if (string.IsNullOrEmpty(getEmpUser))
            {
                Session.Abandon();
                Response.Redirect("~/Login");
            }
            else
            {
                getUserInfo();
                getUserLeaves();
                ScriptManager.RegisterStartupScript(this, GetType(), "HideLoadingScreen", "hideLoadingScreen();", true);
                if(!IsPostBack)
                {
                    
                    ddlViewBy.SelectedIndex = 0;
                    getLeaveBy(ddlViewBy.SelectedIndex.ToString());
                }
                else
                {
                    if(fselect == true)
                    {
                        int hldIndex = ddlViewBy.SelectedIndex;
                        if (hldIndex != 3)
                        {
                            getLeaveBy(hldIndex.ToString());
                        }
                        else
                        {
                            viewMonthSearch(DateTime.Parse(dateFrom.Text));
                        }
                        fselect = false;
                    }
                }
            }
            
        }
        private void getUserInfo()
        {
            dtQuery = null;
            string sQuery = "Select * from seihaHRMIS.dbo.HREmpInfo where empno = '" + getEmpUser + "'";
            dtQuery = HRMIS.Module.GetData(sQuery);
            if (dtQuery.Rows.Count > 0)
            {
                getUserDept = dtQuery.Rows[0]["empDept"].ToString();
                getUserPos = dtQuery.Rows[0]["empPos"].ToString();
                getUserAdmin = dtQuery.Rows[0]["empadmin"].ToString();
                getUserRole = dtQuery.Rows[0]["emprole"].ToString();
                if (getUserAdmin == "1")
                {
                    AllLeaves.Visible = true;
                    //getAllLeaves();
                    //getAllApp();
                    //getAllDenied();
                    //ddlViewBy.SelectedIndex = 0;
                }
                else
                {
                    if (getUserRole == "RLD" || getUserRole == "RSL")
                    {
                        AllLeaves.Visible = true;
                        //getAllLeaves();
                        //getAllApp();
                        //getAllDenied();
                        //ddlViewBy.SelectedIndex = 0;
                        
                    }
                    else if (getUserPos == "ITM" || getUserPos == "TMR" || getUserPos == "SMR" || getUserPos == "TOM" || getUserDept == "MGR")
                    {
                        AllLeaves.Visible = true;
                        //getAllLeaves();
                        //getAllApp();
                        //getAllDenied();
                        //ddlViewBy.SelectedIndex = 0;
                        
                    }
                    else
                    {
                        AllLeaves.Visible = false;
                    }
                    
                }

            }
        }
        protected void lblbtnDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dash");
        }
        protected void lblbtnDash1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Leaves");
        }
        protected void ddlViewBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            
            if(ddlViewBy.SelectedIndex == 0)
            {
                getLeaveBy("0");
                viewMonth.Visible = false;
                managers.Visible = false;
            }
            else if (ddlViewBy.SelectedIndex == 1)
            {
                getLeaveBy("1");
                viewMonth.Visible = false;
                managers.Visible = false;
            }
            else if(ddlViewBy.SelectedIndex == 2)
            {
                getLeaveBy("2");
                viewMonth.Visible = false;
                managers.Visible = false;
            }
            else if(ddlViewBy.SelectedIndex == 3)
            {
                getLeaveBy("3");
                viewMonth.Visible = false;
                managers.Visible = false;
            }
            else if (ddlViewBy.SelectedIndex == 4)
            {
                string dNow = DateTime.Now.ToString("yyyy-MM");
                dateFrom.Text = dNow;
                viewMonth.Visible = true;
                managers.Visible = false;
                viewMonthSearch(DateTime.Parse(dateFrom.Text));
            }
            else if (ddlViewBy.SelectedIndex == 5)
            {
                viewMonth.Visible = false;
                managers.Visible = true;
                getManagers();
            }
        }
        protected void getLeaveBy(string strViewBy)
        {
            string dtLeaveList = "";
            string strEmpStat = "";
            dtQuery = null;
            if (strViewBy == "0") { strEmpStat = ""; } else if (strViewBy == "1") { strEmpStat = " and a.empStatus = 0 "; } else if (strViewBy == "2") { strEmpStat = " and a.empStatus = 1 "; } else if (strViewBy == "3") { strEmpStat = " and a.empStatus = 2 "; }
            if (getUserAdmin == "1")
            {
                dtLeaveList = "Select a.identity_column, a.empNo, convert(varchar, a.empDate, 101) as Dfiled, convert(varchar, a.empDate, 108) as Tfiled, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
                          "(empDept) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
                          "where a.empNo <> '" + getEmpUser + "'" + strEmpStat + " order by a.identity_column desc";
            }
            else
            {
                if (getUserRole == "RLD" || getUserRole == "RSL")
                {
                    dtLeaveList = "Select a.identity_column, a.empNo, convert(varchar, a.empDate, 101) as Dfiled, convert(varchar, a.empDate, 108) as Tfiled, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
                          "(empDept) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
                          "where a.empNo <> '" + getEmpUser + "' and b.empReportHead = '" + getEmpUser + "'" + strEmpStat + " order by a.identity_column desc";
                }
                else if (getUserPos == "ITM" || getUserPos == "TMR" || getUserPos == "SMR" || getUserPos == "TOM")
                {
                    dtLeaveList = "Select a.identity_column, a.empNo, convert(varchar, a.empDate, 101) as Dfiled, convert(varchar, a.empDate, 108) as Tfiled, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
                          "(empDept) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
                          "where a.empNo <> '" + getEmpUser + "' and b.empDept = '" + getUserDept + "'" + strEmpStat + " order by a.identity_column desc";
                }
                else if (getUserDept == "MGR")
                {
                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Name, convert(varchar, empDate, 101) as Dfiled, convert(varchar, empDate, 108) as Tfiled, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                          " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=empTOL) as Type, (select empPos from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Pos, " +
                          "(select empDept from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=empStatus) as Stat, " +
                          "*  from seihaHRMIS.dbo.HRLeaveInfo where empNo <> '" + getEmpUser + "'" + strEmpStat + " order by identity_column desc";
                }

            }

            dtQuery = HRMIS.Module.GetData(dtLeaveList);
            if (dtQuery.Rows.Count > 0)
            {
                int count = checked(dtQuery.Rows.Count - 1);
                string sStatementFirst = "<table id='res-config' class='table table-striped table-bordered break'><thead>" +
                                         "<tr><th>Leave ID</th><th>Name</th><th>Reason</th><th>Type</th><th>Leave Date</th><th>Status</th>" +
                                         "<th>Position</th><th>Department</th><th>Action</th><th>Date/Time Filed:</th></tr></thead><tbody>";
                Panel5.Controls.Add(new LiteralControl(sStatementFirst));
                for (int x = 0; x <= count; x = checked(x + 1))
                {
                    Button button = new Button();
                    string Lstat = "";
                    if (dtQuery.Rows[x]["Stat"].ToString() == "Pending") { Lstat = "lightgray"; }
                    else if (dtQuery.Rows[x]["Stat"].ToString() == "Approved") { Lstat = "palegreen"; }
                    else if (dtQuery.Rows[x]["Stat"].ToString() == "Denied") { Lstat = "lightpink"; }
                    else if (dtQuery.Rows[x]["Stat"].ToString() == "Cancelled") { Lstat = "indianred"; }
                    string sStatement1 = "<tr><td>" + dtQuery.Rows[x]["identity_column"].ToString() + "</td>" +
                                         "<td style='white-space: nowrap;'>" + dtQuery.Rows[x]["Name"].ToString() + "</td>" +
                                         "<td style='white-space: pre-wrap;'>" + dtQuery.Rows[x]["empReason"].ToString() + "</td>" +
                                         "<td>" + dtQuery.Rows[x]["Type"].ToString() + "</td>" +
                                         "<td style='white-space: nowrap;'>" + dtQuery.Rows[x]["FDate"].ToString() + " - " + dtQuery.Rows[x]["TDate"].ToString() + "</td>" +
                                         "<td style='background-color:" + Lstat + ";'>" + dtQuery.Rows[x]["Stat"].ToString() + "</td>" +
                                         "<td>" + getPosition(dtQuery.Rows[x]["Pos"].ToString()) + "</td>" +
                                         "<td>" + getDpart(dtQuery.Rows[x]["Dep"].ToString()) + "</td>" +
                                         "<td>";
                    Panel5.Controls.Add(new LiteralControl(sStatement1));
                    button.ID = dtQuery.Rows[x]["identity_column"].ToString();
                    button.Text = "View";
                    button.CssClass = "btn btn-out-dashed waves-effect waves-light btn-primary btn-square";
                    button.OnClientClick = "openNewTab('" + dtQuery.Rows[x]["identity_column"].ToString() + "'); return false;";
                    //button.PostBackUrl = "~/LeaveDet?param=" + dtQuery.Rows[x]["identity_column"].ToString() + "";
                    Panel5.Controls.Add(button);
                    Panel5.Controls.Add(new LiteralControl("</td><td>" + dtQuery.Rows[x]["Dfiled"].ToString() + " - " + dtQuery.Rows[x]["Tfiled"].ToString() + "</td></tr>"));
                }
                string sStatementLast = "</tbody></table>";
                Panel5.Controls.Add(new LiteralControl(sStatementLast));

            }
        }
        private string getManagers()
        {
            string pos = "";
            string dtQry = "";
            dtQry = "Select EmpFName, EmpNo from seihaHRMIS.dbo.HREmpInfo where EmpPos in ('TMR', 'SMR')";
            DataTable dt = HRMIS.Module.GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                managers.DataSource = dt;
                managers.DataTextField = "EmpFName";
                managers.DataValueField = "EmpNo";
                managers.DataBind();
            }
            else
            {
                managers.DataSource = null;
                managers.Items.Clear();
                managers.ClearSelection();
            }

            return pos;
        }
        protected void lnkbtnLookDate_Click(object sender, EventArgs e)
        {
            if(dateFrom.Text != "")
            {
                viewMonthSearch(DateTime.Parse(dateFrom.Text));
            }
        }
        protected void viewMonthSearch(DateTime dtTime)
        {
            string dtLeaveList = "";
            if (getUserAdmin == "1")
            {
                dtLeaveList = "Select a.identity_column, a.empNo, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
                          "(empDept) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
                          "where a.empNo <> '" + getEmpUser + "' and month(empDateFrom) = " + dtTime.Month + " and year(empDateFrom) = " + dtTime.Year + " order by a.identity_column desc";
            }
            else
            {
                if (getUserRole == "RLD" || getUserRole == "RSL")
                {
                    dtLeaveList = "Select a.identity_column, a.empNo, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
                          "(empDept) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
                          "where a.empNo <> '" + getEmpUser + "' and b.empReportHead = '" + getEmpUser + "' and month(empDateFrom) = " + dtTime.Month + " and year(empDateFrom) = " + dtTime.Year + " order by a.identity_column desc";
                }
                else if (getUserPos == "ITM" || getUserPos == "TMR" || getUserPos == "SMR" || getUserPos == "TOM")
                {
                    dtLeaveList = "Select a.identity_column, a.empNo, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
                          "(empDept) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
                          "where a.empNo <> '" + getEmpUser + "' and b.empDept = '" + getUserDept + "' and month(empDateFrom) = " + dtTime.Month + " and year(empDateFrom) = " + dtTime.Year + " order by a.identity_column desc";
                }
                else if (getUserDept == "MGR")
                {
                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                          " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=empTOL) as Type, (select empPos from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Pos, " +
                          "(select empDept from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=empStatus) as Stat, " +
                          "*  from seihaHRMIS.dbo.HRLeaveInfo where empNo <> '" + getEmpUser + "' and month(empDateFrom) = " + dtTime.Month + " and year(empDateFrom) = " + dtTime.Year + " order by identity_column desc";
                }

            }

            dtQuery = HRMIS.Module.GetData(dtLeaveList);
            if (dtQuery.Rows.Count > 0)
            {
                int count = checked(dtQuery.Rows.Count - 1);
                string sStatementFirst = "<table id='res-config' class='table table-striped table-bordered break'><thead>" +
                                         "<tr><th>Leave ID</th><th>Name</th><th>Reason</th><th>Type</th><th>Leave Date</th><th>Status</th>" +
                                         "<th>Position</th><th>Department</th><th>Action</th></tr></thead><tbody>";
                Panel5.Controls.Add(new LiteralControl(sStatementFirst));
                for (int x = 0; x <= count; x = checked(x + 1))
                {
                    Button button = new Button();
                    string Lstat = "";
                    if (dtQuery.Rows[x]["Stat"].ToString() == "Pending") { Lstat = "lightgray"; }
                    else if (dtQuery.Rows[x]["Stat"].ToString() == "Approved") { Lstat = "palegreen"; }
                    else if (dtQuery.Rows[x]["Stat"].ToString() == "Denied") { Lstat = "lightpink"; }
                    else if (dtQuery.Rows[x]["Stat"].ToString() == "Cancelled") { Lstat = "indianred"; }
                    string sStatement1 = "<tr><td>" + dtQuery.Rows[x]["identity_column"].ToString() + "</td>" +
                                         "<td style='white-space: nowrap;'>" + dtQuery.Rows[x]["Name"].ToString() + "</td>" +
                                         "<td style='white-space: pre-wrap;'>" + dtQuery.Rows[x]["empReason"].ToString() + "</td>" +
                                         "<td>" + dtQuery.Rows[x]["Type"].ToString() + "</td>" +
                                         "<td style='white-space: nowrap;'>" + dtQuery.Rows[x]["FDate"].ToString() + " - " + dtQuery.Rows[x]["TDate"].ToString() + "</td>" +
                                         "<td style='background-color:" + Lstat + ";'>" + dtQuery.Rows[x]["Stat"].ToString() + "</td>" +
                                         "<td>" + getPosition(dtQuery.Rows[x]["Pos"].ToString()) + "</td>" +
                                         "<td>" + getDpart(dtQuery.Rows[x]["Dep"].ToString()) + "</td>" +
                                         "<td>";
                    Panel5.Controls.Add(new LiteralControl(sStatement1));
                    button.ID = dtQuery.Rows[x]["identity_column"].ToString();
                    button.Text = "View";
                    button.CssClass = "btn btn-out-dashed waves-effect waves-light btn-primary btn-square";
                    button.OnClientClick = "openNewTab('" + dtQuery.Rows[x]["identity_column"].ToString() + "'); return false;";
                    //button.PostBackUrl = "~/LeaveDet?param=" + dtQuery.Rows[x]["identity_column"].ToString() + "";
                    Panel5.Controls.Add(button);
                    Panel5.Controls.Add(new LiteralControl("</td></tr>"));
                }
                string sStatementLast = "</tbody></table>";
                Panel5.Controls.Add(new LiteralControl(sStatementLast));

            }

        }
        //protected void getAllLeaves()
        //{
        //    try
        //    {
        //        if (AllLeaves.Visible == true)
        //        {
        //            string dtLeaveList = "";
        //            if (getUserAdmin=="1")
        //            {
        //                dtLeaveList = "Select a.identity_column, a.empNo, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
        //                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
        //                          "(empDept) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
        //                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
        //                          "where a.empNo <> '" + getEmpUser + "' order by a.identity_column desc";
        //            }
        //            else
        //            {
        //                if (getUserRole == "RLD" || getUserRole == "RSL")
        //                {
        //                    dtLeaveList = "Select a.identity_column, a.empNo, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
        //                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
        //                          "(empDept) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
        //                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
        //                          "where a.empNo <> '" + getEmpUser + "' and b.empReportHead = '" + getEmpUser + "' order by a.identity_column desc";
        //                }
        //                else if (getUserPos == "ITM" || getUserPos == "TMR" || getUserPos == "SMR" || getUserPos == "TOM")
        //                {
        //                    dtLeaveList = "Select a.identity_column, a.empNo, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
        //                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
        //                          "(empDept) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
        //                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
        //                          "where a.empNo <> '" + getEmpUser + "' and b.empDept = '" + getUserDept + "' order by a.identity_column desc";
        //                }
        //                else if(getUserDept=="MGR")
        //                {
        //                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
        //                          " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=empTOL) as Type, (select empPos from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Pos, " +
        //                          "(select empDept from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=empStatus) as Stat, " +
        //                          "*  from seihaHRMIS.dbo.HRLeaveInfo where empNo <> '" + getEmpUser + "' order by identity_column desc";
        //                }
                    
        //            }
                
        //            dtQuery = HRMIS.Module.GetData(dtLeaveList);
        //            if (dtQuery.Rows.Count > 0)
        //            {
        //                int count = checked(dtQuery.Rows.Count - 1);
        //                string sStatementFirst = "<table id='res-config' class='table table-striped table-bordered break'><thead>" +
        //                                         "<tr><th>Leave ID</th><th>Name</th><th>Reason</th><th>Type</th><th>Leave Date</th><th>Status</th>" +
        //                                         "<th>Position</th><th>Department</th><th>Action</th></tr></thead><tbody>";
        //                Panel2.Controls.Add(new LiteralControl(sStatementFirst));
        //                for (int x = 0; x <= count; x = checked(x + 1))
        //                {
        //                    Button button = new Button();
        //                    string Lstat = "";
        //                    if (dtQuery.Rows[x]["Stat"].ToString() == "Pending") { Lstat = "lightgray"; }
        //                    else if (dtQuery.Rows[x]["Stat"].ToString() == "Approved") { Lstat = "palegreen"; }
        //                    else if (dtQuery.Rows[x]["Stat"].ToString() == "Denied") { Lstat = "lightpink"; }
        //                    else if (dtQuery.Rows[x]["Stat"].ToString() == "Cancelled") { Lstat = "indianred"; }
        //                    string sStatement1 = "<tr><td>" + dtQuery.Rows[x]["identity_column"].ToString() + "</td>" +
        //                                         "<td style='white-space: nowrap;'>" + dtQuery.Rows[x]["Name"].ToString() + "</td>" +
        //                                         "<td style='white-space: pre-wrap;'>" + dtQuery.Rows[x]["empReason"].ToString() + "</td>" +
        //                                         "<td>" + dtQuery.Rows[x]["Type"].ToString() + "</td>" +
        //                                         "<td style='white-space: nowrap;'>" + dtQuery.Rows[x]["FDate"].ToString() + " - " + dtQuery.Rows[x]["TDate"].ToString() + "</td>" +
        //                                         "<td style='background-color:" + Lstat + ";'>" + dtQuery.Rows[x]["Stat"].ToString() + "</td>" +
        //                                         "<td>" + getPosition(dtQuery.Rows[x]["Pos"].ToString()) + "</td>" +
        //                                         "<td>" + getDpart(dtQuery.Rows[x]["Dep"].ToString()) + "</td>" +
        //                                         "<td>";
        //                    Panel2.Controls.Add(new LiteralControl(sStatement1));
        //                    button.ID = dtQuery.Rows[x]["identity_column"].ToString();
        //                    button.Text = "View";
        //                    button.CssClass = "btn btn-out-dashed waves-effect waves-light btn-primary btn-square";
        //                    button.PostBackUrl = "~/LeaveDet?param=" + dtQuery.Rows[x]["identity_column"].ToString() + "";
        //                    Panel2.Controls.Add(button);
        //                    Panel2.Controls.Add(new LiteralControl("</td></tr>"));
        //                }
        //                string sStatementLast = "</tbody></table>";
        //                Panel2.Controls.Add(new LiteralControl(sStatementLast));

        //            }
        //        }
                

        //    }
        //    catch (System.Net.WebException ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //}
        //protected void getAllApp()
        //{
        //    try
        //    {
        //        if (AllLeaves.Visible == true)
        //        {
        //            string dtLeaveList = "";
        //            if (getUserAdmin == "1")
        //            {
        //                dtLeaveList = "Select a.identity_column, a.empNo, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
        //                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
        //                          "(empDept) as Dep, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = a.empManaEmpNo) as approveBy, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
        //                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
        //                          "where a.empNo <> '" + getEmpUser + "' and a.empStatus = 1 order by a.identity_column desc";
        //            }
        //            else
        //            {
        //                if (getUserRole == "RLD" || getUserRole == "RSL")
        //                {
        //                    dtLeaveList = "Select a.identity_column, a.empNo, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
        //                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
        //                          "(empDept) as Dep, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = a.empManaEmpNo) as approveBy, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
        //                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
        //                          "where a.empNo <> '" + getEmpUser + "' and b.empReportHead = '" + getEmpUser + "' and a.empStatus = 1 order by a.identity_column desc";
        //                }
        //                else if (getUserPos == "ITM" || getUserPos == "TMR" || getUserPos == "SMR" || getUserPos == "TOM")
        //                {
        //                    dtLeaveList = "Select a.identity_column, a.empNo, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
        //                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
        //                          "(empDept) as Dep, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = a.empManaEmpNo) as approveBy, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
        //                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
        //                          "where a.empNo <> '" + getEmpUser + "' and b.empDept = '" + getUserDept + "' and a.empStatus = 1 order by a.identity_column desc";
        //                }
        //                else if (getUserDept == "MGR")
        //                {
        //                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
        //                          " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=empTOL) as Type, (select empPos from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Pos, " +
        //                          "(select empDept from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Dep, " +
        //                          "(Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = empManaEmpNo) as approveBy, " + 
        //                          "(Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=empStatus) as Stat " +
        //                          "from seihaHRMIS.dbo.HRLeaveInfo where empNo <> '" + getEmpUser + "' and a.empStatus = 1 order by identity_column desc";
        //                }

        //            }

        //            dtQuery = HRMIS.Module.GetData(dtLeaveList);
        //            if (dtQuery.Rows.Count > 0)
        //            {
        //                int count = checked(dtQuery.Rows.Count - 1);
        //                string sStatementFirst = "<table id='single-select' class='table table-striped table-bordered break'><thead>" +
        //                                         "<tr><th>Name</th><th>Reason</th><th>Type</th><th>Leave Date</th><th>Status</th>" +
        //                                         "<th>Approved By</th><th>Department</th><th>Action</th></tr></thead><tbody>";
        //                Panel3.Controls.Add(new LiteralControl(sStatementFirst));
        //                for (int x = 0; x <= count; x = checked(x + 1))
        //                {
        //                    Button button = new Button();
        //                    string sStatement1 = "<tr><td style='white-space: nowrap;'>" + dtQuery.Rows[x]["Name"].ToString() + "</td>" +
        //                                         "<td style='white-space: pre-wrap;'>" + dtQuery.Rows[x]["empReason"].ToString() + "</td>" +
        //                                         "<td>" + dtQuery.Rows[x]["Type"].ToString() + "</td>" +
        //                                         "<td style='white-space: nowrap;'>" + dtQuery.Rows[x]["FDate"].ToString() + " - " + dtQuery.Rows[x]["TDate"].ToString() + "</td>" +
        //                                         "<td>" + dtQuery.Rows[x]["Stat"].ToString() + "</td>" +
        //                                         "<td>" + dtQuery.Rows[x]["approveBy"].ToString() + "</td>" +
        //                                         "<td>" + getDpart(dtQuery.Rows[x]["Dep"].ToString()) + "</td>" +
        //                                         "<td>";
        //                    Panel3.Controls.Add(new LiteralControl(sStatement1));
        //                    button.ID = dtQuery.Rows[x]["identity_column"].ToString();
        //                    button.Text = "View";
        //                    button.CssClass = "btn btn-out-dashed waves-effect waves-light btn-primary btn-square";
        //                    button.PostBackUrl = "~/LeaveDet?param=" + dtQuery.Rows[x]["identity_column"].ToString() + "";
        //                    Panel3.Controls.Add(button);
        //                    Panel3.Controls.Add(new LiteralControl("</td></tr>"));
        //                }
        //                string sStatementLast = "</tbody></table>";
        //                Panel3.Controls.Add(new LiteralControl(sStatementLast));

        //            }
        //        }


        //    }
        //    catch (System.Net.WebException ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //}
        //protected void getAllDenied()
        //{
        //    try
        //    {
        //        if (AllLeaves.Visible == true)
        //        {
        //            string dtLeaveList = "";
        //            if (getUserAdmin == "1")
        //            {
        //                dtLeaveList = "Select a.identity_column, a.empNo, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
        //                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
        //                          "(empDept) as Dep, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = a.empManaEmpNo) as approveBy, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
        //                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
        //                          "where a.empNo <> '" + getEmpUser + "' and a.empStatus = 2 order by a.identity_column desc";
        //            }
        //            else
        //            {
        //                if (getUserRole == "RLD" || getUserRole == "RSL")
        //                {
        //                    dtLeaveList = "Select a.identity_column, a.empNo, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
        //                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
        //                          "(empDept) as Dep, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = a.empManaEmpNo) as approveBy, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
        //                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
        //                          "where a.empNo <> '" + getEmpUser + "' and b.empReportHead = '" + getEmpUser + "' and a.empStatus = 2 order by a.identity_column desc";
        //                }
        //                else if (getUserPos == "ITM" || getUserPos == "TMR" || getUserPos == "SMR" || getUserPos == "TOM")
        //                {
        //                    dtLeaveList = "Select a.identity_column, a.empNo, (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
        //                          "DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=a.empTOL) as Type, (empPos) as Pos, " +
        //                          "(empDept) as Dep, (Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = a.empManaEmpNo) as approveBy, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=a.empStatus) as Stat, a.empReason " +
        //                          "from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
        //                          "where a.empNo <> '" + getEmpUser + "' and b.empDept = '" + getUserDept + "' and a.empStatus = 2 order by a.identity_column desc";
        //                }
        //                else if (getUserDept == "MGR")
        //                {
        //                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
        //                          " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=empTOL) as Type, (select empPos from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Pos, " +
        //                          "(select empDept from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Dep, " +
        //                          "(Select EmpFName from seihaHRMIS.dbo.HREmpInfo where empno = empManaEmpNo) as approveBy, " +
        //                          "(Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=empStatus) as Stat " +
        //                          "from seihaHRMIS.dbo.HRLeaveInfo where empNo <> '" + getEmpUser + "' and a.empStatus = 2 order by identity_column desc";
        //                }

        //            }

        //            dtQuery = HRMIS.Module.GetData(dtLeaveList);
        //            if (dtQuery.Rows.Count > 0)
        //            {
        //                int count = checked(dtQuery.Rows.Count - 1);
        //                string sStatementFirst = "<table id='all-app' class='table table-striped table-bordered break'><thead>" +
        //                                         "<tr><th>Name</th><th style='white-space: pre-wrap;'>Reason</th><th>Type</th><th aria-sort='descending'>Leave Date</th><th>Status</th>" +
        //                                         "<th>Denied By</th><th>Department</th><th>Action</th></tr></thead><tbody>";
        //                Panel4.Controls.Add(new LiteralControl(sStatementFirst));
        //                for (int x = 0; x <= count; x = checked(x + 1))
        //                {
        //                     Button button = new Button();
        //                    string sStatement1 = "<tr><td style='white-space: nowrap;'>" + dtQuery.Rows[x]["Name"].ToString() + "</td>" +
        //                                         "<td style='white-space: pre-wrap;'>" + dtQuery.Rows[x]["empReason"].ToString() + "</td>" +
        //                                         "<td>" + dtQuery.Rows[x]["Type"].ToString() + "</td>" +
        //                                         "<td style='white-space: nowrap;'>" + dtQuery.Rows[x]["FDate"].ToString() + " - " + dtQuery.Rows[x]["TDate"].ToString() + "</td>" +
        //                                         "<td>" + dtQuery.Rows[x]["Stat"].ToString() + "</td>" +
        //                                         "<td style='white-space: nowrap;'>" + dtQuery.Rows[x]["approveBy"].ToString() + "</td>" +
        //                                         "<td>" + getDpart(dtQuery.Rows[x]["Dep"].ToString()) + "</td>" +
        //                                         "<td>";
        //                    Panel4.Controls.Add(new LiteralControl(sStatement1));
        //                    button.ID = dtQuery.Rows[x]["identity_column"].ToString();
        //                    button.Text = "View";
        //                    button.CssClass = "btn btn-out-dashed waves-effect waves-light btn-primary btn-square";
        //                    button.PostBackUrl = "~/LeaveDet?param=" + dtQuery.Rows[x]["identity_column"].ToString() + "";
        //                    Panel4.Controls.Add(button);
        //                    Panel4.Controls.Add(new LiteralControl("</td></tr>"));
        //                }
        //                string sStatementLast = "</tbody></table>";
        //                Panel4.Controls.Add(new LiteralControl(sStatementLast));

        //            }
        //        }


        //    }
        //    catch (System.Net.WebException ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //}
        protected void getUserLeaves()
        {
            try
            {
                string dtLeaveList = "";
                dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                              " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='TOL' and cspopupval=empTOL) as Type, (select empPos from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Pos, " +
                              "(select empDept from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Dep, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupfor='LVSTAT' and cspopupval=empStatus) as Stat, " +
                              "convert(varchar, empdate, 107) as DateFiled, *  from seihaHRMIS.dbo.HRLeaveInfo where empNo = '" + getEmpUser + "' order by identity_column desc";
                dtQuery = HRMIS.Module.GetData(dtLeaveList);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        Button button = new Button();
                        string Lstat = "";
                        if (dtQuery.Rows[x]["Stat"].ToString() == "Pending") { Lstat = "lightgray"; }
                        else if (dtQuery.Rows[x]["Stat"].ToString() == "Approved") { Lstat = "palegreen"; }
                        else if (dtQuery.Rows[x]["Stat"].ToString() == "Denied") { Lstat = "palevioletred"; }
                        else if (dtQuery.Rows[x]["Stat"].ToString() == "Cancelled") { Lstat = "red"; }
                        string sStatement1 = "<tr><td>" + dtQuery.Rows[x]["identity_column"].ToString() + "</td>" +
                                         "<td>" + dtQuery.Rows[x]["Name"].ToString() + "</td>" +
                                         "<td>" + dtQuery.Rows[x]["Type"].ToString() + "</td>" +
                                         "<td>" + dtQuery.Rows[x]["DateFiled"].ToString() + "</td>" +
                                         "<td>" + dtQuery.Rows[x]["FDate"].ToString() + " - " + dtQuery.Rows[x]["TDate"].ToString() + "</td>" +
                                         "<td>" + dtQuery.Rows[x]["days"].ToString() + "</td>" +
                                         "<td style='background-color:" + Lstat + ";'>" + dtQuery.Rows[x]["Stat"].ToString() + "</td>" +
                                         "<td>" + dtQuery.Rows[x]["empReason"].ToString() + "</td>" +
                                         "<td>";
                        Panel1.Controls.Add(new LiteralControl(sStatement1));
                        button.ID = dtQuery.Rows[x]["identity_column"].ToString();
                        button.Text = "View";
                        button.CssClass = "btn btn-out-dashed waves-effect waves-light btn-primary btn-square";
                        button.PostBackUrl = "~/LeaveDet?param=" + dtQuery.Rows[x]["identity_column"].ToString() + "";
                        Panel1.Controls.Add(button);
                        Panel1.Controls.Add(new LiteralControl("</td></tr>"));
                    }



                }

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        private string getDpart(string eDpart)
        {
            string nameDepart = "";
            string dtQry = "";
            dtQry = "Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = '" + eDpart + "' and cspopupfor = 'DPT'";
            DataTable dt = HRMIS.Module.GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                nameDepart = dt.Rows[0]["cspopuptext"].ToString();
            }
            return nameDepart;

        }
        private string getPosition(string Gposition)
        {
            string pos = "";
            string dtQry = "";
            dtQry = "Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = '" + Gposition + "' and cspopupfor = 'POS'";
            DataTable dt = HRMIS.Module.GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                pos = dt.Rows[0]["cspopuptext"].ToString();
            }

            return pos;
        }
    }
}
