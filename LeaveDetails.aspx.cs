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


public partial class LeaveDetails : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection();
    OleDbCommand sqlComm = new OleDbCommand();
    OleDbDataAdapter sqlDA = new OleDbDataAdapter();
    private static string strcCon = "";
    private static DataTable dtEmpQuery;
    private static DataTable dtLeaveQuery;
    private static string gtParam = "";
    string getEmpNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/404");
        //getEmpNo = Session["Uname"] as string;
        //Session["hldLeave"] = Request.QueryString["param"];
        //if (string.IsNullOrEmpty(getEmpNo))
        //{
        //    Session.Abandon();
        //    Response.Redirect("~/Login");
        //}
        //else
        //{
        //    gtParam = Session["hldLeave"] as string;
        //    getUserInfo();
        //    getLeaveInfo();
        //    loadGrid(gtParam);
        //}
        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myModal", "$('#myModal').modal('show');", true);
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
    protected void HRChoiceLeave_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if(HRChoiceLeave.SelectedIndex == 1)
        //{
        //    noOfLeave.Text = lblVacL.Text;
        //}
        //else if(HRChoiceLeave.SelectedIndex == 2)
        //{
        //    noOfLeave.Text = lblSckL.Text;
        //}
        //else if(HRChoiceLeave.SelectedIndex == 3)
        //{
        //    noOfLeave.Text = lblLolL.Text;
        //}
        //else if(HRChoiceLeave.SelectedIndex == 4)
        //{
        //    noOfLeave.Text = lblSpcL.Text;
        //}
        //else if(HRChoiceLeave.SelectedIndex == 5)
        //{
        //    noOfLeave.Text = "0";
        //}
        //else if(HRChoiceLeave.SelectedIndex == 0)
        //{
        //    noOfLeave.Text = "";
        //}
    }
    private void populatePosDept()
    {
        try
        {
            //department.DataSource = null;
            //string dtQry = "";
            //dtQry = "Select cspopuptext, identity_column from seihaHRMIS.dbo.cspopup where cspopupfor = 'DPT' order by identity_column";
            //DataTable dt = GetData(dtQry);
            //if (dt.Rows.Count > 0)
            //{
            //    department.DataSource = dt;
            //    department.DataTextField = "cspopuptext";
            //    department.DataValueField = "identity_column";
            //    department.DataBind();
            //}
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
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
                    Response.Redirect("~/Leave");
                }
                else
                {
                    HRMIS.Module.gblInsert(delQuery);
                    HRMIS.Module.gblInsert(delNotInfo);
                    Response.Redirect("~/Leave");
                }
                
            }
            
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(@"<script> alert('There is something wrong in deleting this leave') </script>");
        }
    }
    protected void deleteFolder(string getFold)
    {
        bool exists = System.IO.Directory.Exists(Server.MapPath(getFold));
        if(exists == true)
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
    private void getUserInfo()
    {
        try
        {
            dtEmpQuery = null;
            string dtEmp = "";
            dtEmp = "Select (EmpFName + ' ' + EmpLName) as Name, * from seihaHRMIS.dbo.HREmpInfo a where empno = '" + getEmpNo + "' order by empno";
            DataTable dt = HRMIS.Module.GetData(dtEmp);
                if (dt.Rows.Count > 0)
                {
                    
                    lblUser.Text = dt.Rows[0]["empFname"].ToString();
                    if (dt.Rows[0]["empGen"].ToString() == "0")
                    {
                        UserPic.Attributes.Add("src", "images/img_avatar.png");
                    }
                    else
                    {
                        UserPic.Attributes.Add("src", "images/img_avatar2.png");
                    }
                    string strIfMe = "";
                    strIfMe = "Select * from seihaHRMIS.dbo.HRLeaveInfo where identity_column = '" + gtParam + "'";
                    DataTable dtFM = HRMIS.Module.GetData(strIfMe);
                    if(dtFM.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["empno"].ToString() == dtFM.Rows[0]["empNo"].ToString())
                        {
                            sapproved.Enabled = false;
                            sdenied.Enabled = false;
                            mapproved.Enabled = false;
                            mdenied.Enabled = false;
                            txtSRemarks.ReadOnly = true;
                            txtMRemarks.ReadOnly = true;

                            ///-------if leave is the owner--------////

                            if (dtFM.Rows[0]["empStatus"].ToString() == "0")
                            {
                                adminSection.Visible = true;
                                btnPrint.Visible = false;
                                btnDelete.Visible = true;
                            }
                            else
                            {
                                adminSection.Visible = false;
                                btnPrint.Visible = false;
                                btnDelete.Visible = false;
                            }
                            

                        }
                        else
                        {
                            adminSection.Visible = false;
                            btnPrint.Visible = false;
                            if (dt.Rows[0]["EmpRole"].ToString() == "RLD" || dt.Rows[0]["EmpRole"].ToString() == "RSL")
                            {
                                string qryLStat = "Select identity_column from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + gtParam + "'";
                                string fndLstat = HRMIS.Module.GetField(qryLStat);
                                if(fndLstat != "")
                                {
                                    string qryStr = "Select * from seihaHRMIS.dbo.leaveStatInfo where identity_column = '" + fndLstat+ "'";
                                    DataTable dtLstat = HRMIS.Module.GetData(qryStr);
                                    if(dtLstat.Rows.Count > 0)
                                    {
                                        sapproved.Enabled = false;
                                        sdenied.Enabled = false;
                                        mapproved.Enabled = false;
                                        mdenied.Enabled = false;
                                        txtSRemarks.ReadOnly = true;
                                        txtMRemarks.ReadOnly = true;
                                        ///----------Leaders----------------
                                        if(dtLstat.Rows[0]["leaveSupNo"].ToString() != "")
                                        { SupName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveSupNo"].ToString() + "'"); }
                                        if(dtLstat.Rows[0]["leaveSupType"].ToString() == "1")
                                        { sapproved.Checked = true; sapproved.Attributes.Add("style", "background-color: green;"); }
                                        else if (dtLstat.Rows[0]["leaveSupType"].ToString() == "2") { sdenied.Checked = true; sdenied.Attributes.Add("style", "background-color: pink;"); }
                                        txtSRemarks.Text = dtLstat.Rows[0]["leaveSupRemarks"].ToString().Replace("''", "'");
                                        ///----------Manager----------------
                                        if (dtLstat.Rows[0]["leaveManaNo"].ToString() != "")
                                        { ManName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveManaNo"].ToString() + "'"); }
                                        if (dtLstat.Rows[0]["leaveManaType"].ToString() == "1")
                                        { mapproved.Checked = true; mapproved.Attributes.Add("style", "background-color: green;"); }
                                        else if (dtLstat.Rows[0]["leaveManaType"].ToString() == "2") { mdenied.Checked = true; mdenied.Attributes.Add("style", "background-color: pink;"); }
                                        txtMRemarks.Text = dtLstat.Rows[0]["leaveManaRemarks"].ToString().Replace("''", "'");
                                    }
                                }
                                else
                                {
                                    if (dtFM.Rows[0]["empHeadEmpNo"].ToString() == getEmpNo)
                                    {
                                        sapproved.Enabled = true;
                                        sdenied.Enabled = true;
                                        mapproved.Enabled = false;
                                        mdenied.Enabled = false;
                                        txtSRemarks.ReadOnly = false;
                                        txtMRemarks.ReadOnly = true;
                                        btnSaveSup.Visible = true;
                                        SupName.Text = dt.Rows[0]["Name"].ToString();
                                    }
                                    else
                                    {
                                        sapproved.Enabled = false;
                                        sdenied.Enabled = false;
                                        mapproved.Enabled = false;
                                        mdenied.Enabled = false;
                                        txtSRemarks.ReadOnly = true;
                                        txtMRemarks.ReadOnly = true;
                                    }
                                }
                            } ///---------------------End Role---------------------------
                            else
                            {
                                if (dt.Rows[0]["EmpDept"].ToString() == "MGR")
                                {
                                    string qryLStat = "Select identity_column from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + gtParam + "'";
                                    string fndLstat = HRMIS.Module.GetField(qryLStat);
                                    if (fndLstat != "")
                                    {
                                        string qryStr = "Select * from seihaHRMIS.dbo.leaveStatInfo where identity_column = '" + fndLstat + "'";
                                        DataTable dtLstat = HRMIS.Module.GetData(qryStr);
                                        if (dtLstat.Rows.Count > 0)
                                        {
                                           
                                            ///----------Leaders----------------
                                            if (dtLstat.Rows[0]["leaveSupNo"].ToString() != "")
                                            { SupName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveSupNo"].ToString() + "'"); }
                                            if (dtLstat.Rows[0]["leaveSupType"].ToString() == "1")
                                            { sapproved.Checked = true; sapproved.Attributes.Add("style", "background-color: green;"); }
                                            else if (dtLstat.Rows[0]["leaveSupType"].ToString() == "2") { sdenied.Checked = true; sdenied.Attributes.Add("style", "background-color: pink;"); }
                                            txtSRemarks.Text = dtLstat.Rows[0]["leaveSupRemarks"].ToString().Replace("''", "'");
                                            ///----------Manager----------------
                                            if (dtLstat.Rows[0]["leaveManaNo"].ToString() != "")
                                            {
                                                sapproved.Enabled = false;
                                                sdenied.Enabled = false;
                                                mapproved.Enabled = false;
                                                mdenied.Enabled = false;
                                                txtSRemarks.ReadOnly = true;
                                                txtMRemarks.ReadOnly = true;
                                                ManName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveManaNo"].ToString() + "'");
                                                if (dtLstat.Rows[0]["leaveManaType"].ToString() == "1")
                                                { mapproved.Checked = true; mapproved.Attributes.Add("style", "background-color: green;"); }
                                                else if (dtLstat.Rows[0]["leaveManaType"].ToString() == "2") { mdenied.Checked = true; mdenied.Attributes.Add("style", "background-color: pink;"); }
                                                txtMRemarks.Text = dtLstat.Rows[0]["leaveManaRemarks"].ToString().Replace("''", "'");
                                            }
                                            else
                                            {
                                                sapproved.Enabled = false;
                                                sdenied.Enabled = false;
                                                mapproved.Enabled = true;
                                                mdenied.Enabled = true;
                                                txtSRemarks.ReadOnly = true;
                                                txtMRemarks.ReadOnly = false;
                                                btnSaveMana.Visible = true;
                                                ManName.Text = dt.Rows[0]["Name"].ToString();
                                            }
                                            
                                        }
                                    }
                                    else
                                    {
                                        sapproved.Enabled = false;
                                        sdenied.Enabled = false;
                                        mapproved.Enabled = true;
                                        mdenied.Enabled = true;
                                        txtSRemarks.ReadOnly = true;
                                        txtMRemarks.ReadOnly = false;
                                        btnSaveMana.Visible = true;
                                        ManName.Text = dt.Rows[0]["Name"].ToString();
                                    }
                                   
                                }
                                else if (dt.Rows[0]["EmpDept"].ToString() == "IT" || dt.Rows[0]["EmpDept"].ToString() == "FCT")
                                {
                                    if (dt.Rows[0]["EmpPos"].ToString() == "ITM" || dt.Rows[0]["EmpPos"].ToString() == "TMR" || dt.Rows[0]["EmpPos"].ToString() == "SMR")
                                    {
                                        string qryLStat = "Select identity_column from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + gtParam + "'";
                                        string fndLstat = HRMIS.Module.GetField(qryLStat);
                                        if (fndLstat != "")
                                        {
                                            string qryStr = "Select * from seihaHRMIS.dbo.leaveStatInfo where identity_column = '" + fndLstat + "'";
                                            DataTable dtLstat = HRMIS.Module.GetData(qryStr);
                                            if (dtLstat.Rows.Count > 0)
                                            {

                                                ///----------Leaders----------------
                                                if (dtLstat.Rows[0]["leaveSupNo"].ToString() != "")
                                                { SupName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveSupNo"].ToString() + "'"); }
                                                if (dtLstat.Rows[0]["leaveSupType"].ToString() == "1")
                                                { sapproved.Checked = true; sapproved.Attributes.Add("style", "background-color: green;"); }
                                                else if (dtLstat.Rows[0]["leaveSupType"].ToString() == "2") { sdenied.Checked = true; sdenied.Attributes.Add("style", "background-color: pink;"); }
                                                txtSRemarks.Text = dtLstat.Rows[0]["leaveSupRemarks"].ToString().Replace("''", "'");
                                                ///----------Manager----------------
                                                if (dtLstat.Rows[0]["leaveManaNo"].ToString() != "")
                                                {
                                                    sapproved.Enabled = false;
                                                    sdenied.Enabled = false;
                                                    mapproved.Enabled = false;
                                                    mdenied.Enabled = false;
                                                    txtSRemarks.ReadOnly = true;
                                                    txtMRemarks.ReadOnly = true;
                                                    ManName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveManaNo"].ToString() + "'");
                                                    if (dtLstat.Rows[0]["leaveManaType"].ToString() == "1")
                                                    { mapproved.Checked = true; mapproved.Attributes.Add("style", "background-color: green;"); }
                                                    else if (dtLstat.Rows[0]["leaveManaType"].ToString() == "2") { mdenied.Checked = true; mdenied.Attributes.Add("style", "background-color: pink;"); }
                                                    txtMRemarks.Text = dtLstat.Rows[0]["leaveManaRemarks"].ToString().Replace("''", "'");
                                                }
                                                else
                                                {
                                                    sapproved.Enabled = false;
                                                    sdenied.Enabled = false;
                                                    mapproved.Enabled = true;
                                                    mdenied.Enabled = true;
                                                    txtSRemarks.ReadOnly = true;
                                                    txtMRemarks.ReadOnly = false;
                                                    btnSaveMana.Visible = true;
                                                    ManName.Text = dt.Rows[0]["Name"].ToString();
                                                }

                                            }
                                        }
                                        else
                                        {
                                            sapproved.Enabled = false;
                                            sdenied.Enabled = false;
                                            mapproved.Enabled = true;
                                            mdenied.Enabled = true;
                                            txtSRemarks.ReadOnly = true;
                                            txtMRemarks.ReadOnly = false;
                                            btnSaveMana.Visible = true;
                                            ManName.Text = dt.Rows[0]["Name"].ToString();
                                        }
                                        
                                    }
                                    else if (dt.Rows[0]["EmpPos"].ToString() == "SMR")
                                    {
                                        sapproved.Enabled = true;
                                        sdenied.Enabled = true;
                                        mapproved.Enabled = false;
                                        mdenied.Enabled = false;
                                        txtSRemarks.ReadOnly = false;
                                        txtMRemarks.ReadOnly = true;
                                        btnSaveSup.Visible = true;
                                        SupName.Text = dt.Rows[0]["Name"].ToString();
                                    }
                                }
                                else
                                {
                                    sapproved.Enabled = false;
                                    sdenied.Enabled = false;
                                    mapproved.Enabled = false;
                                    mdenied.Enabled = false;
                                    txtSRemarks.ReadOnly = true;
                                    txtMRemarks.ReadOnly = true;
                                }
                            }
                        }
                        
                    }
                    else
                    {
                        if (dt.Rows[0]["EmpRole"].ToString() == "RLD")
                        {
                            if (dt.Rows[0]["EmpReportHead"].ToString() == getEmpNo)
                            {
                
                                sapproved.Enabled = true;
                                sdenied.Enabled = true;
                                mapproved.Enabled = false;
                                mdenied.Enabled = false;
                                txtSRemarks.ReadOnly = false;
                                txtMRemarks.ReadOnly = true;
                                btnSaveSup.Visible = true;
                                SupName.Text = dt.Rows[0]["Name"].ToString();
                            }
                            else
                            {
                                sapproved.Enabled = false;
                                sdenied.Enabled = false;
                                mapproved.Enabled = false;
                                mdenied.Enabled = false;
                                txtSRemarks.ReadOnly = true;
                                txtMRemarks.ReadOnly = true;
                            }
                        }
                        else
                        {
                            if (dt.Rows[0]["EmpDept"].ToString() == "MGR")
                            {
                                sapproved.Enabled = false;
                                sdenied.Enabled = false;
                                mapproved.Enabled = true;
                                mdenied.Enabled = true;
                                txtSRemarks.ReadOnly = true;
                                txtMRemarks.ReadOnly = false;
                                btnSaveMana.Visible = true;
                                ManName.Text = dt.Rows[0]["Name"].ToString();
                            }
                            else if (dt.Rows[0]["EmpDept"].ToString() == "IT" || dt.Rows[0]["EmpDept"].ToString() == "FCT")
                            {
                                if (dt.Rows[0]["EmpPos"].ToString() == "ITM" || dt.Rows[0]["EmpPos"].ToString() == "TMR")
                                {
                                    sapproved.Enabled = false;
                                    sdenied.Enabled = false;
                                    mapproved.Enabled = true;
                                    mdenied.Enabled = true;
                                    txtSRemarks.ReadOnly = true;
                                    txtMRemarks.ReadOnly = false;
                                    btnSaveMana.Visible = true;
                                    ManName.Text = dt.Rows[0]["Name"].ToString();
                                }
                                //else
                                //{
                                //    //forMana.Visible = false;
                                //    //sapproved.Enabled = false;
                                //    //sdenied.Enabled = false;
                                //    //mapproved.Enabled = false;
                                //    //mdenied.Enabled = false;
                                //    //txtSRemarks.ReadOnly = false;
                                //    //txtMRemarks.ReadOnly = false;
                                //    //ManName.Text = dt.Rows[0]["Name"].ToString();
                                //}
                            }
                            else
                            {
                                sapproved.Enabled = false;
                                sdenied.Enabled = false;
                                mapproved.Enabled = false;
                                mdenied.Enabled = false;
                                txtSRemarks.ReadOnly = true;
                                txtMRemarks.ReadOnly = true;
                                //ManName.Text = dt.Rows[0]["Name"].ToString();
                            }
                        }
                    }
                    if(dt.Rows[0]["empAdmin"].ToString() == "1")
                    {
                        btnPrint.Visible = true;
                        lblReg.Visible = true;

                        string qryLStat = "Select leaveAdminStat from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + gtParam + "'";
                        string fndLstat = HRMIS.Module.GetField(qryLStat);
                        if(fndLstat == "1")
                        {
                            adminSection.Visible = false;
                            adApproWho.Visible = true;
                        }
                        else
                        {
                            adminSection.Visible = true;
                            if (dtFM.Rows.Count > 0)
                            {
                                if (dtFM.Rows[0]["empStatus"].ToString() == "1")
                                {
                                    forAdminSection.Visible = true;
                                    hrName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as Name, * from seihaHRMIS.dbo.HREmpInfo a where empno = '" + getEmpNo + "' order by empno");
                                    string qryLStat2 = "Select identity_column from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + gtParam + "'";
                                    string fndLstat2 = HRMIS.Module.GetField(qryLStat2);
                                    if (fndLstat2 != "")
                                    {
                                        string qryStr = "Select * from seihaHRMIS.dbo.leaveStatInfo where identity_column = '" + fndLstat2 + "'";
                                        DataTable dtLstat = HRMIS.Module.GetData(qryStr);
                                        if (dtLstat.Rows.Count > 0)
                                        {
                                            if (dtLstat.Rows[0]["leaveSupNo"].ToString() != "")
                                            { SupName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveSupNo"].ToString() + "'"); }
                                            if (dtLstat.Rows[0]["leaveSupType"].ToString() == "1")
                                            { sapproved.Checked = true; sapproved.Attributes.Add("style", "background-color: green;"); }
                                            else if (dtLstat.Rows[0]["leaveSupType"].ToString() == "2") { sdenied.Checked = true; sdenied.Attributes.Add("style", "background-color: pink;"); }
                                            txtSRemarks.Text = dtLstat.Rows[0]["leaveSupRemarks"].ToString().Replace("''", "'");
                                            ///----------Manager----------------
                                            if (dtLstat.Rows[0]["leaveManaNo"].ToString() != "")
                                            {
                                                ManName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) as name from seihaHRMIS.dbo.HREmpInfo where empno = '" + dtLstat.Rows[0]["leaveManaNo"].ToString() + "'");
                                                if (dtLstat.Rows[0]["leaveManaType"].ToString() == "1")
                                                { mapproved.Checked = true; mapproved.Attributes.Add("style", "background-color: green;"); }
                                                else if (dtLstat.Rows[0]["leaveManaType"].ToString() == "2") { mdenied.Checked = true; mdenied.Attributes.Add("style", "background-color: pink;"); }
                                                txtMRemarks.Text = dtLstat.Rows[0]["leaveManaRemarks"].ToString().Replace("''", "'");
                                            }
                                            else
                                            {
                                                ManName.Text = dt.Rows[0]["Name"].ToString();
                                            }
                                        }
                                    }
                                   
                                }
                                else
                                {
                                    forAdminSection.Visible = false;
                                }
                            }
                            else
                            {
                                forAdminSection.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        lblReg.Visible = false;
                    }
                    
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
    private void getLeaveInfo()
    {
        try
        {
            dtLeaveQuery = null;
            string dtLeave = "";
            dtLeave = "Select convert(varchar, a.empdate, 101) as dateFiled, convert(varchar, a.empDateFrom, 101) as dateFrom, convert(varchar, a.empDateTo, 101) as dateTo, " +
                      "convert(varchar, a.empTimeFrom, 108) as timeFrom, convert(varchar, a.empTimeTo, 108) as timeTo, " +
                      "a.*, b.empfname, b.emplname, b.EmpDept, b.EmpPos, b.EmpRole, DATEDIFF(day, a.empdatefrom, a.empdateto) + 1 AS days from seihaHRMIS.dbo.HRLeaveInfo a  LEFT JOIN seihaHRMIS.dbo.HREmpInfo b ON a.empno = b.empno " + 
                      "where a.identity_column = '" + gtParam + "' order by a.empno";
            DataTable dt = HRMIS.Module.GetData(dtLeave);
            if (dt.Rows.Count > 0)
            {
                empno.Text = dt.Rows[0]["empno"].ToString();
                fname.Text = dt.Rows[0]["empfname"].ToString();
                lname.Text = dt.Rows[0]["emplname"].ToString();
                DateFiled.Text = dt.Rows[0]["dateFiled"].ToString();
                position.Text = getPosition(dt.Rows[0]["emppos"].ToString()) + " " + getRole(dt.Rows[0]["emprole"].ToString());
                address.Text = dt.Rows[0]["empAdd"].ToString();
                dateFrom.Text = dt.Rows[0]["dateFrom"].ToString();
                dateTo.Text = dt.Rows[0]["dateTo"].ToString();
                reason.Text = dt.Rows[0]["empReason"].ToString().Replace("''", "'");
                getLeaveType(dt.Rows[0]["empTOL"].ToString());
                timeFrom.Text = dt.Rows[0]["timeFrom"].ToString();
                timeTo.Text = dt.Rows[0]["timeTo"].ToString();
                txtStoreDays.Text = dt.Rows[0]["days"].ToString();
                getLeaveCredits(empno.Text);
            }

        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
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
        if(mapproved.Checked == true)
        {
            mdenied.Checked = false;
        }
    }
    protected void mdenied_Checked(object sender, EventArgs e)
    {
        if(mdenied.Checked == true)
        {
            mapproved.Checked = false;
        }
    }
    protected void btnSaveSup_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnSaveMana_Click(object sender, EventArgs e)
    {
        try
        {
            if (mapproved.Checked == true || mdenied.Checked == true || sapproved.Checked == true || sdenied.Checked == true)
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
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
                    string saveNotify = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
                                        "values('" + empno.Text + "', '" + gtParam + "', '" + getEmpNo + "', '" + strType + "', " + statType + ", 1, '" + DateTime.Now + "')";
                    HRMIS.Module.gblInsert(saveNotify);

                    string fndLeaveStat = "Select identity_column from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + gtParam + "'";
                    string fndGet = HRMIS.Module.GetField(fndLeaveStat);
                    if(fndGet == "")
                    {
                        string saveLeaveStat = "Insert into seihaHRMIS.dbo.leaveStatInfo(empno, leaveLeaveNo, leaveSupNo, leaveSupType, leaveSupRemarks, leaveManaNo, leaveManaType, leaveManaRemarks, leaveLeaveStat, leaveLeaveCredApp, leaveAdminEval, leaveAdminNo, leaveAdminType, leaveAdminStat) " +
                                          "values('" + empno.Text + "', '" + gtParam + "', '', 0, '', '" + getEmpNo + "', " + statType + ", '" + txtMRemarks.Text.Replace("'", "''") + "', " + statType + ", 0, '', '', '0', 0)";
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
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnAdminPass_Click(object sender, EventArgs e)
    {

        try
        {
            //if (HRChoiceLeave.SelectedIndex != 0)
            //{
            //    string confirmValue = Request.Form["confirm_value"];
            //     if (confirmValue == "Yes")
            //     {

            //         int hldDays = int.Parse(txtStoreDays.Text);
            //         int hldCredit = 0;
            //         string hldCredWhat = "";
            //         if (HRChoiceLeave.SelectedIndex == 1) { hldCredit = int.Parse(lblVacL.Text); hldCredWhat = "leavCredVL"; }
            //         else if (HRChoiceLeave.SelectedIndex == 2) { hldCredit = int.Parse(lblSckL.Text); hldCredWhat = "leavCredSL"; }
            //         else if (HRChoiceLeave.SelectedIndex == 3) { hldCredit = int.Parse(lblLolL.Text); hldCredWhat = "leavCredLoyal"; }
            //         else if (HRChoiceLeave.SelectedIndex == 4) { hldCredit = int.Parse(lblSpcL.Text); hldCredWhat = "leavCredSpecL"; }
            //         else if (HRChoiceLeave.SelectedIndex == 5) { hldCredit = 0; }
            //         //int calcDeduct = hldCredit - hldDays;
            //         int calcDeduct = -1;
            //         if(calcDeduct < 0)
            //         {
                         
            //             ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "myModal", "$('#myModal').modal('toggle');", false);
            //         }
            //         else
            //         {
                         
            //             //string saveNotify = "Insert into seihaHRMIS.dbo.notInfo(notEmpno, notLeaveNo, notWho, notType, notStat, notView, notDate) " +
            //             //                   "values('" + empno.Text + "', '" + gtParam + "', '" + getEmpNo + "', 'Credited', " + HRChoiceLeave.SelectedValue + ", 1, '" + DateTime.Now + "')";
            //             //gblInsert(saveNotify);
            //             //string fndLeaveStat = "Select identity_column from seihaHRMIS.dbo.leaveStatInfo where leaveLeaveNo = '" + gtParam + "'";
            //             //string fndGet = GetField(fndLeaveStat);
            //             //string UpdateLeaveStat = "Update seihaHRMIS.dbo.leaveStatInfo set leaveLeaveCredApp = " + hldCredit + ", leaveAdminEval = " + hrRemarks.Text.Replace("'", "''") + ", leaveAdminNo = '" + getEmpNo + "', leaveAdminType = " + HRChoiceLeave.SelectedValue + ", leaveAdminStat = 1 where identity_column = '" + fndGet + "'";
            //             //gblInsert(UpdateLeaveStat);
            //             //string UpdateLeaveCredit = "Update seihaHRMIS.dbo.LeaveCreditInfo set " + hldCredWhat + " = " + calcDeduct + " where identity_column = '" + fndGet + "'";
            //             //gblInsert(UpdateLeaveCredit);
            //         }
                     
            //     }
            //}
        }
        catch(System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }

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
    
}