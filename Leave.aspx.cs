using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using System.Drawing;

public partial class Leave : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection();
    OleDbCommand sqlComm = new OleDbCommand();
    OleDbDataAdapter sqlDA = new OleDbDataAdapter();
    private static string strcCon = "";
    private static DataTable dtQuery;
    private static DataTable dtQueryYourL;
    private static DataTable dtQueryAllLeave;
    private static DataTable dtQueryAllApp;
    private static DataTable dtQueryAllDen;
    private static string getEmpNo = "";
    private static string getDepart = "";
    private static string getPosUser = "";
    private static string getRolUser = "";
    private static string getUserAdmin = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/404");
        //getEmpNo = Session["Uname"] as string;
        ////getUserInfo();
        ////getAllLeave();
        //if (!IsPostBack)
        //{
        //    getUserInfo();
        //    getYourLeave();
        //    getAllLeave();
        //    getDeniedLeave(); 
        //    getApprovedLeave();
        //}
        //else
        //{
        //    if (getEmpNo == "" || string.IsNullOrEmpty(getEmpNo))
        //    {
        //        Session.Abandon();
        //        Response.Redirect("~/Login");
        //    }
        //    else
        //    {
               
        //    }

        //}
    }
    protected void lblDash_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Dashboard");
    }
    protected void lblLeaveA_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LeaveApplication");
    }
    protected void lblReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Registration");
    }
    protected void lblEmployee_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Employee");
    }
    protected void lblLeave_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Leave");
    }
    protected void lblLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Login");
    }
    protected void lblAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account?param=" + getEmpNo);
    }
    protected void lblLeaveC_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LeaveCredits");
    }
    protected void lblNoti_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Notification");
    }
    protected void lblPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ChangePassword?accpass=" + getEmpNo);
    }
    protected void lblCalendar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Calendar");
    }
    protected void searchBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(searchBy.SelectedIndex == 0)
        {
            datepicker.Text = "";
            sDate.Visible = false;
            sName.Visible = false;
            sAllBtn.Visible = false;
            DropDownList1.Visible = false;
            getAllLeave();
            //Response.Redirect(Request.RawUrl);
        }
        else if (searchBy.SelectedIndex == 1)
        {
            datepicker.Text = "";
            sDate.Visible = true;
            sAllBtn.Visible = true;
            sName.Visible = false;
            DropDownList1.Visible = false;
            getAllLeave();
        }
        else if (searchBy.SelectedIndex == 2)
        {
            sDate.Visible = false;
            sAllBtn.Visible = true;
            sName.Visible = true;
            DropDownList1.Visible = false;
            getAllLeave();
        }
        else if (searchBy.SelectedIndex == 3)
        {
            DropDownList1.Visible = true;
            sName.Visible = false;
            sDate.Visible = false;
            sAllBtn.Visible = false;
        }
        
    }
    protected void btnSearchOwn_Click(object sender, EventArgs e)
    {
        try
        {
            getYourLeave();
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnSearchAll_Click(object sender, EventArgs e)
    {

        try
        {
            getAllLeave();
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnResetAll_Click(object sender, EventArgs e)
    {
        try
        {
            datepicker.Text = "";
            getAllLeave();
            //Response.Redirect(Request.RawUrl);
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnResetOwn_Click(object sender, EventArgs e)
    {
        try
        {
            datepicker1.Text = "";
            getYourLeave();
            //Response.Redirect(Request.RawUrl);
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void getUserInfo()
    {
        dtQuery = null;
        string sQuery = "Select * from seihaHRMIS.dbo.HREmpInfo where empno = '" + getEmpNo + "'";
        dtQuery = HRMIS.Module.GetData(sQuery);
        if (dtQuery.Rows.Count > 0)
        {
            getDepart = dtQuery.Rows[0]["empdept"].ToString();
            getPosUser = dtQuery.Rows[0]["emppos"].ToString();
            getRolUser = dtQuery.Rows[0]["emprole"].ToString();
            getUserAdmin = dtQuery.Rows[0]["empadmin"].ToString();
            lblUser.Text = dtQuery.Rows[0]["empFname"].ToString();
            if (dtQuery.Rows[0]["empGen"].ToString() == "0")
            {
                UserPic.Attributes.Add("src", "images/img_avatar.png");
            }
            else
            {
                UserPic.Attributes.Add("src", "images/img_avatar2.png");
            }
            if (dtQuery.Rows[0]["empadmin"].ToString() != "1")
            {
                lblReg.Visible = false;
            }
            else
            {
                lblReg.Visible = true;
            }
            if (getDepart == "MGR" || getDepart == "ADM")
            {
                if (getDepart == "ADM")
                {
                    if (getPosUser == "DVR")
                    {
                        AllLeave.Visible = false;
                        //getYourLeave();
                    }
                    else
                    {
                        AllLeave.Visible = true;
                        //getYourLeave();
                    }
                }
                else
                {
                    //getYourLeave();
                    AllLeave.Visible = true;
                }
            }
            else
            {
                if (getDepart == "IT" || getDepart == "FCT")
                {
                    if (getPosUser == "ITM" || getPosUser == "TMR" || getPosUser == "SMR")
                    {
                        //getYourLeave();
                        AllLeave.Visible = true;
                    }
                    else
                    {
                        if (getRolUser == "RLD" || getRolUser == "RSL")
                        {
                            //getYourLeave();
                            AllLeave.Visible = true;
                        }
                        else
                        {
                            //getYourLeave();
                            AllLeave.Visible = false;
                        }
                    }

                }
            }
            
        }
    }
    private void getYourLeave()
    {
        try
        {
            dtQueryYourL = null;
            string dtLeaveList = "";
            string fileDate = "";
            if (datepicker1.Text != "")
            {
                DateTime loadedDate = DateTime.Parse(datepicker1.Text);
                fileDate = "and month(empdatefrom) = " + loadedDate.Month + " and year(empdatefrom) = " + loadedDate.Year;
            }
            dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                         " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo where empstatus = 0 and empNo = '" + getEmpNo + "' " + fileDate + " order by empdate desc";

            dtQueryYourL = HRMIS.Module.GetData(dtLeaveList);
            if (dtQueryYourL.Rows.Count > 0)
            {
                int count = checked(dtQueryYourL.Rows.Count - 1);
                string sStatement1 = "<table class='table pmd-table table-hover'><thead><tr><th style='font-size: 1.5rem;'>Employee</th>" +
                                     "<th style='font-size: 1.5rem;'>Leave Type</th><th style='font-size: 1.5rem;'>Start Date</th>" +
                                     "<th style='font-size: 1.5rem;'>End Date</th><th style='font-size: 1.5rem;'>Reason</th><th style='font-size: 1.5rem;'>No of Leave</th>" +
                                     "<th style='font-size: 1.5rem;'>Status</th><th style='font-size: 1.5rem;'></th></tr></thead><tbody>";
                Panel1.Controls.Add(new LiteralControl(sStatement1));
                for (int x = 0; x <= count; x = checked(x + 1))
                {
                    string tol = "";
                    string Lstat = "";
                    Button button = new Button();
                    if (dtQueryYourL.Rows[x]["empTOL"].ToString() == "vct") { tol = "Vacation/Loyalty Leave"; }
                    else if (dtQueryYourL.Rows[x]["empTOL"].ToString() == "sck") { tol = "Sick Leave"; }
                    else if (dtQueryYourL.Rows[x]["empTOL"].ToString() == "mty") { tol = "Maternity Leave"; }
                    else if (dtQueryYourL.Rows[x]["empTOL"].ToString() == "pty") { tol = "Paternity Leave"; }
                    else if (dtQueryYourL.Rows[x]["empTOL"].ToString() == "emy") { tol = "Emergency"; }
                    else if (dtQueryYourL.Rows[x]["empTOL"].ToString() == "udt") { tol = "Undertime"; }
                    else if (dtQueryYourL.Rows[x]["empTOL"].ToString() == "chg") { tol = "Change Day Off/Shift"; }

                    if (dtQueryYourL.Rows[x]["empstatus"].ToString() == "0") { Lstat = "Pending"; }
                    else if (dtQueryYourL.Rows[x]["empstatus"].ToString() == "1") { Lstat = "Approved"; }
                    else if (dtQueryYourL.Rows[x]["empstatus"].ToString() == "2") { Lstat = "Denied"; }
                    else if (dtQueryYourL.Rows[x]["empstatus"].ToString() == "3") { Lstat = "Cancelled"; }
                    string sStatement = "<tr><td style='font-size: 1.4rem;'>" + dtQueryYourL.Rows[x]["Name"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + tol + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryYourL.Rows[x]["FDate"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryYourL.Rows[x]["TDate"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryYourL.Rows[x]["empReason"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryYourL.Rows[x]["days"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + Lstat + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>";
                    Panel1.Controls.Add(new LiteralControl(sStatement));
                    button.ID = dtQueryYourL.Rows[x]["identity_column"].ToString();
                    button.Text = "View More";
                    button.CssClass = "btn pmd-btn-flat pmd-ripple-effect btn-primary";
                    button.Attributes.Add("style", "font-size: 1.4rem");
                    button.PostBackUrl = "~/LeaveDetails?param=" + dtQueryYourL.Rows[x]["identity_column"].ToString() + "";
                    Panel1.Controls.Add(button);

                }
                Panel1.Controls.Add(new LiteralControl("</td></tr></tbody></table>"));
            }
            else
            {
                string sStatement1 = "<table class='table pmd-table table-hover'><thead><tr><th style='font-size: 1.5rem;'>Employee</th>" +
                                     "<th style='font-size: 1.5rem;'>Leave Type</th><th style='font-size: 1.5rem;'>Start Date</th>" +
                                     "<th style='font-size: 1.5rem;'>End Date</th><th style='font-size: 1.5rem;'>Reason</th><th style='font-size: 1.5rem;'>No of Leave</th>" +
                                     "<th style='font-size: 1.5rem;'>Status</th><th style='font-size: 1.5rem;'></th></tr></thead><tbody></tbody></table>";
                Panel1.Controls.Add(new LiteralControl(sStatement1));
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(@"<script> alert('There is something in retrieving your leave/s) </script>");
        }
    }
    private void getAllLeave()
    {
        try
        {
            dtQueryAllLeave = null;
            string dtLeaveList = "";
            string fileDate = "";
            if (datepicker.Text != "")
            {
                DateTime loadedDate = DateTime.Parse(datepicker.Text);
                fileDate = "and month(empdatefrom) = " + loadedDate.Month + " and year(empdatefrom) = " + loadedDate.Year;
            }
            
            
            if (getUserAdmin=="1")
            {
                dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empno) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                          " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo where empNo != '" + getEmpNo + "' and empstatus = 0 " + fileDate + " order by empdate desc";
            }
            else
            {
                if (getPosUser == "ITM" || getPosUser == "TMR" || getPosUser == "SMR")
                {
                    dtLeaveList = "Select (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
                                  " DATEDIFF(day, a.empdatefrom, a.empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN " +
                                  "seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno where a.empNo != '" + getEmpNo + "' and  a.empstatus = 0 and b.empDept = '" + getDepart + "' " + fileDate + " order by a.empdate desc";
                
                }
                else if (getDepart == "MGR" || getDepart == "ADM")
                {
                    
                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empno) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                          " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo where empNo != '" + getEmpNo + "' and empstatus = 0 " + fileDate + " order by empdate desc";
                }
                else if (getRolUser == "RLD" || getRolUser == "RSL")
                {
                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = a.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                         " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.notInfo b On a.identity_column = b.notLeaveNo and a.Empno = b.notEmpno where a.empNo != '" + getEmpNo + "' and empstatus = 0 and notWho = '" + getEmpNo + "' " + fileDate + " order by empdate desc";
                }
            }

            dtQueryAllLeave = HRMIS.Module.GetData(dtLeaveList);
            if (dtQueryAllLeave.Rows.Count > 0)
            {
                int count = checked(dtQueryAllLeave.Rows.Count - 1);
                string sStatement1 = "<table class='table pmd-table table-hover'><thead><tr><th style='font-size: 1.5rem;'>Employee</th>" +
                                     "<th style='font-size: 1.5rem;'>Leave Type</th><th style='font-size: 1.5rem;'>Start Date</th>" +
                                     "<th style='font-size: 1.5rem;'>End Date</th><th style='font-size: 1.5rem;'>Reason</th><th style='font-size: 1.5rem;'>No of Leave</th>" +
                                     "<th style='font-size: 1.5rem;'>Status</th><th style='font-size: 1.5rem;'></th></tr></thead><tbody>";
                Panel4.Controls.Add(new LiteralControl(sStatement1));
                for (int x = 0; x <= count; x = checked(x + 1))
                {
                    string tol = "";
                    string Lstat = "";
                    Button button = new Button();
                    if (dtQueryAllLeave.Rows[x]["empTOL"].ToString() == "vct") { tol = "Vacation/Loyalty Leave"; }
                    else if (dtQueryAllLeave.Rows[x]["empTOL"].ToString() == "sck") { tol = "Sick Leave"; }
                    else if (dtQueryAllLeave.Rows[x]["empTOL"].ToString() == "mty") { tol = "Maternity Leave"; }
                    else if (dtQueryAllLeave.Rows[x]["empTOL"].ToString() == "pty") { tol = "Paternity Leave"; }
                    else if (dtQueryAllLeave.Rows[x]["empTOL"].ToString() == "emy") { tol = "Emergency"; }
                    else if (dtQueryAllLeave.Rows[x]["empTOL"].ToString() == "udt") { tol = "Undertime"; }
                    else if (dtQueryAllLeave.Rows[x]["empTOL"].ToString() == "chg") { tol = "Change Day Off/Shift"; }

                    if (dtQueryAllLeave.Rows[x]["empstatus"].ToString() == "0") { Lstat = "Pending"; }
                    else if (dtQueryAllLeave.Rows[x]["empstatus"].ToString() == "1") { Lstat = "Approved"; }
                    else if (dtQueryAllLeave.Rows[x]["empstatus"].ToString() == "2") { Lstat = "Denied"; }
                    else if (dtQueryAllLeave.Rows[x]["empstatus"].ToString() == "3") { Lstat = "Cancelled"; }
                    string sStatement = "<tr><td style='font-size: 1.4rem;'>" + dtQueryAllLeave.Rows[x]["Name"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + tol + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryAllLeave.Rows[x]["FDate"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryAllLeave.Rows[x]["TDate"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryAllLeave.Rows[x]["empReason"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryAllLeave.Rows[x]["days"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + Lstat + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>";
                    Panel4.Controls.Add(new LiteralControl(sStatement));
                    button.ID = dtQueryAllLeave.Rows[x]["identity_column"].ToString();
                    button.Text = "View More";
                    button.CssClass = "btn pmd-btn-flat pmd-ripple-effect btn-primary";
                    button.Attributes.Add("style", "font-size: 1.4rem");
                    button.PostBackUrl = "~/LeaveDetails?param=" + dtQueryAllLeave.Rows[x]["identity_column"].ToString() + "";
                    Panel4.Controls.Add(button);

                }
                Panel4.Controls.Add(new LiteralControl("</td></tr></tbody></table>"));
            }
            else
            {
                string sStatement1 = "<table class='table pmd-table table-hover'><thead><tr><th style='font-size: 1.5rem;'>Employee</th>" +
                                     "<th style='font-size: 1.5rem;'>Leave Type</th><th style='font-size: 1.5rem;'>Start Date</th>" +
                                     "<th style='font-size: 1.5rem;'>End Date</th><th style='font-size: 1.5rem;'>Reason</th><th style='font-size: 1.5rem;'>No of Leave</th>" +
                                     "<th style='font-size: 1.5rem;'>Status</th><th style='font-size: 1.5rem;'></th></tr></thead><tbody></tbody></table>";
                Panel4.Controls.Add(new LiteralControl(sStatement1));
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(@"<script> alert('There is something in retrieving All Leave/s) </script>");
        }
    }
    private void getApprovedLeave()
    {
        try
        {
            dtQueryAllApp = null;
            string dtLeaveList = "";
            string fileDate = "";
            //if (datepicker.Text != "")
            //{
            //    DateTime loadedDate = DateTime.Parse(datepicker.Text);
            //    fileDate = "and month(empdatefrom) = " + loadedDate.Month + " and year(empdatefrom) = " + loadedDate.Year;
            //}


            if (getUserAdmin == "1")
            {
                dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empno) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                          " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo where empstatus = 1 " + fileDate + " order by empdate desc";
            }
            else
            {
                if (getPosUser == "ITM" || getPosUser == "TMR" || getPosUser == "SMR")
                {
                    dtLeaveList = "Select (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
                                  " DATEDIFF(day, a.empdatefrom, a.empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN " +
                                  "seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno where a.empstatus = 1 and b.empDept = '" + getDepart + "' " + fileDate + " order by a.empdate desc";

                }
                else if (getDepart == "MGR" || getDepart == "ADM")
                {

                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empno) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                          " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo where empstatus = 1 " + fileDate + " order by empdate desc";
                }
                else if (getRolUser == "RLD")
                {
                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = a.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                         " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo a where (empHeadEmpNo = '" + getEmpNo + "' or a.empno = '" + getEmpNo + "') and empstatus = 1 " + fileDate + " order by empdate desc";
                }
                else
                {
                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = a.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                         " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo a where empstatus = 1 and empno = '" + getEmpNo + "' order by empdate desc";
                }
            }

            dtQueryAllApp = HRMIS.Module.GetData(dtLeaveList);
            if (dtQueryAllApp.Rows.Count > 0)
            {
                int count = checked(dtQueryAllApp.Rows.Count - 1);
                string sStatement1 = "<table class='table pmd-table table-hover'><thead><tr><th style='font-size: 1.5rem;'>Employee</th>" +
                                     "<th style='font-size: 1.5rem;'>Leave Type</th><th style='font-size: 1.5rem;'>Start Date</th>" +
                                     "<th style='font-size: 1.5rem;'>End Date</th><th style='font-size: 1.5rem;'>Reason</th><th style='font-size: 1.5rem;'>No of Leave</th>" +
                                     "<th style='font-size: 1.5rem;'>Status</th><th style='font-size: 1.5rem;'></th></tr></thead><tbody>";
                Panel2.Controls.Add(new LiteralControl(sStatement1));
                for (int x = 0; x <= count; x = checked(x + 1))
                {
                    string tol = "";
                    string Lstat = "";
                    Button button = new Button();
                    if (dtQueryAllApp.Rows[x]["empTOL"].ToString() == "vct") { tol = "Vacation/Loyalty Leave"; }
                    else if (dtQueryAllApp.Rows[x]["empTOL"].ToString() == "sck") { tol = "Sick Leave"; }
                    else if (dtQueryAllApp.Rows[x]["empTOL"].ToString() == "mty") { tol = "Maternity Leave"; }
                    else if (dtQueryAllApp.Rows[x]["empTOL"].ToString() == "pty") { tol = "Paternity Leave"; }
                    else if (dtQueryAllApp.Rows[x]["empTOL"].ToString() == "emy") { tol = "Emergency"; }
                    else if (dtQueryAllApp.Rows[x]["empTOL"].ToString() == "udt") { tol = "Undertime"; }
                    else if (dtQueryAllApp.Rows[x]["empTOL"].ToString() == "chg") { tol = "Change Day Off/Shift"; }

                    if (dtQueryAllApp.Rows[x]["empstatus"].ToString() == "0") { Lstat = "Pending"; }
                    else if (dtQueryAllApp.Rows[x]["empstatus"].ToString() == "1") { Lstat = "Approved"; }
                    else if (dtQueryAllApp.Rows[x]["empstatus"].ToString() == "2") { Lstat = "Denied"; }
                    else if (dtQueryAllApp.Rows[x]["empstatus"].ToString() == "3") { Lstat = "Cancelled"; }
                    string sStatement = "<tr><td style='font-size: 1.4rem;'>" + dtQueryAllApp.Rows[x]["Name"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + tol + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryAllApp.Rows[x]["FDate"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryAllApp.Rows[x]["TDate"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryAllApp.Rows[x]["empReason"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryAllApp.Rows[x]["days"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + Lstat + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>";
                    Panel2.Controls.Add(new LiteralControl(sStatement));
                    button.ID = dtQueryAllApp.Rows[x]["identity_column"].ToString();
                    button.Text = "View More";
                    button.CssClass = "btn pmd-btn-flat pmd-ripple-effect btn-primary";
                    button.Attributes.Add("style", "font-size: 1.4rem");
                    button.PostBackUrl = "~/LeaveDetails?param=" + dtQueryAllApp.Rows[x]["identity_column"].ToString() + "";
                    Panel2.Controls.Add(button);

                }
                Panel2.Controls.Add(new LiteralControl("</td></tr></tbody></table>"));
            }
            else
            {
                string sStatement1 = "<table class='table pmd-table table-hover'><thead><tr><th style='font-size: 1.5rem;'>Employee</th>" +
                                     "<th style='font-size: 1.5rem;'>Leave Type</th><th style='font-size: 1.5rem;'>Start Date</th>" +
                                     "<th style='font-size: 1.5rem;'>End Date</th><th style='font-size: 1.5rem;'>Reason</th><th style='font-size: 1.5rem;'>No of Leave</th>" +
                                     "<th style='font-size: 1.5rem;'>Status</th><th style='font-size: 1.5rem;'></th></tr></thead><tbody></tbody></table>";
                Panel2.Controls.Add(new LiteralControl(sStatement1));
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(@"<script> alert('There is something in retrieving Approved Leave/s) </script>");
        }
    }
    private void getDeniedLeave()
    {
        try
        {
            dtQueryAllDen = null;
            string dtLeaveList = "";
            string fileDate = "";
            //if (datepicker.Text != "")
            //{
            //    DateTime loadedDate = DateTime.Parse(datepicker.Text);
            //    fileDate = "and month(empdatefrom) = " + loadedDate.Month + " and year(empdatefrom) = " + loadedDate.Year;
            //}


            if (getUserAdmin == "1")
            {
                dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empno) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                          " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo where empstatus = 2 " + fileDate + " order by empdate desc";
            }
            else
            {
                if (getPosUser == "ITM" || getPosUser == "TMR" || getPosUser == "SMR")
                {
                    dtLeaveList = "Select (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
                                  " DATEDIFF(day, a.empdatefrom, a.empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN " +
                                  "seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno where a.empstatus = 2 and b.empDept = '" + getDepart + "' " + fileDate + " order by a.empdate desc";

                }
                else if (getDepart == "MGR" || getDepart == "ADM")
                {

                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empno) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                          " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo where empstatus = 2 " + fileDate + " order by empdate desc";
                }
                else if (getRolUser == "RLD")
                {
                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = a.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                         " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo a where (empHeadEmpNo = '" + getEmpNo + "' or a.empno = '" + getEmpNo + "') and empstatus = 2 " + fileDate + " order by empdate desc";
                }
                else
                {
                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = a.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                         " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo a where empstatus = 2 and empno = '" + getEmpNo + "' order by empdate desc";
                }
            }

            dtQueryAllDen = HRMIS.Module.GetData(dtLeaveList);
            if (dtQueryAllDen.Rows.Count > 0)
            {
                int count = checked(dtQueryAllDen.Rows.Count - 1);
                string sStatement1 = "<table class='table pmd-table table-hover'><thead><tr><th style='font-size: 1.5rem;'>Employee</th>" +
                                     "<th style='font-size: 1.5rem;'>Leave Type</th><th style='font-size: 1.5rem;'>Start Date</th>" +
                                     "<th style='font-size: 1.5rem;'>End Date</th><th style='font-size: 1.5rem;'>Reason</th><th style='font-size: 1.5rem;'>No of Leave</th>" +
                                     "<th style='font-size: 1.5rem;'>Status</th><th style='font-size: 1.5rem;'></th></tr></thead><tbody>";
                Panel3.Controls.Add(new LiteralControl(sStatement1));
                for (int x = 0; x <= count; x = checked(x + 1))
                {
                    string tol = "";
                    string Lstat = "";
                    Button button = new Button();
                    if (dtQueryAllDen.Rows[x]["empTOL"].ToString() == "vct") { tol = "Vacation/Loyalty Leave"; }
                    else if (dtQueryAllDen.Rows[x]["empTOL"].ToString() == "sck") { tol = "Sick Leave"; }
                    else if (dtQueryAllDen.Rows[x]["empTOL"].ToString() == "mty") { tol = "Maternity Leave"; }
                    else if (dtQueryAllDen.Rows[x]["empTOL"].ToString() == "pty") { tol = "Paternity Leave"; }
                    else if (dtQueryAllDen.Rows[x]["empTOL"].ToString() == "emy") { tol = "Emergency"; }
                    else if (dtQueryAllDen.Rows[x]["empTOL"].ToString() == "udt") { tol = "Undertime"; }
                    else if (dtQueryAllDen.Rows[x]["empTOL"].ToString() == "chg") { tol = "Change Day Off/Shift"; }

                    if (dtQueryAllDen.Rows[x]["empstatus"].ToString() == "0") { Lstat = "Pending"; }
                    else if (dtQueryAllDen.Rows[x]["empstatus"].ToString() == "1") { Lstat = "Approved"; }
                    else if (dtQueryAllDen.Rows[x]["empstatus"].ToString() == "2") { Lstat = "Denied"; }
                    else if (dtQueryAllDen.Rows[x]["empstatus"].ToString() == "3") { Lstat = "Cancelled"; }
                    string sStatement = "<tr><td style='font-size: 1.4rem;'>" + dtQueryAllDen.Rows[x]["Name"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + tol + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryAllDen.Rows[x]["FDate"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryAllDen.Rows[x]["TDate"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryAllDen.Rows[x]["empReason"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQueryAllDen.Rows[x]["days"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + Lstat + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>";
                    Panel3.Controls.Add(new LiteralControl(sStatement));
                    button.ID = dtQueryAllDen.Rows[x]["identity_column"].ToString();
                    button.Text = "View More";
                    button.CssClass = "btn pmd-btn-flat pmd-ripple-effect btn-primary";
                    button.Attributes.Add("style", "font-size: 1.4rem");
                    button.PostBackUrl = "~/LeaveDetails?param=" + dtQueryAllDen.Rows[x]["identity_column"].ToString() + "";
                    Panel3.Controls.Add(button);

                }
                Panel3.Controls.Add(new LiteralControl("</td></tr></tbody></table>"));
            }
            else
            {
                string sStatement1 = "<table class='table pmd-table table-hover'><thead><tr><th style='font-size: 1.5rem;'>Employee</th>" +
                                     "<th style='font-size: 1.5rem;'>Leave Type</th><th style='font-size: 1.5rem;'>Start Date</th>" +
                                     "<th style='font-size: 1.5rem;'>End Date</th><th style='font-size: 1.5rem;'>Reason</th><th style='font-size: 1.5rem;'>No of Leave</th>" +
                                     "<th style='font-size: 1.5rem;'>Status</th><th style='font-size: 1.5rem;'></th></tr></thead><tbody></tbody></table>";
                Panel3.Controls.Add(new LiteralControl(sStatement1));
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(@"<script> alert('There is something in retrieving Denied Leave/s) </script>");
        }
    }
    
}