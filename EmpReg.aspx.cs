using System;
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
using System.Web.UI.HtmlControls;

namespace HRMIS
{
    public partial class EmpReg : System.Web.UI.Page
    {
        private static DataTable dtQuery;
        private static string getEmpUser = "";
        private static string dtGetEMPStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            getEmpUser = Session["Uname"] as string;
            if (string.IsNullOrEmpty(getEmpUser))
            {
                Session.Abandon();
                Response.Redirect("~/Login");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    getEmpLists();
                    populatePosDept();
                    populateRole();
                    populatePosPos(department.SelectedValue.ToString());
                    populateReportingHead(department.SelectedValue.ToString(), position.SelectedValue.ToString(), role.SelectedValue.ToString());
                }
                else
                {
                    getEmpLists();
                }
                
                
            }
        }
        protected void lblbtnDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dash");
        }
        protected void lblbtnDash1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
        protected void getEmpLists()
        {
            try
            {

                dtGetEMPStr = "Select EmpFName + ' ' + EmpLName as Name, convert(varchar, EmpDOH, 107) as HDate, * " +
                              "from seihaHRMIS.dbo.HREmpInfo";

                dtQuery = HRMIS.Module.GetData(dtGetEMPStr);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    string sStatementFirst = "<table id='res-config' class='table table-striped table-bordered break'><thead>" +
                                             "<tr><th>Emp No</th><th>Name</th><th>Date Hired</th><th>Pos</th><th>Dept</th>" +
                                             "<th>Status</th></tr></thead><tbody>";
                    Panel1.Controls.Add(new LiteralControl(sStatementFirst));
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {

                        string sStatement1 = "<tr><td>" + dtQuery.Rows[x]["empno"].ToString() + "</td>" +
                                                 "<td>" + dtQuery.Rows[x]["Name"].ToString() + "</td>" +
                                                 "<td style='white-space: nowrap;'>" + dtQuery.Rows[x]["HDate"].ToString() + "</td>" +
                                                 "<td>" + getPosition(dtQuery.Rows[x]["EmpPos"].ToString()) + "</td>" +
                                                 "<td>" + getDpart(dtQuery.Rows[x]["EmpDept"].ToString()) + "</td>" +
                                                 "<td>" + getEmpType(dtQuery.Rows[x]["EmpEmpStat"].ToString()) + "</td></tr>";
                        Panel1.Controls.Add(new LiteralControl(sStatement1));
                    }
                    Panel1.Controls.Add(new LiteralControl("</tbody></table>"));
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
        private string getEmpType(string GStat)
        {
            string emptype = "";
            string dtQry = "";
            dtQry = "Select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = '" + GStat + "' and cspopupfor = 'TOE'";
            DataTable dt = HRMIS.Module.GetData(dtQry);
            if (dt.Rows.Count > 0)
            {
                emptype = dt.Rows[0]["cspopuptext"].ToString();
            }

            return emptype;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
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
                                  "values('" + empno.Text + "', '" + firstname.Text + "', '" + middle.Text + "', '" + lastname.Text + "', '', '', '" + birthDate.Text + "' " +
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
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
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
    }
}
