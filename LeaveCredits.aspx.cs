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

public partial class LeaveCredits : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection();
    OleDbCommand sqlComm = new OleDbCommand();
    OleDbDataAdapter sqlDA = new OleDbDataAdapter();
    private static string strcCon = "";
    private static DataTable dtQuery;
    private static DataTable dtCQuery;
    private static DataTable dtEmpInfo;
    string dtEmpInfoQuery = "";
    private static string getEmpNo = "";
    private static string hldAdmnStat = "";
    private static string hldEmpSelect = "";
    [System.Web.Services.WebMethod]
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/404");
        //getEmpNo = Session["Uname"] as string;
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "PageMethods", "Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(PageLoadedHandler); function PageLoadedHandler(sender, args) { Sys.Application.initialize(); }", true);
        //if (string.IsNullOrEmpty(getEmpNo))
        //{
        //    Session.Abandon();
        //    Response.Redirect("~/Login");
        //}
        //else
        //{
        //    if (!IsPostBack)
        //    {
        //        getInfo();
        //        whatToDisplay();
        //    }
        //    //else
        //    //{
        //    //    ///getInfo();
        //    //   whatToDisplay();
        //    //}
            
        //}
    }
    private void getInfo()
    {
        try
        {
            dtEmpInfo = null;
            dtEmpInfoQuery = "";
            dtEmpInfoQuery = "Select * from seihaHRMIS.dbo.HREmpInfo where EmpNo = '" + getEmpNo + "'";
            dtEmpInfo = HRMIS.Module.GetData(dtEmpInfoQuery);
            if (dtEmpInfo.Rows.Count > 0)
            {
                hldAdmnStat = dtEmpInfo.Rows[0]["EmpAdmin"].ToString();
                lblUser.Text = dtEmpInfo.Rows[0]["empFname"].ToString();
                if (dtEmpInfo.Rows[0]["empGen"].ToString() == "0")
                {
                    UserPic.Attributes.Add("src", "images/img_avatar.png");
                }
                else
                {
                    UserPic.Attributes.Add("src", "images/img_avatar2.png");
                }
                if (dtEmpInfo.Rows[0]["empadmin"].ToString() != "1")
                {
                    lblReg.Visible = false;
                }
                else
                {
                    lblReg.Visible = true;
                }
            }
            else
            {
                Session.Abandon();
                Response.Redirect("~/Login");
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
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
    protected void disPlayEmp(object sender, EventArgs e)
    {
        string filePath = (sender as TableRow).ID;
        fillInfo(filePath);
    }
    protected void btnuvleav_Click(object sender, EventArgs e)
    {
        try
        {
            vleav.Value = uvleav.Text;
            int strV = int.Parse(vleav.Value);
            string dtEmp = "";
            dtEmp = "Update seihaHRMIS.dbo.LeaveCreditInfo set leavCredVL = " + strV + " where empno = '" + empNum.Value + "'";
            HRMIS.Module.gblInsert(dtEmp);
            whatToDisplay();
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnuscleav_Click(object sender, EventArgs e)
    {
        try
        {
            scleav.Value = uscleav.Text;
            int strV = int.Parse(scleav.Value);
            string dtEmp = "";
            dtEmp = "Update seihaHRMIS.dbo.LeaveCreditInfo set leavCredSL = " + strV + " where empno = '" + empNum.Value + "'";
            HRMIS.Module.gblInsert(dtEmp);
            whatToDisplay();
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnulleav_Click(object sender, EventArgs e)
    {
        try
        {
            lleav.Value = ulleav.Text;
            int strV = int.Parse(lleav.Value);
            string dtEmp = "";
            dtEmp = "Update seihaHRMIS.dbo.LeaveCreditInfo set leavCredLoyal = " + strV + " where empno = '" + empNum.Value + "'";
            HRMIS.Module.gblInsert(dtEmp);
            whatToDisplay();
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnuspleav_Click(object sender, EventArgs e)
    {
        try
        {
            spleav.Value = uspleav.Text;
            int strV = int.Parse(spleav.Value);
            string dtEmp = "";
            dtEmp = "Update seihaHRMIS.dbo.LeaveCreditInfo set leavCredSpecL = " + strV + " where empno = '" + empNum.Value + "'";
            HRMIS.Module.gblInsert(dtEmp);
            whatToDisplay();
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void whatToDisplay()
    {
        if (hldAdmnStat == "1")
        {
            getEmpAll();
            //fillInfo(getEmpNo);
            Panel4.Height = 500;
            credEmp.Visible = false;
        }
        else
        {
            Panel4.Height = 250;
            listEmp.Visible = false;
            forDisplayInfo.Visible = false;
            fillInfo(getEmpNo);
        }
    }
    private void fillInfo(string strEmp)
    {
        try
        {
            dtQuery = null;
            string dtEmp = "";
            dtEmp = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar, EmpDOH, 107) as HDate, convert(varchar, EmpDOB, 107) as BDate, * " +
                    "from seihaHRMIS.dbo.HREmpInfo where empno = '" + strEmp + "' order by empno";
            dtQuery = HRMIS.Module.GetData(dtEmp);
            if (dtQuery.Rows.Count > 0)
            {
                //----Employee Basic Info
                getLeaveCredits(strEmp);
                lblFname.Text = dtQuery.Rows[0]["empfname"].ToString();
                lblLname.Text = dtQuery.Rows[0]["emplname"].ToString();
                lblMI.Text = dtQuery.Rows[0]["empmi"].ToString();
                lblConNo.Text = dtQuery.Rows[0]["empconno"].ToString();
                lblemail.InnerText = dtQuery.Rows[0]["empemail"].ToString();
                lblemail.Attributes.Add("href", "mailto:" + dtQuery.Rows[0]["empemail"].ToString());
                lblDOB.Text = dtQuery.Rows[0]["BDate"].ToString();
                if (dtQuery.Rows[0]["empgen"].ToString() == "0")
                {
                    lblgen.Text = "Male";
                }
                else
                {
                    lblgen.Text = "Female";
                }
                //-----Employee Detail
                lblempno.Text = dtQuery.Rows[0]["empno"].ToString();
                lblDOH.Text = dtQuery.Rows[0]["HDate"].ToString();
                lblDept.Text = getDpart(dtQuery.Rows[0]["empdept"].ToString());
                lblPos.Text = getPosition(dtQuery.Rows[0]["emppos"].ToString());

                //--------------------------------------------
                lblrole.Text = getRole(dtQuery.Rows[0]["emprole"].ToString());
                string hldRol = dtQuery.Rows[0]["emprole"].ToString();
                lblstatus.Text = getStat(dtQuery.Rows[0]["empempstat"].ToString());


            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void getLeaveCredits(string strEmp)
    {
        try
        {
            string qryLeave = "Select * from seihaHRMIS.dbo.LeaveCreditInfo where empno = '" + strEmp + "'";
            DataTable dtLCredit = HRMIS.Module.GetData(qryLeave);
            if (dtLCredit.Rows.Count > 0)
            {
                lblVacL.Text = dtLCredit.Rows[0]["leavCredVL"].ToString();
                lblSckL.Text = dtLCredit.Rows[0]["leavCredSL"].ToString();
                lblLolL.Text = dtLCredit.Rows[0]["leavCredLoyal"].ToString();
                lblSpcL.Text = dtLCredit.Rows[0]["leavCredSpecL"].ToString();
            }

        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            string dtEmp = "";
            dtEmp = "Update seihaHRMIS.dbo.LeaveCreditInfo set leavCredVL = 5,  leavCredSL = 5";
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                HRMIS.Module.gblInsert(dtEmp);
            }
            whatToDisplay();
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    public void getLeaves_Click(object sender, EventArgs e)
    {
        try
        {
            Panel1.Visible = true;
            string empQuery = "";
            string getLeavEmpNo = "";
            getLeavEmpNo = empNum.Value;
            if (getLeavEmpNo != "")
            {
                empQuery = "Select (select EmpFName + ' ' + EmpLName from seihaHRMIS.dbo.HREmpInfo where empno = HRLeaveInfo.empNo) as Name, convert(varchar, empdatefrom, 107) as FDate, convert(varchar, empdateto, 107) as TDate , " +
                       " DATEDIFF(day, empdatefrom, empdateto) + 1 AS days, *  from seihaHRMIS.dbo.HRLeaveInfo where empstatus = 0 and empNo = '" + getLeavEmpNo + "' order by empdate desc";
                dtQuery = HRMIS.Module.GetData(empQuery);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
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
                        if (dtQuery.Rows[x]["empTOL"].ToString() == "vct") { tol = "Vacation/Loyalty Leave"; }
                        else if (dtQuery.Rows[x]["empTOL"].ToString() == "sck") { tol = "Sick Leave"; }
                        else if (dtQuery.Rows[x]["empTOL"].ToString() == "mty") { tol = "Maternity Leave"; }
                        else if (dtQuery.Rows[x]["empTOL"].ToString() == "pty") { tol = "Paternity Leave"; }
                        else if (dtQuery.Rows[x]["empTOL"].ToString() == "emy") { tol = "Emergency"; }
                        else if (dtQuery.Rows[x]["empTOL"].ToString() == "udt") { tol = "Undertime"; }
                        else if (dtQuery.Rows[x]["empTOL"].ToString() == "chg") { tol = "Change Day Off/Shift"; }

                        if (dtQuery.Rows[x]["empstatus"].ToString() == "0") { Lstat = "Pending"; }
                        else if (dtQuery.Rows[x]["empstatus"].ToString() == "1") { Lstat = "Approved"; }
                        else if (dtQuery.Rows[x]["empstatus"].ToString() == "2") { Lstat = "Denied"; }
                        else if (dtQuery.Rows[x]["empstatus"].ToString() == "3") { Lstat = "Cancelled"; }
                        string sStatement = "<tr><td style='font-size: 1.4rem;'>" + dtQuery.Rows[x]["Name"].ToString() + "</td>" +
                                                        "<td style='font-size: 1.4rem;'>" + tol + "</td>" +
                                                        "<td style='font-size: 1.4rem;'>" + dtQuery.Rows[x]["FDate"].ToString() + "</td>" +
                                                        "<td style='font-size: 1.4rem;'>" + dtQuery.Rows[x]["TDate"].ToString() + "</td>" +
                                                        "<td style='font-size: 1.4rem;'>" + dtQuery.Rows[x]["empReason"].ToString() + "</td>" +
                                                        "<td style='font-size: 1.4rem;'>" + dtQuery.Rows[x]["days"].ToString() + "</td>" +
                                                        "<td style='font-size: 1.4rem;'>" + Lstat + "</td>" +
                                                        "<td style='font-size: 1.4rem;'>";
                        Panel1.Controls.Add(new LiteralControl(sStatement));
                        button.ID = dtQuery.Rows[x]["identity_column"].ToString();
                        button.Text = "View More";
                        button.CssClass = "btn pmd-btn-flat pmd-ripple-effect btn-primary";
                        button.Attributes.Add("style", "font-size: 1.4rem");
                        button.CommandArgument = dtQuery.Rows[x]["identity_column"].ToString();
                        button.OnClientClick = "window.open('LeaveDetails?param=" + dtQuery.Rows[x]["identity_column"].ToString() + "'); return false;";
                        //button.Attributes.Add("OnClick", "btnViewLeave_Click");
                        button.Click += new EventHandler(btnViewLeave_Click);
                        //button.Attributes.Add("runat", "server");
                        //button.PostBackUrl = "~/LeaveDetails?param=" + dtQuery.Rows[x]["identity_column"].ToString() + "";
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
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    public void btnViewLeave_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        string scrpt = "window.open('~/LeaveDetails?param=" + btn.CommandArgument + "', '_blank');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openWindow", scrpt, true);
    }
    private void getEmpAll()
    {
        try
        {
            string empQuery = "";
            empQuery = "Select (EmpFName + ' ' + EmpLName) as Name, convert(varchar, EmpDOH, 107) as HDate, ((Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = empPos and cspopupfor = 'POS') + ' ' + " +
                       "((Case (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = empRole and cspopupfor = 'ROL')  When 'None' Then '' Else (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = empRole and cspopupfor = 'ROL')  END)))as Position, " +
                       "b.leavCredVL as 'Vac_Leave', b.leavCredSL as 'Sck_Leave', b.leavCredLoyal as 'Lyl_Leave', b.leavCredSpecL as 'Spc_Leave', (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = empdept and cspopupfor='DPT') as emDeptd, " +
                       "(Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = emppos and cspopupfor='POS') as emPost, (Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = emprole and cspopupfor='ROL') as emRole, " +
                       "(Select top 1 cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = empempStat and cspopupfor = 'TOE') as empTOE, * from seihaHRMIS.dbo.HREmpInfo a LEFT JOIN seihaHRMIS.dbo.LeaveCreditInfo b ON a.empno = b.empno where empStatus = 1 order by a.empno ";
            dtQuery = HRMIS.Module.GetData(empQuery);
            if (dtQuery.Rows.Count > 0)
            {
                int count = checked(dtQuery.Rows.Count - 1);
                string sStatement1 = "<table class='table pmd-table table-hover' id='table'><thead style='height: 100px;'><tr><th style='color: #333; font-size: 1.5rem; position: sticky; top: -20px; background-color: #f5f5f5;'>No</th><th style='color: #333; font-size: 1.5rem; position: sticky; top: -20px; background-color: #f5f5f5;'>Name</th>" +
                                     "<th style='color: #333; font-size: 1.5rem; position: sticky; top: -20px; background-color: #f5f5f5;'>Date Hired</th><th style='color: #333; font-size: 1.5rem; position: sticky; top: -20px; background-color: #f5f5f5;'>Position</th>" +
                                     "<th style='color: #333; font-size: 1.5rem; position: sticky; top: -20px; background-color: #f5f5f5;'>Vacation Leave</th><th style='color: #333; font-size: 1.5rem; position: sticky; top: -20px; background-color: #f5f5f5;'>Sick Leave</th>" +
                                     "<th style='color: #333; font-size: 1.5rem; position: sticky; top: -20px; background-color: #f5f5f5;'>Loyalty Leave</th><th style='color: #333; font-size: 1.5rem; position: sticky; top: -20px; background-color: #f5f5f5;'>Special Leave</th>" +
                                     "</tr></thead><tbody>";
                Panel4.Controls.Add(new LiteralControl(sStatement1));
                for (int x = 0; x <= count; x = checked(x + 1))
                {
                    int y = x + 1;
                    string tol = "";
                    string Lstat = "";
                    Button button = new Button();
                    string sStatement = "<tr id='" + dtQuery.Rows[x]["empNo"].ToString() + "'style='cursor:pointer;' runat='server' onclick='displayCredit(" + y + ");'>" +
                        "<td style='font-size: 1.4rem;' runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["empNo"].ToString() + "</td>" +
                        "<td style='font-size: 1.4rem;' runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["Name"].ToString() + "</td>" +
                        "<td style='font-size: 1.4rem;' runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["HDate"].ToString() + "</td>" +
                        "<td style='font-size: 1.4rem;' hidden runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["emDeptd"].ToString() + "</td>" +
                        "<td style='font-size: 1.4rem;' runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["Position"].ToString() + "</td>" +
                        "<td style='font-size: 1.4rem;' hidden runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["emPost"].ToString() + "</td>" +
                        "<td style='font-size: 1.4rem;' hidden runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["emRole"].ToString() + "</td>" +
                        "<td style='font-size: 1.4rem;' hidden runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["empReportHead"].ToString() + "</td>" +
                        "<td style='font-size: 1.4rem;' hidden runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["empTOE"].ToString() + "</td>" +
                        "<td style='font-size: 1.4rem;' runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["Vac_Leave"].ToString() + "</td>" +
                        "<td style='font-size: 1.4rem;' runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["Sck_Leave"].ToString() + "</td>" +
                        "<td style='font-size: 1.4rem;' runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["Lyl_Leave"].ToString() + "</td>" +
                        "<td style='font-size: 1.4rem;' runat='server' onclick='displayCredit(" + y + ");'>" + dtQuery.Rows[x]["Spc_Leave"].ToString() + "</td>";
                    Panel4.Controls.Add(new LiteralControl(sStatement));

                }
                Panel4.Controls.Add(new LiteralControl("</tr></tbody></table>"));
                
            }
            else
            {
                string sStatement1 = "<<table class='table pmd-table table-hover'><thead><tr><th style='font-size: 1.5rem;'>Name</th>" +
                                     "<th style='font-size: 1.5rem;'>Date Hired</th><th style='font-size: 1.5rem;'>Position</th>" +
                                     "<th style='font-size: 1.5rem;'>Leave Credits</th><th style='font-size: 1.5rem;'>Remaining Credits</th></tr></thead><tbody>";
                Panel4.Controls.Add(new LiteralControl(sStatement1));
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
    private string getRole(string gRole)
    {
        string nameRole = "";
        string dtQry = "";
        dtQry = "Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = '" + gRole + "' and cspopupfor = 'ROL'";
        DataTable dt = HRMIS.Module.GetData(dtQry);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["cspopuptext"].ToString() == "None")
            {
                nameRole = "";
            }
            else
            {
                nameRole = dt.Rows[0]["cspopuptext"].ToString();
            }
        }
        return nameRole;
    }
    private string getStat(string gstat)
    {
        string getStatus = "";
        if (int.Parse(gstat) == 0)
        {
            getStatus = "Probationary";
        }
        else if (int.Parse(gstat) == 1)
        {
            getStatus = "Contractual";
        }
        else if (int.Parse(gstat) == 2)
        {
            getStatus = "Part-Time";
        }
        else if (int.Parse(gstat) == 3)
        {
            getStatus = "Regular";
        }
        return getStatus;
    }
   
}
