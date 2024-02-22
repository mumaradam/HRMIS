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


public partial class Registration : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection();
    OleDbCommand sqlComm = new OleDbCommand();
    OleDbDataAdapter sqlDA = new OleDbDataAdapter();
    private static string strcCon = "";
    private string dtEmpInfo = "";
    string getEmpNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/404");
        //getEmpNo = Session["Uname"] as string;
        //if (string.IsNullOrEmpty(getEmpNo))
        //{
        //    Session.Abandon();
        //    Response.Redirect("~/Login");
        //}
        //else
        //{
            
            
        //    if (!Page.IsPostBack)
        //    {
        //        loadAllEmp();
        //        populatePosDept();
        //        populateRole();
        //        populatePosPos(department.SelectedValue.ToString());
        //        populateReportingHead(department.SelectedValue.ToString(), position.SelectedValue.ToString(), role.SelectedValue.ToString());
        //    }
        //    else
        //    {
        //        loadAllEmp();
        //    }
        //}
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
    protected void dgvEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvEmp.PageIndex = e.NewPageIndex;
        dgvEmp.DataBind();
    }
    protected void departmentSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        populatePosPos(department.SelectedValue.ToString());
        populateReportingHead(department.SelectedValue.ToString(), position.SelectedValue.ToString(), role.SelectedValue.ToString());
    }
    protected void position_SelectedIndexChanged(object sender, EventArgs e)
    {
        populateReportingHead(department.SelectedValue.ToString(), position.SelectedValue.ToString(), role.SelectedValue.ToString());
        role.ClearSelection();
    }
    protected void role_SelectedIndexChanged(object sender, EventArgs e)
    {
        populateReportingHead(department.SelectedValue.ToString(), position.SelectedValue.ToString(), role.SelectedValue.ToString());
    }
    private void populateReportingHead(string gdpt, string gpos, string grol)
    {
        string hldDept = findDept(gdpt);

        foreach (ListItem item in role.Items)
        {

            if (position.SelectedValue == "TMR" || position.SelectedValue == "ITM" || position.SelectedValue == "ITN" || department.SelectedValue.ToString() == "3" || department.SelectedValue == "4" || position.SelectedValue == "SMR" || position.SelectedValue == "TRN" || position.SelectedValue == "TOM")
            {
                role.Items.FindByValue(item.Value).Attributes.Add("style", "display:none");
                role.Items.FindByValue(item.Value).Attributes.Add("disabled", "disabled");
                
            }
            else
            {
                role.Items.FindByValue(item.Value);

            }
           
        }
        string dtQry = "";
        if (hldDept == "FCT")
        {
            if (gpos=="TCH")
            {
                if(grol=="RLD")
                {
                    dtQry = "Select (EmpFName + ' ' + EmpLName) as Name, EmpNo from seihaHRMIS.dbo.HREmpInfo where empDept = '" + hldDept + "' and empPos <> '" + gpos + "' and empStatus = 1 order by identity_column";
                }
                else
                {
                    dtQry = "Select (EmpFName + ' ' + EmpLName) as Name, EmpNo from seihaHRMIS.dbo.HREmpInfo where empDept = '" + hldDept + "' and empPos = '" + gpos + "' and empRole = 'RLD' and empStatus = 1  order by identity_column";
                }
            }
            else
            {
                dtQry = "Select (EmpFName + ' ' + EmpLName) as Name, EmpNo from seihaHRMIS.dbo.HREmpInfo where empDept = 'MGR' and empStatus = 1 order by identity_column";
            }
            
        }
        else if (hldDept=="IT")
        {
            if (gpos == "ITM")
            {
                dtQry = "Select (EmpFName + ' ' + EmpLName) as Name, EmpNo from seihaHRMIS.dbo.HREmpInfo where empDept = 'MGR' and empStatus = 1 order by identity_column";
            }
            else
            {
                if (grol == "RLD" || gpos == "ITN")
                {
                    dtQry = "Select (EmpFName + ' ' + EmpLName) as Name, EmpNo from seihaHRMIS.dbo.HREmpInfo where empDept = '" + hldDept + "' and empPos = 'ITM' and empStatus = 1  order by identity_column";
                }
                else
                {
                    dtQry = "Select (EmpFName + ' ' + EmpLName) as Name, EmpNo from seihaHRMIS.dbo.HREmpInfo where empDept = '" + hldDept + "' and empPos = '" + gpos + "' and empRole = 'RLD' and empStatus = 1  order by identity_column";
                }
            }
        }
        else if (hldDept=="ADM")
        {
            dtQry = "Select (EmpFName + ' ' + EmpLName) as Name, EmpNo from seihaHRMIS.dbo.HREmpInfo where empDept = 'MGR' and empStatus = 1 order by identity_column";
        }


        DataTable dt = HRMIS.Module.GetData(dtQry);
        if (dt.Rows.Count > 0)
        {
            reportHead.DataSource = dt;
            reportHead.DataTextField = "Name";
            reportHead.DataValueField = "EmpNo";
            reportHead.DataBind();
        }
        else
        {
            reportHead.Items.Clear();
        }
    }
    private void populatePosDept()
    {
        try
        {
            department.DataSource = null;
            string dtQry = "";
            dtQry = "Select cspopuptext, identity_column from seihaHRMIS.dbo.cspopup where cspopupfor = 'DPT' order by identity_column";
            DataTable dt = HRMIS.Module.GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                department.DataSource = dt;
                department.DataTextField = "cspopuptext";
                department.DataValueField = "identity_column";
                department.DataBind();
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void populatePosPos(string gDept)
    {
        try
        {
            position.DataSource = null;
            string dtQry = "";
            dtQry = "Select cspopuptext, cspopupval from seihaHRMIS.dbo.cspopup where cspopupfor = 'POS' and cspopupconn= " + int.Parse(gDept) + " order by identity_column";
            DataTable dt = HRMIS.Module.GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                position.DataSource = dt;
                position.DataTextField = "cspopuptext";
                position.DataValueField = "cspopupval";
                position.DataBind();
            }
            else
            {
                position.DataSource = null;
                position.Items.Clear();
                position.ClearSelection();
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void populateRole()
    {
        try
        {
            role.DataSource = null;
            string dtQry = "";
            dtQry = "Select cspopuptext, cspopupval from seihaHRMIS.dbo.cspopup where cspopupfor = 'ROL' order by identity_column";
            DataTable dt = HRMIS.Module.GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                role.DataSource = dt;
                role.DataTextField = "cspopuptext";
                role.DataValueField = "cspopupval";
                role.DataBind();
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void loadAllEmp()
    {
        try
        {
            dtEmpInfo = "";
            dtEmpInfo = "Select EmpNo as Emp_No, EmpFName + ' ' + EmpLName as Name from seihaHRMIS.dbo.HREmpInfo order by EmpNo";
            DataTable dt = HRMIS.Module.GetData(dtEmpInfo);
                if (dt.Rows.Count > 0)
                {
                    dgvEmp.DataSource = dt;
                    dgvEmp.DataBind();
                    dgvEmp.BackColor = Color.White;
                    for (int x = 0; x < dgvEmp.Rows.Count; x++)
                    {
                        dgvEmp.Rows[x].Cells[0].Width = 90;
                       

                    }
                }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    
    private string findDept(string gstat)
    {
        string fDept = "";
        string dtQry = "";
        dtQry = "Select cspopupval from seihaHRMIS.dbo.cspopup where cspopupfor = 'DPT' and identity_column = '" + gstat + "'";
        DataTable dt = HRMIS.Module.GetData(dtQry);
        if (dt.Rows.Count > 0)
        {
            fDept = dt.Rows[0]["cspopupval"].ToString();
        }
        return fDept;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        getEmpNo = "";
        getEmpNo = Session["Uname"] as string;
        if (string.IsNullOrEmpty(getEmpNo))
        {
            Session.Abandon();
            Response.Redirect("~/Login");
        }
        else
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);

                int countFind = 0;
                string findEmpNo = "";
                findEmpNo = "Select count(*) from seihaHRMIS.dbo.HREmpInfo where EmpNo = '" + empno.Text.Trim() + "'";

                countFind = int.Parse(HRMIS.Module.GetCount(findEmpNo));
                if (countFind == 1)
                {
                    Response.Write(@"<script> alert('Employee number is already taken!') </script>");
                }
                else
                {
                    string hldReportHead = reportHead.SelectedValue.ToString();
                    string strQuery = "Insert into seihaHRMIS.dbo.HREmpInfo(EmpNo, EmpFName, EmpMI, EmpLName, EmpAdd, EmpEmail, EmpDOB, EmpConNo, EmpGen, EmpEmpStat, EmpComEmail, EmpDept, EmpPos, EmpRole, EmpAdmin, EmpReportHead, EmpDOH, EmpStatus) " +
                                  "values('" + empno.Text + "', '" + firstname.Text + "', '" + middle.Text + "', '" + lastname.Text + "', '" + address.Text + "', '" + email.Text + "', '" + birthDate.Text + "' " +
                                  ",'" + contactNo.Text + "', " + gender.SelectedValue + ", " + empstat.SelectedValue + ", '" + comEmail.Text + "', '" + findDept(department.SelectedValue) + "', '" + position.SelectedValue + "', '" + role.SelectedValue + "', 0, '" + hldReportHead + "', '" + DOH.Text + "', 1) ";
                    HRMIS.Module.gblInsert(strQuery);

                    string srtEmpPass = "senp" + empno.Text;
                    string strUQuery = "Insert into seihaHRMIS.dbo.HRUserInfo(UserEmpNo, UserPass, UserStatus) values('" + empno.Text + "', '" + srtEmpPass + "', 1)";
                    HRMIS.Module.gblInsert(strUQuery);

                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                                              "values('A','" + DateTime.Now + "', '" + empno.Text + "', '" + Environment.MachineName + "', 'Registration', 'HREmpInfo', '" + empno.Text + "', '', '" + empno.Text + "')";
                    HRMIS.Module.gblInsert(LogQuery);

                    Response.Redirect(Request.RawUrl);
                }

            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "", true);
            }
        }
        
    }
}   