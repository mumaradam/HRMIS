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
using System.Web.Services;

namespace HRMIS
{
    public partial class LeaveCred : System.Web.UI.Page
    {
        private static DataTable dtQuery;
        private static string getEmpUser = "";
        private static string getUserAdmin = "";
        private static string hldEmp = "";
        private static string hldEmp2 = "";
        private static string strHldEmp2 = "";
        private static string hldcntVL = "";
        private static string hldcntSL = "";
        private static string hldcntLL = "";
        private static string hldcntSPL = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            getEmpUser = Session["Uname"] as string;
            getUserInfo();
            if (!IsPostBack)
            {
                
                
            }
        }
        protected void lblbtnDash_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dash");
        }
        private void getUserInfo()
        {
            try
            {
                dtQuery = null;
                string sQuery = "Select * from seihaHRMIS.dbo.HREmpInfo a Left Join seihaHRMIS.dbo.LeaveCreditInfo b ON a.empno = b.empno where a.empno = '" + getEmpUser + "'";
                dtQuery = HRMIS.Module.GetData(sQuery);
                if (dtQuery.Rows.Count > 0)
                {
                    lblCountVL.Text = dtQuery.Rows[0]["leavCredVL"].ToString();
                    lblCountSL.Text = dtQuery.Rows[0]["leavCredSL"].ToString();
                    lblCountLL.Text = dtQuery.Rows[0]["leavCredLoyal"].ToString();
                    lblCountSPL.Text = dtQuery.Rows[0]["leavCredSpecL"].ToString();
                    getUserAdmin = dtQuery.Rows[0]["empadmin"].ToString();
                    if (getUserAdmin == "1")
                    {
                        getAllEmp();
                        //loadAllEmp();
                        emplist.Visible = true;
                        empCred.Visible = true;
                        if(hldEmp2 != "")
                        {
                            HtmlTableRow dynamicRow = (HtmlTableRow)FindControl("tr_" + hldEmp2);

                            if (dynamicRow != null)
                            {
                                // Access the <tr> element and do something with it
                                dynamicRow.Attributes["class"] = "selected";
                            }
                        }
                    }
                    else
                    {
                        getLeavRec(getEmpUser);
                        emplist.Visible = false;
                        empCred.Visible = false;
                        emplogs.Attributes["class"] = "col-lg-12";
                        //emplist.Style["display"] = "none";
                    }
                }

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void getAllEmp()
        {
            try
            {
                dtQuery = null;
                string dtEmp = "";
                dtEmp = "Select a.empNo, a.EmpFName + ' ' + a.EmpLName as Name, b.leavCredVl, b.leavCredSL, b.leavCredLoyal, b.leavCredSpecL from seihaHRMIS.dbo.HREmpInfo a Left Join seihaHRMIS.dbo.LeaveCreditInfo b On a.empno = b.empno where a.empStatus = 1 order by a.empno";
                dtQuery = HRMIS.Module.GetData(dtEmp);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    string sStatementFirst = "<table class='table table-striped table-bordered break' id='single-select'><thead><tr><th>Employee</th>" +
                                             "<th>Vacation</th><th>Sick</th>" +
                                             "<th>Loyalty</th><th>Special</th>" +
                                             "<th>Action</th></tr></thead><tbody>";
                    Panel1.Controls.Add(new LiteralControl(sStatementFirst));
                    // Add the controls to the UpdatePanel instead of the Panel directly
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        string gen = "";
                        LinkButton button2 = new LinkButton();
                        string sStatement = "<tr id='" + dtQuery.Rows[x]["empNo"].ToString() + "' runat='server'><td>" + dtQuery.Rows[x]["Name"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["leavCredVl"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["leavCredSL"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["leavCredLoyal"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["leavCredSpecL"].ToString() + "</td> " +
                                            "<td>";
                        Panel1.Controls.Add(new LiteralControl(sStatement));
                        button2.ID = "V" + dtQuery.Rows[x]["empNo"].ToString();
                        button2.Text = "<span class='icofont icofont-eye-alt'></span>";
                        button2.CssClass = "btn btn-inverse waves-effect waves-light";
                        button2.ClientIDMode = ClientIDMode.Static;
                        button2.Attributes["style"] = "margin-left: 10px; font-size: 12px; padding: 5px;";
                        button2.Attributes["data-toggle"] = "tooltip";
                        button2.Attributes["title data-original-title"] = "View";
                        button2.Click += Button_Click;
                        
                        //button2.OnClientClick = "callButtonClick(this);";
                        button2.Attributes["data-empno"] = dtQuery.Rows[x]["empNo"].ToString();
                        AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
                        trigger.ControlID = button2.ID;
                        trigger.EventName = "Click";
                        updateAllEmpPanel.Triggers.Add(trigger);
                        updateAllEmpPanel.ContentTemplateContainer.Controls.Add(button2);
                        Panel1.Controls.Add(button2);
                        Panel1.Controls.Add(new LiteralControl("</td></tr>"));
                        
                    }
                    string sStatementLast = "</tbody></table>";
                    Panel1.Controls.Add(new LiteralControl(sStatementLast));
                    //updateAllEmpPanel.Update();
                }


            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        public void HandleButtonClick(string empNo)
        {
                string strEID = empNo.ToString().Replace("V", "");
                dtQuery = null;
                lbleName.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) from seihaHRMIS.dbo.HREmpInfo where empno = '" + strEID + "'");
                string dtEmp = "";
                dtEmp = "Select (select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = empTOL and cspopupfor = 'TOL') as TOL, " +
                        "(select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = empStatus and cspopupfor = 'LVSTAT') as LeavStat, " +
                        "(convert(varchar, empdatefrom, 107) + ' / ' + convert(varchar, empdateto, 107)) as LDate, * from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + strEID + "' order by identity_column";
                dtQuery = HRMIS.Module.GetData(dtEmp);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    string sStatement1 = "<table id='res-config' class='table table-striped table-bordered nowrap'>" +
                                     "<thead><tr><th>ID</th>" +
                                     "<th>Type</th><th>Reason</th>" +
                                     "<th>Status</th><th>Date</th>" +
                                     "</tr></thead><tbody>";
                    Panel2.Controls.Add(new LiteralControl(sStatement1));
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        string gen = "";
                        string sStatement = "<tr><td style='white-space: pre-wrap;'>" + dtQuery.Rows[x]["identity_column"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["TOL"].ToString() + "</td>" +
                                            "<td style='white-space: pre-wrap;'>" + dtQuery.Rows[x]["empReason"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["LeavStat"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["LDate"].ToString() + "</td>";
                        Panel2.Controls.Add(new LiteralControl(sStatement));
                    }
                    Panel2.Controls.Add(new LiteralControl("</tr></tbody></table>"));

                }
                //updateLogsPanel.Update();
                //Update the corresponding UpdatePanel
                var updatePanelID = "updateLogsPanel"; // Replace with the actual ID of the UpdatePanel
                UpdatePanel updatePanel = FindControlRecursive(Page, updatePanelID) as UpdatePanel;
                if (updatePanel != null)
                {
                    updatePanel.Update();
                }

        }
        public Control FindControlRecursive(Control control, string id)
        {
            if (control.ClientID == id)
            {
                return control;
            }

            foreach (Control childControl in control.Controls)
            {
                Control foundControl = FindControlRecursive(childControl, id);
                if (foundControl != null)
                {
                    return foundControl;
                }
            }

            return null;
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                string dtEmp = "";
                dtEmp = "Update seihaHRMIS.dbo.LeaveCreditInfo set leavCredVL = 5,  leavCredSL = 5";
                HRMIS.Module.gblInsert(dtEmp);
                Response.Redirect(Request.RawUrl);

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        //protected void dgvEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    dgvEmp.PageIndex = e.NewPageIndex;
        //    dgvEmp.DataBind();
        //}
        //protected void dgvEmp_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow row in dgvEmp.Rows)
        //    {
        //        if (row.RowIndex == dgvEmp.SelectedIndex)
        //        {
        //            row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
        //            row.ToolTip = string.Empty;
        //        }
        //        else
        //        {
        //            row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
        //            row.ToolTip = "Click to select this row.";
        //        }
        //    }
        //}
        //private void loadAllEmp()
        //{
        //    try
        //    {
        //        string dtEmp = "";
        //        dtEmp = "Select a.EmpNo as Emp_No, EmpFName + ' ' + EmpLName as Name,  b.leavCredVl as Vacation, b.leavCredSL as Sick, b.leavCredLoyal as Loyal, b.leavCredSpecL as Special from seihaHRMIS.dbo.HREmpInfo a Left Join seihaHRMIS.dbo.LeaveCreditInfo b On a.empno = b.empno where a.empStatus = 1 order by a.empno";
        //        DataTable dt = HRMIS.Module.GetData(dtEmp);
        //        if (dt.Rows.Count > 0)
        //        {
        //            dgvEmp.DataSource = dt;
        //            dgvEmp.DataBind();
        //            dgvEmp.BackColor = Color.White;
        //            for (int x = 0; x < dgvEmp.Rows.Count; x++)
        //            {
        //                dgvEmp.Rows[x].Cells[0].Width = 90;
        //            }
        //        }
        //    }
        //    catch (System.Net.WebException ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //}
        protected void Button_Click(object sender, EventArgs e)
        {
            try
            {
                updateAllEmpPanel.Triggers.Clear();
                LinkButton clickedButton = new LinkButton();
                string strEID = "";

                if (hldEmp2 != "" && hldEmp2 == sender)
                {
                    clickedButton.ID = hldEmp2;
                    strEID = clickedButton.ID.ToString();
                }
                else
                {
                    clickedButton = (LinkButton)sender;
                    strEID = clickedButton.ID.ToString().Replace("V", "");
                }
                
                hldEmp = strEID;
                hldEmp2 = hldEmp;
                dtQuery = null;
                lbleName.Text =  HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) from seihaHRMIS.dbo.HREmpInfo where empno = '" + strEID + "'");
                getCredits(strEID);
                string dtEmp = "";
                dtEmp = "Select (select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = empTOL and cspopupfor = 'TOL') as TOL, " +
                        "(select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = empStatus and cspopupfor = 'LVSTAT') as LeavStat, " +
                        "(convert(varchar, empdatefrom, 107) + ' / ' + convert(varchar, empdateto, 107)) as LDate, * from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + strEID + "' order by identity_column";
                dtQuery = HRMIS.Module.GetData(dtEmp);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    string sStatement1 = "<table id='res-config' class='table table-striped table-bordered nowrap'>" +
                                     "<thead><tr><th>ID</th>" +
                                     "<th>Type</th><th>Reason</th>" +
                                     "<th>Status</th><th>Date</th>" +
                                     "</tr></thead><tbody>";
                    Panel2.Controls.Add(new LiteralControl(sStatement1));
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        string gen = "";
                        string sStatement = "<tr><td style='white-space: pre-wrap;'>" + dtQuery.Rows[x]["identity_column"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["TOL"].ToString() + "</td>" +
                                            "<td style='white-space: pre-wrap;'>" + dtQuery.Rows[x]["empReason"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["LeavStat"].ToString() + "</td>"+
                                            "<td>" + dtQuery.Rows[x]["LDate"].ToString() + "</td>";
                        Panel2.Controls.Add(new LiteralControl(sStatement));
                    }
                    Panel2.Controls.Add(new LiteralControl("</tr></tbody></table>"));

                }
                updateLogsPanel.Update();
            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void getCredits(string strID)
        {
            try
            {
                dtQuery = null;
                hldcntVL = "";
                hldcntSL = "";
                hldcntLL = "";
                hldcntSPL = "";
                lblEmpCred.Text = HRMIS.Module.GetField("Select (EmpFName + ' ' + EmpLName) from seihaHRMIS.dbo.HREmpInfo where empno = '" + strID + "'");
                string getqry = "Select * from seihaHRMIS.dbo.LeaveCreditInfo where empno = '" + strID + "'";
                dtQuery = HRMIS.Module.GetData(getqry);
                if(dtQuery.Rows.Count > 0)
                {

                    lblEVL.Text = dtQuery.Rows[0]["leavCredVL"].ToString();
                    txtEVL.Text = dtQuery.Rows[0]["leavCredVL"].ToString();
                    lblESL.Text = dtQuery.Rows[0]["leavCredSL"].ToString();
                    txtESL.Text = dtQuery.Rows[0]["leavCredSL"].ToString();
                    lblELL.Text = dtQuery.Rows[0]["leavCredLoyal"].ToString();
                    txtELL.Text = dtQuery.Rows[0]["leavCredLoyal"].ToString();
                    lblESPL.Text = dtQuery.Rows[0]["leavCredSpecL"].ToString();
                    txtESPL.Text = dtQuery.Rows[0]["leavCredSpecL"].ToString();
                    hidEmpNo.Value = strID;
                }
                else
                {
                    hidEmpNo.Value = "";
                    lblEVL.Text = "00";
                    lblESL.Text = "00";
                    lblELL.Text = "00";
                    lblESPL.Text = "00";
                }
                updateCredPanel.Update();
            }
            catch(System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void getLeavRec(string strID)
        {
            try
            {
                dtQuery = null;
                string dtEmp = "";
                dtEmp = "Select (select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = empTOL and cspopupfor = 'TOL') as TOL, " +
                        "(select cspopuptext from seihaHRMIS.dbo.cspopup where cspopupval = empStatus and cspopupfor = 'LVSTAT') as LeavStat, " +
                        "(convert(varchar, empdatefrom, 107) + ' / ' + convert(varchar, empdateto, 107)) as LDate, * from seihaHRMIS.dbo.HRLeaveInfo where empno = '" + strID + "' order by identity_column";
                dtQuery = HRMIS.Module.GetData(dtEmp);
                if (dtQuery.Rows.Count > 0)
                {
                    int count = checked(dtQuery.Rows.Count - 1);
                    string sStatement1 = "<table id='cell-select' class='table table-striped table-bordered break'>" + 
                                     "<thead><tr><th>ID</th>" +
                                     "<th>Type</th><th>Date</th>" +
                                     "<th>Reason</th><th>Status</th>" +
                                     "</tr></thead><tbody>";
                    Panel2.Controls.Add(new LiteralControl(sStatement1));
                    for (int x = 0; x <= count; x = checked(x + 1))
                    {
                        string gen = "";
                        Button button = new Button();
                        string sStatement = "<tr><td>" + dtQuery.Rows[x]["identity_column"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["TOL"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["LDate"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["empReason"].ToString() + "</td>" +
                                            "<td>" + dtQuery.Rows[x]["LeavStat"].ToString() + "</td>";
                        Panel2.Controls.Add(new LiteralControl(sStatement));
                    }
                    Panel2.Controls.Add(new LiteralControl("</tr></tbody></table>"));

                }
                updateLogsPanel.Update();

            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void MinusCred(object sender, EventArgs e)
        {
             try
            {
                LinkButton lnkbtn = (LinkButton)sender;
                string strID = lnkbtn.ID.ToString();
                if (strID == "btnVLMinus")
                {
                    if(hidEmpNo.Value != "")
                    {
                        txtEVL.Text = (int.Parse(txtEVL.Text) - 1).ToString();
                    }
                    
                }
            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
        protected void PlusCred(object sender, EventArgs e)
        {
            
            try
            {
                LinkButton lnkbtn = (LinkButton)sender;
                string strID = lnkbtn.ID.ToString();
                if (strID == "btnVLPlus")
                {
                    if (hidEmpNo.Value != "")
                    {
                        updateAllEmpPanel.Triggers.Clear();
                        Panel1.Controls.Clear();
                        getAllEmp();
                        Button_Click(hldEmp2, EventArgs.Empty);
                        if (hldcntVL != "")
                        {
                            txtEVL.Text = (int.Parse(hldcntVL) + 1).ToString();
                        }
                        else
                        {
                            txtEVL.Text = (int.Parse(txtEVL.Text) + 1).ToString();
                        }
                        hldcntVL = txtEVL.Text;
                        updateCredPanel.Update();
                        
                    }
                    
                }
            }
            catch (System.Net.WebException ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}
