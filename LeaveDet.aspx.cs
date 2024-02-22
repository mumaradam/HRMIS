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
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace HRMIS
{
    public partial class LeaveDet : System.Web.UI.Page
    {
        private static DataTable dtEmpInfo;
        private static DataTable dtLeaveQuery;
        private static string gtParam = "";
        private static string getEmpNo = "";
        private static string getUserAdmin = "";
        private static string getUserDept = "";
        private static string getUserPos = "";
        private static string getUserRole = "";
        private static string getUserReportHead = "";
        private static string getLeaveEmpNo = "";
        private static string ipAdd = "";
        private static string getAdminType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            getEmpNo = Session["Uname"] as string;
            gtParam = Request.QueryString["param"] as string;
            ipAdd = GetClientMAC(GetIPAddress());
            if (string.IsNullOrEmpty(getEmpNo))
            {
                Session.Abandon();
                Response.Redirect("~/Login");
            }
            else
            {
                
                if(!IsPostBack)
                {
                    clear();
                    getUser();
                    getLeaveInfo();
                }
                //loadGrid(gtParam);
            }
        }
        protected void clear()
        {
            txtLeadName.Text = "";
            txtSRemarks.Text = "";
            txtManaName.Text = "";
            txtMRemarks.Text = "";
            txtAdminName.Text = "";
            txtAdminRemarks.Text = "";

            chkbxAdmLL.Checked = false;
            chkbxAdmLL.Style.Clear();
            chkbxAdmSL.Checked = false;
            chkbxAdmSL.Style.Clear();
            chkbxAdmSPL.Checked = false;
            chkbxAdmSPL.Style.Clear();
            chkbxAdmVL.Checked = false;
            chkbxAdmVL.Style.Clear();
            vacation.Checked = false;
            vacation.Style.Clear();
            sick.Checked = false;
            sick.Style.Clear();
            maternity.Checked = false;
            maternity.Style.Clear();
            paternity.Checked = false;
            paternity.Style.Clear();
            emergency.Checked = false;
            emergency.Style.Clear();
            undertime.Checked = false;
            undertime.Style.Clear();
            changeOff.Checked = false;
            changeOff.Style.Clear();

            sapproved.Style.Clear();
            sapproved.Checked = false;
            sdenied.Style.Clear();
            sdenied.Checked = false;

            mapproved.Style.Clear();
            mapproved.Checked = false;
            mdenied.Style.Clear();
            mdenied.Checked = false;

            //LeadArea.Visible = false;
            //ManaArea.Visible = false;
            //AdmArea.Visible = false;
            //btnadmSub.Visible = false;

        }
        protected void getUser()
        {
            try
            {
                dtEmpInfo = null;
                string dtEmpInfoQuery = "";
                dtEmpInfoQuery = "Select * from seihaHRMIS.dbo.HREmpInfo where EmpNo = '" + getEmpNo + "'"; ///Get User Info
                dtEmpInfo = HRMIS.Module.GetData(dtEmpInfoQuery);
                if (dtEmpInfo.Rows.Count > 0)
                {
                    getUserDept = dtEmpInfo.Rows[0]["empDept"].ToString();
                    getUserPos = dtEmpInfo.Rows[0]["empPos"].ToString();
                    getUserAdmin = dtEmpInfo.Rows[0]["empadmin"].ToString();
                    getUserRole = dtEmpInfo.Rows[0]["emprole"].ToString();
                    
                    string strIfMe = "";
                    strIfMe = "Select * from seihaHRMIS.dbo.HRLeaveInfo where identity_column = '" + gtParam + "'";
                    DataTable dtFM = HRMIS.Module.GetData(strIfMe);
                    if(dtFM.Rows.Count > 0)
                    {
                        getLeaveEmpNo = dtFM.Rows[0]["empNo"].ToString();
                        getUserReportHead = HRMIS.Module.GetField("Select empReportHead as Name, * from seihaHRMIS.dbo.HREmpInfo a where empno = '" + getLeaveEmpNo + "' order by empno");
                        if (dtEmpInfo.Rows[0]["empno"].ToString() == dtFM.Rows[0]["empNo"].ToString())
                        {
                            if (dtFM.Rows[0]["empStatus"].ToString() == "0")
                            {
                                btnPrint.Visible = false;
                                btnDelete.Visible = true;
                            }
                            else
                            {
                                btnPrint.Visible = false;
                                btnDelete.Visible = false;
                            }
                            disLeaveInfo(gtParam, getUserPos, getUserRole, getUserDept, getLeaveEmpNo);
                        }
                        else
                        {
                            btnPrint.Visible = false;
                            if (getUserAdmin == "1") 
                            { 
                                btnPrint.Visible = true;
                                disLeaveInfo(gtParam, getUserPos, getUserRole, getUserDept, getLeaveEmpNo);
                            } 
                            else 
                            {
                                if (getUserRole == "RLD" || getUserRole == "RSL")
                                {

                                    //LeadArea.Visible = true;
                                    //ManaArea.Visible = false;
                                    //AdmArea.Visible = false;
                                    disLeaveInfo(gtParam, getUserPos, getUserRole, getUserDept, getLeaveEmpNo);
                                }
                                else if (getUserPos == "ITM" || getUserPos == "TMR" || getUserPos == "SMR" || getUserPos == "TOM" || getUserDept == "MGR")
                                {
                                    //LeadArea.Visible = false;
                                    //ManaArea.Visible = true;
                                    //AdmArea.Visible = false;
                                    disLeaveInfo(gtParam, getUserPos, getUserRole, getUserDept, getLeaveEmpNo);
                                }
                                else
                                {
                                    btnPrint.Visible = false;
                                    LeadArea.Visible = false;
                                    ManaArea.Visible = false;
                                    AdmArea.Visible = false;
                                }
                            }

                            ///disLeaveInfo(gtParam, getUserPos, getUserRole, getUserDept);
                        }
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void disLeaveInfo(string strID, string strPos, string strRole, string srtDept, string strLeaveEmp)
        {
            string qryLStat = "Select identity_column from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + strID + "'";
            string fndLstat = HRMIS.Module.GetField(qryLStat);
            if(fndLstat != "")
            {
                string qryStr = "Select * from seihaHRMIS.dbo.leaveStatInfo where identity_column = '" + fndLstat + "'";
                DataTable dtLstat = HRMIS.Module.GetData(qryStr);
                if (dtLstat.Rows.Count > 0)
                {
                    if (getUserAdmin == "1")///Speacial Admin
                    {
                        if (dtLstat.Rows[0]["leaveManaNo"].ToString() != "")
                        {
                            btnPrint.Visible = true;
                            if(dtLstat.Rows[0]["leaveSupNo"].ToString() != "")
                            {
                                leadPanel.Visible = true;
                                btnsupSub.Visible = false;
                                txtLeadName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveSupNo"].ToString() + "'");
                                txtSRemarks.Text = dtLstat.Rows[0]["leaveSupRemarks"].ToString().Replace("''", "'");
                                if (dtLstat.Rows[0]["leaveSupType"].ToString() == "1")
                                { sapproved.Checked = true; sapproved.Attributes.Add("style", "background-color: green;"); }
                                else if (dtLstat.Rows[0]["leaveSupType"].ToString() == "2") { sdenied.Checked = true; sdenied.Attributes.Add("style", "background-color: pink;"); }
                            }
                            else
                            {
                                LeadArea.Visible = false;
                                msgLead.Visible = true;
                                btnsupSub.Visible = false;
                                leadPanel.Visible = false;
                            }
                            manaPanel.Visible = true;
                            btnmanSub.Visible = false  ;
                            txtManaName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveManaNo"].ToString() + "'");
                            txtMRemarks.Text = dtLstat.Rows[0]["leaveManaRemarks"].ToString().Replace("''", "'");
                            if (dtLstat.Rows[0]["leaveManaType"].ToString() == "1")
                            { mapproved.Checked = true; mapproved.Attributes.Add("style", "background-color: green;"); }
                            else if (dtLstat.Rows[0]["leaveManaType"].ToString() == "2") { mdenied.Checked = true; mdenied.Attributes.Add("style", "background-color: pink;"); }
                            txtAdminName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as Name, * from seihaHRMIS.dbo.HREmpInfo a where empno = '" + getEmpNo + "' order by empno");

                            if (dtLstat.Rows[0]["leaveLeaveStat"].ToString() == "3") // Leave Status is Cancelled in Admin
                            {
                                txtAdminName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveAdminNo"].ToString() + "'");
                                txtAdminRemarks.Text = dtLstat.Rows[0]["leaveAdminEval"].ToString();
                                btnadmSub.Visible = false;
                                btnadmCan.Visible = false;
                                msgAdmin.Visible = true;
                                btnDelete.Visible = false;
                                txtAdminRemarks.ReadOnly = true;
                                chkbxWOPay.Enabled = false;
                                chkbxAdmVL.Enabled = false;
                                chkbxAdmSL.Enabled = false;
                                chkbxAdmLL.Enabled = false;
                                chkbxAdmSPL.Enabled = false;
                            }
                            else
                            {
                                if (dtLstat.Rows[0]["leaveLeaveStat"].ToString() == "2") //Leave Status is Denied by Manager
                                {
                                    AdmArea.Visible = false;
                                }
                                else
                                {
                                    //Display Admin Info if Admin Already Approve or Denied
                                    if (dtLstat.Rows[0]["leaveAdminNo"].ToString() != "")
                                    {
                                        txtAdminName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveAdminNo"].ToString() + "'");
                                        txtAdminRemarks.Text = dtLstat.Rows[0]["leaveAdminEval"].ToString();
                                        if (dtLstat.Rows[0]["leaveAdminType"].ToString() == "1") //If Leave Request Approved by Admin
                                        {
                                            if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "0") { chkbxWOPay.Checked = true; chkbxWOPay.Attributes.Add("style", "background-color: green;"); }
                                            if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "1") { chkbxAdmVL.Checked = true; chkbxAdmVL.Attributes.Add("style", "background-color: green;"); }
                                            if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "2") { chkbxAdmSL.Checked = true; chkbxAdmSL.Attributes.Add("style", "background-color: green;"); }
                                            if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "3") { chkbxAdmLL.Checked = true; chkbxAdmLL.Attributes.Add("style", "background-color: green;"); }
                                            if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "3") { chkbxAdmSPL.Checked = true; chkbxAdmSPL.Attributes.Add("style", "background-color: green;"); }
                                            chkbxWOPay.Enabled = false;
                                            chkbxAdmVL.Enabled = false;
                                            chkbxAdmSL.Enabled = false;
                                            chkbxAdmLL.Enabled = false;
                                            chkbxAdmSPL.Enabled = false;
                                            btnadmSub.Visible = false;
                                            btnadmCan.Visible = false;
                                        }
                                        else //If Leave Request is Canceled by Admin
                                        {
                                            chkbxWOPay.Enabled = false;
                                            chkbxAdmVL.Enabled = false;
                                            chkbxAdmSL.Enabled = false;
                                            chkbxAdmLL.Enabled = false;
                                            chkbxAdmSPL.Enabled = false;
                                            msgAdmin.Visible = true;
                                            btnadmSub.Visible = false;
                                            btnadmCan.Visible = false;
                                        }

                                    }
                                    else // Not Yet Approve nor Canceled by Admin
                                    {
                                        btnadmSub.Visible = true;
                                        btnadmCan.Visible = true;
                                        msgAdmin.Visible = false;
                                        txtAdminRemarks.ReadOnly = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dtLstat.Rows[0]["leaveSupNo"].ToString() != "")
                            {
                                leadPanel.Visible = true;
                                btnsupSub.Visible = false;
                                txtLeadName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveSupNo"].ToString() + "'");
                                txtSRemarks.Text = dtLstat.Rows[0]["leaveSupRemarks"].ToString().Replace("''", "'");
                                if (dtLstat.Rows[0]["leaveSupType"].ToString() == "1")
                                { sapproved.Checked = true; sapproved.Attributes.Add("style", "background-color: green;"); }
                                else if (dtLstat.Rows[0]["leaveSupType"].ToString() == "2") { sdenied.Checked = true; sdenied.Attributes.Add("style", "background-color: pink;"); }
                            }
                            else
                            {
                                if (dtLstat.Rows[0]["leaveLeaveStat"].ToString() == "3")
                                {
                                    btnadmSub.Visible = false;
                                    btnadmCan.Visible = false;
                                    msgAdmin.Visible = true;
                                    btnPrint.Visible = false;
                                    btnDelete.Visible = false;
                                    txtAdminRemarks.ReadOnly = true;
                                }
                                else
                                {
                                    if (dtLstat.Rows[0]["leaveLeaveStat"].ToString() == "2")
                                    {
                                        AdmArea.Visible = false;
                                    }
                                    else
                                    {
                                        //Display Admin Info if Admin Already Approve or Denied
                                        if (dtLstat.Rows[0]["leaveAdminNo"].ToString() != "")
                                        {
                                            txtAdminName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveAdminNo"].ToString() + "'");
                                            txtAdminRemarks.Text = dtLstat.Rows[0]["leaveAdminEval"].ToString();
                                            if (dtLstat.Rows[0]["leaveAdminEval"].ToString() == "1") //If Leave Request Approved by Admin
                                            {
                                                if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "0") { chkbxWOPay.Checked = true; chkbxWOPay.Attributes.Add("style", "background-color: green;"); }
                                                if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "1") { chkbxAdmVL.Checked = true; chkbxAdmVL.Attributes.Add("style", "background-color: green;"); }
                                                if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "2") { chkbxAdmSL.Checked = true; chkbxAdmSL.Attributes.Add("style", "background-color: green;"); }
                                                if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "3") { chkbxAdmLL.Checked = true; chkbxAdmLL.Attributes.Add("style", "background-color: green;"); }
                                                if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "3") { chkbxAdmSPL.Checked = true; chkbxAdmSPL.Attributes.Add("style", "background-color: green;"); }
                                                chkbxWOPay.Enabled = false;
                                                chkbxAdmVL.Enabled = false;
                                                chkbxAdmSL.Enabled = false;
                                                chkbxAdmLL.Enabled = false;
                                                chkbxAdmSPL.Enabled = false;
                                                btnadmSub.Visible = false;
                                                btnadmCan.Visible = false;
                                            }
                                            else //If Leave Request is Canceled by Admin
                                            {
                                                chkbxWOPay.Enabled = false;
                                                chkbxAdmVL.Enabled = false;
                                                chkbxAdmSL.Enabled = false;
                                                chkbxAdmLL.Enabled = false;
                                                chkbxAdmSPL.Enabled = false;
                                                msgAdmin.Visible = true;
                                                btnadmSub.Visible = false;
                                                btnadmCan.Visible = false;
                                            }

                                        }
                                        else // Not Yet Approve nor Canceled by Admin
                                        {
                                            btnadmSub.Visible = true;
                                            btnadmCan.Visible = true;
                                            msgAdmin.Visible = false;
                                            txtAdminRemarks.ReadOnly = false;
                                        }
                                    }
                                }
                                LeadArea.Visible = false;
                                msgLead.Visible = true;
                                btnsupSub.Visible = false;
                                leadPanel.Visible = false;
                            }
                            LeadArea.Visible = false;
                            msgLead.Visible = true;
                            btnsupSub.Visible = false;
                            leadPanel.Visible = false;
                            ManaArea.Visible = false;
                            msgMana.Visible = true;
                            btnmanSub.Visible = false;
                            manaPanel.Visible = false;
                        } 
                        
                    }////////////END ADMIN
                    else if (getUserRole == "RLD" || getUserRole == "RSL")///Leaders
                    {
                        string getRptHead = HRMIS.Module.GetField("Select empReportHead from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["empno"].ToString() + "'");
                        if (dtLstat.Rows[0]["leaveSupNo"].ToString() != "")
                        {
                            leadPanel.Visible = true;
                            btnsupSub.Visible = false;
                            txtLeadName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveSupNo"].ToString() + "'");
                            txtSRemarks.Text = dtLstat.Rows[0]["leaveSupRemarks"].ToString().Replace("''", "'");
                        }
                        else
                        {
                             if (dtLstat.Rows[0]["leaveManaNo"].ToString() != "")
                            {
                                msgLead.Visible = true;
                                btnsupSub.Visible = false;
                                leadPanel.Visible = false;
                                btnmanSub.Visible = false;
                                txtManaName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveManaNo"].ToString() + "'");
                                txtMRemarks.Text = dtLstat.Rows[0]["leaveManaRemarks"].ToString().Replace("''", "'");
                                txtAdminName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as Name, * from seihaHRMIS.dbo.HREmpInfo a where empno = '" + getEmpNo + "' order by empno");
                            }
                            else
                            {
                                if (getEmpNo == getRptHead)
                                {
                                    txtLeadName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as Name, * from seihaHRMIS.dbo.HREmpInfo a where empno = '" + getEmpNo + "' order by empno");
                                    sapproved.Enabled = true;
                                    sdenied.Enabled = true;
                                    txtSRemarks.ReadOnly = false;
                                    leadPanel.Visible = true;
                                    btnsupSub.Visible = false;
                                    msgMana.Visible = true;
                                    btnmanSub.Visible = false;
                                    manaPanel.Visible = false;
                                    btnadmSub.Visible = false;
                                    adminPanel.Visible = false;
                                }
                                else
                                {
                                    msgLead.Visible = true;
                                    btnsupSub.Visible = false;
                                    leadPanel.Visible = false;
                                    msgMana.Visible = true;
                                    btnmanSub.Visible = false;
                                    manaPanel.Visible = false;
                                    btnadmSub.Visible = false;
                                    adminPanel.Visible = false;
                                }
                            }
                            
                        }
                        
                    }
                    else if (getUserPos == "ITM" || getUserPos == "TMR" || getUserPos == "SMR" || getUserPos == "TOM" || getUserDept == "MGR") ///Managers
                    {
                        if (dtLstat.Rows[0]["leaveSupNo"].ToString() != "")//Leader Approved or Denied
                        {
                            leadPanel.Visible = true;
                            btnsupSub.Visible = false;
                            txtLeadName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveSupNo"].ToString() + "'");
                            txtSRemarks.Text = dtLstat.Rows[0]["leaveSupRemarks"].ToString().Replace("''", "'");
                            if (dtLstat.Rows[0]["leaveSupType"].ToString() == "1")
                            { sapproved.Checked = true; sapproved.Attributes.Add("style", "background-color: green;"); }
                            else if (dtLstat.Rows[0]["leaveSupType"].ToString() == "2") { sdenied.Checked = true; sdenied.Attributes.Add("style", "background-color: pink;"); }
                            if (dtLstat.Rows[0]["leaveManaNo"].ToString() != "")//Manager Approved or Denied the Leave
                            {
                                btnmanSub.Visible = false;
                                txtManaName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveManaNo"].ToString() + "'");
                                txtMRemarks.Text = dtLstat.Rows[0]["leaveManaRemarks"].ToString().Replace("''", "'");
                                if (dtLstat.Rows[0]["leaveManaType"].ToString() == "1")
                                { mapproved.Checked = true; mapproved.Attributes.Add("style", "background-color: green;"); }
                                else if (dtLstat.Rows[0]["leaveManaType"].ToString() == "2") { mdenied.Checked = true; mdenied.Attributes.Add("style", "background-color: pink;"); }
                                if (dtLstat.Rows[0]["leaveAdminNo"].ToString() != "")
                                {
                                    btnadmCan.Visible = false;
                                    btnadmSub.Visible = false;
                                    txtAdminName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveAdminNo"].ToString() + "'");
                                    txtAdminRemarks.Text = dtLstat.Rows[0]["leaveAdminEval"].ToString();
                                    if (dtLstat.Rows[0]["leaveAdminEval"].ToString() == "1") //If Leave Request Approved by Admin
                                    {
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "0") { chkbxWOPay.Checked = true; chkbxWOPay.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "1") { chkbxAdmVL.Checked = true; chkbxAdmVL.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "2") { chkbxAdmSL.Checked = true; chkbxAdmSL.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "3") { chkbxAdmLL.Checked = true; chkbxAdmLL.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "3") { chkbxAdmSPL.Checked = true; chkbxAdmSPL.Attributes.Add("style", "background-color: green;"); }
                                        chkbxWOPay.Enabled = false;
                                        chkbxAdmVL.Enabled = false;
                                        chkbxAdmSL.Enabled = false;
                                        chkbxAdmLL.Enabled = false;
                                        chkbxAdmSPL.Enabled = false;
                                    }
                                    else //If Leave Request is Canceled by Admin
                                    {
                                        chkbxWOPay.Enabled = false;
                                        chkbxAdmVL.Enabled = false;
                                        chkbxAdmSL.Enabled = false;
                                        chkbxAdmLL.Enabled = false;
                                        chkbxAdmSPL.Enabled = false;
                                        msgAdmin.Visible = true;
                                    }

                                }
                                else
                                {
                                    AdmArea.Visible = false;
                                }
                            }
                            else
                            {
                                ManaArea.Visible = false;
                                AdmArea.Visible = false;
                            }
                        }
                        else
                        {
                            LeadArea.Visible = false;
                            if (dtLstat.Rows[0]["leaveManaNo"].ToString() != "") //Manager Approved the Leave without the Leader
                            {
                                btnmanSub.Visible = false;
                                txtManaName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveManaNo"].ToString() + "'");
                                txtMRemarks.Text = dtLstat.Rows[0]["leaveManaRemarks"].ToString().Replace("''", "'");
                                if (dtLstat.Rows[0]["leaveManaType"].ToString() == "1")
                                { mapproved.Checked = true; mapproved.Attributes.Add("style", "background-color: green;"); }
                                else if (dtLstat.Rows[0]["leaveManaType"].ToString() == "2") { mdenied.Checked = true; mdenied.Attributes.Add("style", "background-color: pink;"); }
                                if (dtLstat.Rows[0]["leaveAdminNo"].ToString() != "")
                                {
                                    btnadmCan.Visible = false;
                                    btnadmSub.Visible = false;
                                    txtAdminName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveAdminNo"].ToString() + "'");
                                    txtAdminRemarks.Text = dtLstat.Rows[0]["leaveAdminEval"].ToString();
                                    if (dtLstat.Rows[0]["leaveAdminEval"].ToString() == "1") //If Leave Request Approved by Admin
                                    {
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "0") { chkbxWOPay.Checked = true; chkbxWOPay.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "1") { chkbxAdmVL.Checked = true; chkbxAdmVL.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "2") { chkbxAdmSL.Checked = true; chkbxAdmSL.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "3") { chkbxAdmLL.Checked = true; chkbxAdmLL.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "3") { chkbxAdmSPL.Checked = true; chkbxAdmSPL.Attributes.Add("style", "background-color: green;"); }
                                        chkbxWOPay.Enabled = false;
                                        chkbxAdmVL.Enabled = false;
                                        chkbxAdmSL.Enabled = false;
                                        chkbxAdmLL.Enabled = false;
                                        chkbxAdmSPL.Enabled = false;
                                    }
                                    else //If Leave Request is Canceled by Admin
                                    {
                                        chkbxWOPay.Enabled = false;
                                        chkbxAdmVL.Enabled = false;
                                        chkbxAdmSL.Enabled = false;
                                        chkbxAdmLL.Enabled = false;
                                        chkbxAdmSPL.Enabled = false;
                                        msgAdmin.Visible = true;
                                    }
                                    
                                }
                                else
                                {
                                    AdmArea.Visible = false;
                                }
                            }
                            else
                            {
                                ManaArea.Visible = false;
                                AdmArea.Visible = false;
                            }
                        }
                    }
                    else ////Ordinary Employee
                    {
                        if (dtLstat.Rows[0]["leaveSupNo"].ToString() != "") // Leader Approved or Denied
                        {
                            leadPanel.Visible = true;
                            btnsupSub.Visible = false;
                            txtLeadName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveSupNo"].ToString() + "'");
                            if (dtLstat.Rows[0]["leaveSupType"].ToString() == "1")
                            { sapproved.Checked = true; sapproved.Attributes.Add("style", "background-color: green;"); }
                            txtSRemarks.Text = dtLstat.Rows[0]["leaveSupRemarks"].ToString().Replace("''", "'");
                            if (dtLstat.Rows[0]["leaveManaNo"].ToString() != "") // Manager Approved or Denied
                            {
                                btnmanSub.Visible = false;
                                txtManaName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveManaNo"].ToString() + "'");
                                txtMRemarks.Text = dtLstat.Rows[0]["leaveManaRemarks"].ToString().Replace("''", "'");
                                if (dtLstat.Rows[0]["leaveManaType"].ToString() == "1")
                                { mapproved.Checked = true; mapproved.Attributes.Add("style", "background-color: green;"); }
                                else if (dtLstat.Rows[0]["leaveManaType"].ToString() == "2") { mdenied.Checked = true; mdenied.Attributes.Add("style", "background-color: pink;"); }
                            }
                            else
                            {
                                ManaArea.Visible = false;
                                if (dtLstat.Rows[0]["leaveAdminNo"].ToString() != "") // Admin Approved or Canceled the Leave
                                {
                                    btnadmCan.Visible = false;
                                    btnadmSub.Visible = false;
                                    txtAdminName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveAdminNo"].ToString() + "'");
                                    txtAdminRemarks.Text = dtLstat.Rows[0]["leaveAdminEval"].ToString();
                                    if (dtLstat.Rows[0]["leaveAdminEval"].ToString() == "1") //If Leave Request Approved by Admin
                                    {
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "0") { chkbxWOPay.Checked = true; chkbxWOPay.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "1") { chkbxAdmVL.Checked = true; chkbxAdmVL.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "2") { chkbxAdmSL.Checked = true; chkbxAdmSL.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "3") { chkbxAdmLL.Checked = true; chkbxAdmLL.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "3") { chkbxAdmSPL.Checked = true; chkbxAdmSPL.Attributes.Add("style", "background-color: green;"); }
                                        chkbxWOPay.Enabled = false;
                                        chkbxAdmVL.Enabled = false;
                                        chkbxAdmSL.Enabled = false;
                                        chkbxAdmLL.Enabled = false;
                                        chkbxAdmSPL.Enabled = false;
                                    }
                                    else //If Leave Request is Canceled by Admin
                                    {
                                        chkbxWOPay.Enabled = false;
                                        chkbxAdmVL.Enabled = false;
                                        chkbxAdmSL.Enabled = false;
                                        chkbxAdmLL.Enabled = false;
                                        chkbxAdmSPL.Enabled = false;
                                        msgAdmin.Visible = true;
                                    }
                                }
                                else
                                {
                                    AdmArea.Visible = false;
                                }
                            }
                           

                        }
                        else
                        {
                            if (dtLstat.Rows[0]["leaveManaNo"].ToString() != "") // Manager Approved or Denied
                            {
                                LeadArea.Visible = false;
                                btnmanSub.Visible = false;
                                txtManaName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveManaNo"].ToString() + "'");
                                txtMRemarks.Text = dtLstat.Rows[0]["leaveManaRemarks"].ToString().Replace("''", "'");
                                if (dtLstat.Rows[0]["leaveManaType"].ToString() == "1")
                                { mapproved.Checked = true; mapproved.Attributes.Add("style", "background-color: green;"); }
                                else if (dtLstat.Rows[0]["leaveManaType"].ToString() == "2") { mdenied.Checked = true; mdenied.Attributes.Add("style", "background-color: pink;"); }
                                if (dtLstat.Rows[0]["leaveAdminNo"].ToString() != "")
                                {
                                    btnadmCan.Visible = false;
                                    btnadmSub.Visible = false;
                                    txtAdminName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveAdminNo"].ToString() + "'");
                                    txtAdminRemarks.Text = dtLstat.Rows[0]["leaveAdminEval"].ToString();
                                    if (dtLstat.Rows[0]["leaveAdminEval"].ToString() == "1") //If Leave Request Approved by Admin
                                    {
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "0") { chkbxWOPay.Checked = true; chkbxWOPay.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "1") { chkbxAdmVL.Checked = true; chkbxAdmVL.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "2") { chkbxAdmSL.Checked = true; chkbxAdmSL.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "3") { chkbxAdmLL.Checked = true; chkbxAdmLL.Attributes.Add("style", "background-color: green;"); }
                                        if (dtLstat.Rows[0]["leaveLeaveCredApp"].ToString() == "3") { chkbxAdmSPL.Checked = true; chkbxAdmSPL.Attributes.Add("style", "background-color: green;"); }
                                        chkbxWOPay.Enabled = false;
                                        chkbxAdmVL.Enabled = false;
                                        chkbxAdmSL.Enabled = false;
                                        chkbxAdmLL.Enabled = false;
                                        chkbxAdmSPL.Enabled = false;
                                    }
                                    else //If Leave Request is Canceled by Admin
                                    {
                                        chkbxWOPay.Enabled = false;
                                        chkbxAdmVL.Enabled = false;
                                        chkbxAdmSL.Enabled = false;
                                        chkbxAdmLL.Enabled = false;
                                        chkbxAdmSPL.Enabled = false;
                                        msgAdmin.Visible = true;
                                    }
                                }
                                else
                                {
                                    AdmArea.Visible = false;
                                }
                            }
                            else
                            {
                                LeadArea.Visible = false;
                                ManaArea.Visible = false;
                                AdmArea.Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    LeadArea.Visible = false;
                    ManaArea.Visible = false;
                    AdmArea.Visible = false;
                }
            }
            else
            {
                if (getUserRole == "RLD" || getUserRole == "RSL")///Leaders
                {
                    if (getUserReportHead == getEmpNo)
                    {
                        txtLeadName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + getEmpNo + "'");
                        sapproved.Enabled = true;
                        sdenied.Enabled = true;
                        txtSRemarks.ReadOnly = false;
                        LeadArea.Visible = true;
                        btnsupSub.Visible = true;
                        leadPanel.Visible = true;
                        btnadmSub.Visible = false;
                        ManaArea.Visible = false;
                        AdmArea.Visible = false;
                    }
                    else
                    {
                        LeadArea.Visible = false;
                        ManaArea.Visible = false;
                        AdmArea.Visible = false;
                    }
                }
                else if (getUserAdmin == "1")
                {
                    LeadArea.Visible = false;
                    ManaArea.Visible = false;
                    AdmArea.Visible = false;
                }
                else if (getUserPos == "ITM" || getUserPos == "TMR" || getUserPos == "SMR" || getUserPos == "TOM" || getUserDept == "MGR") ///Managers
                {
                    txtManaName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + getEmpNo + "'");
                    mapproved.Enabled = true;
                    mdenied.Enabled = true;
                    txtMRemarks.ReadOnly = false;
                    ManaArea.Visible = true;
                    btnmanSub.Visible = true;
                    manaPanel.Visible = true;
                    btnadmSub.Visible = false;
                    LeadArea.Visible = true;
                    leadPanel.Visible = true;
                    btnsupSub.Visible = false;
                    AdmArea.Visible = false;
                    sapproved.Enabled = false;
                    sdenied.Enabled = false;
                    txtSRemarks.ReadOnly = true;
                }
                else
                {
                    LeadArea.Visible = false;
                    ManaArea.Visible = false;
                    AdmArea.Visible = false;
                    btnadmSub.Visible = false;
                }
            }
        }
        private string GetBase64ImageString(string imagePath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
        private void getLeaveInfo()
        {
            try
            {
                dtLeaveQuery = null;
                string dtLeave = "";
                string PicGen = "";
                dtLeave = "Select convert(varchar, a.empdate, 101) as dateFiled, convert(varchar, a.empdate, 108) as timeFiled, convert(varchar, a.empDateFrom, 101) as dateFrom, convert(varchar, a.empDateTo, 101) as dateTo, " +
                          "convert(varchar, a.empTimeFrom, 108) as timeFrom, convert(varchar, a.empTimeTo, 108) as timeTo, " +
                          "a.*, b.empfname, b.emplname, b.EmpDept, b.EmpPos, b.EmpRole, DATEDIFF(day, a.empdatefrom, a.empdateto) + 1 AS days, a.empStatus, " +
                          "(Select top 1 new_fileloc from seihaHRMIS.dbo.emp_pic where empno = b.empno) as 'picloc' from seihaHRMIS.dbo.HRLeaveInfo a  LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " +
                          "where a.identity_column = '" + gtParam + "' order by a.empno";
                DataTable dt = HRMIS.Module.GetData(dtLeave);
                if (dt.Rows.Count > 0)
                {
                    txtEmpNo.Text = dt.Rows[0]["empno"].ToString();
                    txtFname.Text = dt.Rows[0]["empfname"].ToString();
                    txtLname.Text = dt.Rows[0]["emplname"].ToString();
                    txtDateNow.Text = dt.Rows[0]["dateFiled"].ToString() + "-" + dt.Rows[0]["timeFiled"].ToString();
                    txtEmpPos.Text = getPosition(dt.Rows[0]["emppos"].ToString()) + " " + getRole(dt.Rows[0]["emprole"].ToString());
                    txtaddress.Text = dt.Rows[0]["empAdd"].ToString();
                    dateFrom.Text = dt.Rows[0]["dateFrom"].ToString();
                    dateTo.Text = dt.Rows[0]["dateTo"].ToString();
                    txtReason.Text = dt.Rows[0]["empReason"].ToString().Replace("''", "'");
                    getLeaveType(dt.Rows[0]["empTOL"].ToString());
                    timeFrom.Text = dt.Rows[0]["timeFrom"].ToString();
                    timeTo.Text = dt.Rows[0]["timeTo"].ToString();
                    txtStoreDays.Text = dt.Rows[0]["days"].ToString();
                    if (dt.Rows[0]["picloc"].ToString() != "")
                    {
                        string strFilePath = dt.Rows[0]["picloc"].ToString();
                        string base64String = GetBase64ImageString(strFilePath);
                        string dataUri = "data:image/jpeg;base64," + base64String;
                        userpicSide2.ImageUrl = dataUri;
                    }
                    else
                    {
                        userpicSide2.Visible = false;
                    }
                    if (dt.Rows[0]["empStatus"].ToString() == "0")
                    {
                        lblStat.Text = "This Leave is Pending";
                        lblStat.CssClass = "label label-inverse";
                    }
                    else if (dt.Rows[0]["empStatus"].ToString() == "1")
                    {
                        lblStat.Text = "This Leave is Approved";
                        lblStat.CssClass = "label label-success";
                    }
                    else if (dt.Rows[0]["empStatus"].ToString() == "2")
                    {
                        lblStat.Text = "This Leave is Denied";
                        lblStat.CssClass = "label label-danger";
                    }
                    else if (dt.Rows[0]["empStatus"].ToString() == "3")
                    {
                        lblStat.Text = "This Leave is Cancelled";
                        lblStat.CssClass = "label label-info";
                    }
                    getLeaveCredits(txtEmpNo.Text, gtParam);
                    loadGrid(gtParam);
                }

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void getLeaveType(string gLeaveType)
        {
            if (gLeaveType == "vct") { vacation.Checked = true; vacation.Attributes.Add("style", "background-color: yellow;"); }
            else if (gLeaveType == "sck") { sick.Checked = true; sick.Attributes.Add("style", "background-color: yellow;"); }
            else if (gLeaveType == "mty") { maternity.Checked = true; maternity.Attributes.Add("style", "background-color: yellow;"); }
            else if (gLeaveType == "pty") { paternity.Checked = true; paternity.Attributes.Add("style", "background-color: yellow;"); }
            else if (gLeaveType == "emy") { emergency.Checked = true; emergency.Attributes.Add("style", "background-color: yellow;"); }
            else if (gLeaveType == "udt") { undertime.Checked = true; undertime.Attributes.Add("style", "background-color: yellow;"); }
            else if (gLeaveType == "chg") { changeOff.Checked = true; changeOff.Attributes.Add("style", "background-color: yellow;"); }

        }
        protected void lblbtnDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dash");
        }
        protected void lblbtnDash1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Leaves");
        }
        protected void lblbtnDash2_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            string extension = Path.GetExtension(Path.GetFileName(filePath));
            string mimeType = MimeMapping.GetMimeMapping(filePath);
            // Create a new file stream
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // Create a new binary reader
            BinaryReader binaryReader = new BinaryReader(fileStream);
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            if (extension.ToLower() == ".doc" || extension.ToLower() == ".docx")
            {
                Response.ContentType = mimeType;
                Response.AddHeader("Content-Length", fileStream.Length.ToString());
                Response.TransmitFile(filePath);
            }
            else
            {
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.AddHeader("Content-Length", fileStream.Length.ToString());
                Response.TransmitFile(filePath);
            }

            // Write the file to the output stream
            Response.BinaryWrite(binaryReader.ReadBytes((int)fileStream.Length));

            // Close the binary reader and file stream
            binaryReader.Close();
            fileStream.Close();
            // End the response
            Response.End();
        }
        protected void PreviewFile(object sender, EventArgs e)
        {
             // Get the path of the document or picture
            string filePath = (sender as LinkButton).CommandArgument; // Update with your file path
            string modifiedPath = filePath.Replace("\\", "/");
            string fileType = System.IO.Path.GetExtension(filePath);

            // Set the appropriate content type for the response
            string contentType = string.Empty;
            switch (fileType.ToLower())
            {
                case ".pdf":
                    contentType = "application/pdf";
                    break;
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;

                // Add other supported file types here
            }
                //Set the content type and headers for the response
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "inline; filename=" + System.IO.Path.GetFileName(filePath));
                //Write the file content to the response stream
                Response.TransmitFile(filePath);
                // Generate JavaScript code to open the file in a new tab
                string script = "<script>window.open('" + modifiedPath + "', '_blank'); return false;</script>";

                // Register the JavaScript code to be executed on the client side
                Page.ClientScript.RegisterStartupScript(GetType(), "OpenFile", script);
                Response.End();
                //Generate a JavaScript code to open the file in a new tab
            
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string delQuery = "Delete from seihaHRMIS.dbo.HRLeaveInfo where identity_column = '" + gtParam + "'";
                string delQueryDet = "Delete from seihaHRMIS.dbo.HRLeaveDocInfo where empLeaveID = '" + gtParam + "'";
                string delNotInfo = "Delete from seihaHRMIS.dbo.notInfo where notLeaveNo = '" + gtParam + "'";
                string FndDet = "Select * from seihaHRMIS.dbo.HRLeaveDocInfo where empLeaveID = '" + gtParam + "'";
                DataTable dt = HRMIS.Module.GetData(FndDet);
                if (dt.Rows.Count > 0)
                {
                    deleteFolder(dt.Rows[0]["empDocFileFrom"].ToString());
                    HRMIS.Module.gblInsert(delQuery);
                    HRMIS.Module.gblInsert(delQueryDet);
                    HRMIS.Module.gblInsert(delNotInfo);
                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                    "values('D','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + gtParam + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                    HRMIS.Module.gblInsert(LogQuery);
                    Response.Redirect("~/Leaves");
                }
                else
                {
                    HRMIS.Module.gblInsert(delQuery);
                    HRMIS.Module.gblInsert(delNotInfo);
                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                    "values('D','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveApplication', 'HRLeaveInfo', '" + gtParam + "', '" + dateFrom.Text + "', '" + dateTo.Text + "')";
                    HRMIS.Module.gblInsert(LogQuery);
                    Response.Redirect("~/Leaves");
                }

                //string confirmValue = Request.Form["confirm_value"];
                //if (confirmValue == "Yes")
                //{
                    

                //}

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(@"<script> alert('There is something wrong in deleting this leave') </script>");
            }
        }
        private void loadGrid(string strID)
        {
            string dtQry = "";
            dtQry = "Select top 1 empDocFileFrom from seihaHRMIS.dbo.HRLeaveDocInfo where empLeaveID = '" + strID + "'";
            DataTable dt = HRMIS.Module.GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                string[] filePaths = Directory.GetFiles(dt.Rows[0]["empDocFileFrom"].ToString());
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }
                GridView1.DataSource = files;
                GridView1.DataBind();
                for (int x = 0; x < GridView1.Rows.Count; x++)
                {
                    GridView1.Rows[x].Cells[0].Width = 250;
                    GridView1.Rows[x].Cells[1].Width = 100;
                }
            }


        }
        protected void getLeaveCredits(string strEmp, string leaveID)
        {
            try
            {
                getAdminType = HRMIS.Module.GetField("Select leaveAdminType from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + leaveID + "'");
                if (getAdminType == "0")
                {
                    string qryLeave = "Select * from seihaHRMIS.dbo.LeaveCreditInfo where empno = '" + strEmp + "'";
                    DataTable dtLCredit = HRMIS.Module.GetData(qryLeave);
                    if (dtLCredit.Rows.Count > 0)
                    {
                        lblCountVL.Text = dtLCredit.Rows[0]["leavCredVL"].ToString();
                        lblCountSL.Text = dtLCredit.Rows[0]["leavCredSL"].ToString();
                        lblCountLL.Text = dtLCredit.Rows[0]["leavCredLoyal"].ToString();
                        lblCountSPL.Text = dtLCredit.Rows[0]["leavCredSpecL"].ToString();
                        if (Int32.Parse(dtLCredit.Rows[0]["leavCredVL"].ToString()) > 0) //Vacation Leave Credits
                        {
                            chkbxAdmVL.Enabled = true;
                        }
                        else
                        {
                            chkbxAdmVL.Enabled = false;
                        }

                        if (Int32.Parse(dtLCredit.Rows[0]["leavCredSL"].ToString()) > 0) //Sick Leave Credits
                        {
                            chkbxAdmSL.Enabled = true;
                        }
                        else
                        {
                            chkbxAdmSL.Enabled = false;
                        }

                        if (Int32.Parse(dtLCredit.Rows[0]["leavCredLoyal"].ToString()) > 0) //Loyalty Leave Credits
                        {
                            chkbxAdmLL.Enabled = true;
                        }
                        else
                        {
                            chkbxAdmLL.Enabled = false;
                        }

                        if (Int32.Parse(dtLCredit.Rows[0]["leavCredLoyal"].ToString()) > 0) //Special Leave Credits
                        {
                            chkbxAdmSPL.Enabled = true;
                        }
                        else
                        {
                            chkbxAdmSPL.Enabled = false;
                        }

                    }
                    else
                    {
                        chkbxAdmVL.Enabled = false;
                        chkbxAdmSL.Enabled = false;
                        chkbxAdmLL.Enabled = false;
                        chkbxAdmSPL.Enabled = false;
                    }
                }
                else
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
                    else
                    {
                        chkbxAdmVL.Enabled = false;
                        chkbxAdmSL.Enabled = false;
                        chkbxAdmLL.Enabled = false;
                        chkbxAdmSPL.Enabled = false;
                    }

                }
                

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void deleteFolder(string getFold)
        {
            bool exists = System.IO.Directory.Exists(Server.MapPath(getFold));
            if (exists == true)
            {
                string path = Server.MapPath(getFold);
                string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    File.Delete(file);
                }
                //then delete folder
                Directory.Delete(path);
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
        protected void sapproved_Checked(object sender, EventArgs e)
        {
            if (sapproved.Checked == true)
            {
                sdenied.Checked = false;
            }
        }
        protected void sdenied_Cheked(object sender, EventArgs e)
        {
            if (sdenied.Checked == true)
            {
                sapproved.Checked = false;
            }
        }
        protected void mapproved_Checked(object sender, EventArgs e)
        {
            if (mapproved.Checked == true)
            {
                mdenied.Checked = false;
            }
        }
        protected void mdenied_Checked(object sender, EventArgs e)
        {
            if (mdenied.Checked == true)
            {
                mapproved.Checked = false;
            }
        }
        protected void chkReadOnly(object sender, EventArgs e)
        {
            if (chkbxWOPay.Checked == true)
            {
                chkbxAdmVL.Enabled = false;
                chkbxAdmSL.Enabled = false;
                chkbxAdmLL.Enabled = false;
                chkbxAdmSPL.Enabled = false;
            }
            else
            {
                if (lblCountVL.Text != "0")
                {
                    chkbxAdmVL.Enabled = true;
                    if (chkbxAdmVL.Checked == true)
                    {
                        txtAdmVL.ReadOnly = false;
                        txtAdmVL.Text = lblCountVL.Text;
                    }
                    else
                    {
                        txtAdmVL.ReadOnly = true;
                        txtAdmVL.Text = "0";
                    }
                }
                if (lblCountSL.Text != "0")
                {
                    chkbxAdmSL.Enabled = true;
                    if (chkbxAdmSL.Checked == true)
                    {
                        txtAdmSL.ReadOnly = false;
                        txtAdmSL.Text = lblCountSL.Text;
                    }
                    else
                    {
                        txtAdmSL.ReadOnly = true;
                        txtAdmSL.Text = "0";
                    }
                }
                if (lblCountLL.Text != "0")
                {
                    chkbxAdmLL.Enabled = true;
                    if (chkbxAdmLL.Checked == true)
                    {
                        txtAdmLL.ReadOnly = false;
                        txtAdmLL.Text = lblCountLL.Text;
                    }
                    else
                    {
                        txtAdmLL.ReadOnly = true;
                        txtAdmLL.Text = "0";
                    }
                }
                if (lblCountSPL.Text != "0")
                {
                    chkbxAdmSPL.Enabled = true;
                    if (chkbxAdmSPL.Checked == true)
                    {
                        txtAdmSPL.ReadOnly = false;
                        txtAdmSPL.Text = lblCountSPL.Text;
                    }
                    else
                    {
                        txtAdmSPL.ReadOnly = true;
                        txtAdmSPL.Text = "0";
                    }
                }
                
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnID = (Button)sender;
                string getbtnID = btnID.ID;

                string commandArgument = hiddenCommandArgument.Value;
                if(commandArgument == "btnadmSub")
                {
                    if (chkbxAdmVL.Checked == true || chkbxAdmSL.Checked == true || chkbxAdmLL.Checked == true || chkbxAdmSPL.Checked == true)
                    {
                        int calLeaveAmount = 0;
                        string qryLeave = "Select * from seihaHRMIS.dbo.LeaveCreditInfo where empno = '" + txtEmpNo.Text + "'";
                        DataTable dtLCredit = HRMIS.Module.GetData(qryLeave);
                        if (dtLCredit.Rows.Count > 0)
                        {
                            int cntVL = 0;
                            int cntSL = 0;
                            int cntLL = 0;
                            int cntSPL = 0;
                            if (chkbxAdmVL.Checked == true) { cntVL = Int32.Parse(txtAdmVL.Text); } else { cntVL = Int32.Parse(lblCountVL.Text); }
                            if (chkbxAdmSL.Checked == true) { cntSL = Int32.Parse(txtAdmSL.Text); } else { cntSL = Int32.Parse(lblCountSL.Text); }
                            if (chkbxAdmLL.Checked == true) { cntLL = Int32.Parse(txtAdmLL.Text); } else { cntLL = Int32.Parse(lblCountLL.Text); }
                            if (chkbxAdmSPL.Checked == true) { cntSPL = Int32.Parse(txtAdmSPL.Text); } else { cntSPL = Int32.Parse(lblCountSPL.Text); }

                            calLeaveAmount = (cntVL + cntSL + cntLL + cntSPL) - (Int32.Parse(lblCountVL.Text) + Int32.Parse(lblCountSL.Text) + Int32.Parse(lblCountLL.Text) + Int32.Parse(lblCountSPL.Text));
                            string updateLeaveCredit = "Update seihaHRMIS.dbo.LeaveCreditInfo set leavCredVL = " + cntVL + ", leavCredSL = " + cntSL + ", leavCredLoyal = " + cntLL + ", leavCredSpecL = " + cntSPL + " where identity_column = '" + dtLCredit.Rows[0]["identity_column"].ToString() + "'";
                            HRMIS.Module.gblInsert(updateLeaveCredit);
                        }
                        else
                        {
                            calLeaveAmount = 0;
                        }
                        
                        string fndLeaveStat = "Select identity_column from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + gtParam + "'";
                        string fndGet = HRMIS.Module.GetField(fndLeaveStat);
                        if(fndGet != "")
                        {
                            string UpdateLeaveStat = "Update seihaHRMIS.dbo.leaveStatInfo set leaveLeaveCredApp = " + calLeaveAmount + ", leaveAdminEval='" + txtAdminRemarks.Text.Replace("'", "''") + "', leaveAdminNo = '" + getEmpNo + "', leaveAdminStat = 1, leaveAdminType = '1' where identity_column = '" + fndGet + "'";
                            HRMIS.Module.gblInsert(UpdateLeaveStat);
                        }
                        else
                        {
                            //                                                                0           1           2            3               4                5          6              7                  8               9                 10             11            12             13 
                            string saveLeaveStat = "Insert into seihaHRMIS.dbo.leaveStatInfo(empno, leaveLeaveNo, leaveSupNo, leaveSupType, leaveSupRemarks, leaveManaNo, leaveManaType, leaveManaRemarks, leaveLeaveStat, leaveLeaveCredApp, leaveAdminEval, leaveAdminNo, leaveAdminType, leaveAdminStat) " +
                                                "values('" + txtEmpNo.Text + "', '" + gtParam + "', '', 0, '', '', 0, '', 2,  " + calLeaveAmount + ", '" + txtAdminRemarks.Text.Replace("'", "''") + "', '" + getEmpNo + "', '1', 1)";
                            //                                    0                     1            2  3  4   5   6   7  8               9                             10                                      11           12  13 
                            HRMIS.Module.gblInsert(saveLeaveStat);
                        }
                        string whatType = "";
                        if (chkbxAdmVL.Checked == true)
                        { if (whatType == "") { whatType = whatType + "Vacation Leave"; } else { whatType = whatType + ",Vacation Leave"; } } 
                        if (chkbxAdmSL.Checked == true)
                        { if (whatType == "") { whatType = whatType + "Sick Leave"; } else { whatType = whatType + ",Sick Leave"; } }
                        if (chkbxAdmLL.Checked == true)
                        { if (whatType == "") { whatType = whatType + "Loyalty Leave"; } else { whatType = whatType + ",Loyalty Leave"; } }
                        if(chkbxAdmSPL.Checked == true)
                        { if (whatType == "") { whatType = whatType + "Special Leave"; } else { whatType = whatType + ",Special Leave"; } }

                        string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                          "values('LA','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveDetail', 'leaveStatInfo', '" + gtParam + "', 'Approved', '" + txtAdminRemarks.Text.Replace("'", "''") + "-" + whatType + "')";
                        HRMIS.Module.gblInsert(LogQuery);
                        string saveNotify = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                "values('" + txtEmpNo.Text + "', '" + gtParam + "', '" + getEmpNo + "', 'Approved by Admin', 2, 1, '" + DateTime.Now + "')";
                        HRMIS.Module.gblInsert(saveNotify);
                    }
                    else if (chkbxWOPay.Checked == true)
                    {
                        string fndLeaveStat = "Select identity_column from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + gtParam + "'";
                        string fndGet = HRMIS.Module.GetField(fndLeaveStat);
                        if (fndGet != "")
                        {
                            string UpdateLeaveStat = "Update seihaHRMIS.dbo.leaveStatInfo set  leaveAdminEval='" + txtAdminRemarks.Text.Replace("'", "''") + "', leaveAdminNo = '" + getEmpNo + "', leaveAdminStat = 1, leaveAdminType = '1' where identity_column = '" + fndGet + "'";
                            HRMIS.Module.gblInsert(UpdateLeaveStat);
                        }
                        else
                        {
                            //                                                                0           1           2            3               4                5          6              7                  8               9                 10             11            12             13 
                            string saveLeaveStat = "Insert into seihaHRMIS.dbo.leaveStatInfo(empno, leaveLeaveNo, leaveSupNo, leaveSupType, leaveSupRemarks, leaveManaNo, leaveManaType, leaveManaRemarks, leaveLeaveStat, leaveLeaveCredApp, leaveAdminEval, leaveAdminNo, leaveAdminType, leaveAdminStat) " +
                                                "values('" + txtEmpNo.Text + "', '" + gtParam + "', '', 0, '', '', 0, '', 2, 0, '" + txtAdminRemarks.Text.Replace("'", "''") + "', '" + getEmpNo + "', '1', 1)";
                            //                                    0                     1            2  3  4   5   6   7  8  9                             10                                      11           12  13 
                            HRMIS.Module.gblInsert(saveLeaveStat);
                        }
                        string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                          "values('LA','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveDetail', 'leaveStatInfo', '" + gtParam + "', 'Approved', '" + txtAdminRemarks.Text.Replace("'", "''") + " - Without Pay')";
                        HRMIS.Module.gblInsert(LogQuery);
                        string saveNotify = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                "values('" + txtEmpNo.Text + "', '" + gtParam + "', '" + getEmpNo + "', 'Approved by Admin', 2, 1, '" + DateTime.Now + "')";
                        HRMIS.Module.gblInsert(saveNotify);
                    }
                }
                else if (commandArgument == "btnadmCan")
                {
                    string saveNotify = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                "values('" + txtEmpNo.Text + "', '" + gtParam + "', '" + getEmpNo + "', 'Canceled', 3, 1, '" + DateTime.Now + "')";
                    HRMIS.Module.gblInsert(saveNotify);
                    string fndLeaveStat = "Select identity_column from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + gtParam + "'";
                    string fndGet = HRMIS.Module.GetField(fndLeaveStat);
                    if (fndGet == "")
                    {
                        //                                                                0           1           2            3               4                5          6              7                  8               9                 10             11            12             13 
                        string saveLeaveStat = "Insert into seihaHRMIS.dbo.leaveStatInfo(empno, leaveLeaveNo, leaveSupNo, leaveSupType, leaveSupRemarks, leaveManaNo, leaveManaType, leaveManaRemarks, leaveLeaveStat, leaveLeaveCredApp, leaveAdminEval, leaveAdminNo, leaveAdminType, leaveAdminStat) " +
                                            "values('" + txtEmpNo.Text + "', '" + gtParam + "', '', 0, '', '', 0, '', 3, 0, '" + txtAdminRemarks.Text.Replace("'", "''") + "', '" + getEmpNo + "', '2', 2)";
                        //                                    0                     1            2  3  4   5   6   7  8  9                         10                                  11           12  13 
                        HRMIS.Module.gblInsert(saveLeaveStat);
                    }
                    else
                    {
                        string UpdateLeaveStat = "Update seihaHRMIS.dbo.leaveStatInfo set leaveLeaveStat = 3,leaveAdminEval='" + txtAdminRemarks.Text.Replace("'","''") + "', leaveAdminNo = '" + getEmpNo + "', leaveAdminStat = 2, leaveAdminType = '2' where identity_column = '" + fndGet + "'";
                        HRMIS.Module.gblInsert(UpdateLeaveStat);
                    }
                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                                                        "values('LD','" + DateTime.Now + "', '" + txtEmpNo.Text + "', '" + ipAdd + '-' + Environment.MachineName + "', 'LeaveDetail', 'leaveStatInfo', '" + gtParam + "', 'Canceled', '" + txtAdminRemarks.Text.Replace("'", "''") + "')";
                    HRMIS.Module.gblInsert(LogQuery);

                    string updateLeaveInfo = "update seihaHRMIS.dbo.HRLeaveInfo set empStatus = 3 where identity_column = '" + gtParam + "'";
                    HRMIS.Module.gblInsert(updateLeaveInfo);

                    Response.Redirect(Request.RawUrl);
                    
                }
                else
                {
                    if (mapproved.Enabled == true || mdenied.Enabled == true)
                    {
                        if (mapproved.Checked == true || mdenied.Checked == true)
                        {
                            string strType = "";
                            int statType = 0;
                            if (mapproved.Checked == true)
                            {
                                strType = "Approved";
                                statType = 1;
                            }
                            else
                            {
                                strType = "Denied";
                                statType = 2;
                            }
                            string ifNotEmpty = HRMIS.Module.GetField("Select empManaEmpNo from seihaHRMIS.dbo.HRLeaveInfo where identity_column = '" + gtParam + "'");
                            if (ifNotEmpty == "")
                            {
                                string saveNotify = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                "values('" + txtEmpNo.Text + "', '" + gtParam + "', '" + getEmpNo + "', '" + strType + "', " + statType + ", 1, '" + DateTime.Now + "')";
                                HRMIS.Module.gblInsert(saveNotify);

                                string fndLeaveStat = "Select identity_column from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + gtParam + "'";
                                string fndGet = HRMIS.Module.GetField(fndLeaveStat);
                                if (fndGet == "")
                                {
                                    string saveLeaveStat = "Insert into seihaHRMIS.dbo.leaveStatInfo(empno, leaveLeaveNo, leaveSupNo, leaveSupType, leaveSupRemarks, leaveManaNo, leaveManaType, leaveManaRemarks, leaveLeaveStat, leaveLeaveCredApp, leaveAdminEval, leaveAdminNo, leaveAdminType, leaveAdminStat) " +
                                                        "values('" + txtEmpNo.Text + "', '" + gtParam + "', '', 0, '', '" + getEmpNo + "', " + statType + ", '" + txtMRemarks.Text.Replace("'", "''") + "', " + statType + ", 0, '', '', '0', 0)";
                                    HRMIS.Module.gblInsert(saveLeaveStat);
                                }
                                else
                                {
                                    string UpdateLeaveStat = "Update seihaHRMIS.dbo.leaveStatInfo set leaveManaNo = '" + getEmpNo + "', leaveManaType = " + statType + ", leaveManaRemarks = '" + txtMRemarks.Text.Replace("'", "''") + "', leaveLeaveStat = " + statType + " where identity_column = '" + fndGet + "'";
                                    HRMIS.Module.gblInsert(UpdateLeaveStat);
                                }
                                string updateLeaveInfo = "update seihaHRMIS.dbo.HRLeaveInfo set empManaEmpNo = '" + getEmpNo + "', empManaStatus = " + statType + ", empStatus = " + statType + " where identity_column = '" + gtParam + "'";
                                HRMIS.Module.gblInsert(updateLeaveInfo);
                                Response.Redirect(Request.RawUrl);
                            }
                            
                        }
                    }
                    else if (sapproved.Enabled == true || sdenied.Enabled == true)
                    {
                        if (sapproved.Checked == true || sdenied.Checked == true)
                        {
                            string strType = "";
                            int statType = 0;
                            if (sapproved.Checked == true)
                            {
                                strType = "Approved";
                                statType = 1;
                            }
                            else
                            {
                                strType = "Denied";
                                statType = 2;
                            }
                            string saveNotify = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                                "values('" + txtEmpNo.Text + "', '" + gtParam + "', '" + getEmpNo + "', '" + strType + "', " + statType + ", 1, '" + DateTime.Now + "')";
                            HRMIS.Module.gblInsert(saveNotify);

                            string fndLeaveStat = "Select identity_column from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + gtParam + "'";
                            string fndGet = HRMIS.Module.GetField(fndLeaveStat);
                            if (fndGet == "")
                            {
                                string saveLeaveStat = "Insert into seihaHRMIS.dbo.leaveStatInfo(empno, leaveLeaveNo, leaveSupNo, leaveSupType, leaveSupRemarks, leaveManaNo, leaveManaType, leaveManaRemarks, leaveLeaveStat, leaveLeaveCredApp, leaveAdminEval, leaveAdminNo, leaveAdminType, leaveAdminStat) " +
                                                    "values('" + txtEmpNo.Text + "', '" + gtParam + "', '" + getEmpNo + "', " + statType + ", '" + txtSRemarks.Text.Replace("'", "''") + "', '', 0, '', 0, 0, '', '', '0', 0)";
                                HRMIS.Module.gblInsert(saveLeaveStat);
                            }
                            string updateLeaveInfo = "update seihaHRMIS.dbo.HRLeaveInfo set empHeadStatus = " + statType + " where identity_column = '" + gtParam + "'";
                            HRMIS.Module.gblInsert(updateLeaveInfo);
                            Response.Redirect(Request.RawUrl);
                        }
                    }
                }

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
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
}
