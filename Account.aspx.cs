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


public partial class Account : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection();
    OleDbCommand sqlComm = new OleDbCommand();
    OleDbDataAdapter sqlDA = new OleDbDataAdapter();
    private static string strcCon = "";
    private static DataTable dtQuery;
    private static string hldAdmin = "";
    string gEmpno = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/404");
        //Session["hldEmp"] = Request.QueryString["param"];
        //gEmpno = Session["hldEmp"] as string;
        //if (!Page.IsPostBack)
        //{
        //    populatePosDept();
        //    getEInfo();
        //    populateRole();

        //}
    }
    void getEInfo()
    {
        try
        {
            dtQuery = null;
            string dtEmp = "";
            dtEmp = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar, EmpDOH, 107) as HDate, convert(varchar, EmpDOB, 107) as BDate, * " + 
                    "from seihaHRMIS.dbo.HREmpInfo where empno = '" + gEmpno + "' order by empno";
            dtQuery = GetData(dtEmp);
            if (dtQuery.Rows.Count > 0)
            {
                //----Employee Basic Info
                lblEmpName.Text = dtQuery.Rows[0]["Name"].ToString();
                lblPosition.Text = getPosition(dtQuery.Rows[0]["emppos"].ToString()) + " " + getRole(dtQuery.Rows[0]["emprole"].ToString());

                lblFname.Text = dtQuery.Rows[0]["empfname"].ToString();
                txtFName.Text = dtQuery.Rows[0]["empfname"].ToString();
                lblLname.Text = dtQuery.Rows[0]["emplname"].ToString();
                txtLName.Text = dtQuery.Rows[0]["emplname"].ToString();
                lblMI.Text = dtQuery.Rows[0]["empmi"].ToString();
                txtMI.Text = dtQuery.Rows[0]["empmi"].ToString();
                lblConNo.Text = dtQuery.Rows[0]["empconno"].ToString();
                txtConNo.Text = dtQuery.Rows[0]["empconno"].ToString();
                lblemail.InnerText = dtQuery.Rows[0]["empemail"].ToString();
                lblemail.Attributes.Add("href", "mailto:" + dtQuery.Rows[0]["empemail"].ToString());
                txtEmail.Text = dtQuery.Rows[0]["empemail"].ToString();
                lblDOB.Text = dtQuery.Rows[0]["BDate"].ToString();
                txtDOB.Text = dtQuery.Rows[0]["empdob"].ToString();
                if (dtQuery.Rows[0]["empgen"].ToString() == "0")
                {
                    lblgen.Text = "Male";
                    gender.SelectedIndex = int.Parse(dtQuery.Rows[0]["empgen"].ToString());
                    whosImage.ImageUrl = "images/img_avatar.png";
                }
                else
                {
                    lblgen.Text = "Female";
                    gender.SelectedIndex = int.Parse(dtQuery.Rows[0]["empgen"].ToString());
                    whosImage.ImageUrl = "images/img_avatar2.png";
                }
                //-----Employee Detail
                lblempno.Text = dtQuery.Rows[0]["empno"].ToString();
                txtEmpno.Text = dtQuery.Rows[0]["empno"].ToString();
                lblDOH.Text = dtQuery.Rows[0]["HDate"].ToString();
                txtDOH.Text = dtQuery.Rows[0]["empdoh"].ToString();
                lblDept.Text = getDpart(dtQuery.Rows[0]["empdept"].ToString());
                lblPos.Text = getPosition(dtQuery.Rows[0]["emppos"].ToString());
                department.Items.FindByValue(findDept(dtQuery.Rows[0]["empDept"].ToString())).Selected = true;

                //-----------Populate Postion-----------------
                populatePosPos(department.SelectedValue);
                position.Items.FindByValue(dtQuery.Rows[0]["emppos"].ToString()).Selected = true;
                Session["hldPos"] = dtQuery.Rows[0]["emppos"].ToString();
                //--------------------------------------------
                lblrole.Text = getRole(dtQuery.Rows[0]["emprole"].ToString());
                role.SelectedValue = dtQuery.Rows[0]["emprole"].ToString();
                string hldRol = dtQuery.Rows[0]["emprole"].ToString();
                lblstatus.Text = getStat(dtQuery.Rows[0]["empempstat"].ToString());
                empstat.SelectedIndex = int.Parse(dtQuery.Rows[0]["empempstat"].ToString());
                populateReportingHead(department.SelectedValue.ToString(), position.SelectedValue.ToString(), hldRol);

                hldAdmin = dtQuery.Rows[0]["empAdmin"].ToString();
                if (hldAdmin != "1")
                {
                    drpEdit.Visible = false;
                    lblReg.Visible = false;
                }
                else
                {
                    lblReg.Visible = true;
                }
                lblUser.Text = dtQuery.Rows[0]["empFname"].ToString();
                if (dtQuery.Rows[0]["empGen"].ToString() == "0")
                {
                    UserPic.Attributes.Add("src", "images/img_avatar.png");
                }
                else
                {
                    UserPic.Attributes.Add("src", "images/img_avatar2.png");
                }
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
    protected void lblLeave_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Leave");
    }
    protected void btnLeaveAll_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Leave");
    }
    protected void lblAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account?param=" + gEmpno);
    }
    protected void lblNoti_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Notification");
    }
    protected void btnEICancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
    protected void lblPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ChangePassword?accpass=" + gEmpno);
    }
    protected void lblCalendar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Calendar");
    }
    public void dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        populatePosPos(department.SelectedValue);
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
        string hldDept = findValDept(gdpt);

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
            if (gpos == "TCH")
            {
                if (grol == "RLD")
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
        else if (hldDept == "IT")
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
        else if (hldDept == "ADM")
        {
            dtQry = "Select (EmpFName + ' ' + EmpLName) as Name, EmpNo from seihaHRMIS.dbo.HREmpInfo where empDept = 'MGR' and empStatus = 1 order by identity_column";
        }


        DataTable dt = GetData(dtQry);
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
    private void populateRole()
    {
        try
        {
            role.DataSource = null;
            string dtQry = "";
            dtQry = "Select cspopuptext, cspopupval from seihaHRMIS.dbo.cspopup where cspopupfor = 'ROL' order by identity_column";
            DataTable dt = GetData(dtQry);
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
    private void populatePosPos(string gDept)
    {
        try
        {
            position.DataSource = null;
            string dtQry = "";
            dtQry = "Select cspopuptext, cspopupval from seihaHRMIS.dbo.cspopup where cspopupfor = 'POS' and cspopupconn= " + int.Parse(gDept) + " order by identity_column";
            DataTable dt = GetData(dtQry);
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
    private string findDept(string gstat)
    {
        string fDept = "";
        string dtQry = "";
        dtQry = "Select identity_column from seihaHRMIS.dbo.cspopup where cspopupfor = 'DPT' and cspopupval = '" + gstat + "'";
        DataTable dt = GetData(dtQry);
        if (dt.Rows.Count > 0)
        {
            fDept = dt.Rows[0]["identity_column"].ToString();
        }
        return fDept;
    }
    private string findValDept(string gstat)
    {
        string fDept = "";
        string dtQry = "";
        dtQry = "Select cspopupval from seihaHRMIS.dbo.cspopup where cspopupfor = 'DPT' and identity_column = '" + gstat + "'";
        DataTable dt = GetData(dtQry);
        if (dt.Rows.Count > 0)
        {
            fDept = dt.Rows[0]["cspopupval"].ToString();
        }
        return fDept;
    }
    private void populatePosDept()
    {
        try
        {
            department.DataSource = null;
            string dtQry = "";
            dtQry = "Select cspopuptext, identity_column from seihaHRMIS.dbo.cspopup where cspopupfor = 'DPT' order by identity_column";
            DataTable dt = GetData(dtQry);
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
}