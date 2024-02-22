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

public partial class AdminControl : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection();
    OleDbCommand sqlComm = new OleDbCommand();
    OleDbDataAdapter sqlDA = new OleDbDataAdapter();
    private static string strcCon = "";
    private static DataTable dtQuery;
    private string dtQry = "";
    private static string getEmpNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("~/404");
        //getEmpNo = Session["Uname"] as string;
        //if (!Page.IsPostBack)
        //{
        //    //features to run only once

        //    populatePosDept();
        //    loadDept();
        //    loadPos(int.Parse(department.SelectedValue.ToString()));
        //    Session["HoldValue"] = department.SelectedValue.ToString();
        //    loadRole();
        //    getUserInfo();
        //}
        //else
        //{
        //    loadDept();
        //    string getValue = Session["HoldValue"] as string;
        //    if (string.IsNullOrEmpty(getValue))
        //    {

        //    }
        //    else
        //    {
        //        loadPos(int.Parse(Session["HoldValue"].ToString()));
        //    }
        //    loadRole();
        //    //features to run on each post back
        //}
    }
    private void getUserInfo()
    {
        dtQuery = null;
        string sQuery = "Select * from seihaHRMIS.dbo.HREmpInfo where empno = '" + getEmpNo + "'";
        dtQuery = GetData(sQuery);
        if (dtQuery.Rows.Count > 0)
        {
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
    protected void lblAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Account?param=" + getEmpNo);
    }
    protected void lblPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ChangePassword?accpass=" + getEmpNo);
    }
    protected void lblCalendar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Calendar");
    }
    protected void department_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["HoldValue"] = department.SelectedValue.ToString();
        txtPosName.Text = "";
        txtPosValue.Text = "";
        loadPos(int.Parse(department.SelectedValue.ToString()));
    }
    private void populatePosDept()
    {
        try
        {
            department.DataSource = null;
            dtQry = "";
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
    private void loadDept()
    {
        try
        {
            dgvDepartment.DataSource = null;
            dtQry = "";
            dtQry = "Select cspopuptext as Name, cspopupval as Value from seihaHRMIS.dbo.cspopup where cspopupfor = 'DPT' order by identity_column";
            DataTable dt = GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                dgvDepartment.DataSource = dt;
                dgvDepartment.DataBind();
                dgvDepartment.BackColor = Color.White;
                for (int x = 0; x < dgvDepartment.Rows.Count; x++)
                {
                    dgvDepartment.Rows[x].Cells[0].Width = 150;
                    dgvDepartment.Rows[x].Cells[1].Width = 50;
                }
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void loadPos(int gConn)
    {
        try
        {
            dgvPosition.DataSource = null;
            dtQry = "";
            dtQry = "Select cspopuptext as Name, cspopupval as Value from seihaHRMIS.dbo.cspopup where cspopupfor = 'POS' and cspopupconn= " + gConn + " order by identity_column";
            DataTable dt = GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                dgvPosition.DataSource = dt;
                dgvPosition.DataBind();
                dgvPosition.BackColor = Color.White;
                for (int x = 0; x < dgvPosition.Rows.Count; x++)
                {
                    dgvPosition.Rows[x].Cells[0].Width = 150;
                    dgvPosition.Rows[x].Cells[1].Width = 50;
                }
                //loadPos(gConn);
                dgvPosition.Visible = true;
            }
            else
            {
                dgvPosition.DataSource = null;
                //loadPos(gConn);
                dgvPosition.Visible = false;
            }

           
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    private void loadRole()
    {
        try
        {
            dgvRole.DataSource = null;
            dtQry = "";
            dtQry = "Select cspopuptext as Name, cspopupval as Value from seihaHRMIS.dbo.cspopup where cspopupfor = 'Rol' order by identity_column";
            DataTable dt = GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                dgvRole.DataSource = dt;
                dgvRole.DataBind();
                dgvRole.BackColor = Color.White;
                for (int x = 0; x < dgvRole.Rows.Count; x++)
                {
                    dgvRole.Rows[x].Cells[0].Width = 150;
                    dgvRole.Rows[x].Cells[1].Width = 50;
                }
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnSubDept_Click(object sender, EventArgs e)
    {
        try
        {
            int countFind = 0;
            string findValue = "";
            findValue = "Select count(*) from seihaHRMIS.dbo.cspopup where cspopupval = '" + txtDeptValue.Text.Trim() + "' and cspopupfor = 'DPT'";

            countFind = int.Parse(GetCount(findValue));
            if (countFind == 1)
            {
                Response.Write(@"<script> alert('Department is already exist!') </script>");
            }
            else
            {
                string strQuery = "Insert into seihaHRMIS.dbo.cspopup(cspopuptext, cspopupval, cspopupfor, cspopupconn) " +
                                  "values('" + txtDeptName.Text + "', '" + txtDeptValue.Text + "', 'DPT', 0)";
                gblInsert(strQuery);

                string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                  "values('A','" + DateTime.Now + "', '" + txtDeptName.Text + "', '" + Environment.MachineName + "', 'Department', 'cspopup', '" + txtDeptName.Text + "', '" + txtDeptValue.Text + "', '" + txtDeptName.Text + "')";
                gblInsert(LogQuery);

            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    
    protected void dgvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvDepartment.PageIndex = e.NewPageIndex;
        dgvDepartment.DataBind();
    }
    protected void dgvPosition_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
            dgvPosition.PageIndex = e.NewPageIndex;
            dgvPosition.DataBind();
    }
    protected void btnSubPos_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                int countFind = 0;
                string findValue = "";
                findValue = "Select count(*) from seihaHRMIS.dbo.cspopup where cspopupval = '" + txtPosValue.Text.Trim() + "' and cspopupconn = " + int.Parse(department.SelectedValue.ToString()) + " and cspopupfor = 'POS'";

                countFind = int.Parse(GetCount(findValue));
                if (countFind == 1)
                {
                    Response.Write(@"<script> alert('Department is already exist!') </script>");
                }
                else
                {
                    string strQuery = "Insert into seihaHRMIS.dbo.cspopup(cspopuptext, cspopupval, cspopupfor, cspopupconn) " +
                                      "values('" + txtPosName.Text + "', '" + txtPosValue.Text + "', 'POS', " + int.Parse(department.SelectedValue.ToString()) + ")";
                    gblInsert(strQuery);

                    string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                      "values('A','" + DateTime.Now + "', '" + txtPosName.Text + "', '" + Environment.MachineName + "', 'Department', 'cspopup', '" + txtPosName.Text + "', '" + txtPosValue.Text + "', '" + txtPosName.Text + "')";
                    gblInsert(LogQuery);
                    loadPos(int.Parse(department.SelectedValue.ToString()));
                    txtPosName.Text = "";
                    txtPosValue.Text = "";
                }
            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnSubRole_Click(object sender, EventArgs e)
    {
        try
        {
            int countFind = 0;
            string findValue = "";
            findValue = "Select count(*) from seihaHRMIS.dbo.cspopup where cspopupval = '" + txtRoleValue.Text.Trim() + "' and cspopupfor = 'ROL'";

            countFind = int.Parse(GetCount(findValue));
            if (countFind == 1)
            {
                Response.Write(@"<script> alert('Department is already exist!') </script>");
            }
            else
            {
                string strQuery = "Insert into seihaHRMIS.dbo.cspopup(cspopuptext, cspopupval, cspopupfor, cspopupconn) " +
                                  "values('" + txtRoleName.Text + "', '" + txtRoleValue.Text + "', 'ROL', 0)";
                gblInsert(strQuery);

                string LogQuery = "Insert into seihaHRMIS.dbo.syecaudt(fcAction, fcDate, fcUser, fcComputername, fcForm, fcTable, fcField, fcOldVal, fcNewVal) " +
                                  "values('A','" + DateTime.Now + "', '" + txtRoleName.Text + "', '" + Environment.MachineName + "', 'Department', 'cspopup', '" + txtRoleName.Text + "', '" + txtRoleValue.Text + "', '" + txtRoleName.Text + "')";
                gblInsert(LogQuery);

            }
        }
        catch (System.Net.WebException ex)
        {
            Response.Write(ex.Message);
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
}