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
using System.Web.Services;

namespace HRMIS
{
    
    public partial class LeaveApp : System.Web.UI.Page
    {
        private static DataTable dtEmpInfo;
        string dtEmpInfoQuery = "";
        string getEmpNo = "";
        private static string hldStringFile = "";
        private static string[] hldarr;
        private static string hldGUID = "";
        private static string fileNamePath = "";
        private static string DirecPath = "";
        private static string IDguid = "";
        private static string guidHld = "";
        private static string ipAdd = "";
        private static string uniquePageId = "";
        private static string hldDept = "";
        private static string hldEmp = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            getEmpNo = Session["Uname"] as string;
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(getEmpNo))
                {
                    Session.Abandon();
                    Response.Redirect("~/Login");
                }
                else
                {
                    hldGUID = Page.Session.SessionID.ToString();
                    guidHld = Request.QueryString["gid"] as string;
                    hldEmp = Request.QueryString["ep"] as string;
                    //guidHld = Guid.NewGuid().ToString();
                    ipAdd = GetClientMAC(GetIPAddress());
                    IDguid = hldEmp + "-" + guidHld;
                    getInfo();
                }
            }
            else
            {
                string currentPage = Request.Url.AbsolutePath;
                if (currentPage.EndsWith("Dash.aspx"))
                {
                }
                else if (currentPage.EndsWith("LeaveApp.aspx"))
                {
                }
            }
        }
        [WebMethod]
        public static void DeleteFolder()
        {
            if (!string.IsNullOrEmpty(DirecPath) && Directory.Exists(DirecPath))
            {
                Directory.Delete(DirecPath, true);
            }
            
        }
        protected void lblbtnDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dash");
        }
        protected void vacation_Checked(object sender, EventArgs e)
        {
            if (vacation.Checked == true)
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
            if (maternity.Checked == true)
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
            if (paternity.Checked == true)
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
            if (emergency.Checked == true)
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
                    string dNow = DateTime.Now.ToString("MM/dd/yyyy");
                    txtDateNow.Text = dNow;
                    txtEmpNo.Text = dtEmpInfo.Rows[0]["EmpNo"].ToString();
                    txtFname.Text = dtEmpInfo.Rows[0]["EmpFName"].ToString();
                    txtLname.Text = dtEmpInfo.Rows[0]["EmpLName"].ToString();
                    //RHNo.Text = dtEmpInfo.Rows[0]["EmpReportHead"].ToString();
                    //lblUser.Text = dtEmpInfo.Rows[0]["empFname"].ToString();
                    txtEmpPos.Text = getPosition(dtEmpInfo.Rows[0]["EmpPos"].ToString()) + " " + getRole(dtEmpInfo.Rows[0]["EmpRole"].ToString());
                    hldDept = dtEmpInfo.Rows[0]["EmpDept"].ToString();
                    if (dtEmpInfo.Rows[0]["EmpGen"].ToString() == "0")
                    {
                        maternity.Enabled = false;
                    }
                    else
                    {
                        maternity.Enabled = true;
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
        protected void grdvFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the wrapping div
                var div = e.Row.Parent as System.Web.UI.HtmlControls.HtmlGenericControl;
                if (div != null)
                {
                    // Remove the div
                    e.Row.Parent.Controls.Remove(div);
                }
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
                    lblCountVL.Text = dtLCredit.Rows[0]["leavCredVL"].ToString();
                    lblCountSL.Text = dtLCredit.Rows[0]["leavCredSL"].ToString();
                    lblCountLL.Text = dtLCredit.Rows[0]["leavCredLoyal"].ToString();
                    lblCountSPL.Text = dtLCredit.Rows[0]["leavCredSpecL"].ToString();
                }

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
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
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = Path.GetFileName(FileUploadControl.PostedFile.FileName.ToString());
                DirecPath = "//10.110.60.240/Database/LeaveDocFiles/" + IDguid;
                if (fileName != "")
                {
                    if (!Directory.Exists(DirecPath))
                    {
                        Directory.CreateDirectory(DirecPath);
                        FileUploadControl.PostedFile.SaveAs(DirecPath + "/" + fileName);
                    }
                    else
                    {
                        if(grdvFiles.Rows.Count == 2)
                        {
                            
                        }
                        else
                        {
                            FileUploadControl.PostedFile.SaveAs(DirecPath + "/" + fileName);
                        }
                    }
                    string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID, guid) " +
                                          "values('" + txtEmpNo.Text + "', '" + fileName + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '', '" + guidHld + "')";

                    HRMIS.Module.gblInsert(sQueryDetail);

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
            List<ListItem> files = new List<ListItem>();
            
            foreach (string filePath in filePaths)
            {
                files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            }
            grdvFiles.DataSource = files;
            grdvFiles.DataBind();
            for (int x = 0; x < grdvFiles.Rows.Count; x++)
            {
                //grdvFiles.Rows[x].Cells[0].Width = 250;
                grdvFiles.Rows[x].Cells[1].Width = 50;
                grdvFiles.Rows[x].Cells[0].Attributes.Add("style", "font-weight: 700;");
                grdvFiles.Rows[x].Cells[1].Attributes.Add("style", "text-align-last: center;");
            }
        }
        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            loadGrid();
            //Response.Redirect(Request.Url.AbsoluteUri);
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                string holdEmp = txtEmpNo.Text;
                DateTime now = DateTime.Now;
                string holdTimeNow = now.ToString("HH:mm:ss");
                if (holdEmp == hldEmp)
                {
                    if (guidHld != "")
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
                            if (dateFrom.Text != "" && dateTo.Text != "")
                            {
                                DateTime DFrom = Convert.ToDateTime(dateFrom.Text);
                                DateTime DTo = Convert.ToDateTime(dateTo.Text);
                                TimeSpan objDaySpan = DTo - DFrom;
                                int CountDays = int.Parse((objDaySpan.TotalDays.ToString()));
                                if (CountDays > -1) /// Check if total days date filed is not negative
                                {
                                    if (undertime.Checked == true || emergency.Checked == true || changeOff.Checked == true) /// Check if check boxes are checked
                                    {
                                        if (timeFrom.Text != "" && timeTo.Text != "") /// Check if Time inputs are not Empty
                                        {
                                            DateTime TFrom = Convert.ToDateTime(timeFrom.Text);
                                            DateTime TTo = Convert.ToDateTime(timeTo.Text);
                                            TimeSpan objTimeSpan = TTo - TFrom;
                                            double CountTime = (double)objTimeSpan.TotalHours;
                                            if (CountTime > -1 || CountTime == 0)
                                            {
                                                int lookDup = 0;
                                                string findLeaveInfo = "";
                                                DateTime dtFrm = DateTime.Parse(dateFrom.Text);
                                                DateTime dtTo = DateTime.Parse(dateTo.Text);
                                                DateTime dtNow = DateTime.Now;
                                                int calcMonth = (dtFrm.Year - dtNow.Year) * 12 + dtFrm.Month - dtNow.Month;
                                                if (calcMonth == 1) /// Check if date filed if next two month
                                                {
                                                    if (dtNow.Day > 5) /// Check if is not past the day 5 of the month
                                                    {
                                                        if (TOL != "vct")  /// Check if is not Vacation Leave
                                                        {
                                                            findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                                " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";

                                                            lookDup = int.Parse(HRMIS.Module.GetCount(findLeaveInfo));
                                                            if (lookDup == 1)
                                                            {

                                                                string chkStat = "";
                                                                chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                            "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                            "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "') " +
                                                                            " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";
                                                                DataTable dts = HRMIS.Module.GetData(chkStat);
                                                                if (dts.Rows.Count > 0)
                                                                {
                                                                    if (dts.Rows[0]["empStatus"].ToString() == "2" || dts.Rows[0]["empStatus"].ToString() == "3")
                                                                    {
                                                                        string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                        "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                        "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                        HRMIS.Module.gblInsert(SQuery);

                                                                        string dtQry = "";
                                                                        string fIdent = "";
                                                                        dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                        DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                        if (dt.Rows.Count > 0)
                                                                        {
                                                                            fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                            string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                                "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                            HRMIS.Module.gblInsert(sQueryNot);

                                                                            if (DirecPath != "")
                                                                            {
                                                                                string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                                HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                                //string[] filePaths = Directory.GetFiles(DirecPath);
                                                                                //foreach (string filePath in filePaths)
                                                                                //{
                                                                                //    if (filePath != "")
                                                                                //    {
                                                                                //        string sQueryDetail = "Insert into seihaHRMIS.dbo.HRLeaveDocInfo(empNo, empDocFileName, empDocFileFrom, empDocFileTo, empLeaveFileDate, empLeaveDateFrom, empLeaveDateTo, empLeaveID) " +
                                                                                //                        "values('" + txtEmpNo.Text + "', '" + Path.GetFileName(filePath) + "', '" + DirecPath + "', '',  '" + DateTime.Now + "', '" + dateFrom.Text + "', '" + dateTo.Text + "', '" + fIdent + "')";

                                                                                //        HRMIS.Module.gblInsert(sQueryDetail);
                                                                                //    }
                                                                                //}
                                                                            }
                                                                        }

                                                                        string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                            "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                        HRMIS.Module.gblInsert(LogQuery);
                                                                        Response.Redirect("~/Leaves");
                                                                    }
                                                                    else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                                    {
                                                                        string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                        "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                        "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                        HRMIS.Module.gblInsert(SQuery);

                                                                        string dtQry = "";
                                                                        string fIdent = "";
                                                                        dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                        DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                        if (dt.Rows.Count > 0)
                                                                        {
                                                                            fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                            string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                                "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                            HRMIS.Module.gblInsert(sQueryNot);
                                                                            if (DirecPath != "")
                                                                            {
                                                                                string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                                HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                            }
                                                                        }

                                                                        string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                            "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                        HRMIS.Module.gblInsert(LogQuery);
                                                                        Response.Redirect("~/Leaves");
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
                                                                        "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                        "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                HRMIS.Module.gblInsert(SQuery);

                                                                string dtQry = "";
                                                                string fIdent = "";
                                                                dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                    string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                        "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                    HRMIS.Module.gblInsert(sQueryNot);
                                                                    if (DirecPath != "")
                                                                    {
                                                                        string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                        HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                    }
                                                                }

                                                                string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                    "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                HRMIS.Module.gblInsert(LogQuery);
                                                                Response.Redirect("~/Leaves");
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
                                                        findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                                " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";

                                                        lookDup = int.Parse(HRMIS.Module.GetCount(findLeaveInfo));
                                                        if (lookDup == 1)
                                                        {

                                                            string chkStat = "";
                                                            chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                        "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                        "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                                        " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";
                                                            DataTable dts = HRMIS.Module.GetData(chkStat);
                                                            if (dts.Rows.Count > 0)
                                                            {
                                                                if (dts.Rows[0]["empStatus"].ToString() == "2" || dts.Rows[0]["empStatus"].ToString() == "3")
                                                                {
                                                                    string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                    "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                    HRMIS.Module.gblInsert(SQuery);

                                                                    string dtQry = "";
                                                                    string fIdent = "";
                                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                    DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                    if (dt.Rows.Count > 0)
                                                                    {
                                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                            "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                        HRMIS.Module.gblInsert(sQueryNot);
                                                                        if (DirecPath != "")
                                                                        {
                                                                            string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                            HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                        }
                                                                    }

                                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                        "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                    HRMIS.Module.gblInsert(LogQuery);
                                                                    Response.Redirect("~/Leaves");
                                                                }
                                                                else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                                {
                                                                    string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                    "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                    HRMIS.Module.gblInsert(SQuery);

                                                                    string dtQry = "";
                                                                    string fIdent = "";
                                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                    DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                    if (dt.Rows.Count > 0)
                                                                    {
                                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                            "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                        HRMIS.Module.gblInsert(sQueryNot);
                                                                        if (DirecPath != "")
                                                                        {
                                                                            string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                            HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                        }
                                                                    }

                                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                        "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                    HRMIS.Module.gblInsert(LogQuery);
                                                                    Response.Redirect("~/Leaves");
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
                                                                    "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                            HRMIS.Module.gblInsert(SQuery);

                                                            string dtQry = "";
                                                            string fIdent = "";
                                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                            DataTable dt = HRMIS.Module.GetData(dtQry);
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                    "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                HRMIS.Module.gblInsert(sQueryNot);
                                                                if (DirecPath != "")
                                                                {
                                                                    string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                    HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                }
                                                            }

                                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                            HRMIS.Module.gblInsert(LogQuery);
                                                            Response.Redirect("~/Leaves");
                                                        }
                                                    } /// END Check if is not past the day 14 of the month
                                                }
                                                else
                                                {
                                                    findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                                " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";

                                                    lookDup = int.Parse(HRMIS.Module.GetCount(findLeaveInfo));
                                                    if (lookDup == 1)
                                                    {

                                                        string chkStat = "";
                                                        chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                    "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                    "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                                    " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";
                                                        DataTable dts = HRMIS.Module.GetData(chkStat);
                                                        if (dts.Rows.Count > 0)
                                                        {
                                                            if (dts.Rows[0]["empStatus"].ToString() == "2" || dts.Rows[0]["empStatus"].ToString() == "3")
                                                            {
                                                                string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                HRMIS.Module.gblInsert(SQuery);

                                                                string dtQry = "";
                                                                string fIdent = "";
                                                                dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                    string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                        "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                    HRMIS.Module.gblInsert(sQueryNot);
                                                                    if (DirecPath != "")
                                                                    {
                                                                        string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                        HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                    }
                                                                }

                                                                string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                    "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                HRMIS.Module.gblInsert(LogQuery);
                                                                Response.Redirect("~/Leaves");
                                                            }
                                                            else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                            {
                                                                string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + txtEmpNo.Text + "', 0, '', 0, 0, '')";
                                                                HRMIS.Module.gblInsert(SQuery);

                                                                string dtQry = "";
                                                                string fIdent = "";
                                                                dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                    string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                        "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                    HRMIS.Module.gblInsert(sQueryNot);
                                                                    if (DirecPath != "")
                                                                    {
                                                                        string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                        HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                    }
                                                                }

                                                                string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                    "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                HRMIS.Module.gblInsert(LogQuery);
                                                                Response.Redirect("~/Leaves");
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
                                                                "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                        HRMIS.Module.gblInsert(SQuery);

                                                        string dtQry = "";
                                                        string fIdent = "";
                                                        dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                        DataTable dt = HRMIS.Module.GetData(dtQry);
                                                        if (dt.Rows.Count > 0)
                                                        {
                                                            fIdent = dt.Rows[0]["identity_column"].ToString();
                                                            string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                            HRMIS.Module.gblInsert(sQueryNot);
                                                            if (DirecPath != "")
                                                            {
                                                                string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                            }
                                                        }

                                                        string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                            "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                        HRMIS.Module.gblInsert(LogQuery);
                                                        Response.Redirect("~/Leaves");
                                                    }
                                                }/// END Check if date filed if next two month


                                            }/// End of Count Time
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
                                        int lookDup = 0;
                                        string findLeaveInfo = "";
                                        DateTime dtFrm = DateTime.Parse(dateFrom.Text);
                                        DateTime dtTo = DateTime.Parse(dateTo.Text);
                                        DateTime dtNow = DateTime.Now;
                                        int calcMonth = (dtFrm.Year - dtNow.Year) * 12 + dtFrm.Month - dtNow.Month;
                                        if (calcMonth >= 1) /// Check if date filed if next two month
                                        {
                                            if (dtNow.Day > 5) /// Check if is not past the day 5 of the month
                                            {

                                                if (TOL != "vct") /// Check if is not Vacation Leave
                                                {
                                                    findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                        "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                        "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                        " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";

                                                    lookDup = int.Parse(HRMIS.Module.GetCount(findLeaveInfo));
                                                    if (lookDup >= 1)
                                                    {

                                                        string chkStat = "";
                                                        chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                    "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                    "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                                    " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";
                                                        DataTable dts = HRMIS.Module.GetData(chkStat);
                                                        if (dts.Rows.Count > 0)
                                                        {
                                                            if (dts.Rows[0]["empStatus"].ToString() == "2" || dts.Rows[0]["empStatus"].ToString() == "3")
                                                            {

                                                                string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                HRMIS.Module.gblInsert(SQuery);

                                                                string dtQry = "";
                                                                string fIdent = "";
                                                                dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                    string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                        "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                    HRMIS.Module.gblInsert(sQueryNot);
                                                                    if (DirecPath != "")
                                                                    {
                                                                        string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                        HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                    }

                                                                }


                                                                string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                    "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                HRMIS.Module.gblInsert(LogQuery);
                                                                Response.Redirect("~/Leaves");
                                                            }
                                                            else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                            {
                                                                string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                HRMIS.Module.gblInsert(SQuery);

                                                                string dtQry = "";
                                                                string fIdent = "";
                                                                dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                    string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                        "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                    HRMIS.Module.gblInsert(sQueryNot);
                                                                    if (DirecPath != "")
                                                                    {
                                                                        string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                        HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                    }
                                                                }

                                                                string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                    "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                HRMIS.Module.gblInsert(LogQuery);
                                                                Response.Redirect("~/Leaves");
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
                                                                "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                        HRMIS.Module.gblInsert(SQuery);

                                                        string dtQry = "";
                                                        string fIdent = "";
                                                        dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                        DataTable dt = HRMIS.Module.GetData(dtQry);
                                                        if (dt.Rows.Count > 0)
                                                        {

                                                            fIdent = dt.Rows[0]["identity_column"].ToString();
                                                            string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                            HRMIS.Module.gblInsert(sQueryNot);
                                                            if (DirecPath != "")
                                                            {
                                                                string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                            }

                                                        }


                                                        string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                            "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                        HRMIS.Module.gblInsert(LogQuery);
                                                        Response.Redirect("~/Leaves");
                                                    }
                                                }
                                                else
                                                {
                                                    if (hldDept == "IT" || hldDept == "ADM")//for IT and Admin
                                                    {
                                                        findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                        "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                        "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                        " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";

                                                        lookDup = int.Parse(HRMIS.Module.GetCount(findLeaveInfo));
                                                        if (lookDup >= 1)
                                                        {

                                                            string chkStat = "";
                                                            chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                        "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                        "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                                        " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";
                                                            DataTable dts = HRMIS.Module.GetData(chkStat);
                                                            if (dts.Rows.Count > 0)
                                                            {
                                                                if (dts.Rows[0]["empStatus"].ToString() == "2" || dts.Rows[0]["empStatus"].ToString() == "3")
                                                                {

                                                                    string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                    "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                    HRMIS.Module.gblInsert(SQuery);

                                                                    string dtQry = "";
                                                                    string fIdent = "";
                                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                    DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                    if (dt.Rows.Count > 0)
                                                                    {
                                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                            "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                        HRMIS.Module.gblInsert(sQueryNot);
                                                                        if (DirecPath != "")
                                                                        {
                                                                            string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                            HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                        }

                                                                    }


                                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                        "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                    HRMIS.Module.gblInsert(LogQuery);
                                                                    Response.Redirect("~/Leaves");
                                                                }
                                                                else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                                {
                                                                    string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                    "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                    HRMIS.Module.gblInsert(SQuery);

                                                                    string dtQry = "";
                                                                    string fIdent = "";
                                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                    DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                    if (dt.Rows.Count > 0)
                                                                    {
                                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                            "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                        HRMIS.Module.gblInsert(sQueryNot);
                                                                        if (DirecPath != "")
                                                                        {
                                                                            string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                            HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                        }
                                                                    }

                                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                        "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                    HRMIS.Module.gblInsert(LogQuery);
                                                                    Response.Redirect("~/Leaves");
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
                                                                    "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                            HRMIS.Module.gblInsert(SQuery);

                                                            string dtQry = "";
                                                            string fIdent = "";
                                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                            DataTable dt = HRMIS.Module.GetData(dtQry);
                                                            if (dt.Rows.Count > 0)
                                                            {

                                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                    "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                HRMIS.Module.gblInsert(sQueryNot);
                                                                if (DirecPath != "")
                                                                {
                                                                    string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                    HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                }

                                                            }


                                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                            HRMIS.Module.gblInsert(LogQuery);
                                                            Response.Redirect("~/Leaves");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                        "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                        "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                        " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";

                                                        lookDup = int.Parse(HRMIS.Module.GetCount(findLeaveInfo));
                                                        if (lookDup >= 1)
                                                        {
                                                            string chkStat = "";
                                                            chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                        "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                        "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                                        " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";
                                                            DataTable dts = HRMIS.Module.GetData(chkStat);
                                                            if (dts.Rows.Count > 0)
                                                            {
                                                                if (dts.Rows[0]["empStatus"].ToString() == "2" || dts.Rows[0]["empStatus"].ToString() == "3")
                                                                {

                                                                    string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                    "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                    HRMIS.Module.gblInsert(SQuery);

                                                                    string dtQry = "";
                                                                    string fIdent = "";
                                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                    DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                    if (dt.Rows.Count > 0)
                                                                    {
                                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                            "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                        HRMIS.Module.gblInsert(sQueryNot);
                                                                        if (DirecPath != "")
                                                                        {
                                                                            string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                            HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                        }

                                                                    }


                                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                        "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                    HRMIS.Module.gblInsert(LogQuery);
                                                                    Response.Redirect("~/Leaves");
                                                                }
                                                                else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                                {
                                                                    string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                    "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                    "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                    HRMIS.Module.gblInsert(SQuery);

                                                                    string dtQry = "";
                                                                    string fIdent = "";
                                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                    DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                    if (dt.Rows.Count > 0)
                                                                    {
                                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                            "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                        HRMIS.Module.gblInsert(sQueryNot);
                                                                        if (DirecPath != "")
                                                                        {
                                                                            string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                            HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                        }
                                                                    }

                                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                        "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                    HRMIS.Module.gblInsert(LogQuery);
                                                                    Response.Redirect("~/Leaves");
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
                                                                        "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                        "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                            HRMIS.Module.gblInsert(SQuery);

                                                            string dtQry = "";
                                                            string fIdent = "";
                                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                            DataTable dt = HRMIS.Module.GetData(dtQry);
                                                            if (dt.Rows.Count > 0)
                                                            {

                                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                    "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                HRMIS.Module.gblInsert(sQueryNot);
                                                                if (DirecPath != "")
                                                                {
                                                                    string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                    HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                }

                                                            }


                                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                            HRMIS.Module.gblInsert(LogQuery);
                                                            Response.Redirect("~/Leaves");
                                                        }
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                        "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                        "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                        " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";

                                                lookDup = int.Parse(HRMIS.Module.GetCount(findLeaveInfo));
                                                if (lookDup >= 1)
                                                {

                                                    string chkStat = "";
                                                    chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                                " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";
                                                    DataTable dts = HRMIS.Module.GetData(chkStat);
                                                    if (dts.Rows.Count > 0)
                                                    {
                                                        if (dts.Rows[0]["empStatus"].ToString() == "2" || dts.Rows[0]["empStatus"].ToString() == "3")
                                                        {

                                                            string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                            "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                            "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                            HRMIS.Module.gblInsert(SQuery);

                                                            string dtQry = "";
                                                            string fIdent = "";
                                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                            DataTable dt = HRMIS.Module.GetData(dtQry);
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                    "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                HRMIS.Module.gblInsert(sQueryNot);
                                                                if (DirecPath != "")
                                                                {
                                                                    string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                    HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                }

                                                            }


                                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                            HRMIS.Module.gblInsert(LogQuery);
                                                            Response.Redirect("~/Leaves");
                                                        }
                                                        else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                        {
                                                            string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                            "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                            "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                            HRMIS.Module.gblInsert(SQuery);

                                                            string dtQry = "";
                                                            string fIdent = "";
                                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                            DataTable dt = HRMIS.Module.GetData(dtQry);
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                    "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                HRMIS.Module.gblInsert(sQueryNot);
                                                                if (DirecPath != "")
                                                                {
                                                                    string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                    HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                }
                                                            }

                                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                            HRMIS.Module.gblInsert(LogQuery);
                                                            Response.Redirect("~/Leaves");
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
                                                            "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                            "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                    HRMIS.Module.gblInsert(SQuery);

                                                    string dtQry = "";
                                                    string fIdent = "";
                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                    DataTable dt = HRMIS.Module.GetData(dtQry);
                                                    if (dt.Rows.Count > 0)
                                                    {

                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                            "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                        HRMIS.Module.gblInsert(sQueryNot);
                                                        if (DirecPath != "")
                                                        {
                                                            string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                            HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                        }

                                                    }


                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                        "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                    HRMIS.Module.gblInsert(LogQuery);
                                                    Response.Redirect("~/Leaves");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if(TOL == "sck")
                                            {
                                                findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                        "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                        "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                        " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";

                                                lookDup = int.Parse(HRMIS.Module.GetCount(findLeaveInfo));
                                                if (lookDup >= 1)
                                                {
                                                    string chkStat = "";
                                                    chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                                " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";
                                                    DataTable dts = HRMIS.Module.GetData(chkStat);
                                                    if (dts.Rows.Count > 0)
                                                    {
                                                        if (dts.Rows[0]["empStatus"].ToString() == "2" || dts.Rows[0]["empStatus"].ToString() == "3")
                                                        {

                                                            string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                            "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                            "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                            HRMIS.Module.gblInsert(SQuery);

                                                            string dtQry = "";
                                                            string fIdent = "";
                                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                            DataTable dt = HRMIS.Module.GetData(dtQry);
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                    "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                HRMIS.Module.gblInsert(sQueryNot);
                                                                if (DirecPath != "")
                                                                {
                                                                    string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                    HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                }

                                                            }


                                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                            HRMIS.Module.gblInsert(LogQuery);
                                                            Response.Redirect("~/Leaves");
                                                        }
                                                        else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                        {
                                                            string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                            "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                            "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                            HRMIS.Module.gblInsert(SQuery);

                                                            string dtQry = "";
                                                            string fIdent = "";
                                                            dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                            DataTable dt = HRMIS.Module.GetData(dtQry);
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                    "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                HRMIS.Module.gblInsert(sQueryNot);
                                                                if (DirecPath != "")
                                                                {
                                                                    string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                    HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                }
                                                            }

                                                            string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                            HRMIS.Module.gblInsert(LogQuery);
                                                            Response.Redirect("~/Leaves");
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
                                                                "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                    HRMIS.Module.gblInsert(SQuery);

                                                    string dtQry = "";
                                                    string fIdent = "";
                                                    dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                    DataTable dt = HRMIS.Module.GetData(dtQry);
                                                    if (dt.Rows.Count > 0)
                                                    {

                                                        fIdent = dt.Rows[0]["identity_column"].ToString();
                                                        string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                            "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                        HRMIS.Module.gblInsert(sQueryNot);
                                                        if (DirecPath != "")
                                                        {
                                                            string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                            HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                        }

                                                    }


                                                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                        "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                    HRMIS.Module.gblInsert(LogQuery);
                                                    Response.Redirect("~/Leaves");
                                                }
                                            }
                                            else
                                            {
                                                if (dtNow.Day > 5)
                                                {
                                                    Response.Write(@"<script> alert('Deadline Exceeded for Leave Form Submission!') </script>");
                                                }
                                                else
                                                {
                                                    findLeaveInfo = "Select count(*) from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                                " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";

                                                    lookDup = int.Parse(HRMIS.Module.GetCount(findLeaveInfo));
                                                    if (lookDup >= 1)
                                                    {

                                                        string chkStat = "";
                                                        chkStat = "Select empStatus, empTOL from seihaHRMIS.dbo.HRLeaveInfo where EmpNo = '" + txtEmpNo.Text.Trim() + "' " +
                                                                    "and (convert(varchar(10), empdatefrom, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "' " +
                                                                    "OR convert(varchar(10), empdateto, 101) between '" + dtFrm.ToString("MM/dd/yyyy") + "' and '" + dtTo.ToString("MM/dd/yyyy") + "')" +
                                                                    " and YEAR(empDateFrom) = YEAR('" + dtFrm.ToString("MM/dd/yyyy") + "') and YEAR(empdateto) = YEAR('" + dtTo.ToString("MM/dd/yyyy") + "') ";
                                                        DataTable dts = HRMIS.Module.GetData(chkStat);
                                                        if (dts.Rows.Count > 0)
                                                        {
                                                            if (dts.Rows[0]["empStatus"].ToString() == "2")
                                                            {

                                                                string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                HRMIS.Module.gblInsert(SQuery);

                                                                string dtQry = "";
                                                                string fIdent = "";
                                                                dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                    string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                        "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                    HRMIS.Module.gblInsert(sQueryNot);
                                                                    if (DirecPath != "")
                                                                    {
                                                                        string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                        HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                    }

                                                                }


                                                                string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                    "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                HRMIS.Module.gblInsert(LogQuery);
                                                                Response.Redirect("~/Leaves");
                                                            }
                                                            else if (dts.Rows[0]["empTOL"].ToString() != TOL)
                                                            {
                                                                string SQuery = "Insert into seihaHRMIS.dbo.HRLeaveInfo(empno, empdate, empadd, emptol, empreason, empdatefrom, empdateto, emptimefrom, emptimeto, empstatus, empheadempno, empheadstatus, empmanaempno, empmanastatus, empleavecred, empattfileloc) " +
                                                                "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                                HRMIS.Module.gblInsert(SQuery);

                                                                string dtQry = "";
                                                                string fIdent = "";
                                                                dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                                DataTable dt = HRMIS.Module.GetData(dtQry);
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    fIdent = dt.Rows[0]["identity_column"].ToString();
                                                                    string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                        "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                                    HRMIS.Module.gblInsert(sQueryNot);
                                                                    if (DirecPath != "")
                                                                    {
                                                                        string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                        HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                                    }
                                                                }

                                                                string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                    "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                                HRMIS.Module.gblInsert(LogQuery);
                                                                Response.Redirect("~/Leaves");
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
                                                                "values('" + txtEmpNo.Text + "', '" + txtDateNow.Text + " " + holdTimeNow + "', '" + txtaddress.Text + "', '" + TOL + "', '" + txtReason.Text.Replace("'", "''") + "', '" + dateFrom.Text + "', " +
                                                                "'" + dateTo.Text + "', '" + timeFrom.Text + "', '" + timeTo.Text + "', 0, '" + RHNo.Text + "', 0, '', 0, 0, '')";
                                                        HRMIS.Module.gblInsert(SQuery);

                                                        string dtQry = "";
                                                        string fIdent = "";
                                                        dtQry = "Select identity_column from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + txtEmpNo.Text + "' and convert(varchar(10), empdatefrom, 101) = '" + dtFrm.ToString("MM/dd/yyyy") + "' and convert(varchar(10), empdateto, 101) = '" + dtTo.ToString("MM/dd/yyyy") + "'";
                                                        DataTable dt = HRMIS.Module.GetData(dtQry);
                                                        if (dt.Rows.Count > 0)
                                                        {

                                                            fIdent = dt.Rows[0]["identity_column"].ToString();
                                                            string sQueryNot = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                                                "values('" + txtEmpNo.Text + "', '" + fIdent + "', '" + RHNo.Text + "', '', 0, 1, '" + DateTime.Now + "')";
                                                            HRMIS.Module.gblInsert(sQueryNot);
                                                            if (DirecPath != "")
                                                            {
                                                                string sQueryUpdateDet = "Update seihaHRMIS.dbo.HRLeaveDocInfo set empLeaveID = '" + fIdent + "' where CAST(guid as varchar(max)) = '" + guidHld + "'";
                                                                HRMIS.Module.gblInsert(sQueryUpdateDet);
                                                            }

                                                        }


                                                        string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                            "values('L','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + TOL + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                                                        HRMIS.Module.gblInsert(LogQuery);
                                                        Response.Redirect("~/Leaves");
                                                    }
                                                }
                                            }
                                        }//end of Else

                                    }// End of Else
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
                        Session.Abandon();
                        Response.Redirect("~/Login");
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
    }
}
