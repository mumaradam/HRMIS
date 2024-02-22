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


public partial class Dashboard : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection();
    OleDbCommand sqlComm = new OleDbCommand();
    OleDbDataAdapter sqlDA = new OleDbDataAdapter();
    private static string strcCon = "";
    private static DataTable dtQuery;
    string getEmpNo = "";
    int TMonth = 0;
    int TYear = 0;
    int Nmonth = 0;
    string getDepart = "";
    string getPosUser = "";
    string getRolUser = "";
    string getUserAdmin = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Redirect("~/404");
        //TMonth = DateTime.Now.Month;
        //TYear = DateTime.Now.Year;
        //Nmonth = DateTime.Now.AddMonths(1).Month;
        //DateTime Ndt = DateTime.Now.AddMonths(1);
        //getEmpNo = Session["Uname"] as string;
        //if (string.IsNullOrEmpty(getEmpNo))
        //{
        //    Session.Abandon();
        //    Response.Redirect("~/Login");
        //}
        //else
        //{
        //    getUserInfo();
        //    getNumbers();
        //    getUpBirthday();
        //    getToBirthday();
        //    getAnniv();
        //    getAllLeave();
        //    lblTmonth.Text = Ndt.ToString("MMMM");
        //}
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

        }
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
    protected void lblBiometric_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Biometric");
    }
    protected void lblLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Login");
    }
    protected void btnLeaveAll_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Leave");
    }
    protected void lblAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account?param=" + getEmpNo);
    }
    protected void lblLeave_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Leave");
    }
    protected void lblCalendar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Calendar");
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
    private void getNumbers()
    {
        try
        {
            int countFind = 0;
            string findEmpNo = "";
            findEmpNo = "Select count(*) from seihaHRMIS.dbo.HREmpInfo where empstatus=1";
            countFind = int.Parse(HRMIS.Module.GetCount(findEmpNo));
            if (countFind > 0)
            {
                totEmp.Text= countFind.ToString();
            }
            //--------------------
            int countLeave = 0;
            string findLeave = "";
            findLeave = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo";
            countLeave = int.Parse(HRMIS.Module.GetCount(findLeave));
            if (countLeave > 0)
            {
                totLeave.Text = countLeave.ToString();
            }

            int countLeaveM = 0;
            string findLeaveM = "";
            findLeaveM = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where convert(varchar, empdate, 101) = '" + DateTime.Now.ToString("MM/dd/yyyy") + "'";
            countLeaveM = int.Parse(HRMIS.Module.GetCount(findLeaveM));
            if (countLeaveM > 0)
            {
                totLeaveM.Text = countLeaveM.ToString();
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void getAllLeave()
    {
        try
        {
            dtQuery = null;
            string dtLeaveList = "";
            if (getUserAdmin == "1")
            {
                dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empno) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                          " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo where empstatus = 0 order by empdate desc";
            }
            else
            {
                if (getPosUser == "ITM" || getPosUser == "TMR" || getPosUser == "SMR")
                {
                    dtLeaveList = "Select (b.EmpFName + ' ' + b.EmpLName) as Name, convert(varchar, a.empdatefrom, 107) as FDate, convert(varchar, a.empdateto, 107) as TDate , " +
                         " DATEDIFF(day, a.empdatefrom, a.empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo a LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno where a.empstatus = 0 and b.empDept = '" + getDepart + "' order by a.empdate desc";
                }
                else
                {
                    dtLeaveList = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empno) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                         " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo where empstatus = 0 and empNo = '" + getEmpNo + "' order by empdate desc";
                }
               
            }

            dtQuery = HRMIS.Module.GetData(dtLeaveList);
            if (dtQuery.Rows.Count > 0)
            {
                int count = checked(dtQuery.Rows.Count - 1);
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
                    if (dtQuery.Rows[x]["empTOL"].ToString() == "vct") { tol = "Vacation"; }
                    else if (dtQuery.Rows[x]["empTOL"].ToString() == "sck") { tol = "Sick Leave"; }
                    else if (dtQuery.Rows[x]["empTOL"].ToString() == "mty") { tol = "Maternity Leave"; }
                    else if (dtQuery.Rows[x]["empTOL"].ToString() == "pty") { tol = "Paternity Leave"; }
                    else if (dtQuery.Rows[x]["empTOL"].ToString() == "emy") { tol = "Emergency"; }
                    else if (dtQuery.Rows[x]["empTOL"].ToString() == "udt") { tol = "Undertime"; }
                    else if (dtQuery.Rows[x]["empTOL"].ToString() == "chg") { tol = "Change Day Off/Shift"; }

                    if (dtQuery.Rows[x]["empstatus"].ToString() == "0") { Lstat = "Pending"; }
                    string sStatement = "<tr><td style='font-size: 1.4rem;'>" + dtQuery.Rows[x]["Name"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + tol + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQuery.Rows[x]["FDate"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQuery.Rows[x]["TDate"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQuery.Rows[x]["empReason"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + dtQuery.Rows[x]["days"].ToString() + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>" + Lstat + "</td>" +
                                                    "<td style='font-size: 1.4rem;'>";
                    Panel4.Controls.Add(new LiteralControl(sStatement));
                    button.ID = dtQuery.Rows[x]["identity_column"].ToString();
                    button.Text = "View More";
                    button.Attributes.Add("ClientIDMode", "Static"); 
                    button.CssClass = "btn pmd-btn-flat pmd-ripple-effect btn-primary";
                    button.Attributes.Add("style", "font-size: 1.4rem");
                    button.PostBackUrl = "~/LeaveDetails?param=" + dtQuery.Rows[x]["identity_column"].ToString() + "";
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
            Response.Write(ex.Message);
        }
    }
    private void getUpBirthday()
    {
         try
         {
             dtQuery = null;
             string dtUpBirth = "";
             dtUpBirth = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar(6), EmpDOB, 7) as BDate, EmpGen from seihaHRMIS.dbo.HREmpInfo where month(EmpDOB) = " + Nmonth + " order by day(EmpDOB)";
             dtQuery = HRMIS.Module.GetData(dtUpBirth);
             if(dtQuery.Rows.Count > 0)
             {
                 int count = checked(dtQuery.Rows.Count - 1);
                 for (int x = 0; x <= count; x = checked(x + 1))
                 {
                     if (dtQuery.Rows[x]["EmpGen"].ToString() == "0")
                     {
                         Literal1.Text = Literal1.Text + "<li class='list-group-item d-flex flex-row'><a href='javascript:void(0);' class='pmd-avatar-list-img' title='profile-link'><img class='img-fluid' src='images/img_avatar.png' style='height: 40px; width: 40px;'></a> " +
                         "<div class='media-body'><h3 class='pmd-list-title' style='font-size: 1.5rem;'>" + dtQuery.Rows[x]["Name"].ToString() + "</h3><p class='pmd-list-subtitle' style='font-size: 1.175rem;'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></li>";
                     }
                     else
                     {
                         Literal1.Text = Literal1.Text + "<li class='list-group-item d-flex flex-row'><a href='javascript:void(0);' class='pmd-avatar-list-img' title='profile-link'><img class='img-fluid' src='images/img_avatar2.png' style='height: 40px; width: 40px;'></a> " +
                         "<div class='media-body'><h3 class='pmd-list-title' style='font-size: 1.5rem;'>" + dtQuery.Rows[x]["Name"].ToString() + "</h3><p class='pmd-list-subtitle' style='font-size: 1.175rem;'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></li>";
                     }
                 }
             }

         }
         catch (System.Net.WebException ex)
         {
             Response.Write(ex.Message);
         }
    }
    private void getToBirthday()
    {
        try
        {
            dtQuery = null;
            string dtUpBirth = "";
            dtUpBirth = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar(6), EmpDOB, 7) as BDate, EmpGen from seihaHRMIS.dbo.HREmpInfo where month(EmpDOB) = " + TMonth + " order by day(EmpDOB)";
            dtQuery = HRMIS.Module.GetData(dtUpBirth);
            if (dtQuery.Rows.Count > 0)
            {
                int count = checked(dtQuery.Rows.Count - 1);
                for (int x = 0; x <= count; x = checked(x + 1))
                {
                    if (dtQuery.Rows[x]["EmpGen"].ToString() == "0")
                    {
                        Literal2.Text = Literal2.Text + "<li class='list-group-item d-flex flex-row'><a href='javascript:void(0);' class='pmd-avatar-list-img' title='profile-link'><img class='img-fluid' src='images/img_avatar.png' style='height: 40px; width: 40px;'></a> " +
                        "<div class='media-body'><h3 class='pmd-list-title' style='font-size: 1.5rem;'>" + dtQuery.Rows[x]["Name"].ToString() + "</h3><p class='pmd-list-subtitle' style='font-size: 1.175rem;'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></li>";
                    }
                    else
                    {
                        Literal2.Text = Literal2.Text + "<li class='list-group-item d-flex flex-row'><a href='javascript:void(0);' class='pmd-avatar-list-img' title='profile-link'><img class='img-fluid' src='images/img_avatar2.png' style='height: 40px; width: 40px;'></a> " +
                        "<div class='media-body'><h3 class='pmd-list-title' style='font-size: 1.5rem;'>" + dtQuery.Rows[x]["Name"].ToString() + "</h3><p class='pmd-list-subtitle' style='font-size: 1.175rem;'>" + dtQuery.Rows[x]["BDate"].ToString() + "</p></div></li>";
                    }
                }
            }

        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void getAnniv()
    {
        try
        {
            dtQuery = null;
            string dtUpBirth = "";
            dtUpBirth = "Select EmpFName + ' ' + EmpLName as Name, (CAST((DATEDIFF(DD, EmpDOH, getdate())) / 365.25 as int)) as ADate, EmpDOH, EmpGen from seihaHRMIS.dbo.HREmpInfo where month(EmpDOH) = " + TMonth + " order by ADate";
            dtQuery = HRMIS.Module.GetData(dtUpBirth);
            if (dtQuery.Rows.Count > 0)
            {
                int count = checked(dtQuery.Rows.Count - 1);
                for (int x = 0; x <= count; x = checked(x + 1))
                {
                    if (dtQuery.Rows[x]["EmpGen"].ToString() == "0")
                    {
                        Literal3.Text = Literal3.Text + "<li class='list-group-item d-flex flex-row'><a href='javascript:void(0);' class='pmd-avatar-list-img' title='profile-link'><img class='img-fluid' src='images/img_avatar.png' style='height: 40px; width: 40px;'></a> " +
                        "<div class='media-body'><h3 class='pmd-list-title' style='font-size: 1.5rem;'>" + dtQuery.Rows[x]["Name"].ToString() + "</h3><p class='pmd-list-subtitle' style='font-size: 1.175rem;'>" + dtQuery.Rows[x]["ADate"].ToString() + " year/s</p></div></li>";
                    }
                    else
                    {
                        Literal3.Text = Literal3.Text + "<li class='list-group-item d-flex flex-row'><a href='javascript:void(0);' class='pmd-avatar-list-img' title='profile-link'><img class='img-fluid' src='images/img_avatar2.png' style='height: 40px; width: 40px;'></a> " +
                        "<div class='media-body'><h3 class='pmd-list-title' style='font-size: 1.5rem;'>" + dtQuery.Rows[x]["Name"].ToString() + "</h3><p class='pmd-list-subtitle' style='font-size: 1.175rem;'>" + dtQuery.Rows[x]["ADate"].ToString() + " year/s</p></div></li>";
                    }
                }
            }

        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    
}