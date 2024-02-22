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
using System.Runtime.InteropServices;

public partial class LeaveApplication : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection();
    OleDbCommand sqlComm = new OleDbCommand();
    //OleDbDataReader sqlReader = new OleDbDataReader();
    OleDbDataAdapter sqlDA = new OleDbDataAdapter();
    private static DataTable dtEmpInfo;
    string dtEmpInfoQuery = "";
    string getEmpNo = "";
    private static string hldStringFile = "";
    private static string[] hldarr;
    private static string hldGUID = "";
    private static string fileNamePath = "";
    private static string DirecPath = "";
    private static string IDguid = "";
    private static string ipAdd = "";
    private static string uniquePageId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/404");
        //getEmpNo = Session["Uname"] as string;
        
        //getInfo();
        //var magicInput = document.getElementById('__EVENTTARGET');

        //if (magicInput && magicInput.value) {
        //   // the page is being posted back by an ASP control
        //}
        //if (!IsPostBack)
        //{
        //    hldGUID = Page.Session.SessionID.ToString();
        //    IDguid = getEmpNo + Guid.NewGuid().ToString();
        //    ipAdd = GetClientMAC(GetIPAddress());
        //    uniquePageId = Page.ClientID;
        //}
        //else
        //{
        //    if (hldGUID == "" || string.IsNullOrEmpty(getEmpNo))
        //    {
        //        Session.Abandon();
        //        Response.Redirect("~/Login");
        //    }
            
        //}
    }
    private void getInfo()
    {
        try
        {
            dtEmpInfo = null;
            dtEmpInfoQuery = "";
            dtEmpInfoQuery = "Select * from seihaHRMIS.dbo.HREmpInfo where EmpNo = '" + getEmpNo + "'";
            dtEmpInfo = GetData(dtEmpInfoQuery);
            if (dtEmpInfo.Rows.Count > 0)
            {
                string dNow = DateTime.Now.ToString("yyyy-MM-dd");
                DateFiled.Text = dNow;
               empno.Text= dtEmpInfo.Rows[0]["EmpNo"].ToString();
               fname.Text = dtEmpInfo.Rows[0]["EmpFName"].ToString();
               lname.Text = dtEmpInfo.Rows[0]["EmpLName"].ToString();
               RHNo.Text = dtEmpInfo.Rows[0]["EmpReportHead"].ToString();
               lblUser.Text = dtEmpInfo.Rows[0]["empFname"].ToString();
               if (dtEmpInfo.Rows[0]["empadmin"].ToString() != "1")
               {
                   lblReg.Visible = false;
               }
               else
               {
                   lblReg.Visible = true;
               }
               position.Text = getPosition(dtEmpInfo.Rows[0]["EmpPos"].ToString()) + " " + getRole(dtEmpInfo.Rows[0]["EmpRole"].ToString());
               
               if (dtEmpInfo.Rows[0]["EmpGen"].ToString()=="0")
                {
                    maternity.Enabled = false;
                    UserPic.Attributes.Add("src", "images/img_avatar.png");
                }
               else
               {
                   maternity.Enabled = true;
                   UserPic.Attributes.Add("src", "images/img_avatar2.png");
               }
               getLeaveCredits(dtEmpInfo.Rows[0]["EmpNo"].ToString());
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
    protected void getLeaveCredits(string strEmp)
    {
        try
        {
            string qryLeave = "Select * from seihaHRMIS.dbo.LeaveCreditInfo where empno = '" + strEmp + "'";
            DataTable dtLCredit = GetData(qryLeave);
            if(dtLCredit.Rows.Count > 0)
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
    protected void lblDash_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Dashboard");
        
    }
    protected void lblReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Registration");
    }
    protected void lblLeaveA_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LeaveApplication");
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
    protected void lblLeave_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Leave");
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
    protected void vacation_Checked(object sender, EventArgs e)
    {
        if (vacation.Checked==true)
        {
            sick.Checked = false;
            maternity.Checked = false;
            paternity.Checked = false;
            emergency.Checked = false;
            undertime.Checked = false;
            changeOff.Checked = false;
            timeTo.Enabled = false;
            timeFrom.Enabled = false;
        }
       
    }
    protected void sick_Checked(object sender, EventArgs e)
    {
        if (sick.Checked == true)
        {
            vacation.Checked = false;
            maternity.Checked = false;
            paternity.Checked = false;
            emergency.Checked = false;
            undertime.Checked = false;
            changeOff.Checked = false;
            timeTo.Enabled = false;
            timeFrom.Enabled = false;
        }
        
    }
    protected void maternity_Checked(object sender, EventArgs e)
    {
        if(maternity.Checked == true)
        {
            vacation.Checked = false;
            sick.Checked = false;
            paternity.Checked = false;
            emergency.Checked = false;
            undertime.Checked = false;
            changeOff.Checked = false;
            timeTo.Enabled = false;
            timeFrom.Enabled = false;
        }
        
    }
    protected void paternity_Checked(object sender, EventArgs e)
    {
        if(paternity.Checked == true)
        {
            vacation.Checked = false;
            sick.Checked = false;
            maternity.Checked = false;
            emergency.Checked = false;
            undertime.Checked = false;
            changeOff.Checked = false;
            timeTo.Enabled = false;
            timeFrom.Enabled = false;
        }
        
    }
    protected void emergency_Checked(object sender, EventArgs e)
    {
        if(emergency.Checked == true)
        {
            vacation.Checked = false;
            sick.Checked = false;
            maternity.Checked = false;
            paternity.Checked = false;
            undertime.Checked = false;
            changeOff.Checked = false;
            timeTo.Enabled = true;
            timeFrom.Enabled = true;
        }
        
    }
    protected void undertime_Checked(object sender, EventArgs e)
    {
        if (undertime.Checked == true)
        {
            vacation.Checked = false;
            sick.Checked = false;
            maternity.Checked = false;
            paternity.Checked = false;
            emergency.Checked = false;
            changeOff.Checked = false;
            timeTo.Enabled = true;
            timeFrom.Enabled = true;
        }
        
    }
    protected void changeOff_Checked(object sender, EventArgs e)
    {
        if (changeOff.Checked == true)
        {
            vacation.Checked = false;
            sick.Checked = false;
            maternity.Checked = false;
            paternity.Checked = false;
            emergency.Checked = false;
            undertime.Checked = false;
            timeTo.Enabled = true;
            timeFrom.Enabled = true;
        }
        
    }
    private string getDpart(string eDpart)
    {
        string nameDepart = "";
        string dtQry = "";
        dtQry = "Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = '" + eDpart + "' and cspopupfor = 'DPT'";
        DataTable dt = GetData(dtQry);
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
        DataTable dt = GetData(dtQry);
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
        DataTable dt = GetData(dtQry);
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
    protected void UploadButton_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName =  Path.GetFileName(FileUploadControl.PostedFile.FileName.ToString());
            DirecPath = "//10.110.60.240/Database/HRMIS Docs File/" + IDguid;
            if (fileName != "")
            {
                if (!Directory.Exists(DirecPath))
                {
                    Directory.CreateDirectory(DirecPath);
                    FileUploadControl.PostedFile.SaveAs(DirecPath + "/" + fileName);
                }
                else
                {
                    FileUploadControl.PostedFile.SaveAs(DirecPath + "/" + fileName);
                }
                

                loadGrid();
            }
            //Response.Redirect(Request.Url.AbsoluteUri);
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
        //Response.Write(@"<script> alert('Please select Time!') </script>");
    }
 
    protected void loadGrid()
    {
        string[] filePaths = Directory.GetFiles(DirecPath);
        //string[] filePaths = new String[]{hldStringFile};
        List<ListItem> files = new List<ListItem>();
        foreach (string filePath in filePaths)
        {
            files.Add(new ListItem(Path.GetFileName(filePath), filePath));
        }
        grdvFiles.DataSource = files;
        grdvFiles.DataBind();
        for (int x = 0; x < grdvFiles.Rows.Count; x++)
        {
            grdvFiles.Rows[x].Cells[0].Width = 250;
            grdvFiles.Rows[x].Cells[1].Width = 100;
        }
    }
    protected void DeleteFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        File.Delete(filePath);
        loadGrid();
       //Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
            string holdEmp = empno.Text;
            if (holdEmp != "")
            {
                if (vacation.Checked == true || sick.Checked == true || maternity.Checked == true || paternity.Checked == true || emergency.Checked == true || undertime.Checked == true || changeOff.Checked == true)
                {
                    string TOL = "";
                    if (vacation.Checked == true) { TOL = "vct"; }
                    else if (sick.Checked == true) { TOL = "sck"; }
                    else if (maternity.Checked == true) { TOL = "mty"; }
                    else if (paternity.Checked == true) { TOL = "pty"; }
                    else if (emergency.Checked == true) { TOL = "emy"; }
                    else if (undertime.Checked == true) { TOL = "udt"; }
                    else if (changeOff.Checked == true) { TOL = "chg"; }
                    if(dateFrom.Text != "" && dateTo.Text != "")
                    {
                        DateTime DFrom = Convert.ToDateTime(dateFrom.Text);
                        DateTime DTo = Convert.ToDateTime(dateTo.Text);
                        TimeSpan objDaySpan = DTo - DFrom;
                        int CountDays = int.Parse((objDaySpan.TotalDays.ToString()));
                        if (CountDays > -1 ) /// Check if total days date filed is not negative
                        {
                            if (undertime.Checked == true || emergency.Checked == true || changeOff.Checked == true) /// Check if check boxes are checked
                            {
                                if (timeFrom.Text != "" && timeTo.Text != "") /// Check if Time inputs are not Empty
                                {
                                    DateTime TFrom = Convert.ToDateTime(timeFrom.Text);
                                    DateTime TTo = Convert.ToDateTime(timeTo.Text);
                                    TimeSpan objTimeSpan = TTo - TFrom;
                                    double CountTime = (double)objTimeSpan.TotalHours;
                                    if(CountTime > -1 || CountTime == 0)
                                    {
                                        string confirmValue = Request.Form["confirm_value"];
                                        if (confirmValue == "Yes")
                                        {
                                            int lookDup = 0;
                                            string findLeaveInfo= "";
                                            DateTime dtFrm = DateTime.Parse(dateFrom.Text);
                                            DateTime dtTo = DateTime.Parse(dateTo.Text);
                                            DateTime dtNow = DateTime.Now;
                                            int calcMonth = (dtFrm.Year - dtNow.Year) * 12 + dtFrm.Month - dtNow.Month;
                                            if (calcMonth == 1) /// Check if date filed if next two month
                                            {
                                                if (dtNow.Day > 14) /// Check if is not past the day 14 of the month
                                                {
                                                    if (TOL != "vct")  /// Check if is not Vacation Leave
                                                    {
                                                        findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + empno.Text.Trim() + "' " +
                                                            "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                            "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')";

                                                        lookDup = int.Parse(GetCount(findLeaveInfo));
                                                        if (lookDup == 1)
                                                        {

                                                            string chkStat = "";
                                                            chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + empno.Text.Trim() + "' " +
                                                                        "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                        "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')";
                                                            DataTable dts = GetData(chkStat);
                                                            if (dts.Rows.Count > 0)
                                                            {
                                                                if (dts.Rows[0]["empStatus"].ToString() == "2")
                                                                {
                                                                    string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                    "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                    gblInsert(SQuery);

                                                                    string dtQry = "";
                                                                    string fIdent = "";
                                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                    DataTable dt = GetData(dtQry);
                                                                    if (dt.Rows.Count > 0)
                                                                    {
                                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                           "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                        gblInsert(sQueryNot);
                                                                        if (DirecPath != "")
                                                                        {
                                                                            string[] filePaths = Directory.GetFiles(DirecPath);
                                                                            foreach (string filePath in filePaths)
                                                                            {
                                                                                if (filePath != "")
                                                                                {
                                                                                    string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                                   "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                                    gblInsert(sQueryDetail);
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                      "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                    gblInsert(LogQuery);
                                                                    Response.Redirect(Request.RawUrl);
                                                                }
                                                                else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                                {
                                                                    string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                    "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                    gblInsert(SQuery);

                                                                    string dtQry = "";
                                                                    string fIdent = "";
                                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                    DataTable dt = GetData(dtQry);
                                                                    if (dt.Rows.Count > 0)
                                                                    {
                                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                           "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                        gblInsert(sQueryNot);
                                                                        if (DirecPath != "")
                                                                        {
                                                                            string[] filePaths = Directory.GetFiles(DirecPath);
                                                                            foreach (string filePath in filePaths)
                                                                            {
                                                                                if (filePath != "")
                                                                                {
                                                                                    string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                                   "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                                    gblInsert(sQueryDetail);
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                      "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                    gblInsert(LogQuery);
                                                                    Response.Redirect(Request.RawUrl);
                                                                }
                                                                else
                                                                {
                                                                    Response.Write(@"<script> alert('You Already Filed a Leave on this Dates!') </script>");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Response.Write(@"<script> alert('You Already Filed a Leave on this Dates!') </script>");
                                                            }

                                                        }
                                                        else
                                                        {
                                                            string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                    "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                            gblInsert(SQuery);

                                                            string dtQry = "";
                                                            string fIdent = "";
                                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                            DataTable dt = GetData(dtQry);
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                   "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                gblInsert(sQueryNot);
                                                                if (DirecPath != "")
                                                                {
                                                                    string[] filePaths = Directory.GetFiles(DirecPath);
                                                                    foreach (string filePath in filePaths)
                                                                    {
                                                                        if (filePath != "")
                                                                        {
                                                                            string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                           "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                            gblInsert(sQueryDetail);
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                              "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                            gblInsert(LogQuery);
                                                            Response.Redirect(Request.RawUrl);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Response.Write(@"<script> alert('You cannot file leave for next month due to monthly cut off.') </script>");
                                                    }
                                                    /// END Check if is not Vacation Leave
                                                }
                                                else
                                                {
                                                    findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + empno.Text.Trim() + "' " +
                                                            "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                            "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')";

                                                    lookDup = int.Parse(GetCount(findLeaveInfo));
                                                    if (lookDup == 1)
                                                    {

                                                        string chkStat = "";
                                                        chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + empno.Text.Trim() + "' " +
                                                                    "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                    "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')";
                                                        DataTable dts = GetData(chkStat);
                                                        if (dts.Rows.Count > 0)
                                                        {
                                                            if (dts.Rows[0]["empStatus"].ToString() == "2")
                                                            {
                                                                string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                gblInsert(SQuery);

                                                                string dtQry = "";
                                                                string fIdent = "";
                                                                dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                DataTable dt = GetData(dtQry);
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                    string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                       "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                    gblInsert(sQueryNot);
                                                                    if (DirecPath != "")
                                                                    {
                                                                        string[] filePaths = Directory.GetFiles(DirecPath);
                                                                        foreach (string filePath in filePaths)
                                                                        {
                                                                            if (filePath != "")
                                                                            {
                                                                                string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                               "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                                gblInsert(sQueryDetail);
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                  "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                gblInsert(LogQuery);
                                                                Response.Redirect(Request.RawUrl);
                                                            }
                                                            else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                            {
                                                                string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                gblInsert(SQuery);

                                                                string dtQry = "";
                                                                string fIdent = "";
                                                                dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                DataTable dt = GetData(dtQry);
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                    string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                       "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                    gblInsert(sQueryNot);
                                                                    if (DirecPath != "")
                                                                    {
                                                                        string[] filePaths = Directory.GetFiles(DirecPath);
                                                                        foreach (string filePath in filePaths)
                                                                        {
                                                                            if (filePath != "")
                                                                            {
                                                                                string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                               "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                                gblInsert(sQueryDetail);
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                  "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                gblInsert(LogQuery);
                                                                Response.Redirect(Request.RawUrl);
                                                            }
                                                            else
                                                            {
                                                                Response.Write(@"<script> alert('You Already Filed a Leave on this Dates!') </script>");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Response.Write(@"<script> alert('You Already Filed a Leave on this Dates!') </script>");
                                                        }

                                                    }
                                                    else
                                                    {
                                                        string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                        gblInsert(SQuery);

                                                        string dtQry = "";
                                                        string fIdent = "";
                                                        dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                        DataTable dt = GetData(dtQry);
                                                        if (dt.Rows.Count > 0)
                                                        {
                                                            fIdent = dt.Rows[0]["identity_column"].ToString();
                                                            string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                               "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                            gblInsert(sQueryNot);
                                                            if (DirecPath != "")
                                                            {
                                                                string[] filePaths = Directory.GetFiles(DirecPath);
                                                                foreach (string filePath in filePaths)
                                                                {
                                                                    if (filePath != "")
                                                                    {
                                                                        string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                       "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                        gblInsert(sQueryDetail);
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                          "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                        gblInsert(LogQuery);
                                                        Response.Redirect(Request.RawUrl);
                                                    }
                                                } /// END Check if is not past the day 14 of the month
                                            }
                                            else
                                            {
                                                findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + empno.Text.Trim() + "' " +
                                                            "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                            "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')";

                                                lookDup = int.Parse(GetCount(findLeaveInfo));
                                                if (lookDup == 1)
                                                {

                                                    string chkStat = "";
                                                    chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + empno.Text.Trim() + "' " +
                                                                "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')";
                                                    DataTable dts = GetData(chkStat);
                                                    if (dts.Rows.Count > 0)
                                                    {
                                                        if (dts.Rows[0]["empStatus"].ToString() == "2")
                                                        {
                                                            string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                            "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                            "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                            gblInsert(SQuery);

                                                            string dtQry = "";
                                                            string fIdent = "";
                                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                            DataTable dt = GetData(dtQry);
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                   "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                gblInsert(sQueryNot);
                                                                if (DirecPath != "")
                                                                {
                                                                    string[] filePaths = Directory.GetFiles(DirecPath);
                                                                    foreach (string filePath in filePaths)
                                                                    {
                                                                        if (filePath != "")
                                                                        {
                                                                            string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                           "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                            gblInsert(sQueryDetail);
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                              "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                            gblInsert(LogQuery);
                                                            Response.Redirect(Request.RawUrl);
                                                        }
                                                        else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                        {
                                                            string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                            "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                            "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                            gblInsert(SQuery);

                                                            string dtQry = "";
                                                            string fIdent = "";
                                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                            DataTable dt = GetData(dtQry);
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                   "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                gblInsert(sQueryNot);
                                                                if (DirecPath != "")
                                                                {
                                                                    string[] filePaths = Directory.GetFiles(DirecPath);
                                                                    foreach (string filePath in filePaths)
                                                                    {
                                                                        if (filePath != "")
                                                                        {
                                                                            string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                           "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                            gblInsert(sQueryDetail);
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                              "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                            gblInsert(LogQuery);
                                                            Response.Redirect(Request.RawUrl);
                                                        }
                                                        else
                                                        {
                                                            Response.Write(@"<script> alert('You Already Filed a Leave on this Dates!') </script>");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Response.Write(@"<script> alert('You Already Filed a Leave on this Dates!') </script>");
                                                    }

                                                }
                                                else
                                                {
                                                    string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                            "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                            "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                    gblInsert(SQuery);

                                                    string dtQry = "";
                                                    string fIdent = "";
                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                    DataTable dt = GetData(dtQry);
                                                    if (dt.Rows.Count > 0)
                                                    {
                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                           "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                        gblInsert(sQueryNot);
                                                        if (DirecPath != "")
                                                        {
                                                            string[] filePaths = Directory.GetFiles(DirecPath);
                                                            foreach (string filePath in filePaths)
                                                            {
                                                                if (filePath != "")
                                                                {
                                                                    string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                   "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                    gblInsert(sQueryDetail);
                                                                }
                                                            }
                                                        }
                                                    }

                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                      "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                    gblInsert(LogQuery);
                                                    Response.Redirect(Request.RawUrl);
                                                }
                                            }/// END Check if date filed if next two month
                                            
                                        }
                                        else
                                        {
                                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "", true);
                                        }
                                        
                                    }
                                    else
                                    {
                                        Response.Write(@"<script> alert('Invalid Selected Time!') </script>");
                                    }

                                }
                                else
                                {
                                    Response.Write(@"<script> alert('Please select Time!') </script>");
                                }
                            }
                            else
                            {
                                string confirmValue = Request.Form["confirm_value"];
                                if (confirmValue == "Yes")
                                {
                                    int lookDup = 0;
                                    string findLeaveInfo = "";
                                    DateTime dtFrm = DateTime.Parse(dateFrom.Text);
                                    DateTime dtTo = DateTime.Parse(dateTo.Text);
                                    DateTime dtNow = DateTime.Now;
                                    int calcMonth = (dtFrm.Year - dtNow.Year) * 12 + dtFrm.Month - dtNow.Month;
                                    if (calcMonth == 1) /// Check if date filed if next two month
                                    {
                                        if (dtNow.Day > 14) /// Check if is not past the day 14 of the month
                                        {
                                            if (TOL != "vct") /// Check if is not Vacation Leave
                                            {
                                                findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + empno.Text.Trim() + "' " +
                                                    "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                    "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')";

                                                lookDup = int.Parse(GetCount(findLeaveInfo));
                                                if (lookDup >= 1)
                                                {

                                                    string chkStat = "";
                                                    chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + empno.Text.Trim() + "' " +
                                                                "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')";
                                                    DataTable dts = GetData(chkStat);
                                                    if (dts.Rows.Count > 0)
                                                    {
                                                        if (dts.Rows[0]["empStatus"].ToString() == "2")
                                                        {

                                                            string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                            "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                            "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                            gblInsert(SQuery);

                                                            string dtQry = "";
                                                            string fIdent = "";
                                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                            DataTable dt = GetData(dtQry);
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                   "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                gblInsert(sQueryNot);
                                                                if (DirecPath != "")
                                                                {
                                                                    string[] filePaths = Directory.GetFiles(DirecPath);
                                                                    foreach (string filePath in filePaths)
                                                                    {
                                                                        if (filePath != "")
                                                                        {
                                                                            string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                       "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                            gblInsert(sQueryDetail);
                                                                        }
                                                                    }
                                                                }

                                                            }


                                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                              "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                            gblInsert(LogQuery);
                                                            Response.Redirect(Request.RawUrl);
                                                        }
                                                        else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                        {
                                                            string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                            "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                            "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                            gblInsert(SQuery);

                                                            string dtQry = "";
                                                            string fIdent = "";
                                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                            DataTable dt = GetData(dtQry);
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                   "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                gblInsert(sQueryNot);
                                                                if (DirecPath != "")
                                                                {
                                                                    string[] filePaths = Directory.GetFiles(DirecPath);
                                                                    foreach (string filePath in filePaths)
                                                                    {
                                                                        if (filePath != "")
                                                                        {
                                                                            string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                           "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                            gblInsert(sQueryDetail);
                                                                        }
                                                                    }
                                                                }
                                                            }

                                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                              "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                            gblInsert(LogQuery);
                                                            Response.Redirect(Request.RawUrl);
                                                        }
                                                        else
                                                        {
                                                            Response.Write(@"<script> alert('You Already Filed a Leave on this Dates!') </script>");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Response.Write(@"<script> alert('You Already Filed a Leave on this Dates!') </script>");
                                                    }
                                                }
                                                else
                                                {
                                                    string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                            "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                            "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                    gblInsert(SQuery);

                                                    string dtQry = "";
                                                    string fIdent = "";
                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                    DataTable dt = GetData(dtQry);
                                                    if (dt.Rows.Count > 0)
                                                    {

                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                           "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                        gblInsert(sQueryNot);
                                                        if (DirecPath != "")
                                                        {
                                                            string[] filePaths = Directory.GetFiles(DirecPath);
                                                            foreach (string filePath in filePaths)
                                                            {
                                                                if (filePath != "")
                                                                {
                                                                    string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                               "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                    gblInsert(sQueryDetail);
                                                                }
                                                            }
                                                        }

                                                    }


                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                      "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                    gblInsert(LogQuery);
                                                    Response.Redirect(Request.RawUrl);
                                                }
                                            }
                                            else
                                            {
                                                Response.Write(@"<script> alert('You cannot file leave for next month due to monthly cut off.') </script>");
                                            }
                                        }
                                        else
                                        {
                                            findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + empno.Text.Trim() + "' " +
                                                    "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                    "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')";

                                            lookDup = int.Parse(GetCount(findLeaveInfo));
                                            if (lookDup >= 1)
                                            {

                                                string chkStat = "";
                                                chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + empno.Text.Trim() + "' " +
                                                            "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                            "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')";
                                                DataTable dts = GetData(chkStat);
                                                if (dts.Rows.Count > 0)
                                                {
                                                    if (dts.Rows[0]["empStatus"].ToString() == "2")
                                                    {

                                                        string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                        "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                        "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                        gblInsert(SQuery);

                                                        string dtQry = "";
                                                        string fIdent = "";
                                                        dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                        DataTable dt = GetData(dtQry);
                                                        if (dt.Rows.Count > 0)
                                                        {
                                                            fIdent = dt.Rows[0]["identity_column"].ToString();
                                                            string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                               "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                            gblInsert(sQueryNot);
                                                            if (DirecPath != "")
                                                            {
                                                                string[] filePaths = Directory.GetFiles(DirecPath);
                                                                foreach (string filePath in filePaths)
                                                                {
                                                                    if (filePath != "")
                                                                    {
                                                                        string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                   "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                        gblInsert(sQueryDetail);
                                                                    }
                                                                }
                                                            }

                                                        }


                                                        string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                          "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                        gblInsert(LogQuery);
                                                        Response.Redirect(Request.RawUrl);
                                                    }
                                                    else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                    {
                                                        string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                        "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                        "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                        gblInsert(SQuery);

                                                        string dtQry = "";
                                                        string fIdent = "";
                                                        dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                        DataTable dt = GetData(dtQry);
                                                        if (dt.Rows.Count > 0)
                                                        {
                                                            fIdent = dt.Rows[0]["identity_column"].ToString();
                                                            string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                               "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                            gblInsert(sQueryNot);
                                                            if (DirecPath != "")
                                                            {
                                                                string[] filePaths = Directory.GetFiles(DirecPath);
                                                                foreach (string filePath in filePaths)
                                                                {
                                                                    if (filePath != "")
                                                                    {
                                                                        string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                       "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                        gblInsert(sQueryDetail);
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                          "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                        gblInsert(LogQuery);
                                                        Response.Redirect(Request.RawUrl);
                                                    }
                                                    else
                                                    {
                                                        Response.Write(@"<script> alert('You Already Filed a Leave on this Dates!') </script>");
                                                    }
                                                }
                                                else
                                                {
                                                    Response.Write(@"<script> alert('You Already Filed a Leave on this Dates!') </script>");
                                                }
                                            }
                                            else
                                            {
                                                string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                        "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                        "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                gblInsert(SQuery);

                                                string dtQry = "";
                                                string fIdent = "";
                                                dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                DataTable dt = GetData(dtQry);
                                                if (dt.Rows.Count > 0)
                                                {

                                                    fIdent = dt.Rows[0]["identity_column"].ToString();
                                                    string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                       "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                    gblInsert(sQueryNot);
                                                    if (DirecPath != "")
                                                    {
                                                        string[] filePaths = Directory.GetFiles(DirecPath);
                                                        foreach (string filePath in filePaths)
                                                        {
                                                            if (filePath != "")
                                                            {
                                                                string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                           "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                gblInsert(sQueryDetail);
                                                            }
                                                        }
                                                    }

                                                }


                                                string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                  "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                gblInsert(LogQuery);
                                                Response.Redirect(Request.RawUrl);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + empno.Text.Trim() + "' " +
                                                     "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                     "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')";

                                        lookDup = int.Parse(GetCount(findLeaveInfo));
                                        if (lookDup >= 1)
                                        {

                                            string chkStat = "";
                                            chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + empno.Text.Trim() + "' " +
                                                        "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                        "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')";
                                            DataTable dts = GetData(chkStat);
                                            if (dts.Rows.Count > 0)
                                            {
                                                if (dts.Rows[0]["empStatus"].ToString() == "2")
                                                {

                                                    string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                    "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                    gblInsert(SQuery);

                                                    string dtQry = "";
                                                    string fIdent = "";
                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                    DataTable dt = GetData(dtQry);
                                                    if (dt.Rows.Count > 0)
                                                    {
                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                           "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                        gblInsert(sQueryNot);
                                                        if (DirecPath != "")
                                                        {
                                                            string[] filePaths = Directory.GetFiles(DirecPath);
                                                            foreach (string filePath in filePaths)
                                                            {
                                                                if (filePath != "")
                                                                {
                                                                    string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                               "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                    gblInsert(sQueryDetail);
                                                                }
                                                            }
                                                        }

                                                    }


                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                      "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                    gblInsert(LogQuery);
                                                    Response.Redirect(Request.RawUrl);
                                                }
                                                else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                {
                                                    string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                    "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                    gblInsert(SQuery);

                                                    string dtQry = "";
                                                    string fIdent = "";
                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                    DataTable dt = GetData(dtQry);
                                                    if (dt.Rows.Count > 0)
                                                    {
                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                           "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                        gblInsert(sQueryNot);
                                                        if (DirecPath != "")
                                                        {
                                                            string[] filePaths = Directory.GetFiles(DirecPath);
                                                            foreach (string filePath in filePaths)
                                                            {
                                                                if (filePath != "")
                                                                {
                                                                    string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                   "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                    gblInsert(sQueryDetail);
                                                                }
                                                            }
                                                        }
                                                    }

                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                      "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                    gblInsert(LogQuery);
                                                    Response.Redirect(Request.RawUrl);
                                                }
                                                else
                                                {
                                                    Response.Write(@"<script> alert('You Already Filed a Leave on this Dates!') </script>");
                                                }
                                            }
                                            else
                                            {
                                                Response.Write(@"<script> alert('You Already Filed a Leave on this Dates!') </script>");
                                            }
                                        }
                                        else
                                        {
                                            string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                    "values('" + empno.Text + "', '" + DateFiled.Text + "', '" + address.Text + "', '" + TOL + "', '" + reason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                            gblInsert(SQuery);

                                            string dtQry = "";
                                            string fIdent = "";
                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + empno.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                            DataTable dt = GetData(dtQry);
                                            if (dt.Rows.Count > 0)
                                            {

                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                   "values('" + empno.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                gblInsert(sQueryNot);
                                                if (DirecPath != "")
                                                {
                                                    string[] filePaths = Directory.GetFiles(DirecPath);
                                                    foreach (string filePath in filePaths)
                                                    {
                                                        if (filePath != "")
                                                        {
                                                            string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                       "values('" + empno.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                            gblInsert(sQueryDetail);
                                                        }
                                                    }
                                                }

                                            }


                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                              "values('L','" + DateTime.Now + "', '" + empno.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                            gblInsert(LogQuery);
                                            Response.Redirect(Request.RawUrl);
                                        }     
                                    }
                                    
                                }
                                else
                                {
                                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "", true);
                                }
                                
                            }
                        }
                        else
                        {
                            Response.Write(@"<script> alert('Invalid Dates Selected!') </script>");
                        }
                    }
                    else
                    {
                        Response.Write(@"<script> alert('Please Select Dates!') </script>");
                    }
                }
                else
                {
                    Response.Write(@"<script> alert('Please Select Type of Absence/Leave!') </script>");
                }
            }
            else
            {
                Response.Write(@"<script> alert('No Employee Number to be save!') </script>");
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void gblInsert(string strSql)
    {
        conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        string sql = strSql;
        conn.Open();
        try
        {
            if (sql != "")
            {
                using (sqlComm = new OleDbCommand(sql, conn))
                {

                    sqlComm.CommandText = strSql;
                    sqlComm.Connection = conn;
                    sqlComm.ExecuteNonQuery();
                    sqlComm.CommandTimeout = 0;
                    sqlComm = null;
                }
            }
            conn.Close();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }
        catch (System.Net.WebException ex)
        {
            Response.Write(@"<script> alert('" + ex.Message + "') </script>");
        }
    }
    private DataTable GetData(string strQuery)
    {
        DataTable dt = new DataTable();
        conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
        string sql = strQuery;
        conn.Open();
        if (sql != "")
        {
            using (sqlComm = new OleDbCommand(sql, conn))
            {
                sqlComm.Connection = conn;
                using (sqlDA = new OleDbDataAdapter(sqlComm))
                {
                    sqlDA.Fill(dt);
                }
            }
        }

        conn.Close();
        return dt;
    }
    private string GetCount(string strQuery)
    {
        string sql = strQuery;
        string retAns = "";
        if (sql != "")
        {
            conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["connect"].ToString());
            conn.Open();
            sqlComm = new OleDbCommand(sql, conn);
            sqlComm.CommandTimeout = 0;
            retAns = sqlComm.ExecuteScalar().ToString();
            conn.Close();
        }
        return retAns;

    }
    private static string GetClientMAC(string strClientIP)
    {
        string mac_dest = "";
        try
        {
            Int32 ldest = inet_addr(strClientIP);
            Int32 lhost = inet_addr("");
            Int64 macinfo = new Int64();
            Int32 len = 6;
            int res = SendARP(ldest, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");

            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }

            for (int i = 0; i < 11; i++)
            {
                if (0 == (i % 2))
                {
                    if (i == 10)
                    {
                        mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                    else
                    {
                        mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                }
            }
        }
        catch (Exception err)
        {
            throw new Exception("L?i " + err.Message);
        }
        return mac_dest;
    }
    public string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }
    [DllImport("Iphlpapi.dll")]
    private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
    [DllImport("Ws2_32.dll")]
    private static extern Int32 inet_addr(string ip);
}